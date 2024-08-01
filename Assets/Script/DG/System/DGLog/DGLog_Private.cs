/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/8/15
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace DG
{
	public static partial class DGLog
	{
		private static readonly StringBuilder _Convert_To_Msg_String_Builder = new StringBuilder(1000);
		private const string _STRING_FORMAT_ARG_COUNT_PATTERN = @"{[0-9]+}";
		private static readonly HashSet<string> _String_Format_Arg_Count_Hash_Set = new HashSet<string>();
		private static StringBuilder _Decorate_Log_String_Builder = new StringBuilder(1000);

		public static string GetLogString(bool isStackTrace = false, params object[] args)
		{
			var msg = _ConvertToMsg(args);
			msg = _DecorateLog(msg, isStackTrace);
			return msg;
		}

		private static string _ConvertToMsg(params object[] args)
		{
			if (args == null || args.Length == 0)
				return string.Empty;
			int totalLength = args.Length;
			string[] dgStringArgs = new string[totalLength];
			for (int m = 0; m < args.Length; m++)
			{
				dgStringArgs[m] = args[m].DGToString();
				if (!_Log_Cfg.enableArgFormat)
					_Convert_To_Msg_String_Builder.Append(string.Format("  {0}", dgStringArgs[m]));
			}

			if (_Log_Cfg.enableArgFormat)
			{
				int i = 0;
				do
				{
					var format = dgStringArgs[i];
					var formatArgCount = _GetStringFormatArgCount(format);
					var formatArgs = new string[formatArgCount];
					Array.Copy(dgStringArgs, i + 1, formatArgs, 0, formatArgCount);
					_Convert_To_Msg_String_Builder.Append(formatArgCount != 0
						? string.Format("  " + format, formatArgs)
						: string.Format("  {0}", format));
					i = i + formatArgCount + 1;
				} while (i < totalLength);
			}

			var result = _Convert_To_Msg_String_Builder.ToString();
			_Convert_To_Msg_String_Builder.Clear();
			return result;
		}

		private static int _GetStringFormatArgCount(string format)
		{
			var matches = Regex.Matches(format, _STRING_FORMAT_ARG_COUNT_PATTERN);
			//去重
			for (int i = 0; i < matches.Count; i++)
			{
				var match = matches[i];
				_String_Format_Arg_Count_Hash_Set.Add(match.Value);
			}

			var result = _String_Format_Arg_Count_Hash_Set.Count;
			_String_Format_Arg_Count_Hash_Set.Clear();
			return result;
		}

		private static string _DecorateLog(string msg, bool? isStackTrace)
		{
			if (_Log_Cfg.enableTime)
				_Decorate_Log_String_Builder.Append(string.Format("[{0}]", DateTime.Now.ToString("hh:mm:ss--fff")));
			if (_Log_Cfg.enableThreadId)
				_Decorate_Log_String_Builder.Append(string.Format("[ThreadId:{0}]",
					Thread.CurrentThread.ManagedThreadId));
			_Decorate_Log_String_Builder.Append(string.Format("{0} {1}", _Log_Cfg.logPrefix, msg));
			bool needStackTrace = isStackTrace.GetValueOrDefault(_Log_Cfg.enableStackTrace);
			if (needStackTrace)
			{
				_Decorate_Log_String_Builder.Append("\nStackTrace:");
				var stackTrace = new StackTrace(_Log_Cfg.stackTraceOffSet, true);
				for (int i = 0; i < stackTrace.FrameCount; i++)
				{
					StackFrame stackFrame = stackTrace.GetFrame(i);
					var methodBase = stackFrame.GetMethod();
					_Decorate_Log_String_Builder.Append(string.Format("\n	{0}:{1}()(at {2}:{3})",
						methodBase.DeclaringType.FullName, methodBase.Name, stackFrame.GetFileName(),
						stackFrame.GetFileLineNumber()));
				}
			}

			var result = _Decorate_Log_String_Builder.ToString();
			_Decorate_Log_String_Builder.Clear();
			return result;
		}

		private static void _WriteToFile(string msg)
		{
			if (_Log_Stream_Writer != null)
			{
				try
				{
					_Log_Stream_Writer.WriteLine(msg);
				}
				catch (Exception e)
				{
					_Log_Stream_Writer = null;
				}
			}
		}

		private static void _CheckInitCfg()
		{
			if (_Log_Cfg == null)
				InitCfg();
		}
	}
}
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
using System.IO;

namespace DG
{
	public static partial class DGLog
	{
		public static void InitCfg(DGLogCfg logCfg = null)
		{
			if (logCfg == null)
				logCfg = new DGLogCfg();
			DGLog._Log_Cfg = logCfg;
			switch (logCfg.logType)
			{
				case DGLogType.Console:
					_Logger = new DGConsoleLogger();
					break;
				case DGLogType.Unity:
					_Logger = new DGUnityLogger();
					break;
			}

			_InitLogStreamWriter();
		}

		public static void _InitLogStreamWriter()
		{
			if (!_Log_Cfg.enableSave)
			{
				_Log_Stream_Writer = null;
				return;
			}

			try
			{
				string fileDir = _Log_Cfg.saveDirPath;
				string fileName = _Log_Cfg.saveFileName;
				if (!_Log_Cfg.isSaveReplace)
				{
					string prefix = DateTime.Now.ToString("yyyyMMdd_HH-mm-ss");
					fileName = prefix + _Log_Cfg.saveFileName;
				}

				var filePath = fileDir + fileName;
				_CheckCreateFilePath(fileDir, fileName, _Log_Cfg.isSaveReplace);
				_Log_Stream_Writer = File.AppendText(filePath);
				_Log_Stream_Writer.AutoFlush = true;
			}
			catch (Exception e)
			{
				_Log_Stream_Writer = null;
			}
		}

		private static void _CheckCreateFilePath(string fileDir, string fileName, bool isReplace)
		{
			var filePath = fileDir + fileName;
			if (Directory.Exists(fileDir))
			{
				if (File.Exists(filePath) && isReplace)
					File.Delete(filePath);
			}
			else
			{
				Directory.CreateDirectory(_Log_Cfg.saveDirPath);
			}
		}
	}
}
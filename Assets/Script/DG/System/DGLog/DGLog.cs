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

using System.IO;

namespace DG
{
    public static partial class DGLog
    {
        private static DGLogCfg _Log_Cfg;
        private static IDGLogger _Logger;
        private static StreamWriter _Log_Stream_Writer;

        public static void InfoFull(DGLogColor? logColor, bool? isStackTrace, params object[] args)
        {
            _CheckInitCfg();
            if (!_Log_Cfg.enable)
                return;
            string msg = _ConvertToMsg(args);
            msg = _DecorateLog(msg, isStackTrace);
            _Logger.Info(msg, logColor);
            if (_Log_Cfg.enableSave)
                _WriteToFile(string.Format("[Log]{0}", msg));
        }

        public static void WarnFull(DGLogColor? logColor, bool? isStackTrace, params object[] args)
        {
            _CheckInitCfg();
            if (!_Log_Cfg.enable)
                return;
            string msg = _ConvertToMsg(args);
            msg = _DecorateLog(msg, isStackTrace);
            _Logger.Warn(msg, logColor);
            if (_Log_Cfg.enableSave)
                _WriteToFile(string.Format("[Warn]{0}", msg));
        }

        public static void ErrorFull(DGLogColor? logColor, bool? isStackTrace, params object[] args)
        {
            _CheckInitCfg();
            if (!_Log_Cfg.enable)
                return;
            string msg = _ConvertToMsg(args);
            msg = _DecorateLog(msg, isStackTrace);
            _Logger.Error(msg, logColor);
            if (_Log_Cfg.enableSave)
                _WriteToFile(string.Format("[Error]{0}", msg));
        }


        public static void Info(params object[] args)
        {
            InfoFull(null, null, args);
        }

        public static void Warn(params object[] args)
        {
            WarnFull(null, null, args);
        }

        public static void Error(params object[] args)
        {
            ErrorFull(null, null, args);
        }

        public static void InfoWithColor(DGLogColor logColor = default, params object[] args)
        {
            InfoFull(logColor, null, args);
        }

        public static void WarnWithColor(DGLogColor logColor = default, params object[] args)
        {
            WarnFull(logColor, null, args);
        }

        public static void ErrorWithColor(DGLogColor logColor = default, params object[] args)
        {
            ErrorFull(logColor, null, args);
        }

        public static void InfoWithTrace(params object[] args)
        {
            InfoFull(null, true, args);
        }

        public static void WarnWithTrace(params object[] args)
        {
            WarnFull(null, true, args);
        }

        public static void ErrorWithTrace(params object[] args)
        {
            ErrorFull(null, true, args);
        }

        public static void InfoFormat(string format, params object[] args)
        {
            InfoFull(null, null, string.Format(format, args));
        }

        public static void WarnFormat(string format, params object[] args)
        {
            WarnFull(null, null, string.Format(format, args));
        }

        public static void ErrorFormat(string format, params object[] args)
        {
            ErrorFull(null, null, string.Format(format, args));
        }

        public static void InfoFormatWithTrace(string format, params object[] args)
        {
            InfoFull(null, true, string.Format(format, args));
        }

        public static void WarnFormatWithTrace(string format, params object[] args)
        {
            WarnFull(null, true, string.Format(format, args));
        }

        public static void ErrorFormatWithTrace(string format, params object[] args)
        {
            ErrorFull(null, true, string.Format(format, args));
        }
    }
}
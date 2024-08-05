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


using UnityEngine;

namespace DG
{
    public class DGLogCfg
    {
        public bool enable = true; //是否开启log
        public EDGLogType logType = EDGLogType.Unity; //日志类型
        public bool enableTime = true; //是否显示log的时刻
        public bool enableThreadId = false; //是否显示线程id
        public bool enableArgFormat = true; //参数是否是可以用string.format的
        public bool enableStackTrace = false; //是否显示堆栈信息
        public int stackTraceOffSet = 3; //堆栈偏移
        public string logPrefix = ">>";


        public bool enableSave = false; //是否保存到文件
        public bool isSaveReplace = true; //是否保存的时候，覆盖之前的文件
#if UNITY_STANDALONE
        public string saveDirPath = string.Format("{0}Logs\\", Application.persistentDataPath); //保存的目录路径
#else
	public string saveDirPath = string.Format("{0}Logs\\", AppDomain.CurrentDomain.BaseDirectory);
#endif
        public string saveFileName = "DGLog.txt"; //保存的文件名称
    }
}
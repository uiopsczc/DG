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

public class DGLogCfg
{
	public bool enable = true;
	public bool enableTime = true;
	public bool enableThreadId = false;
	public bool enableStackTrace = false;
	public int stackTraceOffSet = 3;
	public string logPrefix = ">>";


	public bool enableSave = false;
	public bool isSaveReplace = true;
#if UNITY_STANDALONE
	public string saveDirPath = string.Format("{0}Logs\\", UnityEngine.Application.persistentDataPath);
#else
	public string saveDirPath = string.Format("{0}Logs\\", AppDomain.CurrentDomain.BaseDirectory);
#endif
	public string saveFileName = "DGLog.txt";

	public DGLogType logType = DGLogType.Unity;
}

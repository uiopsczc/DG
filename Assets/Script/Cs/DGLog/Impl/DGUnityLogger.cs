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

public class DGUnityLogger:IDGLogger
{
	public void Info(string msg, DGLogColor? logColor = default)
	{
		Debug.Log(_ColorUnityLog(msg, logColor));
	}

	public void Warn(string msg, DGLogColor? logColor = default)
	{
		Debug.LogWarning(_ColorUnityLog(msg, logColor));
	}

	public void Error(string msg, DGLogColor? logColor = default)
	{
		Debug.LogError(_ColorUnityLog(msg, logColor));
	}

	private string _ColorUnityLog(string msg, DGLogColor? logColor)
	{
		switch (logColor.GetValueOrDefault(DGLogColor.None))
		{
			case DGLogColor.Red:
				msg = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(Color.red), msg);
				break;
			case DGLogColor.Green:
				msg = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(Color.green), msg);
				break;
			case DGLogColor.Blue:
				msg = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(Color.blue), msg);
				break;
			case DGLogColor.Cyan:
				msg = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(Color.cyan), msg);
				break;
			case DGLogColor.Magenta:
				msg = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(Color.magenta), msg);
				break;
			case DGLogColor.Yellow:
				msg = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(Color.yellow), msg);
				break;
		}
		return msg;
	}
}

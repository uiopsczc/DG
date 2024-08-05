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
    public class DGUnityLogger : IDGLogger
    {
        public void Info(string msg, EDGLogColor? logColor = default)
        {
            Debug.Log(_ColorUnityLog(msg, logColor));
        }

        public void Warn(string msg, EDGLogColor? logColor = default)
        {
            Debug.LogWarning(_ColorUnityLog(msg, logColor));
        }

        public void Error(string msg, EDGLogColor? logColor = default)
        {
            Debug.LogError(_ColorUnityLog(msg, logColor));
        }

        private string _ColorUnityLog(string msg, EDGLogColor? logColor)
        {
            switch (logColor.GetValueOrDefault(EDGLogColor.None))
            {
                case EDGLogColor.Red:
                    msg = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(Color.red), msg);
                    break;
                case EDGLogColor.Green:
                    msg = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(Color.green), msg);
                    break;
                case EDGLogColor.Blue:
                    msg = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(Color.blue), msg);
                    break;
                case EDGLogColor.Cyan:
                    msg = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(Color.cyan), msg);
                    break;
                case EDGLogColor.Magenta:
                    msg = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(Color.magenta), msg);
                    break;
                case EDGLogColor.Yellow:
                    msg = string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(Color.yellow), msg);
                    break;
            }

            return msg;
        }
    }
}
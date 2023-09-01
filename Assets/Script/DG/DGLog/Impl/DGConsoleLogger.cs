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

namespace DG
{
	public class DGConsoleLogger : IDGLogger
	{
		public void Info(string msg, DGLogColor? logColor = default)
		{
			WriteConsoleLog(msg, logColor);
		}

		public void Warn(string msg, DGLogColor? logColor = default)
		{
			WriteConsoleLog(msg, logColor);
		}

		public void Error(string msg, DGLogColor? logColor = default)
		{
			WriteConsoleLog(msg, logColor);
		}

		public void WriteConsoleLog(string msg, DGLogColor? logColor = default)
		{
			var orgColor = Console.ForegroundColor;
			switch (logColor.GetValueOrDefault(DGLogColor.None))
			{
				case DGLogColor.Red:
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case DGLogColor.Green:
					Console.ForegroundColor = ConsoleColor.Green;
					break;
				case DGLogColor.Blue:
					Console.ForegroundColor = ConsoleColor.Blue;
					break;
				case DGLogColor.Cyan:
					Console.ForegroundColor = ConsoleColor.Cyan;
					break;
				case DGLogColor.Magenta:
					Console.ForegroundColor = ConsoleColor.Magenta;
					break;
				case DGLogColor.Yellow:
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
			}
			Console.WriteLine(msg);
			Console.ForegroundColor = orgColor;
		}
	}
}


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
        public void Info(string msg, EDGLogColor? logColor = default)
        {
            WriteConsoleLog(msg, logColor);
        }

        public void Warn(string msg, EDGLogColor? logColor = default)
        {
            WriteConsoleLog(msg, logColor);
        }

        public void Error(string msg, EDGLogColor? logColor = default)
        {
            WriteConsoleLog(msg, logColor);
        }

        public void WriteConsoleLog(string msg, EDGLogColor? logColor = default)
        {
            var orgColor = Console.ForegroundColor;
            switch (logColor.GetValueOrDefault(EDGLogColor.None))
            {
                case EDGLogColor.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case EDGLogColor.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case EDGLogColor.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case EDGLogColor.Cyan:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case EDGLogColor.Magenta:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case EDGLogColor.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }

            Console.WriteLine(msg);
            Console.ForegroundColor = orgColor;
        }
    }
}
using System;
using System.Diagnostics;

namespace DG
{
    public class StopwatchScope : IDisposable
    {
        private readonly Stopwatch _stopwatch;
        private string _name;

        public StopwatchScope(string name = StringConst.STRING_EMPTY)
        {
            _name = name;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            DGLog.Info(string.Format("{0} 开始统计耗时", _name));
        }


        public void Dispose()
        {
            _stopwatch.Stop();
            var timeSpan = _stopwatch.Elapsed;
            DGLog.Info(string.Format("{0} 统计耗时结束,总共耗时{1}秒", _name, timeSpan.TotalMilliseconds / 1000));
        }
    }
}
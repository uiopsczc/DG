using System;
using UnityEngine.Profiling;

namespace DG
{
    public class ProfilerBeginSampleScope : IDisposable
    {
        public ProfilerBeginSampleScope(string name)
        {
            Profiler.BeginSample(name);
        }


        public void Dispose()
        {
            Profiler.EndSample();
        }
    }
}
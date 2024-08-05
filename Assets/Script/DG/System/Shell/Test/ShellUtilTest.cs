using UnityEngine;

namespace DG
{
    public class ShellUtilTest
    {
        public static async void Test()
        {
            await ShellUtil.ProcessCommand("mkdir shellTest", Application.dataPath);
        }
    }
}
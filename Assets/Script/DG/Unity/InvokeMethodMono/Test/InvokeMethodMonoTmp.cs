using UnityEngine;

namespace DG
{
    public class InvokeMethodMonoTmp : MonoBehaviour
    {
        public void Hello(string s)
        {
            DGLog.Error("hello " + s);
        }
    }
}
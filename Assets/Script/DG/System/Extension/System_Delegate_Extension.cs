using System;

namespace DG
{
    public static class System_Delegate_Extension
    {
        public static void InvokeIfNotNull(this Delegate self, params object[] delegationArgs)
        {
            DelegateUtil.InvokeIfNotNull(self, delegationArgs);
        }

        public static Delegate InsertFirst(this Delegate self, Delegate firstDelegation)
        {
            return DelegateUtil.InsertFirst(self, firstDelegation);
        }
    }
}
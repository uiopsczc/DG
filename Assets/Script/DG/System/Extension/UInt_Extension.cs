namespace DG
{
    public static class UInt_Extension
    {
        public static bool IsContains(this uint value, uint beContainedValue)
        {
            return UIntUtil.IsContains(value, beContainedValue);
        }
    }
}
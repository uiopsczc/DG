namespace DG
{
    public partial class GUIUtil
    {
        public static GUIDepthScope Depth(int newDepth)
        {
            return new GUIDepthScope(newDepth);
        }
    }
}
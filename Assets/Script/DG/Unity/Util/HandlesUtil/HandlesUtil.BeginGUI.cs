#if UNITY_EDITOR
namespace DG
{
    public partial class HandlesUtil
    {
        public static HandlesBeginGUIScope BeginGUI()
        {
            return new HandlesBeginGUIScope();
        }
    }
}
#endif
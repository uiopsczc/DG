#if UNITY_EDITOR
namespace DG
{
    public partial class EditorGUIUtil
    {
        public static EditorGUIDisabledGroupScope DisabledGroup(bool isDisable)
        {
            return new EditorGUIDisabledGroupScope(isDisable);
        }
    }
}
#endif
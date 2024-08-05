#if UNITY_EDITOR
namespace DG
{
    public partial class EditorGUIUtil
    {
        public static EditorGUIBeginToggleGroupScope BeginToggleGroup(bool isToggle,
            string name = StringConst.STRING_EMPTY)
        {
            return new EditorGUIBeginToggleGroupScope(isToggle, name);
        }
    }
}
#endif
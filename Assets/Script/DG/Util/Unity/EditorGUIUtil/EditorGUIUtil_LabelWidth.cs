#if UNITY_EDITOR
namespace DG
{
	public partial class EditorGUIUtil
	{
		public static EditorGUILabelWidthScope LabelWidth(float lableWidth)
		{
			return new EditorGUILabelWidthScope(lableWidth);
		}
	}
}
#endif
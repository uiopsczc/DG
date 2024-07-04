#if UNITY_EDITOR
namespace DG
{
	public partial class EditorGUIUtil
	{
		public static EditorGUIIndentLevelScope IndentLevel(int add = 1)
		{
			return new EditorGUIIndentLevelScope(add);
		}
	}
}
#endif
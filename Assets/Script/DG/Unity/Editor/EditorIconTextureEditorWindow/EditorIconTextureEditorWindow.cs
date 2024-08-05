using UnityEditor;
using UnityEngine;

namespace DG
{
    public class EditorIconTextureEditorWindow : EditorWindow
    {
        private Vector2 scrollPosition;

        void OnGUI()
        {
            using (new GUILayoutBeginScrollViewScope(ref scrollPosition))
            {
                //内置图标
                int columnCount = 20;
                using (new GUILayoutBeginHorizontalScope())
                {
                    for (int i = 0; i < EnumUtil.GetCount<EEditorIconTextureType>(); ++i)
                    {
                        if (i > 0 && i % columnCount == 0)
                        {
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                        }

                        if (GUILayout.Button(EditorIconTexture.GetSystem((EEditorIconTextureType)i),
                                GUILayout.Width(50), GUILayout.Height(36)))
                        {
                            string displayValue = string.Format("EditorIconTextureType.{0}\n{1}",
                                EnumUtil.GetName<EEditorIconTextureType>(i),
                                EditorIconTextureConst.IconTextureNames[i]
                            );
                            this.ShowNotificationAndLog(displayValue);
                        }
                    }
                }
            }
        }
    }
}
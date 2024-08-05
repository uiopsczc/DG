#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DG
{
    [CustomEditor(typeof(GUIToolbarTest))]
    public class GUIToolbarTestEditor : Editor
    {
        private GUIToolbar _guiToolbar;

        private readonly List<GUIContent> _buttonGUIContentList = new()
        {
            "A".ToGUIContent(),
            "B".ToGUIContent(),
            "C".ToGUIContent(),
            "D".ToGUIContent(),
        };

        void OnEnable()
        {
            _guiToolbar = new GUIToolbar(_buttonGUIContentList);
            _guiToolbar.onToolSelected += OnToolSelected;
            _guiToolbar.TriggerButton(1);
            //    guiToolbar.SetHighlight(3, true);
        }

        void OnDisable()
        {
            _guiToolbar.onToolSelected -= OnToolSelected;
        }

        public void OnToolSelected(GUIToolbar guiToolbar, int selectedIndex, int preSelectedIndex)
        {
            switch (selectedIndex)
            {
                case 0: //对应button_guiContent_list的"A"
                    DGLog.Info("A");
                    break;
                case 1: //对应button_guiContent_list的"B"
                    DGLog.Info("B");
                    break;
                case 2: //对应button_guiContent_list的"C"
                    DGLog.Info("C");
                    break;
                case 3: //对应button_guiContent_list的"D"
                    DGLog.Info("D");
                    break;
            }
        }

        void OnSceneGUI()
        {
            _guiToolbar.DrawGUI(Vector2.zero, new Vector2(40, 40), Color.white, Color.black);
        }
    }
}
#endif
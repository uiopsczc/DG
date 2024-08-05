using System;
using UnityEngine;

namespace DG
{
    public class GUIBackgroundColorScope : IDisposable
    {
        [SerializeField] private Color _preColor { get; }

        public GUIBackgroundColorScope(Color newColor)
        {
            _preColor = GUI.backgroundColor;
            GUI.backgroundColor = newColor;
        }


        public void Dispose()
        {
            GUI.backgroundColor = _preColor;
        }
    }
}
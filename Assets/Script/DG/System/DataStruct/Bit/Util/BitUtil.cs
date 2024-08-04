using System;
using System.Text;
using UnityEngine;

namespace DG
{
    public class BitUtil
    {
        public static bool Contains(int container, int value)
        {
            return (container & value) == value;
        }
    }
}
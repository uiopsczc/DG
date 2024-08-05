using System;

namespace DG
{
    /// <summary>
    /// 参考Unity 的MenuItem
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class DGGenericMenuItemAttribute : Attribute
    {
        public string itemName;

        /// <summary>
        /// 根节点名称
        /// </summary>
        public string rootName;

        /// <summary>
        /// 名称s
        /// </summary>
        public string[] names;

        /// <summary>
        /// 是否校验
        /// </summary>
        public bool isValidate;

        /// <summary>
        /// 优先顺序  以最小的为单位
        /// </summary>
        public int priority;

        /// <summary>
        /// 当前的nameIndex，用于获取subNames，在GenericMenuItemInfo中使用
        /// </summary>
        public int currentNameIndex = 0;


        public DGGenericMenuItemAttribute(string itemName)
            : this(itemName, false)
        {
        }


        public DGGenericMenuItemAttribute(string itemName, bool isValidate)
            : this(itemName, isValidate, 0)
        {
        }


        public DGGenericMenuItemAttribute(string itemName, bool isValidate, int priority)
        {
            names = itemName.Split("/");
            rootName = names[0];
            this.itemName = itemName;
            this.isValidate = isValidate;
            this.priority = priority;
        }

        public DGGenericMenuItemAttribute(string itemName, bool isValidate, int priority, object source)
        {
            names = itemName.Split("/");
            rootName = names[0];
            this.isValidate = isValidate;
            this.priority = priority;
        }
    }
}
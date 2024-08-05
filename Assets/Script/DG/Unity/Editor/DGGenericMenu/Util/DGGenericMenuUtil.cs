using System.Collections.Generic;
using System.Reflection;

namespace DG
{
    public class DGGenericMenuUtil
    {
        /// <summary>
        /// 所有GenericMenuCat菜单放在dict中
        /// </summary>
        public static readonly Dictionary<string, DGGenericMenu> name2DGGenericMenu = new();

        /// <summary>
        /// 加载所有GenericMenuCat菜单到dict中
        /// </summary>
        public static void Load()
        {
            name2DGGenericMenu.Clear();
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (MemberInfo memberInfo in assembly.GetCustomAttributeMemberInfos<DGGenericMenuItemAttribute>())
            {
                DGGenericMenuItemAttribute genericMenuItemAttribute =
                    memberInfo.GetCustomAttribute<DGGenericMenuItemAttribute>();
                DGGenericMenu genericMenuCat =
                    name2DGGenericMenu.GetOrAddByDefaultFunc(genericMenuItemAttribute.rootName,
                        () => new DGGenericMenu());
                genericMenuCat.InitOrUpdateRoot(genericMenuItemAttribute, (MethodInfo)memberInfo);
            }
        }
    }
}
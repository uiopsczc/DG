using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DG
{
    public class GameObjectUtil
    {
        /// <summary>
        ///   有T返回T，没T添加T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static Component GetOrAddComponent(GameObject gameObject, Type type)
        {
            if (gameObject == null) return null;
            var component = gameObject.GetComponent(type);
            if (component == null)
                component = gameObject.AddComponent(type);
            return component;
        }

        public static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
        {
            return GetOrAddComponent(gameObject, typeof(T)) as T;
        }

        /// <summary>
        ///   使某个类型的组件enable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="isEnable"></param>
        public static void EnableComponents(GameObject gameObject, Type type, bool isEnable)
        {
            var components = gameObject.GetComponents(type);
            if (components == null) return;
            var num = components.Length;
            for (var i = 0; i < num; i++) ((MonoBehaviour)components[i]).enabled = isEnable;
        }

        public static void EnableComponents<T>(GameObject gameObject, bool isEnable) where T : MonoBehaviour
        {
            EnableComponents(gameObject, typeof(T), isEnable);
        }

        /// <summary>
        ///   销毁子孩子节点
        /// </summary>
        /// <param name="gameObject"></param>
        public static void DestroyChildren(GameObject gameObject)
        {
            if (gameObject == null) return;
            var transform = gameObject.transform;
            if (transform == null) return;
            var num = transform.childCount;
            while (--num >= 0)
                transform.GetChild(num).gameObject.Destroy();
        }


        /// <summary>
        ///   只有包含全部的Components才会返回True
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static bool IsHasComponents(GameObject gameObject, params Type[] types)
        {
            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (!gameObject.GetComponent(type))
                    return false;
            }

            return true;
        }

        public static bool IsHasComponent(GameObject gameObject, Type type)
        {
            return gameObject.GetComponent(type) != null;
        }

        public static bool IsHasComponent<T>(GameObject gameObject) where T : Component
        {
            return IsHasComponent(gameObject, typeof(T));
        }

        /// <summary>
        ///   获取该gameObject下的组件，不包括剔除的组件类型
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="excludeComponentTypes">剔除的组件类型</param>
        /// <returns></returns>
        public static Component[] GetComponentsExclude(GameObject gameObject, params Type[] excludeComponentTypes)
        {
            var result = new List<Component>();
            var components = gameObject.GetComponents<Component>();
            for (var i = 0; i < components.Length; i++)
            {
                var component = components[i];
                if (excludeComponentTypes.Length > 0) //如果剔除的类型个数不为0
                {
                    var isContinueThisRound = false; //是否结束这个round
                    for (var j = 0; j < excludeComponentTypes.Length; j++)
                    {
                        var excludeComponentType = excludeComponentTypes[j];
                        if (component.GetType().IsSubTypeOf(excludeComponentType)) //如果是组件类型是其中的剔除的类型或其子类
                        {
                            isContinueThisRound = true;
                            break;
                        }
                    }

                    if (isContinueThisRound)
                        continue;
                }

                result.Add(component);
            }

            return result.ToArray();
        }

        /// <summary>
        ///   获取该gameObject下的组件，不包括剔除的组件类型
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="excludeComponentTypes">剔除的组件类型</param>
        /// <param name="excludeSeparator"></param>
        /// <returns></returns>
        public static Component[] GetComponentsExclude(GameObject gameObject, string excludeComponentTypes,
            string excludeSeparator = StringConst.STRING_VERTICAL)
        {
            var excludeTypeList = new List<Type>();
            if (string.IsNullOrEmpty(excludeComponentTypes))
                return gameObject.GetComponentsExclude(excludeTypeList.ToArray());
            var excludeComponentTypeList = excludeComponentTypes.ToList<string>(excludeSeparator);
            for (var i = 0; i < excludeComponentTypeList.Count; i++)
            {
                var excludeComponentType = excludeComponentTypeList[i];
                if (TypeUtil.GetType(excludeComponentType) != null)
                    excludeTypeList.Add(TypeUtil.GetType(excludeComponentType));
                else if (TypeUtil.GetType(excludeComponentType, "UnityEngine") != null)
                    excludeTypeList.Add(TypeUtil.GetType(excludeComponentType, "UnityEngine"));
            }

            return gameObject.GetComponentsExclude(excludeTypeList.ToArray());
        }

        public static GameObject GetOrNewGameObject(string path, GameObject parentGameObject)
        {
            if (parentGameObject == null)
            {
                var result = GameObject.Find(path);
                if (result != null)
                    return result;
            }
            else
            {
                var result = parentGameObject.transform.Find(path);
                if (result != null)
                    return result.gameObject;
            }

            string name = path.GetPreString(StringConst.STRING_SLASH);
            var gameObject = new GameObject(name);
            if (parentGameObject != null)
                gameObject.transform.ResetToParent(parentGameObject.transform);
            return !name.Equals(path)
                ? GetOrNewGameObject(path.GetPostString(StringConst.STRING_SLASH), gameObject)
                : gameObject;
        }

        public static GameObject GetSocketGameObject(GameObject gameObject, string socketName = null)
        {
            return gameObject.transform.GetSocketTransform(socketName).gameObject;
        }

        public static void SetPause(GameObject gameObject, object cause)
        {
            PauseUtil.SetPause(gameObject, cause);
        }

        public static RectTransform RectTransform(GameObject gameObject)
        {
            return gameObject.GetComponent<RectTransform>();
        }

        public static bool IsSceneGameObject(GameObject self)
        {
            return self.scene.IsValid();
        }

        public static void DeSpawn(GameObject gameObject)
        {
            if (gameObject == null)
                return;
            if (gameObject.IsCacheContainsKey(DGPoolConst.POOL_ITEM))
            {
                DGPoolItem<GameObject> poolItem = gameObject.GetCache<DGPoolItem<GameObject>>(DGPoolConst.POOL_ITEM);
                poolItem.DeSpawn();
            }
        }

        public static void SetCache(GameObject gameObject, string key, object obj)
        {
            CacheMonoBehaviour cache = gameObject.GetOrAddComponent<CacheMonoBehaviour>();
            cache.Set(obj, key);
        }

        public static void SetCache(GameObject gameObject, string key, string subKey, object obj)
        {
            CacheMonoBehaviour cache = gameObject.GetOrAddComponent<CacheMonoBehaviour>();
            cache.SetSubKey(obj, key, subKey);
        }

        public static T GetCache<T>(GameObject gameObject, string key = null)
        {
            CacheMonoBehaviour cache = gameObject.GetOrAddComponent<CacheMonoBehaviour>();
            return cache.Get<T>(key);
        }


        public static T GetCache<T>(GameObject gameObject, string key, string subKey)
        {
            CacheMonoBehaviour cache = gameObject.GetOrAddComponent<CacheMonoBehaviour>();
            return cache.GetSubKey<T>(key, subKey);
        }

        public static T GetOrAddCache<T>(GameObject gameObject, string key, Func<T> defaultFunc)
        {
            CacheMonoBehaviour cache = gameObject.GetOrAddComponent<CacheMonoBehaviour>();
            return cache.GetOrAddByDefaultFunc(key, defaultFunc);
        }

        public static object GetOrAddCache(GameObject gameObject, string key, Func<object> defaultFunc)
        {
            return GetOrAddCache<object>(gameObject, key, defaultFunc);
        }

        public static T GetOrAddCache<T>(GameObject gameObject, string key, string subKey, Func<T> defaultFunc)
        {
            CacheMonoBehaviour cache = gameObject.GetOrAddComponent<CacheMonoBehaviour>();
            return cache.GetSubKeyOrAddByDefaultFunc(key, subKey, defaultFunc);
        }

        public static bool IsCacheContainsKey(GameObject gameObject, string key)
        {
            CacheMonoBehaviour cache = gameObject.GetOrAddComponent<CacheMonoBehaviour>();
            return cache.ContainsKey(key);
        }

        public static GameObject NewChildGameObject(GameObject gameObject, string path = null)
        {
            if (gameObject == null)
                return null;
            return gameObject.transform.NewChildGameObject(path);
        }

        public static Component NewChildWithComponent(GameObject gameObject, Type componentType, string path = null)
        {
            return gameObject == null ? null : gameObject.transform.NewChildWithComponent(componentType, path);
        }

        public static T NewChildWithComponent<T>(GameObject gameObject, string path = null) where T : Component
        {
            if (gameObject == null)
                return null;
            return gameObject.transform.NewChildWithComponent<T>(path);
        }

        public static RectTransform NewChildWithRectTransform(GameObject gameObject, string path = null)
        {
            return gameObject.transform.NewChildWithComponent<RectTransform>(path);
        }

        public static Image NewChildWithImage(GameObject gameObject, string path = null)
        {
            return gameObject.transform.NewChildWithImage(path);
        }

        public static Text NewChildWithText(GameObject gameObject, string path = null, string content = null,
            int fontSize = 20, Color? color = null, TextAnchor? alignment = null, Font font = null)
        {
            return gameObject.transform.NewChildWithText(path, content, fontSize, color, alignment);
        }

        public static void SetIsGray(GameObject gameObject, bool isGray, bool isRecursive = true)
        {
            gameObject.transform.SetIsGray(isGray, isRecursive);
        }

        public static void DoActionRecursive(GameObject gameObject, Action<GameObject> doAction)
        {
            gameObject.transform.DoActionRecursive(transform => doAction(transform.gameObject));
        }

        public static void SetAlpha(GameObject gameObject, float alpha, bool isRecursive = true)
        {
            gameObject.transform.SetAlpha(alpha, isRecursive);
        }

        public static void SetColor(GameObject gameObject, Color color, bool isNotUseColorAlpha = false,
            bool isRecursive = true)
        {
            gameObject.transform.SetColor(color, isNotUseColorAlpha, isRecursive);
        }


        public static (bool, string) GetRelativePath(GameObject gameObject, GameObject parentGameObject = null)
        {
            return TransformUtil.GetRelativePath(gameObject.transform,
                parentGameObject == null ? null : parentGameObject.transform);
        }

        public static float GetParticleSystemDuration(GameObject gameObject, bool isRecursive = true)
        {
            if (!isRecursive)
            {
                var particleSystem = gameObject.GetComponent<ParticleSystem>();
                if (particleSystem == null)
                    return 0;
                return particleSystem.GetDuration(false);
            }

            float maxDuration = 0;
            var particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
            for (var i = 0; i < particleSystems.Length; i++)
            {
                var particleSystem = particleSystems[i];
                var duration = particleSystem.GetDuration(false);
                if (duration == -1)
                    return duration;
                if (maxDuration < duration)
                    maxDuration = duration;
            }

            return maxDuration;
        }


        #region GameObject 反射

        #region FiledValue

        public static T GetFieldValue<T>(GameObject gameObject, string fieldInfoString, T defaultValue,
            params Type[] excludeComponentTypes)
        {
            var exclude = gameObject.GetComponentsExclude(excludeComponentTypes);
            for (var i = 0; i < exclude.Length; i++)
            {
                var component = exclude[i];
                var fieldInfo = component.GetType().GetFieldInfo(fieldInfoString);
                if (fieldInfo != null)
                    return (T)fieldInfo.GetValue(fieldInfoString);
            }

            return defaultValue;
        }

        public static void SetFieldValue(GameObject gameObject, string fieldInfoString, object value,
            params Type[] excludeComponentTypes)
        {
            var exclude = gameObject.GetComponentsExclude(excludeComponentTypes);
            for (var i = 0; i < exclude.Length; i++)
            {
                var component = exclude[i];
                var fieldInfo = component.GetType().GetFieldInfo(fieldInfoString);
                if (fieldInfo != null)
                    fieldInfo.SetValue(fieldInfoString, value);
            }
        }

        #endregion

        #region ProperyValue

        public static T GetPropertyValue<T>(GameObject gameObject, string propertyInfoString, T defaultValue,
            object[] index = null, params Type[] excludeComponentTypes)
        {
            var exclude = gameObject.GetComponentsExclude(excludeComponentTypes);
            for (var i = 0; i < exclude.Length; i++)
            {
                var component = exclude[i];
                var propertyInfo = component.GetType().GetPropertyInfo(propertyInfoString);
                if (propertyInfo != null)
                    return (T)propertyInfo.GetValue(propertyInfoString, index);
            }

            return defaultValue;
        }

        public static void SetPropertyValue(GameObject gameObject, string fieldInfoString, object value,
            object[] index = null, params Type[] excludeComponentTypes)
        {
            var exclude = gameObject.GetComponentsExclude(excludeComponentTypes);
            for (var i = 0; i < exclude.Length; i++)
            {
                var component = exclude[i];
                var propertyInfo = component.GetType().GetPropertyInfo(fieldInfoString);
                if (propertyInfo != null)
                    propertyInfo.SetValue(fieldInfoString, value, index);
            }
        }

        #endregion

        #region Invoke

        /// <summary>
        ///   调用callMethod的方法
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="invokeMethodName"></param>
        /// <param name="excludeComponentTypes"></param>
        /// <param name="parameters"></param>
        public static void Invoke(GameObject gameObject, string invokeMethodName, string excludeComponentTypes = null,
            params object[] parameters)
        {
            var exclude = gameObject.GetComponentsExclude(excludeComponentTypes);
            for (var i = 0; i < exclude.Length; i++)
            {
                var component = exclude[i];
                ReflectionUtil.Invoke(component, invokeMethodName, true, parameters);
            }
        }

        #endregion

        #endregion
    }
}
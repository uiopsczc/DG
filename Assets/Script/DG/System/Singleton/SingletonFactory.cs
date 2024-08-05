using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    /// <summary>
    /// 所有单例都从这里出来
    /// </summary>
    public class SingletonFactory : ISingleton
    {
        /// <summary>
        /// 非mono类的单例集合
        /// </summary>
        private readonly Dictionary<Type, object> _type2Singleton = new();

        /// <summary>
        /// Mono类的单例集合
        /// </summary>
        private readonly Dictionary<Type, GameObject> _type2SingletonMono = new();


        private static SingletonFactory _instance;


        public static SingletonFactory instance => _instance ??= new SingletonFactory();


        public void Init()
        {
        }


        /// <summary>
        /// 获取非mono类的单例集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : ISingleton, new()
        {
            Type type = typeof(T);
            if (_type2Singleton.TryGetValue(type, out var result))
                return (T)result;
            result = SingletonUtil.GetInstance(default(T));
            return (T)result;
        }

        /// <summary>
        /// Mono类的单例集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetMono<T>() where T : MonoBehaviour, ISingleton
        {
            Type type = typeof(T);
            if (_type2SingletonMono.TryGetValue(type, out var result))
            {
                if (result != null)
                    return result.GetComponent<T>();
            }

            result = SingletonUtil.GetInstanceMono(default(T)).gameObject;
            _type2SingletonMono[type] = result;
            return result.GetComponent<T>();
        }

        public void Destroy()
        {
            _type2Singleton.Clear();
            _type2SingletonMono.Clear();
            _instance = null;
        }
    }
}
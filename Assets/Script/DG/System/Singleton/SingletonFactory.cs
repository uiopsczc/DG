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
		private readonly Dictionary<Type, object> _dict = new Dictionary<Type, object>();

        /// <summary>
        /// Mono类的单例集合
        /// </summary>
        private Dictionary<Type, GameObject> _monoDict = new Dictionary<Type, GameObject>();


		private static SingletonFactory _instance;


		public static SingletonFactory instance => _instance ?? (_instance = new SingletonFactory());


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
			if (_dict.TryGetValue(type, out var result))
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
			if (_monoDict.TryGetValue(type, out var result))
			{
				if (result != null)
					return result.GetComponent<T>();
			}
			result = SingletonUtil.GetInstanceMono(default(T)).gameObject;
			_monoDict[type] = result;
			return result.GetComponent<T>();
		}

	    public void Destroy()
	    {
	        _dict.Clear();
	        _monoDict.Clear();
	        _instance = null;
	    }
	}
}
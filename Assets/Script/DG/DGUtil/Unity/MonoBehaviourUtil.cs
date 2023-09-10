using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DG
{
	public class MonoBehaviourUtil
	{
		#region IEnumerator

		/// <summary>
		/// ���ieSave��Ϊnull����ûִ���꣬��ͣ��ieSave��Ȼ��ieToStart��ֵ��ieSave��Ȼ��ʼieToStart
		/// </summary>
		/// <param name="monoBehaviour"></param>
		/// <param name="saveEnumerator"></param>
		/// <param name="toStartEnumerator"></param>
		/// <returns></returns>
		public static IEnumerator StopAndStartIEnumerator(MonoBehaviour monoBehaviour, ref IEnumerator saveEnumerator,
			IEnumerator toStartEnumerator)
		{
			if (saveEnumerator != null)
				monoBehaviour.StopCoroutine(saveEnumerator);
			saveEnumerator = toStartEnumerator;
			monoBehaviour.StartCoroutine(saveEnumerator);
			return saveEnumerator;
		}

		/// <summary>
		///  ��GetIEnumeratorDict()[ieName]��ֵ��ieSave��
		///  Ȼ���߼���  StopAndStartCacheIEnumerator(this MonoBehaviour mono, ref IEnumerator ieSave, IEnumerator ieToStart) һ��
		///  �����Ҫ��StopAndStartCoroutine(this MonoBehaviour mono, ref IEnumerator ieSave, IEnumerator ieToStart)��ֵ��ֵ��IEnumeratorDict[ieName]������IEnumeratorDict[ieName]��ֵ����仯�����µ�ֵ�滻��
		///  ���ӣ�  this.StopAndStartCacheIEnumerator("CountNum2", CountNum(100));   CountNum(100)ΪIEnumerator����
		/// </summary>
		/// <param name="monoBehaviour"></param>
		/// <param name="enumeratorName"></param>
		/// <param name="toStartEnumerator"></param>
		public static void StopAndStartCacheIEnumerator(MonoBehaviour monoBehaviour, string enumeratorName,
			IEnumerator toStartEnumerator)
		{
			Dictionary<string, IEnumerator> enumeratorDict = monoBehaviour.GetCacheIEnumeratorDict();
			IEnumerator saveEnumerator = enumeratorDict.GetOrAddDefault<IEnumerator>(enumeratorName);
			enumeratorDict[enumeratorName] =
				monoBehaviour.StopAndStartIEnumerator(ref saveEnumerator, toStartEnumerator);
		}

		/// <summary>
		/// ֹͣ������GetIEnumeratorDict�е�IEnumerator
		/// </summary>
		/// <param name="monoBehaviour"></param>
		public static void StopCacheIEnumeratorDict(MonoBehaviour monoBehaviour)
		{
			Dictionary<string, IEnumerator> enumeratorDict = monoBehaviour.GetCacheIEnumeratorDict();
			foreach (var kv in enumeratorDict)
			{
				var enumerator = kv.Value;
				if (enumerator == null) continue;
				monoBehaviour.StopCoroutine(enumerator);
			}
			enumeratorDict.Clear();
		}

		public static void StopCacheIEnumerator(MonoBehaviour monoBehaviour, string name)
		{
			Dictionary<string, IEnumerator> enumeratorDict = monoBehaviour.GetCacheIEnumeratorDict();
			if (!enumeratorDict.ContainsKey(name))
				return;
			if (enumeratorDict[name] == null)
				return;
			monoBehaviour.StopCoroutine(enumeratorDict[name]);
			enumeratorDict.Remove(name);
		}

		public static void RemoveCacheIEnumerator(MonoBehaviour monoBehaviour, string name)
		{
			Dictionary<string, IEnumerator> enumeratorDict = monoBehaviour.GetCacheIEnumeratorDict();
			if (enumeratorDict.ContainsKey(name))
				enumeratorDict.Remove(name);
		}

		#endregion

		#region PausableCoroutine

		public static PausableCoroutine StartPausableCoroutine(MonoBehaviour monoBehaviour,
			IEnumerator toStartEnumerator)
		{
			PausableCoroutine result =
				PausableCoroutineManager.instance.StartCoroutine(toStartEnumerator, monoBehaviour);
			return result;
		}

		public static PausableCoroutine StopAndStartPausableCoroutine(MonoBehaviour monoBehaviour,
			ref PausableCoroutine saveEnumerator, IEnumerator toStartEnumerator)
		{
			if (saveEnumerator != null)
				PausableCoroutineManager.instance.StopCoroutine(saveEnumerator.routine, monoBehaviour);
			PausableCoroutine result =
				PausableCoroutineManager.instance.StartCoroutine(toStartEnumerator, monoBehaviour);
			saveEnumerator = result;
			return result;
		}

		public static PausableCoroutine StartCachePausableCoroutine(MonoBehaviour monoBehaviour, string enumeratorName,
			IEnumerator toStartEnumerator)
		{
			Dictionary<string, PausableCoroutine> pausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
			pausableCoroutineDict[enumeratorName] = monoBehaviour.StartPausableCoroutine(toStartEnumerator);
			return pausableCoroutineDict[enumeratorName];
		}

		public static PausableCoroutine StopAndStartCachePausableCoroutine(MonoBehaviour monoBehaviour,
			string enumeratorName,
			IEnumerator toStartEnumerator)
		{
			Dictionary<string, PausableCoroutine> pausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
			PausableCoroutine saveEnumerator =
				pausableCoroutineDict.GetOrAddDefault<PausableCoroutine>(enumeratorName);
			pausableCoroutineDict[enumeratorName] =
				monoBehaviour.StopAndStartPausableCoroutine(ref saveEnumerator, toStartEnumerator);
			return pausableCoroutineDict[enumeratorName];
		}

		public static void StopCachePausableCoroutineDict(MonoBehaviour monoBehaviour)
		{
			Dictionary<string, PausableCoroutine> pausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
			foreach (var kv in pausableCoroutineDict)
			{
				var pausableCoroutine = kv.Value;
				if (pausableCoroutine != null)
					PausableCoroutineManager.instance.StopCoroutine(pausableCoroutine.routine, monoBehaviour);
			}
			pausableCoroutineDict.Clear();
		}

		public static void StopCachePausableCoroutine(MonoBehaviour monoBehaviour, string name)
		{
			Dictionary<string, PausableCoroutine> pausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
			if (!pausableCoroutineDict.ContainsKey(name))
				return;
			if (pausableCoroutineDict[name] == null)
				return;
			PausableCoroutineManager.instance.StopCoroutine(pausableCoroutineDict[name].routine, name);
			pausableCoroutineDict.Remove(name);
		}

		public static void RemoveCachePausableCoroutine(MonoBehaviour monoBehaviour, string name)
		{
			Dictionary<string, PausableCoroutine> pausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
			if (pausableCoroutineDict.ContainsKey(name))
				pausableCoroutineDict.Remove(name);
		}

		#endregion

		#region CacheMono

		public static MonoBehaviourCache GetMonoBehaviourCache(MonoBehaviour monoBehaviour)
		{
			return monoBehaviour.GetPropertyValue<MonoBehaviourCache>(MonoBehaviourCacheConst.MonoBehaviourCache);
		}

		/// <summary>
		/// ��mono����Ҫ���������  protected CacheMono cacheMono=new CacheMono(this);
		/// ��ȡ�������cacheMono.dict[dictName]
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="monoBehaviour"></param>
		/// <param name="dictName"></param>
		/// <param name="whenNotContainKeyFunc">��monoBehaviourDicts��Key�в�����dictNameʱ�ĵ��õĴ�������</param>
		/// <returns></returns>
		public static T GetOrAddCacheDict<T>(MonoBehaviour monoBehaviour, string dictName,
			Func<T> whenNotContainKeyFunc)
		{
			MonoBehaviourCache monoBehaviourCache = monoBehaviour.GetMonoBehaviourCache();
			return monoBehaviourCache.dict.GetOrAddDefault(dictName, () => whenNotContainKeyFunc());
		}

		/// <summary>
		/// ��mono����Ҫ���������  protected CacheMono cacheMono=new CacheMono(this);
		/// ��ȡ������� cacheMono.dict[dictName]
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="monoBehaviour"></param>
		/// <param name="dictName"></param>
		/// <returns></returns>
		public static T GetOrAddCacheDict<T>(MonoBehaviour monoBehaviour, string dictName) where T : new()
		{
			return monoBehaviour.GetOrAddCacheDict(dictName, () => new T());
		}

		/// <summary>
		/// ��ȡ����� Dictionary<string, IEnumerator> cacheMono.dict["IEnumeratorDict"]
		/// </summary>
		/// <param name="monoBehaviour"></param>
		/// <returns></returns>
		public static Dictionary<string, IEnumerator> GetCacheIEnumeratorDict(MonoBehaviour monoBehaviour)
		{
			return monoBehaviour.GetOrAddCacheDict<Dictionary<string, IEnumerator>>(MonoBehaviourCacheConst
				.IEnumeratorDict);
		}

		public static Dictionary<string, PausableCoroutine> GetCachePausableCoroutineDict(MonoBehaviour monoBehaviour)
		{
			return monoBehaviour.GetOrAddCacheDict<Dictionary<string, PausableCoroutine>>(MonoBehaviourCacheConst
				.PausableCoroutineDict);
		}

		#endregion
	}
}
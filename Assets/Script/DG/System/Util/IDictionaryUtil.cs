using System;
using System.Collections;
using System.Collections.Generic;

namespace DG
{
	public static class IDictionaryUtil
	{
		//////////////////////////////////////////////////////////////////////
		// Diff相关
		//////////////////////////////////////////////////////////////////////
		// 必须和ApplyDiff使用
		// 以new为基准，获取new相对于old不一样的部分
		// local diff = table.GetDiff(old, new)
		//  table.ApplyDiff(old, diff)
		// 这样old的就变成和new一模一样的数据
		public static LinkedHashtable GetDiff(IDictionary oldDict, IDictionary newDict)
		{
			var diff = new LinkedHashtable();
			foreach (DictionaryEntry dictionaryEntry in newDict)
			{
				var newKey = dictionaryEntry.Key;
				var newValue = dictionaryEntry.Value;
				bool isOldDictContainsNewKey = oldDict.Contains(newKey);
				var oldValue = isOldDictContainsNewKey ? oldDict[newKey] : null;
				if (newValue is IDictionary newValueDict)
				{
					int newValueDictCount = newValueDict.Count;
					if (newValueDictCount == 0)
					{
						if ((!isOldDictContainsNewKey) || oldValue.GetType() != newValueDict.GetType() ||
						    (oldValue is IDictionary oldValueDict &&
						     (oldValueDict.Count != 0)))
						{
							diff[newKey] = StringConst.STRING_NEW_IN_TABLE + newValueDict.GetType();
							continue;
						}
					}

					if (isOldDictContainsNewKey && oldValue is IDictionary oldValueDict2)
					{
						diff[newKey] = GetDiff(oldValueDict2, newValueDict);
						continue;
					}

					if (!isOldDictContainsNewKey || !newValueDict.Equals(oldValue))
					{
						diff[newKey] = CloneUtil.CloneDeep(newValue);
						continue;
					}
				}

				if (newValue is IList list && isOldDictContainsNewKey && oldValue is IList oldValueList)
				{
					diff[newKey] = IListUtil.GetDiff(oldValueList, list);
					continue;
				}

				if (!isOldDictContainsNewKey || !newValue.Equals(oldValue))
				{
					diff[newKey] = newValue;
				}
			}

			foreach (DictionaryEntry oldDictionaryEntry in oldDict)
			{
				var key = oldDictionaryEntry.Key;
				if (!newDict.Contains(key))
					diff[key] = StringConst.STRING_NIL_IN_TABLE;
			}

			if (diff.Count == 0)
				diff = null;
			return diff;
		}

		// table.ApplyDiff(old, diff)
		// 将diff中的东西应用到old中
		public static void ApplyDiff(IDictionary oldDict, LinkedHashtable diffDict)
		{
			if (diffDict == null)
			{
				return;
			}

			foreach (DictionaryEntry dictionaryEntry in diffDict)
			{
				var key = dictionaryEntry.Key;
				var value = dictionaryEntry.Value;
				if (StringConst.STRING_NIL_IN_TABLE.Equals(value))
				{
					oldDict.Remove(key);
					continue;
				}

				var valueString = value.ToString();
				if (valueString.StartsWith(StringConst.STRING_NEW_IN_TABLE))
				{
					string typeString = valueString.Substring(StringConst.STRING_NEW_IN_TABLE.Length);
					Type type = TypeUtil.GetType(typeString);
					oldDict[key] = type.CreateInstance<object>();
					continue;
				}

				if (oldDict.Contains(key))
				{
					var oldValue = oldDict[key];
					if (oldValue is IDictionary oldValueDict && value is LinkedHashtable hashtable)
					{
						ApplyDiff(oldValueDict, hashtable);
						continue;
					}

					if (oldValue is IList oldValueList && value is LinkedHashtable linkedHashtable)
					{
						oldDict[key] = IListUtil.ApplyDiff(oldValueList, linkedHashtable);
						continue;
					}
				}

				oldDict[key] = value;
			}
		}

		// 必须和ApplyDiff使用
		// 以new为基准，获取new中有，但old中没有的
		// local diff = table.GetNotExist(old, new)
		// table.ApplyDiff(old, diff)
		// 这样old就有new中的字段
		public static LinkedHashtable GetNotExist(IDictionary oldDict, IDictionary newDict)
		{
			var diff = new LinkedHashtable();
			foreach (DictionaryEntry dictionaryEntry in newDict)
			{
				var newK = dictionaryEntry.Key;
				var newV = dictionaryEntry.Value;
				if (!oldDict.Contains(newK))
					diff[newK] = newV;
				else
				{
					var oldValue = oldDict[newK];
					if (newV is IDictionary dictionary && oldValue is IDictionary dictionary1)
						diff[newK] = GetNotExist(dictionary1, dictionary);
					else if (newV is IList list && oldValue is IList list1)
						diff[newK] = IListUtil.GetNotExist(list1, list);
					//其他情况不用处理
				}
			}

			return diff;
		}

		//两个table是否不一样
		public static bool IsDiff(IDictionary oldDict, IDictionary newDict)
		{
			foreach (DictionaryEntry dictionaryEntry in oldDict)
			{
				var key = dictionaryEntry.Key;
				if (!newDict.Contains(key))
					return true;
			}

			foreach (DictionaryEntry dictionaryEntry in newDict)
			{
				var newKey = dictionaryEntry.Key;
				var newValue = dictionaryEntry.Value;
				if (!oldDict.Contains(newKey))
					return false;
				var oldValue = oldDict[newKey];
				switch (newValue)
				{
					case IDictionary _ when !(oldValue is IDictionary):
						return false;
					case IDictionary dictionary when IsDiff((IDictionary) oldValue, dictionary):
						return true;
					case IDictionary _:
						break;
					case IList _ when !(oldValue is IList):
						return false;
					case IList list when IListUtil.IsDiff(oldValue as IList, list):
						return true;
					case IList _:
						break;
					default:
					{
						if (!newValue.Equals(oldDict[newKey]))
							return true;
						break;
					}
				}
			}

			return false;
		}

		
		public static T Get<T>(IDictionary dict, object key)
		{
			return dict.Contains(key) ? dict[key].To<T>() : default;
		}

		public static T GetOrGetDefault<T>(IDictionary dict, object key, T defaultValue = default)
		{
			return dict.Contains(key) ? dict[key].To<T>() : defaultValue;
		}

		public static T GetOrGetByDefaultFunc<T>(IDictionary dict, object key, Func<T> defaultFunc = null)
		{
			if (dict.Contains(key))
				return dict[key].To<T>();
			if (defaultFunc != null)
				return defaultFunc();
			return default(T);
		}
		public static T GetOrGetNew<T>(IDictionary dict, object key)where T:new()
		{
			return dict.Contains(key) ? dict[key].To<T>() : new T();
		}

		public static T GetOrGetByNewFunc<T>(IDictionary dict, object key, Func<T> newFunc = null) where T : new()
		{
			if (dict.Contains(key))
				return dict[key].To<T>();
			if (newFunc != null)
				return newFunc();
			return new T();
		}
		
		public static T GetOrAddDefault<T>(IDictionary dict, object key, T defaultValue = default)
		{
			if (dict.Contains(key))
				return dict[key].To<T>();
			dict[key] = defaultValue;
			return defaultValue;
		}

		public static T GetOrAddByDefaultFunc<T>(IDictionary dict, object key, Func<T> defaultFunc = null)
		{
			if (dict.Contains(key))
				return dict[key].To<T>();
			var result = defaultFunc != null ? defaultFunc() : default;
			dict[key] = result;
			return result;
		}
		public static T GetOrAddNew<T>(IDictionary dict, object key) where T : new()
		{
			if (dict.Contains(key))
				return dict[key].To<T>();
			var result = new T();
			dict[key] = result;
			return result;
		}

		public static T GetOrAddByNewFunc<T>(IDictionary dict, object key, Func<T> newFunc = null) where T : new()
		{
			if (dict.Contains(key))
				return dict[key].To<T>();
			var result = newFunc != null ? newFunc() : new T();
			dict[key] = result;
			return result;
		}


		public static V Remove2<V>(IDictionary dict, object key)
		{
			if (!dict.Contains(key))
				return default;

			V result = (V)dict[key];
			dict.Remove(key);
			return result;
		}

		

		//删除值为null值、0数值、false逻辑值、空字符串、空集合等数据项
		public static void Trim(IDictionary dict)
		{
			List<object> toRemoveKeyList = new List<object>();
			foreach (DictionaryEntry dictionaryEntry in dict)
			{
				var key = dictionaryEntry.Key;
				var value = dictionaryEntry.Value;
				switch (value)
				{
					//删除值为null的数值
					case null:
						toRemoveKeyList.Add(key);
						break;
					default:
						{
							if (value.IsNumber() && value.To<double>() == 0) //删除值为0的数值
								toRemoveKeyList.Add(key);
							else if (value.IsBool() && (bool)value == false) //删除值为false的逻辑值
								toRemoveKeyList.Add(key);
							else if (value.IsString() && ((string)value).IsNullOrWhiteSpace()) //删除值为空的字符串
								toRemoveKeyList.Add(key);
							else if (value is ICollection collection && collection.Count == 0) //删除为null的collection
								toRemoveKeyList.Add(key);
							else if (value is IDictionary dictionary)
								Trim(dictionary);
							break;
						}
				}
			}

			for (var i = 0; i < toRemoveKeyList.Count; i++)
			{
				var toRemoveKey = toRemoveKeyList[i];
				dict.Remove(toRemoveKey);
			}
		}

		//删除值为null值、0数值、false逻辑值、空字符串、空集合等数据项
		public static Hashtable ToHashtable(IDictionary dict)
		{
			Hashtable result = new Hashtable();
			foreach (DictionaryEntry dictionaryEntry in dict)
			{
				var key = dictionaryEntry;
				result[key] = dict[key];
			}
			return result;
		}

		public static void Combine(IDictionary dict, IDictionary another)
		{
			foreach (DictionaryEntry anotherDictionaryEntry in another)
			{
				var anotherKey = anotherDictionaryEntry.Key;
				if (!dict.Contains(anotherKey))
					dict[anotherKey] = another[anotherKey];
			}
		}

		public static void RemoveByFunc(IDictionary dict, Func<object, object, bool> func)
		{
			List<object> toRemoveKeyList = new List<object>();
			foreach (DictionaryEntry dictionaryEntry in dict)
			{
				var key = dictionaryEntry.Key;
				var value = dictionaryEntry.Value;
				if (func(key, value))
					toRemoveKeyList.Add(key);
			}

			for (var i = 0; i < toRemoveKeyList.Count; i++)
			{
				var toRemoveKey = toRemoveKeyList[i];
				dict.Remove(toRemoveKey);
			}
		}
	}
}
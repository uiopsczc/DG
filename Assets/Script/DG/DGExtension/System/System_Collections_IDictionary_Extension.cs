using System;
using System.Collections;

namespace DG
{
	public static class System_Collections_IDictionary_Extension
	{
		//////////////////////////////////////////////////////////////////////
		// Diff���
		//////////////////////////////////////////////////////////////////////
		// �����ApplyDiffʹ��
		// ��newΪ��׼����ȡnew�����old��һ���Ĳ���
		// local diff = table.GetDiff(old, new)
		//  table.ApplyDiff(old, diff)
		// ����old�ľͱ�ɺ�newһģһ��������
		public static LinkedHashtable GetDiff(this IDictionary self, IDictionary newDict)
		{
			return IDictionaryUtil.GetDiff(self, newDict);
		}

		// table.ApplyDiff(old, diff)
		// ��diff�еĶ���Ӧ�õ�old��
		public static void ApplyDiff(this IDictionary self, LinkedHashtable diffDict)
		{
			IDictionaryUtil.ApplyDiff(self, diffDict);
		}

		// �����ApplyDiffʹ��
		// ��newΪ��׼����ȡnew���У���old��û�е�
		// local diff = table.GetNotExist(old, new)
		// table.ApplyDiff(old, diff)
		// ����old����new�е��ֶ�
		public static LinkedHashtable GetNotExist(this IDictionary self, IDictionary newDict)
		{
			return IDictionaryUtil.GetNotExist(self, newDict);
		}

		//����table�Ƿ�һ��
		public static bool IsDiff(this IDictionary self, IDictionary newDict)
		{
			return IDictionaryUtil.IsDiff(self, newDict);
		}


		public static T Get<T>(this IDictionary self, object key)
		{
			return IDictionaryUtil.Get<T>(self, key);
		}

		public static T GetOrGetDefault<T>(this IDictionary self, object key, T defaultValue = default)
		{
			return IDictionaryUtil.GetOrGetDefault<T>(self, key, defaultValue);
		}

		public static T GetOrGetDefault<T>(this IDictionary self, object key, Func<T> defaultFunc = null)
		{
			return IDictionaryUtil.GetOrGetDefault<T>(self, key, defaultFunc);
		}
		public static T GetOrGetNew<T>(this IDictionary self, object key) where T : new()
		{
			return IDictionaryUtil.GetOrGetNew<T>(self, key);
		}

		public static T GetOrGetNew<T>(this IDictionary self, object key, Func<T> newFunc = null) where T : new()
		{
			return IDictionaryUtil.GetOrGetNew<T>(self, key, newFunc);
		}

		public static T GetOrAddDefault<T>(this IDictionary self, object key, T defaultValue = default)
		{
			return IDictionaryUtil.GetOrAddDefault<T>(self, key, defaultValue);
		}

		public static T GetOrAddDefault<T>(this IDictionary self, object key, Func<T> defaultFunc = null)
		{
			return IDictionaryUtil.GetOrAddDefault<T>(self, key, defaultFunc);
		}
		public static T GetOrAddNew<T>(this IDictionary self, object key) where T : new()
		{
			return IDictionaryUtil.GetOrAddNew<T>(self, key);
		}

		public static T GetOrAddNew<T>(this IDictionary self, object key, Func<T> newFunc = null) where T : new()
		{
			return IDictionaryUtil.GetOrAddNew<T>(self, key, newFunc);
		}


		public static V Remove2<V>(this IDictionary self, object key)
		{
			return IDictionaryUtil.Remove2<V>(self, key);
		}



		//ɾ��ֵΪnullֵ��0��ֵ��false�߼�ֵ�����ַ������ռ��ϵ�������
		public static void Trim(this IDictionary self)
		{
			IDictionaryUtil.Trim(self);
		}

		//ɾ��ֵΪnullֵ��0��ֵ��false�߼�ֵ�����ַ������ռ��ϵ�������
		public static Hashtable ToHashtable(this IDictionary self)
		{
			return IDictionaryUtil.ToHashtable(self);
		}

		public static void Combine(this IDictionary self, IDictionary another)
		{
			IDictionaryUtil.Combine(self, another);
		}

		public static void RemoveByFunc(this IDictionary self, Func<object, object, bool> func)
		{
			IDictionaryUtil.RemoveByFunc(self, func);
		}
	}
}
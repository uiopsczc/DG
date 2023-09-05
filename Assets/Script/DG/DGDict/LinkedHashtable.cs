using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DG
{
	public partial class LinkedHashtable : Hashtable, IDGToString
	{
		private readonly ArrayList _keyList = new ArrayList();
		private readonly ArrayList _valueList = new ArrayList();
		private DictionaryEnumerator __enumerator;

		private DictionaryEnumerator _enumerator =>
			__enumerator ?? (__enumerator = new DictionaryEnumerator(_keyList, _valueList));

		public override ICollection Keys => _keyList;
		public override ICollection Values => _valueList;

		public new object this[object key]
		{
			get => base[key];
			set => Put(key, value);
		}


		#region override method

		public override void Add(object key, object value)
		{
			_keyList.Add(key);
			_valueList.Add(value);
			base.Add(key, value);
		}

		public override void Clear()
		{
			_keyList.Clear();
			_valueList.Clear();
			base.Clear();
		}

		public override void Remove(object key)
		{
			int index = _keyList.IndexOf(key);
			if (index == -1) return;
			_keyList.RemoveAt(index);
			_valueList.RemoveAt(index);
			base.Remove(key);
		}

		public override IDictionaryEnumerator GetEnumerator()
		{
			_enumerator.Reset();
			return _enumerator;
		}

		#endregion


		public void Put(object key, object value)
		{
			int index = _keyList.IndexOf(key);
			//删除原来的
			if (index != -1)
			{
				_keyList.RemoveAt(index);
				_valueList.RemoveAt(index);
			}

			_keyList.Add(value);
			_valueList.Add(value);
			//然后放到最后
			base[key] = value;
		}

		public void Sort(Func<object, object, bool> compareFunc)
		{
			_keyList.QuickSort(compareFunc);
			_valueList.Clear();
			for (var i = 0; i < _keyList.Count; i++)
			{
				var key = _keyList[i];
				_valueList.Add(this[key]);
			}
		}

		public string DGToString(bool isFillStringWithDoubleQuote = false)
		{
			bool first = true;
			var stringBuilder = new StringBuilder();
			stringBuilder.Append(CharConst.Char_LeftCurlyBrackets);
			for (var i = 0; i < _keyList.Count; i++)
			{
				object key = _keyList[i];
				if (first)
					first = false;
				else
					stringBuilder.Append(CharConst.Char_Comma);
				stringBuilder.Append(key.DGToString(isFillStringWithDoubleQuote));
				stringBuilder.Append(CharConst.Char_Colon);
				object value = base[key];
				stringBuilder.Append(value.DGToString(isFillStringWithDoubleQuote));
			}

			stringBuilder.Append(CharConst.Char_RightCurlyBrackets);
			return stringBuilder.ToString();
		}

		public new LinkedHashtable Clone() //深clone
		{
			MemoryStream stream = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, this);
			stream.Position = 0;
			return formatter.Deserialize(stream) as LinkedHashtable;
		}
	}
}
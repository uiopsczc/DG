using System.Collections.Generic;

namespace DG
{
	public class BalanceGroup
	{
		private int _openStartIndex;
		private int _openEndIndex;

		private int _closeStartIndex;
		private int _closeEndIndex;

		private List<BalanceGroup> _childrenList = new();

		public void SetOpenIndexes(int openStartIndex, int openEndIndex)
		{
			_openStartIndex = openStartIndex;
			_openEndIndex = openEndIndex;
		}

		public void SetCloseIndexes(int closeStartIndex, int closeEndIndex)
		{
			_closeStartIndex = closeStartIndex;
			_closeEndIndex = closeEndIndex;
		}

		public string GetContent(string s)
		{
			return s.Substring(_openEndIndex + 1, _closeStartIndex - _openEndIndex - 1);
		}
	}
}
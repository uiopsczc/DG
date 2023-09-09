using UnityEngine;

namespace DG
{
	public static class UnityEngine_AnimatorStateInfo_Extension
	{
		public static bool IsName(this AnimatorStateInfo self, string prefix, params string[] suffixes)
		{
			return AnimatorStateInfoUtil.IsName(self, prefix, suffixes);
		}
	}
}



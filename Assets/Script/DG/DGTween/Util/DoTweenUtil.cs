using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
	public class DOTweenUtil
	{
		public static DOTweenId GetDOTweenId(object source = null,
			string prefix = StringConst.String_DOTweenId_Use_GameTime)
		{
			return new DOTweenId(source, prefix);
		}

		public static List<Tween> GetDOTweens(object source = null,
			string prefix = StringConst.String_DOTweenId_Use_GameTime)
		{
			List<Tween> tweenList = new List<Tween>();
			if (DOTween.PlayingTweens() == null) return tweenList;
			var list = DOTween.PlayingTweens();
			for (var i = 0; i < list.Count; i++)
			{
				var tween = list[i];
				if (source == null)
				{
					if (tween.id is DOTweenId id && id.prefix == prefix)
						tweenList.Add(tween);
				}
				else
				{
					if (tween.id is DOTweenId && tween.id.Equals(new DOTweenId(source, prefix)))
						tweenList.Add(tween);
				}

				if (tween.id is string s && s.Equals(prefix))
					tweenList.Add(tween);
			}

			return tweenList;
		}

		public static Tween SetDOTweenId(Tween tween, object objOfDOTweenId = null)
		{
			objOfDOTweenId = objOfDOTweenId ?? tween.target;

			return tween.SetId(objOfDOTweenId.DOTweenId());
		}

		public static Tweener DOBlendableLocalMoveXBy(Transform target, float byValue, float duration,
			bool snapping = false)
		{
			return target.DOBlendableLocalMoveBy(
				new Vector3(byValue, 0, 0), duration,
				snapping);
		}

		public static Tweener DOBlendableLocalMoveYBy(Transform target, float byValue, float duration,
			bool snapping = false)
		{
			return target.DOBlendableLocalMoveBy(
				new Vector3(0, byValue, 0), duration,
				snapping);
		}

		public static Tweener DOBlendableLocalMoveZBy(Transform target, float byValue, float duration,
			bool snapping = false)
		{
			return target.DOBlendableLocalMoveBy(
				new Vector3(0, 0, byValue), duration,
				snapping);
		}


		public static Tweener DOBlendableMoveXBy(Transform target, float byValue, float duration,
			bool snapping = false)
		{
			return target.DOBlendableMoveBy(
				new Vector3(byValue, 0, 0), duration,
				snapping);
		}

		public static Tweener DOBlendableMoveYBy(Transform target, float byValue, float duration,
			bool snapping = false)
		{
			return target.DOBlendableMoveBy(
				new Vector3(0, byValue, 0), duration,
				snapping);
		}

		public static Tweener DOBlendableMoveZBy(Transform target, float byValue, float duration,
			bool snapping = false)
		{
			return target.DOBlendableMoveBy(
				new Vector3(0, 0, byValue), duration,
				snapping);
		}
	}
}
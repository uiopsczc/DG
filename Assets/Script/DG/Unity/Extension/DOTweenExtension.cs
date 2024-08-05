using DG.Tweening;
using UnityEngine;

namespace DG
{
    public static class DOTweenExtension
    {
        public static Tween SetDOTweenId(this Tween self, object objOfDOTweenId = null)
        {
            return DOTweenUtil.SetDOTweenId(self, objOfDOTweenId);
        }

        public static Tweener DOBlendableLocalMoveXBy(this Transform target, float byValue, float duration,
            bool snapping = false)
        {
            return DOTweenUtil.DOBlendableLocalMoveXBy(target, byValue, duration, snapping);
        }

        public static Tweener DOBlendableLocalMoveYBy(this Transform target, float byValue, float duration,
            bool snapping = false)
        {
            return DOTweenUtil.DOBlendableLocalMoveYBy(target, byValue, duration, snapping);
        }

        public static Tweener DOBlendableLocalMoveZBy(this Transform target, float byValue, float duration,
            bool snapping = false)
        {
            return DOTweenUtil.DOBlendableLocalMoveZBy(target, byValue, duration, snapping);
        }


        public static Tweener DOBlendableMoveXBy(this Transform target, float byValue, float duration,
            bool snapping = false)
        {
            return DOTweenUtil.DOBlendableMoveXBy(target, byValue, duration, snapping);
        }

        public static Tweener DOBlendableMoveYBy(this Transform target, float byValue, float duration,
            bool snapping = false)
        {
            return DOTweenUtil.DOBlendableMoveYBy(target, byValue, duration, snapping);
        }

        public static Tweener DOBlendableMoveZBy(this Transform target, float byValue, float duration,
            bool snapping = false)
        {
            return DOTweenUtil.DOBlendableMoveZBy(target, byValue, duration, snapping);
        }
    }
}
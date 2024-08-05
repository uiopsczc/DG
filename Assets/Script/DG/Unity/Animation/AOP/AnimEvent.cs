using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    public class AnimEvent : MonoBehaviour
    {
        private static readonly AnimationEvent[] _emptyEvent = new AnimationEvent[0];
        private static readonly string _onTriggerString = "OnTrigger";

        /// <summary>
        ///   每个切片上的所有事件(key:该Animator上的ClipName)
        /// </summary>
        private readonly Dictionary<string, List<string>> _clipName2ClipEventList = new();

        /// <summary>
        ///   每个事件对应回调(key:clipName + percentage)
        /// </summary>
        private readonly Dictionary<string, List<DelegateStruct>> _clipNamePct2eventCallbackList = new();


        private Animator _animator => GetComponent<Animator>();
        private AnimationClip[] _clips => _animator.runtimeAnimatorController.animationClips;


        #region private method

        private void _OnTrigger(string eventKey)
        {
            if (!_clipNamePct2eventCallbackList.ContainsKey(eventKey))
            {
                DGLog.WarnFormat("{0}:不存在eventCallbacks {1}", GetType().Name, eventKey);
                return;
            }

            for (var i = 0; i < _clipNamePct2eventCallbackList[eventKey].Count; i++)
            {
                var callbackStruct = _clipNamePct2eventCallbackList[eventKey][i];
                callbackStruct.Invoke();
            }
        }

        #endregion

        #region public method

        public string GetEventKey(string clipName, float percentage)
        {
            return string.Format("{0}_{1}", clipName, percentage);
        }

        #region add

        //只添加一个AnimationEvent到AnimationClip指定时间的上，然后触发的时候，调用strOnComplete方法（里面会调用eventCallbacks对应的方法（key：clipName + percentage）触发callBackList)
        //如何将Delegate作为参数   使用lamba表达式,()=>{} 作为参数要将该delegate设置为Callback等，如 (Action)((args)=>{LogCat.Log(args);})
        //例子: AddEvents((Action<string, string>)((a, b) => { LogCat.LogWarning(a+b);}), "TestAnimationClip", 1f,"aabbcc","ddff");
        public AnimEvent AddEvents(Delegate callback, string clipName, float percentage, params object[] callbackArgs)
        {
            _clipName2ClipEventList.GetOrAddByDefaultFunc(clipName, () => new List<string>());

            var eventKey = GetEventKey(clipName, percentage);
            var hasEventCallback = _clipNamePct2eventCallbackList.ContainsKey(eventKey);
            _clipNamePct2eventCallbackList.GetOrAddByDefaultFunc(eventKey, () => new List<DelegateStruct>());
            _clipNamePct2eventCallbackList[eventKey].Add(new DelegateStruct(callback, callbackArgs));
            if (_clipName2ClipEventList[clipName].FindIndex(a => a.Equals(eventKey)) == -1)
                _clipName2ClipEventList[clipName].Add(eventKey);

            DGLog.Warn(_clipName2ClipEventList[clipName].Count);

            for (var i = 0; i < _clips.Length; i++)
            {
                var clip = _clips[i];
                if (!clip.name.Equals(clipName)) continue;
                if (hasEventCallback) continue;
                var animationEvent = new AnimationEvent();
                animationEvent.functionName = _onTriggerString;
                animationEvent.messageOptions = SendMessageOptions.RequireReceiver;
                animationEvent.time = clip.length * percentage;
                animationEvent.stringParameter = eventKey;
                clip.AddEvent(animationEvent);
            }

            return this;
        }

        public AnimEvent AddEvents(Action callback, string clipName, float percentage)
        {
            return AddEvents((Delegate)callback, clipName, percentage);
        }

        #endregion

        #region replace

        public AnimEvent ReplaceEvents(Delegate callback, string clipName, float percentage,
            params object[] callbackArgs)
        {
            var eventKey = GetEventKey(clipName, percentage);
            if (_clipNamePct2eventCallbackList.TryGetValue(eventKey, out var value))
                value.Clear(); //确保只有一个
            return AddEvents(callback, clipName, percentage, callbackArgs);
        }

        public AnimEvent ReplaceEvents(Action callback, string clipName, float percentage)
        {
            return ReplaceEvents((Delegate)callback, clipName, percentage);
        }

        #endregion

        #region remove

        public AnimEvent RemoveClipEvents(string clipName)
        {
            if (_clips != null)
                for (var i = 0; i < _clips.Length; i++)
                {
                    var clip = _clips[i];
                    if (clip == null) continue;
                    if (!clip.name.Equals(clipName)) continue;
                    clip.events = _emptyEvent;
                }

            List<string> eventList;
            if (_clipName2ClipEventList.TryGetValue(clipName, out eventList))
            {
                for (var i = 0; i < eventList.Count; i++)
                {
                    var eventName = eventList[i];
                    _clipNamePct2eventCallbackList.Remove(eventName);
                }

                _clipName2ClipEventList.Remove(clipName);
            }

            return this;
        }

        public void RemoveAllEvent(bool destroy = false)
        {
            var clipEventKeys = new List<string>(_clipName2ClipEventList.Keys);
            for (var i = 0; i < clipEventKeys.Count; i++)
            {
                var clipName = clipEventKeys[i];
                RemoveClipEvents(clipName);
            }

            if (destroy) this.Destroy();
        }

        #endregion

        #endregion
    }
}
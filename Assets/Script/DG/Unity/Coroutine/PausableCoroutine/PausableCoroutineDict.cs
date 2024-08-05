using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    public class PausableCoroutineDict
    {
        private readonly MonoBehaviour _monoBehaviour;
        private readonly DGPoolItemDict<ulong> _idPoolItemDict = new(new IdPool());
        private readonly Dictionary<string, PausableCoroutine> _name2PausableCoroutine = new();
        private readonly List<string> _toRemoveKeyList = new();

        public PausableCoroutineDict(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
        }

        public MonoBehaviour GetMonoBehaviour()
        {
            return _monoBehaviour;
        }

        public string StartCoroutine(IEnumerator iEnumerator, string key = null)
        {
            _CleanFinishedCoroutines();
            key ??= _idPoolItemDict.Get().ToString();
            var coroutine = _monoBehaviour.StopAndStartCachePausableCoroutine(key.ToGuid(this), iEnumerator);
            _name2PausableCoroutine[key] = coroutine;
            return key;
        }

        /// <summary>
        /// 此处的key与StartCoroutine的key保持一致
        /// </summary>
        /// <param name="key"></param>
        public void StopCoroutine(string key)
        {
            _CleanFinishedCoroutines();
            if (!_name2PausableCoroutine.ContainsKey(key))
                return;
            _name2PausableCoroutine.Remove(key);
            if (ulong.TryParse(key, out ulong id))
                _idPoolItemDict.Remove(id);
            _monoBehaviour.StopCachePausableCoroutine(key.ToGuid(this));
        }

        public void StopAllCoroutines()
        {
            foreach (var keyValue in _name2PausableCoroutine)
            {
                var key = keyValue.Key;
                _monoBehaviour.StopCachePausableCoroutine(key.ToGuid(this));
            }

            _name2PausableCoroutine.Clear();
            _idPoolItemDict.Clear();
        }

        public void SetIsPaused(bool isPaused)
        {
            _CleanFinishedCoroutines();
            foreach (var keyValue in _name2PausableCoroutine)
            {
                var key = keyValue.Key;
                _name2PausableCoroutine[key].SetIsPaused(isPaused);
            }
        }

        void _CleanFinishedCoroutines()
        {
            foreach (var keyValue in _name2PausableCoroutine)
            {
                var key = keyValue.Key;
                var coroutine = keyValue.Value;
                if (coroutine.isFinished)
                {
                    if (ulong.TryParse(key, out var id))
                        _idPoolItemDict.Remove(id);
                    _toRemoveKeyList.Add(key);
                }
            }

            for (var i = 0; i < _toRemoveKeyList.Count; i++)
            {
                var toRemoveKey = _toRemoveKeyList[i];
                _name2PausableCoroutine.Remove(toRemoveKey);
            }

            _toRemoveKeyList.Clear();
        }
    }
}
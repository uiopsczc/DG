using System.Collections.Generic;
using DG.Tweening;

namespace DG
{
    public class DOTweenDict
    {
        Dictionary<string, Tween> _name2Tween = new();
        private IdPool _idPool = new();

        public Sequence AddDOTweenSequence(string key)
        {
            if (key != null && _name2Tween.ContainsKey(key))
                RemoveDOTween(key);
            key ??= _idPool.SpawnValue().ToString();
            var sequence = DOTween.Sequence();
            _name2Tween[key] = sequence;
            sequence.OnKill(() => RemoveDOTween(key));
            return sequence;
        }

        public Tween AddDOTween(string key, Tween tween)
        {
            if (key != null && _name2Tween.ContainsKey(key))
                RemoveDOTween(key);
            key ??= _idPool.SpawnValue().ToString();
            _name2Tween[key] = tween;
            tween.OnKill(() => RemoveDOTween(key));
            return tween;
        }

        public void RemoveDOTween(string key)
        {
            if (_name2Tween.TryGetValue(key, out var tween))
            {
                if (tween.IsActive())
                    _name2Tween[key].Kill();
                _idPool.DeSpawnValue(key);
                _name2Tween.Remove(key);
            }
        }

        public void RemoveDOTween(Tween tween)
        {
            string key = null;
            foreach (var kv in _name2Tween)
            {
                var dictKey = kv.Key;
                if (_name2Tween[dictKey] != tween) continue;
                key = dictKey;
                break;
            }

            if (key != null)
                RemoveDOTween(key);
        }

        public void SetIsPaused(bool isPaused)
        {
            foreach (var kv in _name2Tween)
            {
                var tween = kv.Value;
                if (!tween.IsActive())
                    continue;
                if (isPaused)
                    tween.Pause();
                else
                    tween.Play();
            }
        }

        public void RemoveDOTweens()
        {
            List<string> keyList = new List<string>(_name2Tween.Keys);
            for (int i = 0; i < keyList.Count; i++)
            {
                var key = keyList[i];
                _name2Tween[key].Kill();
            }
        }

        public void Reset()
        {
            RemoveDOTweens();
        }

        public void Destroy()
        {
            Reset();
            _name2Tween = null;
            _idPool = null;
        }
    }
}
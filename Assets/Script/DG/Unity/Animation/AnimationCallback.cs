using UnityEngine;
using UnityEngine.Events;

namespace DG
{
    public class AnimationCallback : MonoBehaviour
    {
        public UnityEvent myEvent;

        public void DestroyOnFinish()
        {
            gameObject.Destroy();
        }

        public void DeactiveOnFinish()
        {
            gameObject.SetActive(false);
        }

        public void DesSpawnOnFinish()
        {
            gameObject.DeSpawn();
        }

        public void OnFinish()
        {
            myEvent?.Invoke();
        }
    }
}
using UnityEngine;
using UnityEngine.Audio;

namespace DG
{
    public class SingletonMaster : MonoBehaviour, ISingleton
    {
        public AudioMixer audioMixer;
        public GameObject[] inActiveGameObjects;
        public static SingletonMaster instance => SingletonFactory.instance.GetMono<SingletonMaster>();

        public void Init()
        {
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
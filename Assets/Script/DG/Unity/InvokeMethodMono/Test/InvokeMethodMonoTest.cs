using UnityEngine;

namespace DG
{
    public class InvokeMethodMonoTest : MonoBehaviour
    {
        void Start()
        {
            transform.GetComponent<InvokeMethodMono>().Invoke();
        }
    }
}
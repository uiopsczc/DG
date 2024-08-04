using UnityEngine;

namespace DG
{
	public class InvokeMethodMonoTest : MonoBehaviour
	{
		void Start()
		{
			this.transform.GetComponent<InvokeMethodMono>().Invoke();
		}
	}
}
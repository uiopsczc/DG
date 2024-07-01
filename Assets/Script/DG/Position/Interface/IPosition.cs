using UnityEngine;

namespace DG
{
	public interface IPosition
	{
		Vector3 GetPosition();
		Transform GetTransform();
		void SetSocketName(string socketName);
		bool IsValid();
	}
}
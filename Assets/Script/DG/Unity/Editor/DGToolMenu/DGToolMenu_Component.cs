using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace DG
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class DGToolMenu
	{
		static Component[] copiedComponents;

		[MenuItem(DGToolConst.Menu_Root + "Component/Copy All Components #C")]
		//crtl+alt+shift+c
		static void CopyAllComponents()
		{
			if (Selection.activeGameObject == null)
				return;

			copiedComponents = Selection.activeGameObject.GetComponents<Component>();
			Debug.LogWarning("Copyed");
		}

		[MenuItem(DGToolConst.Menu_Root + "Component/Paste All Components #P")]
		//crtl+alt+shift+P
		static void PasteAllComponents()
		{
			if (copiedComponents == null)
			{
				Debug.LogError("Nothing is copied!");
				return;
			}

			foreach (var targetGameObject in Selection.gameObjects)
			{
				if (!targetGameObject)
					continue;

				Undo.RegisterCompleteObjectUndo(targetGameObject, targetGameObject.name + ": Paste All Components");

				foreach (var copiedComponent in copiedComponents)
				{
					if (!copiedComponent)
						continue;

					ComponentUtility.CopyComponent(copiedComponent);

					var targetComponent = targetGameObject.GetComponent(copiedComponent.GetType());

					if (targetComponent) // if gameObject already contains the component
					{
						if (!ComponentUtility.PasteComponentValues(targetComponent))
							Debug.LogError("Failed to copy: " + copiedComponent.GetType());
					}
					else // if gameObject does not contain the component
					{
						if (!ComponentUtility.PasteComponentAsNew(targetGameObject))
							Debug.LogError("Failed to copy: " + copiedComponent.GetType());
					}
				}
			}

			copiedComponents = null; // to prevent wrong pastes in future
			Debug.LogWarning("Paseted");
		}
	}
}
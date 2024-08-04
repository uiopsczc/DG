using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DG
{
	//https://github.com/marijnz/unity-editor-coroutines/edit/master/Assets/EditorCoroutines/Editor/EditorCoroutines.cs
	//不仅可以在运行模式下运行，还可以在Editor模式中运行
	public class PausableCoroutineManager : MonoBehaviour, ISingleton
	{
		private Dictionary<string, List<PausableCoroutine>> _coroutineDict = new();

		private List<List<PausableCoroutine>> _tempCoroutineList = new();

		private Dictionary<string, Dictionary<string, PausableCoroutine>> _coroutineOwnerDict = new();

		private float _previousTimeSinceStartup;
		private bool _isInited;
		private bool _isPaused;


		public static PausableCoroutineManager instance
		{
			get
			{
				var result = SingletonFactory.instance.GetMono<PausableCoroutineManager>();
				return result;
			}
		}

		public void Init()
		{
			if (_isInited)
				return;
			_previousTimeSinceStartup = Time.realtimeSinceStartup;
#if UNITY_EDITOR
			EditorApplication.update -= Update;
			if (!EditorApplication.isPlayingOrWillChangePlaymode)
			{
				EditorApplication.update += Update;
			}
#endif
			_isInited = true;
		}


		public void SetIsPaused(bool isPaused)
		{
			_isPaused = isPaused;
		}

		/// <summary>Starts a coroutine.</summary>
		/// <param name="routine">The coroutine to start.</param>
		/// <param name="thisReference">Reference to the instance of the class containing the method.</param>
		public PausableCoroutine StartCoroutine(IEnumerator routine, object thisReference)
		{
			return instance.GoStartCoroutine(routine, thisReference);
		}

		/// <summary>Starts a coroutine.</summary>
		/// <param name="methodName">The name of the coroutine method to start.</param>
		/// <param name="thisReference">Reference to the instance of the class containing the method.</param>
		public new PausableCoroutine StartCoroutine(string methodName, object thisReference)
		{
			return StartCoroutine(methodName, null, thisReference);
		}

		/// <summary>Starts a coroutine.</summary>
		/// <param name="methodName">The name of the coroutine method to start.</param>
		/// <param name="value">The parameter to pass to the coroutine.</param>
		/// <param name="thisReference">Reference to the instance of the class containing the method.</param>
		public PausableCoroutine StartCoroutine(string methodName, object value, object thisReference)
		{
			MethodInfo methodInfo = thisReference.GetType()
				.GetMethodInfo2(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (methodInfo == null)
				DGLog.Error("Coroutine '" + methodName + "' couldn't be started, the method doesn't exist!");

			var returnValue = methodInfo.Invoke(thisReference, value == null ? null : new[] {value});

			if (returnValue is IEnumerator enumerator)
				return instance.GoStartCoroutine(enumerator, thisReference);

			DGLog.Error("Coroutine '" + methodName +
			            "' couldn't be started, the method doesn't return an IEnumerator!");

			return null;
		}

		/// <summary>Stops all coroutines being the routine running on the passed instance.</summary>
		/// <param name="routine"> The coroutine to stop.</param>
		/// <param name="thisReference">Reference to the instance of the class containing the method.</param>
		public void StopCoroutine(IEnumerator routine, object thisReference)
		{
			instance.GoStopCoroutine(routine, thisReference);
		}

		/// <summary>
		/// Stops all coroutines named methodName running on the passed instance.</summary>
		/// <param name="methodName"> The name of the coroutine method to stop.</param>
		/// <param name="thisReference">Reference to the instance of the class containing the method.</param>
		public void StopCoroutine(string methodName, object thisReference)
		{
			instance.GoStopCoroutine(methodName, thisReference);
		}

		/// <summary>
		/// Stops all coroutines running on the passed instance.</summary>
		/// <param name="thisReference">Reference to the instance of the class containing the method.</param>
		public void StopAllCoroutines(object thisReference)
		{
			instance.GoStopAllCoroutines(thisReference);
		}

		#region 私有方法

		void GoStopCoroutine(IEnumerator routine, object thisReference)
		{
			GoStopActualRoutine(CreateCoroutine(routine, thisReference));
		}

		void GoStopCoroutine(string methodName, object thisReference)
		{
			GoStopActualRoutine(CreateCoroutineFromString(methodName, thisReference));
		}

		void GoStopActualRoutine(PausableCoroutine routine)
		{
			if (!_coroutineDict.ContainsKey(routine.routineUniqueHash)) return;
			_coroutineOwnerDict[routine.ownerUniqueHash].Remove(routine.routineUniqueHash);
			_coroutineDict.Remove(routine.routineUniqueHash);
		}

		void GoStopAllCoroutines(object thisReference)
		{
			PausableCoroutine coroutine = CreateCoroutine(null, thisReference);
			if (!_coroutineOwnerDict.TryGetValue(coroutine.ownerUniqueHash, out var value)) return;
			foreach (var couple in value)
				_coroutineDict.Remove(couple.Value.routineUniqueHash);

			_coroutineOwnerDict.Remove(coroutine.ownerUniqueHash);
		}

		PausableCoroutine GoStartCoroutine(IEnumerator routine, object thisReference)
		{
			if (routine == null)
				DGLog.Error(new Exception("IEnumerator is null!"), null);

			PausableCoroutine coroutine = CreateCoroutine(routine, thisReference);
			GoStartCoroutine(coroutine);
			return coroutine;
		}

		void GoStartCoroutine(PausableCoroutine coroutine)
		{
			if (!_coroutineDict.ContainsKey(coroutine.routineUniqueHash))
			{
				List<PausableCoroutine> newCoroutineList = new List<PausableCoroutine>();
				_coroutineDict.Add(coroutine.routineUniqueHash, newCoroutineList);
			}

			_coroutineDict[coroutine.routineUniqueHash].Add(coroutine);

			if (!_coroutineOwnerDict.ContainsKey(coroutine.ownerUniqueHash))
			{
				Dictionary<string, PausableCoroutine> newCoroutineDict = new Dictionary<string, PausableCoroutine>();
				_coroutineOwnerDict.Add(coroutine.ownerUniqueHash, newCoroutineDict);
			}

			// If the method from the same owner has been stored before, it doesn't have to be stored anymore,
			// One reference is enough in order for "StopAllCoroutines" to work
			if (!_coroutineOwnerDict[coroutine.ownerUniqueHash].ContainsKey(coroutine.routineUniqueHash))
			{
				_coroutineOwnerDict[coroutine.ownerUniqueHash].Add(coroutine.routineUniqueHash, coroutine);
			}

			MoveNext(coroutine);
		}

		PausableCoroutine CreateCoroutine(IEnumerator routine, object thisReference)
		{
			return new PausableCoroutine(routine, thisReference.GetHashCode(), thisReference.GetType().ToString());
		}

		PausableCoroutine CreateCoroutineFromString(string methodName, object thisReference)
		{
			return new PausableCoroutine(methodName, thisReference.GetHashCode(),
				thisReference.GetType().ToString());
		}

		void Update()
		{
			//      LogCat.log("ggggg");
			float deltaTime = Time.deltaTime;
#if UNITY_EDITOR
			if (!EditorApplication.isPlayingOrWillChangePlaymode)
			{
				deltaTime = Time.realtimeSinceStartup - _previousTimeSinceStartup;
				_previousTimeSinceStartup = Time.realtimeSinceStartup;
			}
#endif
			if (_isPaused)
				return;
			if (deltaTime == 0f)
				return;
			if (_coroutineDict.Count == 0)
			{
				return;
			}

			_tempCoroutineList.Clear();
			foreach (var pair in _coroutineDict)
				_tempCoroutineList.Add(pair.Value);

			for (var i = _tempCoroutineList.Count - 1; i >= 0; i--)
			{
				List<PausableCoroutine> coroutines = _tempCoroutineList[i];

				for (int j = coroutines.Count - 1; j >= 0; j--)
				{
					PausableCoroutine coroutine = coroutines[j];

					if (coroutine.isPaused)
					{
						continue;
					}

					if (!coroutine.currentYield.IsDone(deltaTime))
					{
						continue;
					}

					if (!MoveNext(coroutine))
					{
						coroutines.RemoveAt(j);
						coroutine.currentYield = null;
						coroutine.isFinished = true;
					}

					if (coroutines.Count == 0)
					{
						_coroutineDict.Remove(coroutine.ownerUniqueHash);
					}
				}
			}
		}

		bool MoveNext(PausableCoroutine coroutine)
		{
			if (coroutine.routine.MoveNext())
			{
				return Process(coroutine);
			}

			return false;
		}

		// returns false if no next, returns true if OK
		bool Process(PausableCoroutine coroutine)
		{
			object current = coroutine.routine.Current;
			switch (current)
			{
				case null:
					coroutine.currentYield = new YieldDefault();
					break;
				case WaitForSeconds _:
					coroutine.currentYield = new YieldWaitForSeconds(current.GetFieldValue<float>("m_Seconds"));
					break;
				case CustomYieldInstruction customYield:
					coroutine.currentYield = new YieldCustomYieldInstruction(customYield);
					break;
				default:
				{
					switch (current)
					{
						case WWW _:
							coroutine.currentYield = new YieldWWW((WWW) current);
							break;
						case WaitForFixedUpdate _:
						case WaitForEndOfFrame _:
							coroutine.currentYield = new YieldDefault();
							break;
						case AsyncOperation asyncOperation:
							coroutine.currentYield = new YieldAsync(asyncOperation);
							break;
						case PausableCoroutine co:
							coroutine.currentYield = new YieldNestedCoroutine(co);
							break;
						default:
							DGLog.Error(
								new Exception("<" + coroutine.methodName +
								              "> yielded an unknown or unsupported type! (" +
								              current.GetType() +
								              ")"),
								null);
							coroutine.currentYield = new YieldDefault();
							break;
					}

					break;
				}
			}

			return true;
		}

		void OnApplicationQuit()
		{
			_isInited = false;
		}

		#endregion
	}
}
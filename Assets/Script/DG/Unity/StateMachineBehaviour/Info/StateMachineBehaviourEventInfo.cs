using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    [Serializable]
    public class StateMachineBehaviourEventInfo
    {
        public List<ValueParse> argList = new();
        public EStateMachineBehaviourEventName eventName;
        [HideInInspector] public bool isTriggered;
        public bool isTriggerOnExit;
        [Range(0f, 1f)] public float normalizedTime;
    }
}
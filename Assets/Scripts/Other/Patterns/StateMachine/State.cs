using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FSM
{
    public abstract class State<T> : MonoBehaviour where T : class
    {
        public T FSM { get; set; }

        public virtual void Initialize()
        {

        }

        public virtual void OnEnterState()
        {

        }

        public virtual void OnExitState()
        {

        }
    }
}

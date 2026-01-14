using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class PoolableItem: MonoBehaviour
    {
        private Action<PoolableItem> _killAction;

        public void Init(Action<PoolableItem> killAction)
        {
            _killAction = killAction;
        }
        
        // тут надо вызвать _killAction(this) в каком-то методе, может быть virtual использовать
        // или просто вот так, я хз: 

        public void Kill()
        {
            _killAction(this);
        }
    }
}
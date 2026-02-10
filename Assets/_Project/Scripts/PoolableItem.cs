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

        public void Kill()
        {
            _killAction(this);
        }

    }
}
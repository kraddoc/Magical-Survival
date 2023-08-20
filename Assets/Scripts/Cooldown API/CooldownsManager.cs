using System;
using UnityEngine;

namespace Project.Cooldown_API
{
    internal class CooldownsManager : MonoBehaviour
    {
        /// The class that manages and calls all the cooldowns
        /// No need to use or call it manually 
        /// Tutorial and Examples: https://github.com/jozzzzep/CooldownAPI

        private Action _cooldownsUpdates;

        private void Update() =>
            _cooldownsUpdates();

        internal void AddToManager(Action call)
        {
            if (_cooldownsUpdates == null)
                _cooldownsUpdates = call;
            else
                _cooldownsUpdates += call;
        }
    }
}
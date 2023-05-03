using System;
using UnityEngine;

namespace Player
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            PlayerHealthController.OnPlayerDeath += HandlePlayerDeath;
        }
        
        private void OnDisable()
        {
            PlayerHealthController.OnPlayerDeath -= HandlePlayerDeath;
        }
        
        private void HandlePlayerDeath()
        {
            //TODO; 
        }
    }
}
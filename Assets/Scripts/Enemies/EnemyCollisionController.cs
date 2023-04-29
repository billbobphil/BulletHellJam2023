using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyCollisionController : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            
            //TODO: add some sort of logic here for a damage tick-over etc.
            Debug.Log("Player was hit!");
            Destroy(gameObject);
        }
    }
}
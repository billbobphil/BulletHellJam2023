using UnityEngine;

namespace Towers
{
    public abstract class Tower : MonoBehaviour
    {
        protected Animator Animator;
        
        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
        }
    }
}
using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        public float speed;
        
        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += Vector3.up * speed;
            }
            
            if(Input.GetKey(KeyCode.S)  || Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += Vector3.down * speed;
            }
            
            if(Input.GetKey(KeyCode.A)  || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * speed;
            }
            
            if(Input.GetKey(KeyCode.D)  || Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * speed;
            }
        }
    }
}
using UnityEngine;

namespace AztechGames 
{ 
    public class Rotate : MonoBehaviour 
    { 
        public float speed = 10f;
        public float floatHeight = 0.2f;
        public float floatSpeed = 2f;

        private float startY;

        void Start()
        {
            startY = transform.position.y;
        }

        void Update()  
        {  
            // Rotación
            transform.Rotate(Vector3.up, speed * Time.deltaTime);

            // Movimiento de subida y bajada
            float newY = startY + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            
            transform.position = new Vector3(
                transform.position.x,
                newY,
                transform.position.z
            );
        }  
    } 
}
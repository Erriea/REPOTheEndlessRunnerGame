using UnityEngine;

namespace Remakes
{
    public class Boss2Move : MonoBehaviour
    {
        // assign from GameManager
        public Transform playerTransform;    // assign from GameManager
        public float followDistance = 10f;   // desired distance behind
        public float smoothSpeed = 5f;       // how quickly boss catches up (tweak as needed)
        public float xLimit = 7.5f;          // same side-to-side bounds as player
        public float sideMoveSpeed = 2f;
        
        float _direction = 1f;

        //KEEP BOSS SAME DISTANCE AWAY FROM PLAYER
        void Update()
        {
            if (playerTransform == null) return;
        
            //Z Position
            float targetZ = playerTransform.position.z - followDistance;
            float newZ = Mathf.Lerp(transform.position.z, targetZ, smoothSpeed * Time.deltaTime);

            //X Position
            float newX = transform.position.x + _direction * sideMoveSpeed * Time.deltaTime;

            if (Mathf.Abs(newX) > xLimit)
            {
                newX = Mathf.Clamp(newX, -xLimit, xLimit);
                _direction *= -1f;
            }

            // Final position — keep current Y
            transform.position = new Vector3(newX, transform.position.y, newZ);
            
            /*
            //Z POSITION
            Vector3 targetPos = playerTransform.position - new Vector3(0, 0, followDistance);
            Vector3 newPos = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, targetPos.z), smoothSpeed * Time.deltaTime);

            // === X POSITION (side-to-side motion) ===
            float x = transform.position.x + _direction * sideMoveSpeed * Time.deltaTime;
            if (Mathf.Abs(x) > xLimit)
            {
                x = Mathf.Clamp(x, -xLimit, xLimit);
                _direction *= -1f;
            }

            // Apply new position
            transform.position = new Vector3(x, newPos.y, newPos.z);
            */
        }

    }
}

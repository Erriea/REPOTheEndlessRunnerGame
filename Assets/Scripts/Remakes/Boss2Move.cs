using UnityEngine;

namespace Remakes
{
    public class Boss2Move : MonoBehaviour
    {
        public float xLimit = 7.5f;      // same as player's x bounds
        public float speed = 6f;
        
        private float _direction = 1f;

        //move him side to side
        void Update()
        {
            if (!gameObject.activeInHierarchy) return;

            Vector3 pos = transform.localPosition;
            pos.x += _direction * speed * Time.deltaTime;

            // Reverse direction if at xLimit
            if (Mathf.Abs(pos.x) > xLimit)
            {
                pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
                _direction *= -1f;
            }

            transform.localPosition = pos;
        }

    }
}

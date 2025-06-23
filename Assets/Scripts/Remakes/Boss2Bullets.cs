using System.Collections;
using UnityEngine;

namespace Remakes
{
    public class Boss2Bullets : MonoBehaviour
    {
        [SerializeField] public GameObject bulletPrefab;
        public Transform firePoint;             // where bullet spawns from
        public float fireIntervalMin = 1.5f;
        public float fireIntervalMax = 3f;
        public float bulletSpeed = 0f;
        public float bulletSpeedOffset = 6;

        private bool _isFiring = false;

        void OnEnable()
        {
            _isFiring = true;
            StartCoroutine(FireLoop());
        }

        void OnDisable()
        {
            _isFiring = false;
        }

        IEnumerator FireLoop()
        {
            while (_isFiring)
            {
                yield return new WaitForSeconds(Random.Range(fireIntervalMin, fireIntervalMax));
                Fire();
            }
        }

        void Fire()
        {
            float playerSpeed = TheGameManager.Instance.thePlayer
                .GetComponent<PlayerControllerRemake>().currentSpeed;

            float bulletSpeed = playerSpeed + bulletSpeedOffset;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
                rb.linearVelocity = firePoint.forward * bulletSpeed;

            Destroy(bullet, 10f);
        }

    }
}

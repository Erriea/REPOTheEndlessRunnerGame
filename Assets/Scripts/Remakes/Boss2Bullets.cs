using System.Collections;
using UnityEngine;

namespace Remakes
{
    public class Boss2Bullets : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public Transform firePoint;
        public float fireRate = 2f;

        private Coroutine firingRoutine;

        void OnEnable()
        {
            firingRoutine = StartCoroutine(FireLoop());
        }

        void OnDisable()
        {
            if (firingRoutine != null)
                StopCoroutine(firingRoutine);
        }

        IEnumerator FireLoop()
        {
            while (true)
            {
                Fire();
                yield return new WaitForSeconds(fireRate);
            }
        }
        
        void Fire()
        {
            if (TheGameManager.Instance.isBoss2Active == true)
            {
                Debug.Log("Bullet fired!");

                if (bulletPrefab == null || firePoint == null || TheGameManager.Instance.thePlayer == null)
                    return;

                float playerSpeed = TheGameManager.Instance.thePlayer
                    .GetComponent<PlayerControllerRemake>().currentSpeed;

                float bulletSpeed = playerSpeed + 15f;

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position + firePoint.up * 1f, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.velocity = firePoint.forward * bulletSpeed;

                Destroy(bullet, 5f);
            }
            else
            {
                return;
            }
        }

    }
}

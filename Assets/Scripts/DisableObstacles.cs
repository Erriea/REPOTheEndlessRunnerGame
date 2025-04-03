using System.Collections;
using UnityEngine;

public class DisableObstacles : MonoBehaviour
{
   //public GameObject player;
   //public GameObject ground;
   //public Collider[] allColliders; // Assign in Inspector

   //private void Start()
   //{
   //    allColliders = FindObjectsOfType<Collider>();
   //}

   //private void OnTriggerEnter(Collider other)
   //{
   //    if (other.CompareTag("Player"))
   //    {
   //        StartCoroutine(DisablePlayerColliders());
   //    }
   //}
   //

   //private IEnumerator DisablePlayerColliders() 
   //{
   //    // all colliders that are not the player and the ground will temporarily disable their 
   //    foreach (Collider collider in allColliders)
   //    {
   //        if (collider.gameObject != player && collider.gameObject != ground)
   //        {
   //            collider.enabled = false;
   //        }
   //    }

   //    yield return new WaitForSeconds(5f);

   //    // sets all the collider back to their is trigger states
   //    foreach (Collider collider in allColliders)
   //    {
   //        collider.enabled = true;
   //    }

   //    // destroys the gameobject that the player hits into 
   //    Destroy(gameObject);
   //}
    
    
}

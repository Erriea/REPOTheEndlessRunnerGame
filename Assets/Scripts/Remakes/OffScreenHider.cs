using System.Collections.Generic;
using UnityEngine;

namespace Remakes
{
    //HIDE OBJECTS WHEN NOT NEEDED
    [DisallowMultipleComponent]
    public class OffScreenHider : MonoBehaviour
    {
        public void HideAllWithTag(string tag)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objs)
            {
                foreach (Renderer r in obj.GetComponentsInChildren<Renderer>(true))
                    r.enabled = false;

                foreach (Collider c in obj.GetComponentsInChildren<Collider>(true))
                    c.enabled = false;
            }
        }

        public void ShowAllWithTag(string tag)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objs)
            {
                foreach (Renderer r in obj.GetComponentsInChildren<Renderer>(true))
                    r.enabled = true;

                foreach (Collider c in obj.GetComponentsInChildren<Collider>(true))
                    c.enabled = true;
            }
        }

    }
}

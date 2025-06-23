using UnityEngine;
//ROTATES PICK UPS TO MAKE THE MORE NOTICABLE
namespace Remakes
{
    public class PickUpRotate : MonoBehaviour
    {
        
        [SerializeField] float rotateSpeed = 1f;
        
        void Update()
        {
            //ROTATE ON Y-AXIS RELATIVE TO WORLD
            transform.Rotate(0, rotateSpeed, 0, Space.World);
        }
    }
}

using UnityEngine;

namespace Remakes
{
    //HIDE OBJECTS WHEN NOT NEEDED
    [DisallowMultipleComponent]
    public class OffScreenHider : MonoBehaviour
    {
        [Tooltip("Y to push offscreen")]
        public float offscreenY = 50f;

        float _origY;

        void Awake()
        {
            _origY = transform.position.y;
        }

        public void Hide()
        {
            var p = transform.position;
            p.y = offscreenY;
            transform.position = p;
        }

        public void Show()
        {
            var p = transform.position;
            p.y = _origY;
            transform.position = p;
        }
    }
}

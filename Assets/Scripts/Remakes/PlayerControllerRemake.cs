using UnityEngine;

//CODE TO MAKE CHARACTER MOVE REDONE
//USE TRANSFORMS AGAIN
namespace Remakes // IN SEPARATE FOLDER WITHIN SCRIPTS TO AVOID CONFUSION
{
    public class PlayerControllerRemake : MonoBehaviour
    {
        //PLAYER COMPONENTS FOR JUMP
        [SerializeField] public Rigidbody rb;
        private bool _isGrounded = false;
        //PLAYER MOVEMENT STATS
        [SerializeField] float playerSpeed = 2f;
        [SerializeField] float horizontalSpeed = 3f;
        [SerializeField] float jumpForce = 8f;
        //PLAYER MOVEMENT LIMITATIONS
        public float rightLimit = 7.5f;
        public float leftLimit = -7.5f;

        void Awake()
        {
            //GET PLAYER RIGIDBODY
            rb = GetComponent<Rigidbody>();
        }
        void Update()
        {
            //CONTINUOUS FORWARD MOTION
            /*
             moves it with a Vector 3 coz 3D we know this
             deltaTime is the game speed so player moves relative to game speed
             Space.World make the speed relative to game world
             translate(DesiredDirection * GameSpeed * DesiredSpeed)
             */
            transform.Translate(Vector3.forward * (Time.deltaTime * playerSpeed), Space.World);
            
            //CHECK FOR KEY MOVE LEFT
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            //CHECK FOR KEY MOVE RIGHT
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //CHECK IF PLAYER ON GROUND
                _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
                Jump();
            }
        }

        //MOVE PLAYER LEFT
        private void MoveLeft()
        {
            //LIMIT SO PLAYER NOT FALL OFF PLATFORM ON LEFT SIDE
            if (this.gameObject.transform.position.x > leftLimit)
            {
                transform.Translate(Vector3.left * (Time.deltaTime * horizontalSpeed));
            }
        }
        
        //MOVE PLAYER RIGHT
        private void MoveRight()
        {
            //LIMIT SO PLAYER NOT FALL OFF PLATFORM ON RIGHT SIDE
            if (this.gameObject.transform.position.x < rightLimit)
            {
                transform.Translate(Vector3.left * (Time.deltaTime * horizontalSpeed * -1));
            }
        }

        //MAKES PLAYER JUMP
        private void Jump()
        {
            //PREVENT INFINITE JUMPING
            if (_isGrounded)
                //rb.AddForce(Vector3.up * jumpForce);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

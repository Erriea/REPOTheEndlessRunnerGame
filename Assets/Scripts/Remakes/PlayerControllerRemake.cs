using UnityEngine;

//CODE TO MAKE CHARACTER MOVE REDONE
//USE TRANSFORMS AGAIN
namespace Remakes // IN SEPARATE FOLDER WITHIN SCRIPTS TO AVOID CONFUSION
{
    public class PlayerControllerRemake : MonoBehaviour
    {
        //PLAYER COMPONENTS FOR JUMP
        [SerializeField]  Rigidbody rb;
        [SerializeField]  AudioSource jumpFX;//Audio
        [SerializeField] int maxJumpCount = 1;
        public static bool _isGrounded = false;
        private int _jumpCount = 0;
        public static bool AllowDoubleJump = false;
        
        //PLAYER MOVEMENT STATS
        [SerializeField] float playerSpeed = 2f;
        [SerializeField] float horizontalSpeed = 3f;
        [SerializeField] public static float jumpForce = 8f;
        
        //PLAYER MOVEMENT LIMITATIONS
        public float rightLimit = 7.5f;
        public float leftLimit = -7.5f;
        
        //PLAYER STATUS
        public static bool IsAlive = true;
        public static bool IsInvincible = false;

        void Awake()
        {
            //GET PLAYER RIGIDBODY
            rb = GetComponent<Rigidbody>();
        }
        
        void Update()
        {
            //STUFF FOR DOUBLE JUMP
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

            if (_isGrounded)
                _jumpCount = 0;
            
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

            //JUMP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //CHECK IF PLAYER ON GROUND
                _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
                Jump();
            }
        }

        //METHODS-------------------------------------------------------------------------------
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
            if (_isGrounded)
            {
                _jumpCount = 0; // Reset count on ground
            }

            bool canDoubleJump = AllowDoubleJump && _jumpCount < maxJumpCount;

            if (_isGrounded || canDoubleJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpFX.Play();

                _jumpCount++;
            }

            
            /*
             * //PREVENT INFINITE JUMPING
                if (_isGrounded)
                    //rb.AddForce(Vector3.up * jumpForce);
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                //PLAY AUDIO ONCE
                jumpFX.Play();
             */
        }
    }
}

using UnityEngine;

//CODE TO MAKE CHARACTER MOVE REDONE
//USE TRANSFORMS AGAIN
namespace Remakes // IN SEPARATE FOLDER WITHIN SCRIPTS TO AVOID CONFUSION
{
    public class PlayerControllerRemake : MonoBehaviour
    {
        //PLAYER COMPONENTS FOR JUMP
        [SerializeField]  Rigidbody rb;
        [SerializeField] int maxJumpCount = 1;
        private bool _isGrounded = false;
        private int _jumpCount = 0;
        public static bool AllowDoubleJump = false;
        
        //PLAYER MOVEMENT STATS
        [SerializeField] float playerSpeed = 6f;
        [SerializeField] float speedIncreaseRate = 0.2f; // units per second
        [SerializeField] float maxSpeed = 35f;
        [SerializeField] float horizontalSpeed = 3f;
        [SerializeField] float horizontalSpeedIncreaseRate = 0.1f;
        [SerializeField] float maxHorizontalSpeed = 20f;
        [SerializeField] float jumpForce = 8f;

        
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
            if (!TheGameManager.Instance._isRunnerScene) return;
            
            //CHECK IF PLAYER ON GROUND
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
            if (_isGrounded)
                _jumpCount = 0;
            
            //GRADUALLY INCREASE SPEED
            playerSpeed = Mathf.Min(playerSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
            horizontalSpeed = Mathf.Min(horizontalSpeed + horizontalSpeedIncreaseRate * Time.deltaTime, maxHorizontalSpeed);
            
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

            // CHECK FOR JUMP
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
            if (!TheGameManager.Instance._isRunnerScene) return;
            
            //LIMIT SO PLAYER NOT FALL OFF PLATFORM ON LEFT SIDE
            if (this.gameObject.transform.position.x > leftLimit)
            {
                transform.Translate(Vector3.left * (Time.deltaTime * horizontalSpeed));
            }
        }
        
        //MOVE PLAYER RIGHT
        private void MoveRight()
        {
            if (!TheGameManager.Instance._isRunnerScene) return;
            
            //LIMIT SO PLAYER NOT FALL OFF PLATFORM ON RIGHT SIDE
            if (this.gameObject.transform.position.x < rightLimit)
            {
                transform.Translate(Vector3.left * (Time.deltaTime * horizontalSpeed * -1));
            }
        }

        //MAKES PLAYER JUMP
        private void Jump()
        {
            if (!TheGameManager.Instance._isRunnerScene) return;
            
            if (_isGrounded)
            {
                _jumpCount = 0; // Reset count on ground
            }

            bool canDoubleJump = AllowDoubleJump && _jumpCount < maxJumpCount;

            if (_isGrounded || canDoubleJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                AudioManager.Instance.jumpSFX.Play();

                _jumpCount++;
            }
        }
    }
}

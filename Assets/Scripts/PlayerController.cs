using System;
using System.Collections;
using UnityEngine;
// Using transforms for prototype. Change to using vectors when have time
//cleared for past

//to do - make player register platform ground to jump
public class PlayerController : MonoBehaviour
{
    public bool isInvincible = true; // added to player controller
    private Coroutine invincibiltyCoroutine; //added to player controller

    private float baseJumpForce;//added to player controller
    private Coroutine doubleJumpCoroutine;//added to player controller

    public bool isAlive = true; 
    public float runSpeed; 
    public float horizontalSpeed; 
    //private bool _destroysObstacles = false;
    private bool _pickIsActive;

    public Rigidbody rb; //make reference to the rigidbody i added to the capusle 
    
    private Collider _playerCollider; //stores player collider 
    
    float _horizontalInput; 

    //serialized coz u can make it public but cannot change it from other scripts
    //can also then see variables in an inspector even if private
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private LayerMask groundMask; //used to see whats ground and when to jump
    [SerializeField] private LayerMask platformMask; //find surface of platform
    [SerializeField] private LayerMask obstacleMask;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<Collider>(); //instantiates the player collider
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 forwardMovement = runSpeed * Time.fixedDeltaTime * transform.forward;
            Vector3 horizontalMovement = _horizontalInput * horizontalSpeed * Time.fixedDeltaTime * transform.right;  
            rb.MovePosition(rb.position + forwardMovement + horizontalMovement);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseJumpForce = jumpForce;
        // checks if player collides with obstacle (g)
        
        Collider playerCollider = GetComponent<Collider>();
        
        GameObject obstacle = GameObject.FindWithTag("Obstacle");

        if (obstacle != null)
        {
            // Get the collider of the obstacle
            Collider obstacleCollider = obstacle.GetComponent<Collider>();

            if (obstacleCollider != null)
            {
                // Ignore collision between the player's collider and the obstacle's collider
                Physics.IgnoreCollision(playerCollider, obstacleCollider, true);
                Debug.Log("Collision ignored with obstacle.");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        
        //find out size of the player using colliders to determine jump height
        float playerHeight = _playerCollider.bounds.size.y; 
        //find if the player is on the ground or not
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (playerHeight / 2 + 0.1f), groundMask);
        bool isOnPlatform = Physics.Raycast(transform.position, Vector3.down, (playerHeight / 2 + 0.5f), platformMask);
        
        
        if (Input.GetKeyDown(KeyCode.Space) && isAlive && (isGrounded || isOnPlatform))
        {
            if (isOnPlatform == false)
            {
                Debug.Log("not linked to bottom of platform.");
            }
            
            Jump();
        }
        
        
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }
    
    // added for when the player dies by collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (isInvincible)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                Dead();
            }
            return;
        }

        // Rock obstacle
        if (collision.gameObject.name == "RockObsticle")
        {
            if (isInvincible)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                Dead();
            }
            return;
        }

        // Icicle obstacle
        if (collision.gameObject.name == "IcicleObsticle")
        {
            if (isInvincible)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                Dead();
            }
            return;
        }

        // Platform Kill Zone
        if (collision.gameObject.name == "PlatformKillZone")
        {
            if (isInvincible)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                Dead();
            }
            return;
        }
        

        // Clean up any remaining obstacle if all are gone
        if (GameObject.FindGameObjectsWithTag("Obstacle").Length == 0)
        {
            Destroy(collision.gameObject);
        }
        
    }

    
    // Added for part 2
    public IEnumerator InvincibiltityCoroutine(float invincDuration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincDuration);
        isInvincible = false;
        invincibiltyCoroutine = null;
    }

    public void StartInvincibility(float invincDuration)
    {
        if (invincibiltyCoroutine != null)
        {
            StopCoroutine(invincibiltyCoroutine);
        }

        invincibiltyCoroutine = StartCoroutine(InvincibiltityCoroutine(invincDuration));
    }
    
    
    // added for part 2
    
    public IEnumerator DoubleJumpCoroutine(float doubleJumpForce, float jumpDuration)
    {
        jumpForce = doubleJumpForce;
        yield return new WaitForSeconds(jumpDuration);
        jumpForce = baseJumpForce;
        doubleJumpCoroutine = null;
    }

    public void StartDoubleJump(float doubleJumpForce, float jumpDuration)
    {
        if (doubleJumpCoroutine != null)
        {
            StopCoroutine(doubleJumpCoroutine);
        }

        doubleJumpCoroutine = StartCoroutine(DoubleJumpCoroutine(doubleJumpForce, jumpDuration));
    }
        
    //
    void Dead()
    {
        isAlive = false;
        StopAllCoroutines();
        // game over panel pops up and user can try again
        GameManager.Instance.gameOverPanel.SetActive(true);
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Player has hit obstacle ");
            ScoreManager.Instance.StopScoring();
        }
    }
    
   //public void ActivateDestroyMode() {
   //    destroysObstacles = true;  // Enable object destruction
   //    StartCoroutine(DisableDestroyMode());  // Start timer to disable it
   //}

   //private IEnumerator DisableDestroyMode() {
   //    yield return new WaitForSeconds(5f);  // Wait 5 seconds
   //    destroysObstacles = false;  // Disable object destruction
   //}
    
   
   //shouldnt it be in pickup?
   //ill look at it if we have time
   
    public void ActivatePickup()
    {
        _pickIsActive = true;

        // Disable collisions between player and all obstacles
        Collider[] allColliders = FindObjectsOfType<Collider>();
        foreach (Collider obstacle in allColliders)
        {
            if (obstacle.gameObject.CompareTag("Obstacle")) 
            {
                Physics.IgnoreCollision(obstacle, _playerCollider, true);
            }
        }

        StartCoroutine(DisablePickup());
    }
    
    private IEnumerator DisablePickup()
    {
        // waits for 5 seconds before re-enabling the colliders for the obstacles
        yield return new WaitForSeconds(5f);

        _pickIsActive = false;

        // Re-enable collisions with all obstacles
        Collider[] obstacleColliders = FindObjectsOfType<Collider>();
        foreach (Collider obstacleCollider in obstacleColliders)
        {
            if (obstacleCollider.CompareTag("Obstacle")) 
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), _playerCollider, false);
            }
        }
    }
    
    
}

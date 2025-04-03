using System;
using System.Collections;
using UnityEngine;
// Using transforms for prototype. Change to using vectors when have time
//cleared for past

//to do - make player register platform ground to jump
public class PlayerController : MonoBehaviour
{
    public bool isAlive = true; 
    public float runSpeed; 
    public float horizontalSpeed; 
    //private bool _destroysObstacles = false;
    private bool _pickIsActive = false;

    public Rigidbody rb; //make reference to the rigidbody i added to the capusle 
    
    private Collider _playerCollider; //stores player collider 
    
    float _horizontalInput; 

    //serialized coz u can make it public but cannot change it from other scripts
    //can also then see variables in an inspector even if private
    [SerializeField] private float jumpForce = 500;
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
        if (!_pickIsActive && collision.gameObject.CompareTag("Obstacle")) 
        {
            // Ignore collisions while the pickup is active
            Dead();
        }
        
        if (collision.gameObject.name == "RockObsticle" && _pickIsActive == false)
        {
            Dead();
        }
        else if (GameObject.FindGameObjectsWithTag("Obstacle").Length == 0)
        {
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.name == "IcicleObsticle")
        {
            Dead();
        }
        else if (collision.gameObject.name == "IcicleObsticle" && _pickIsActive == true)
        {
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.name == "PlatformKillZone")
        {
            Dead();
        }
        else if (collision.gameObject.name == "PlatformKillZone" && _pickIsActive == true)
        {
            Destroy(collision.gameObject);
        }
        
    }

    void Dead()
    {
        isAlive = false;
        StopAllCoroutines();
        // game over panel pops up and user can try again
        GameManager.Instance.gameOverPanel.SetActive(true);
        
    }
    
   //public void ActivateDestroyMode() {
   //    destroysObstacles = true;  // Enable object destruction
   //    StartCoroutine(DisableDestroyMode());  // Start timer to disable it
   //}

   //private IEnumerator DisableDestroyMode() {
   //    yield return new WaitForSeconds(5f);  // Wait 5 seconds
   //    destroysObstacles = false;  // Disable object destruction
   //}
    
   
   
   /*
    public void ActivatePickup()
    {
        pickIsActive = true;

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
        yield return new WaitForSeconds(5f);

        pickIsActive = false;

        // Re-enable collisions with all obstacles
        Collider[] allColliders = FindObjectsOfType<Collider>();
        foreach (Collider collider in allColliders)
        {
            if (collider.gameObject.CompareTag("Obstacle")) 
            {
                Physics.IgnoreCollision(collider, _playerCollider, false);
            }
        }
    }
    */
    
}

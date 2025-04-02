using System;
using UnityEngine;
// Using transforms for prototype. Change to using vectors when have time

//to do - make player register platform ground to jump
public class PlayerController : MonoBehaviour
{
    public bool isAlive = true;
    public float runSpeed;
    public float horizontalSpeed;

    public Rigidbody rb; //make reference to the rigidbody i added to the capusle
    
    private Collider _playerCollider; //stores player collider
    
    float _horizontalInput;

    //serialized coz u can make it public but cannot change it from other scripts
    //can also then see variables in an inspector even if private
    [SerializeField] private float jumpForce = 500;
    [SerializeField] private LayerMask groundMask; //used to see whats ground and when to jump
    [SerializeField] private LayerMask platformMask; //find surface of platform

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
        if (collision.gameObject.name == "RockObsticle")
        {
            Dead();
        }
        else if (collision.gameObject.name == "IcicleObsticle")
        {
            Dead();
        }
        else if (collision.gameObject.name == "DeathZone")
        {
            Dead();
        }
    }

    void Dead()
    {
        isAlive = false;
        // game over panel pops up and user can try again
        GameManager.Instance.gameOverPanel.SetActive(true);
    }
}

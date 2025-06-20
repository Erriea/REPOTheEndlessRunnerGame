using UnityEngine;

public class BossMovementController : MonoBehaviour
{
    public float speed = 3f; // Speed of movement
    private Vector3 moveDirection;
    private float moveTimer = 0f;
    private bool isFirstMove = true;

    void Start()
    {
        // Choose random initial direction (left or right)
        moveDirection = Random.value > 0.5f ? Vector3.right : Vector3.left;
    }

    void Update()
    {
        // Move the object using transform
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Update timer
        moveTimer += Time.deltaTime;

        // Check if it's time to swap direction
        if ((isFirstMove && moveTimer >= 2f) || (!isFirstMove && moveTimer >= 4f))
        {
            moveTimer = 0f; // Reset timer
            moveDirection *= -1; // Flip direction

            isFirstMove = false; // Ensure all future cycles use 4 seconds
        }
    }

}

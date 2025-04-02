using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Vector3 _offset;

    private void Awake()
    {
        //player = FindObjectOfType<PlayerController>().transform; //finds the player
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        _offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetpos = player.position + _offset; //set camera sistance
        targetpos.x = 0; //locks x position
        transform.position = targetpos;
    }
}

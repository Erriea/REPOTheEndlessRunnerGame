using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float activationDuration = 5f;
    private Material _material;  
    private Renderer _renderer;
    [SerializeField] Color[] colours;


// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _material = GetComponentInChildren<Renderer>().material;
        _material.color = colours[Random.Range(0, colours.Length)];
    }


    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
  
        Renderer playerRenderer = other.GetComponent<Renderer>();
  
        Color playerColor = playerRenderer.material.color;


        if (playerRenderer.material.color == _material.color)
            return;
  
        playerRenderer.material.color = _material.color;
  
        StartCoroutine(ReturnNormalColour(playerRenderer, playerColor, activationDuration));
  
        Destroy(gameObject);
    }


    IEnumerator ReturnNormalColour(Renderer playerRenderer, Color playerColor, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        
        playerRenderer.material.color = playerColor;
        
        
    }

}

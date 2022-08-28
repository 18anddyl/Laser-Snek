using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //Variables
    public float speed = 2.5f;
    public GameManager gameManager;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //Move the laser forwards
    void Update()
    {
        if (gameManager.gameStart == true)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.down);
        }
        
    }

    //Destroy the laser when it hits the wall
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

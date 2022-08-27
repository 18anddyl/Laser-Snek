using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public int speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    
    void Update()
    {
        //Player Moving
        if(gameManager.gameStart == true)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.rotation = Quaternion.Euler(0, 90, 90);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.rotation = Quaternion.Euler(0, 270, 90);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0, 180, 90);
            }
        }

    }

    //If snake touches apple, destroy apple, tell other script to spawn apple
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            gameManager.appleAlive = false;
        }
        else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Laser"))
        {
            gameManager.gameStart = false;
            gameManager.death = true;
        }

    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Apple Variables
    public GameObject apple;
    public bool appleAlive = true;
    private int minBorder = -5;
    private int maxBorder = 5;

    //Highscore Variable
    public int score;

    //Title screen Variables
    public GameObject titleScreen;
    public bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(appleAlive == false)
        {
            SpawnApple();
            appleAlive = true;
            score++;
        }
    }

    void SpawnApple()
    {
        var spawnApplePos = new Vector3(Random.Range(minBorder, maxBorder), 1.25f, Random.Range(minBorder, maxBorder));
        Instantiate(apple, spawnApplePos, transform.rotation);
    }

    public void OnStartButtonPress()
    {
        titleScreen.SetActive(false);
        gameStart = true;
    }
}

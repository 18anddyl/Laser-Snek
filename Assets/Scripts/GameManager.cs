using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Xml.Serialization;
using System.IO;

public class GameManager : MonoBehaviour
{
    //Apple Variables
    public GameObject apple;
    public bool appleAlive = true;
    private float minBorder = -3.0f;
    private float maxBorder = 3.0f;

    //Highscore Variables
    public int score;

    //Title screen Variables
    public GameObject titleScreen;
    public bool gameStart = false;
    public GameObject deathScreen;
    public bool death = false;
    public GameObject scoreText;
    public GameObject deathScoreText;

    //Turret shooting variable
    public GameObject[] turrets;



    void Start()
    {
        //Titlescreen UI
        titleScreen.SetActive(true);
        deathScreen.SetActive(false);
        scoreText.SetActive(false);

        StartCoroutine(RandomShoot());

    }

    //Start game
    public void OnStartButtonPress()
    {
        titleScreen.SetActive(false);
        scoreText.SetActive(true);
        gameStart = true;
    }
    //Play again
    public void OnPlayAgainButtonPress()
    {
        SceneManager.LoadScene("Game Scene");
    }

    void Update()
    {

        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Score : " + score.ToString();


        //Spawn new apple when snake eats apple
        if(!appleAlive)
        {
            SpawnApple();
            appleAlive = true;
            score++;
        }
        if(death)
        {
            deathScreen.SetActive(true);
            scoreText.SetActive(false);
            deathScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
        }
    }
    //Choose random place for apple to spawn
    void SpawnApple()
    {
        Vector3 spawnApplePos = new(Random.Range(minBorder, maxBorder), 1.25f, Random.Range(minBorder, maxBorder));
        Instantiate(apple, spawnApplePos, transform.rotation);
    }

    IEnumerator RandomShoot()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        Shooting();
        StartCoroutine(RandomShoot());
    }
    void Shooting()
    {
        int number = Random.Range(0, turrets.Length);
        GameObject shootingTurret = turrets[number];
        LaserTurret shootingTurretScript = shootingTurret.GetComponent<LaserTurret>();
        shootingTurretScript.shoot = true;

    }

    



}

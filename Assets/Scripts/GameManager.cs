using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //Apple Variables
    public GameObject apple;
    public bool appleAlive = true;
    private float Border = 3.0f;

    //Highscore Variables
    public int playerScore;
    private HighscoreTable highscoreScript;
    public string playerName;

    //Title screen Variables
    public GameObject titleScreen;
    public GameObject deathScreen;
    public GameObject scoreText;
    public GameObject deathScoreText;
    public GameObject highscoreScreen;
    public TMPro.TMP_InputField inputField;

    //Game start/stop variables
    public bool death = false;
    public bool gameStart = false;

    //Turret shooting variable
    public GameObject[] turrets;



    void Start()
    {
        //Titlescreen UI
        titleScreen.SetActive(true);
        deathScreen.SetActive(false);
        scoreText.SetActive(false);
        highscoreScreen.SetActive(false);

        //Sets the character limit to 3 of input field
        inputField.characterLimit = 3;
        //Sends the character that user tpyes to MyValidate function to see whether it is a letter or not
        inputField.onValidateInput = delegate(string input, int charIndex, char addedChar)
            {
                return MyValidate(addedChar);
            };

        highscoreScript = highscoreScreen.GetComponent<HighscoreTable>();
    }

    //Checks whether character typed by user is letter. If not, return nothing so nothing is typed into input field
    private char MyValidate(char charToValidate)
    {
        if(!System.Char.IsLetter(charToValidate))
        {
            charToValidate = '\0';
        }
        return charToValidate;
    }

    void Update()
    {
        //Sets score text to current score
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Score : " + playerScore.ToString();


        //Spawn new apple when snake eats apple
        if(!appleAlive)
        {
            SpawnApple();
            appleAlive = true;
            playerScore++;
        }

        //If the player dies, stop turrets shooting and load up the death screen.
        if(death)
        {
            deathScreen.SetActive(true);
            scoreText.SetActive(false);
            deathScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = playerScore.ToString();
            StopAllCoroutines();

            
        }
    }
    //Choose random place for apple to spawn
    void SpawnApple()
    {
        Vector3 spawnApplePos = new(Random.Range(-Border, Border), 1.25f, Random.Range(-Border, Border));
        Instantiate(apple, spawnApplePos, transform.rotation);
    }

    //Makes the turrets shoot at a random time
    IEnumerator RandomShoot()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        Shooting();
        StartCoroutine(RandomShoot());
    }
    //Tells a random turret to shoot
    void Shooting()
    {
        int number = Random.Range(0, turrets.Length);
        GameObject shootingTurret = turrets[number];
        LaserTurret shootingTurretScript = shootingTurret.GetComponent<LaserTurret>();
        StartCoroutine(shootingTurretScript.ShootTurret());
    }

    
    //Start game
    public void OnStartButtonPress()
    {
        titleScreen.SetActive(false);
        scoreText.SetActive(true);
        gameStart = true;

        //Starts loop of turrets shooting
        StartCoroutine(RandomShoot());

        playerName = inputField.text;
    }
    //Play again button
    public void OnPlayAgainButtonPress()
    {
        //Add a new score to the highscores
        highscoreScript.AddHighscoreEntry(playerScore, playerName);

        //Reloads Scene
        SceneManager.LoadScene("Game Scene");
    }

    //Highscore UI Button
    public void OnHighscoreButtonPress()
    {
        highscoreScreen.SetActive(true);
        titleScreen.SetActive(false);
    }

    //Back Button 
    public void OnBackButtonPress()
    {
        highscoreScreen.SetActive(false);
        titleScreen.SetActive(true);
    }
}
    



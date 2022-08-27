using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    //Gamemanager script
    public GameManager gameManager;

    //Rotate variables
    private float rotAngleY = 90;
    private int turretNumber;

    //Shooting variables
    public GameObject laser;
    public GameObject barrel;
    public bool shoot = false;

    //Colour change variables
    private Renderer colourChange;



    void Start()
    {
        //Gamemanager script
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Defines which turret it is
        string turretTag = gameObject.tag;
        string turretTagSplit = turretTag.Split(" "[0])[1];
        turretNumber = int.Parse(turretTagSplit);

        //Gets child object to change colour
        colourChange = gameObject.GetComponentInChildren<Renderer>(); ;
        


    }

    void Update()
    {
        //Rotates back and forth when game starts
        if(gameManager.gameStart)
        {
            float rY = Mathf.SmoothStep((rotAngleY * turretNumber), (rotAngleY * turretNumber + 90), Mathf.PingPong(Time.time, 1));
            transform.rotation = Quaternion.Euler(0, rY, 0);
            
            //shooting
            if(shoot)
            {
                StartCoroutine(ShootTurret());
                shoot = false;
            }
        }





    }
    IEnumerator ShootTurret()
    {
        colourChange.material.SetColor("_Color", Color.red);
        Instantiate(laser, barrel.transform.position + new Vector3(0, -0.25f, 0), barrel.transform.rotation);
        yield return new WaitForSeconds(1);
        colourChange.material.SetColor("_Color", Color.yellow);
    }

}
//for (int i = 0; i < 1f; i + (Time.deltaTime);)
//gameObject.GetComponentInChildren<Renderer>().material.color = Color.Lerp(Color.yellow, Color.red, colourChange);
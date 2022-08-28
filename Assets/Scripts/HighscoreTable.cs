using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    //Variables
    public Transform entryContainer;
    public Transform entryTemplate;
    private List<Transform> highscoreTransformList;



    private void Awake()
    {
        //PlayerPrefs.DeleteAll();

        //Template is set to false so newly created lines are then set to true.
        entryTemplate.gameObject.SetActive(false);

        //Unloads the list of highscores from the previous Save. Turns the Playerprefs back into the list of highscores.
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //If no highscores in table
        if (highscores == null)
        {
            AddHighscoreEntry(1, "TST");

            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }

        //Sort list by highest score
        for(int i = 0; i < highscores.highscoreList.Count; i++)
        {
            //Sorts through every entry after the first entry being checked
            for (int o = i + 1; o <highscores.highscoreList.Count; o++)
            {
            //If entry being cheked is a lower score than that being compared then swap the two in the list
                if (highscores.highscoreList[o].score > highscores.highscoreList[i].score)
                {
                Highscore tmp = highscores.highscoreList[i];
                highscores.highscoreList[i] = highscores.highscoreList[o];
                highscores.highscoreList[o] = tmp;
                }
            }
        }

        
        //Runs the NewHighscore funtion for however many times that there are highscores in the list
        highscoreTransformList = new List<Transform>();
        for(int i = 0; i < highscores.highscoreList.Count && i < 10; i++)
        {
            NewHighscore(highscores.highscoreList[i], entryContainer, highscoreTransformList);
        }
    }



    private void NewHighscore(Highscore highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 20f;

        //Creates new lines of highscores using the template in Unity
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        //ranking text variables. List starts at 0 so add 1.
        int rank = transformList.Count + 1;
        string rankString;

        //This switch compares the rank and makes position text 1st, 2nd, 3rd, or all other numbers. E.g, 1st,3rd, 4th, 7th, 10th.
        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default: rankString = rank + "TH"; break;
        }
        entryTransform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = rankString; // Position text is set to 1st, 3rd, 9th etc.

        //sets the score text to the score
        int score = highscoreEntry.score;
        entryTransform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();  //Score

        //sets the name text to the name
        string name = highscoreEntry.name;
        entryTransform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = name;  //Name

        //Aesthetics, if rank is odd then activate background
        entryTransform.GetChild(0).gameObject.SetActive(rank % 2 == 1);
        transformList.Add(entryTransform);
    }


    //This function allows a highscore to be added to the highscore list. It is public so it can be called from the GameManager Script.
    public void AddHighscoreEntry(int score, string name)
    {
        //Create a new Highscore
        Highscore highscore = new Highscore { score = score, name = name};

        //Loads the already save highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //If no highscores, make new highscore list
        if(highscores == null)
        {
            highscores = new Highscores()
            {
            highscoreList = new List<Highscore>()
            };
        }

        //Adds a new entry to the highscores
        highscores.highscoreList.Add(highscore);

        //Save updated highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }


    //Class used for json to save highscores
    private class Highscores
    {
        public List<Highscore> highscoreList;
    }

    //Holds a highscore entry. entrys need a score value and a name value.
    //Allows the Highscores to be serialized
    [System.Serializable]
    private class Highscore
    {
        public int score;
        public string name;
    }

}

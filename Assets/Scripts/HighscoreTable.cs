using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    //Table Variables
    public Transform entryContainer;
    public Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        //Template is set to false so newly created lines are then set to true.
        entryTemplate.gameObject.SetActive(false);

        highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry{ score = 521854, name = "Cat"},
            new HighscoreEntry{ score = 54354, name = "Dog"},
            new HighscoreEntry{ score = 8764, name = "Pen"},
            new HighscoreEntry{ score = 986543, name = "ASS"},
            new HighscoreEntry{ score = 4256, name = "JOD"},
            new HighscoreEntry{ score = 1, name = "JGA"},
            new HighscoreEntry{ score = 23, name = "OSK"},
        };

        foreach(HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            NewHighscoreEntry(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }
    private void NewHighscoreEntry(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 20f;

        //Creates new lines of highscores using the template in Unity
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        //position text variables. List starts at 0 so add 1.
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
        entryTransform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = rankString; // Position text is set to 1st, 3rd, 9th etc.


        int score = highscoreEntry.score;
        entryTransform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();  //Score

        string name = highscoreEntry.name;
        entryTransform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = name;  //Name

        transformList.Add(entryTransform);
    }

    //Holds a highscore entry. entrys need a score value and a name value.
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }

}

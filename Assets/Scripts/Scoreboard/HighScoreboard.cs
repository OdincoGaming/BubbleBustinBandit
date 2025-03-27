using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class HighScoreboard : MonoBehaviour
{
    private HighScoreData highscores;
    [SerializeField] private string jsonFileName;
    [SerializeField] private TextMeshProUGUI sTextbox;
    [SerializeField] private List<TextMeshProUGUI> hsTextboxes;
    [SerializeField] private Scoreboard scoreboard;
    private int newScoreIndex = 0;

    private void OnEnable()
    {
        // read from file to retrieve old data
        ReadData();
        // update scoreboard
        UpdateScoreboard();
        // write to file to overwrite old data, if necessary
        WriteData();
    }

    private void UpdateScoreboard()
    {
        float newScore = scoreboard.GetScore();
        sTextbox.text = newScore.ToString();

        // Call the helper method that checks and inserts the new score
        bool isNewHighscore = InsertNewHighScore(newScore);

        for (int i = 0; i < hsTextboxes.Count; i++)
        {
            hsTextboxes[i].text = highscores.scores[i].ToString();
        }

        if (isNewHighscore)
        {
            // play some fancy anim on the correct textbox to show which score is the new one

        }
    }

    // Helper method to insert newScore if it's higher than any of the existing scores.
    // It loops through highscores.scores and inserts newScore in the correct position,
    // shifting down lower scores, and then removes the extra score so the list remains with 3 items.
    private bool InsertNewHighScore(float newScore)
    {
        // Loop through the list of high scores in order.
        for (int i = 0; i < highscores.scores.Count; i++)
        {
            if (newScore > highscores.scores[i])
            {
                highscores.scores.Insert(i, newScore);
                newScoreIndex = i;
                // Ensure only the top 3 scores are maintained.
                if (highscores.scores.Count > 3)
                {
                    highscores.scores.RemoveAt(highscores.scores.Count - 1);
                }
                return true;
            }
        }
        return false;
    }

    private void ReadData()
    {
        string path = Application.streamingAssetsPath + $"/{jsonFileName}.json";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, JsonUtility.ToJson(new HighScoreData()));
        }

        string json = File.ReadAllText(path);

        // Fix: assign the parsed data to highscores so that it's used later.
        highscores = JsonUtility.FromJson<HighScoreData>(json);
    }

    private void WriteData()
    {
        string path = Application.streamingAssetsPath + $"/{jsonFileName}.json";
        highscores.scores = new List<float> { highscores.scores[0], highscores.scores[1], highscores.scores[2] };
        string json = JsonUtility.ToJson(highscores, true);

        File.WriteAllText(path, json);

        AssetDatabase.Refresh();
    }
}

[System.Serializable]
public class HighScoreData
{
    public List<float> scores;

    public HighScoreData(List<float> scores)
    {
        this.scores = scores;
    }

    public HighScoreData()
    {
        this.scores = new List<float>();
        for (int i = 0; i < 3; i++) 
        {
            scores.Add(0.0f);
        }
    }
}
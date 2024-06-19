using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class TextFiles
{
    private static readonly string ScoreFilePath = Path.Combine(Application.dataPath, "StreamingAssets", "score.txt");
    private static readonly string SentencesFilePath = Path.Combine(Application.dataPath, "StreamingAssets", "sentences.txt");

    public static int ReadHighScore()
    {
        if (File.Exists(ScoreFilePath))
        {
            string scoreText = File.ReadAllText(ScoreFilePath);
            if (int.TryParse(scoreText, out int score))
            {
                return score;
            }
            else
            {
                Debug.LogError("Invalid high score format in score.txt");
            }
        }
        else
        {
            Debug.LogError("score.txt not found");
        }
        return 0;
    }

    public static void CheckAndUpdateHighScore(int currentScore)
    {
        int highScore = ReadHighScore();
        if (currentScore > highScore)
        {
            highScore = currentScore;
            File.WriteAllText(ScoreFilePath, highScore.ToString());
            // Debug.Log("New High Score: " + highScore);
        }
    }

    public static List<string> ReadSentences()
    {
        List<string> sentencesList = new List<string>();
        if (File.Exists(SentencesFilePath))
        {
            string[] sentences = File.ReadAllLines(SentencesFilePath);
            sentencesList.AddRange(sentences);
        }
        else
        {
            // Debug.LogError("sentences.txt not found");
        }
        return sentencesList;
    }
}

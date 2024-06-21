using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class TextFiles
{
    private static readonly string ScoreFilePath = Path.Combine(Application.dataPath, "StreamingAssets", "score.txt");
    private static readonly string SentencesFilePath = Path.Combine(Application.dataPath, "StreamingAssets", "sentences.txt");

    // Functia citeste scorul maxim din fisierul "score.txt" si il returneaza
    public static int ReadHighScore()
    {
        if (File.Exists(ScoreFilePath))
        {
            if (int.TryParse(File.ReadAllText(ScoreFilePath), out int score))
            {
                return score;
            }
        }
        return 0;
    }

    // Functia verifica si actualizeaza scorul maxim daca scorul curent este mai mare
    public static void CheckAndUpdateHighScore(int currentScore)
    {
        int highScore = ReadHighScore();
        if (currentScore > highScore)
        {
            File.WriteAllText(ScoreFilePath, currentScore.ToString());
        }
    }

    // Functia citeste propozitiile din fisierul "sentences.txt" si le returneaza sub forma de lista de string-uri
    public static List<string> ReadSentences()
    {
        List<string> sentencesList = new List<string>();
        if (File.Exists(SentencesFilePath))
        {
            sentencesList.AddRange(File.ReadAllLines(SentencesFilePath));
        }
        return sentencesList;
    }
}
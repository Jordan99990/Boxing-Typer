using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Player player;
    public Text TargetText;
    public Text UserInputText;

    public Text Current_Score;
    public Text High_Score;

    private int currentScore = 0;
    private int highScore = 0;
    private List<string> targetStrings;

    private string originalTargetString;
    private string originalUserInput = "";
    private bool mistakeMade = false;
    private int playerHearts = 3;
    private int enemyHearts = 10000;

    void Start()
    {
        targetStrings = TextFiles.ReadSentences();
        AssignNewTargetString();
        highScore = TextFiles.ReadHighScore();
        UpdateScoreUI();
        Current_Score.gameObject.SetActive(true);
        High_Score.gameObject.SetActive(true);
    }

    void Update()
    {
        HandleTypingInput();
    }

    void HandleTypingInput()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b')
            {
                HandleBackspace();
            }
            else if (c != '\n' && c != '\r') 
            {
                HandleCharacterInput(c);
            }

            UpdateUserInputText();

            if (originalUserInput.Equals(originalTargetString))
            {
                player.PlayerAttack();
                enemyHearts--;
                currentScore += 10; 
                AssignNewTargetString();
                CheckGameOver();
                UpdateScoreUI();
            }
        }
    }

    void HandleBackspace()
    {
        if (originalUserInput.Length > 0)
        {
            originalUserInput = originalUserInput.Substring(0, originalUserInput.Length - 1);
            mistakeMade = originalUserInput.Length > 0 && originalUserInput != originalTargetString.Substring(0, originalUserInput.Length);
        }
    }

    void HandleCharacterInput(char c)
    {
        if (originalUserInput.Length < originalTargetString.Length)
        {
            originalUserInput += c;
            if (originalUserInput != originalTargetString.Substring(0, originalUserInput.Length))
            {
                mistakeMade = true;
                player.EnemyAttack();
                playerHearts--;
                CheckGameOver();
                UpdateScoreUI();
            }
            else
            {
                mistakeMade = false;
            }
        }
    }

    void UpdateUserInputText()
    {
        if (UserInputText == null || TargetText == null)
        {
            return;
        }

        string coloredText = "";
        for (int i = 0; i < originalUserInput.Length; i++)
        {
            if (originalUserInput[i] == originalTargetString[i])
            {
                coloredText += $"<color=green>{originalUserInput[i]}</color>";
            }
            else
            {
                coloredText += $"<color=red>{originalUserInput[i]}</color>";
            }
        }

        UserInputText.text = coloredText;
        UpdateTargetText();
    }

    void UpdateTargetText()
    {
        if (TargetText == null)
        {
            return;
        }

        string targetColoredText = "";
        for (int i = 0; i < originalTargetString.Length; i++)
        {
            if (i < originalUserInput.Length)
            {
                if (originalUserInput[i] == originalTargetString[i])
                {
                    targetColoredText += $"<color=green>{originalTargetString[i]}</color>";
                }
                else
                {
                    targetColoredText += $"<color=red>{originalTargetString[i]}</color>";
                }
            }
            else
            {
                targetColoredText += originalTargetString[i];
            }
        }

        TargetText.text = targetColoredText;
    }

    void AssignNewTargetString()
    {
        originalUserInput = "";
        originalTargetString = targetStrings[Random.Range(0, targetStrings.Count)];
        if (TargetText != null)
        {
            TargetText.text = originalTargetString;
        }
        if (UserInputText != null)
        {
            UserInputText.text = "";
        }
    }

    void CheckGameOver()
    {
        if (playerHearts <= 0)
        {
            Debug.Log("Game Over: You Lose!");
            Debug.Log("Final Score: " + currentScore);
            TextFiles.CheckAndUpdateHighScore(currentScore);
            highScore = TextFiles.ReadHighScore();
            UpdateScoreUI();
            Current_Score.gameObject.SetActive(true);
            High_Score.gameObject.SetActive(true);
        }
        else if (enemyHearts <= 0)
        {
            Debug.Log("Game Over: You Win!");
            Debug.Log("Final Score: " + currentScore);
            TextFiles.CheckAndUpdateHighScore(currentScore);
            highScore = TextFiles.ReadHighScore();
            UpdateScoreUI();
            Current_Score.gameObject.SetActive(true);
            High_Score.gameObject.SetActive(true);
        }
    }

    void UpdateScoreUI()
    {
        
        if (Current_Score != null)
        {
            Current_Score.text = "Score		 : " + currentScore;
        }

        if (High_Score != null)
        {
            High_Score.text = "High Score: " + highScore;
        }
    }
}
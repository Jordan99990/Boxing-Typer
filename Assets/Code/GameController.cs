using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Player player;
    public Text TargetText, UserInputText, Current_Score, High_Score;
    public RawImage WinImage, LoseImage;
    public GameObject Health;

    private int currentScore = 0, highScore = 0, playerHearts = 3, enemyHearts = 2;
    private List<string> targetStrings;
    private string originalTargetString, originalUserInput = "";
    private bool mistakeMade = false, isGameOver = false;

    void Start()
    {
        // Functia Start este apelata la inceputul jocului
        // Aceasta initializeaza variabilele si actualizeaza interfata utilizatorului
        targetStrings = TextFiles.ReadSentences();
        AssignNewTargetString();
        highScore = TextFiles.ReadHighScore();
        UpdateScoreUI();

        Health.gameObject.SetActive(true);
        UpdateHeartsDisplay();

        WinImage.gameObject.SetActive(false);
        LoseImage.gameObject.SetActive(false);
        Current_Score.gameObject.SetActive(true);
        High_Score.gameObject.SetActive(true);
    }

    void Update()
    {
        // Functia Update este apelata in fiecare frame
        // Aceasta verifica daca jocul s-a terminat si gestioneaza intrarea de la tastatura
        if (!isGameOver) HandleTypingInput();
    }

    void UpdateHeartsDisplay()
    {
        // Functia UpdateHeartsDisplay actualizeaza afisajul inimilor
        // Aceasta parcurge fiecare copil al obiectului Health si activeaza sau dezactiveaza obiectele in functie de numarul de vieti ale jucatorului
        for (int i = 0; i < Health.transform.childCount; i++)
        {
            Health.transform.GetChild(i).gameObject.SetActive(i < playerHearts);
        }
    }

    void HandleTypingInput()
    {
        // Functia HandleTypingInput gestioneaza intrarea de la tastatura
        // Aceasta verifica daca a fost apasata tasta Backspace sau daca a fost introdus un caracter
        // Apoi actualizeaza interfata utilizatorului
        foreach (char c in Input.inputString)
        {
            if (c == '\b') HandleBackspace();
            else if (c != '\n' && c != '\r') HandleCharacterInput(c);

            UpdateUI();
        }
    }

    void HandleBackspace()
    {
        // Functia HandleBackspace gestioneaza apasarea tastei Backspace
        // Aceasta sterge ultimul caracter introdus de utilizator si verifica daca a fost facuta o greseala
        if (originalUserInput.Length > 0)
        {
            originalUserInput = originalUserInput.Substring(0, originalUserInput.Length - 1);
            mistakeMade = originalUserInput.Length > 0 && originalUserInput != originalTargetString.Substring(0, originalUserInput.Length);
        }
    }

    void HandleCharacterInput(char c)
    {
        // Functia HandleCharacterInput gestioneaza introducerea unui caracter de la tastatura
        // Aceasta adauga caracterul la input-ul utilizatorului si verifica daca a fost facuta o greseala
        // Daca nu a fost facuta o greseala si input-ul utilizatorului este identic cu textul tinta, atunci se ataca inamicul
        // Daca a fost facuta o greseala, atunci inamicul ataca jucatorul
        // Daca input-ul utilizatorului este identic cu textul tinta, se actualizeaza scorul si se atribuie un nou text tinta
        // Apoi se verifica daca jocul s-a terminat
        if (player.IsGamePaused() || originalUserInput.Length >= originalTargetString.Length) return;

        originalUserInput += c;
        mistakeMade = originalUserInput != originalTargetString.Substring(0, originalUserInput.Length);

        if (mistakeMade)
        {
            player.EnemyAttack();
            playerHearts--;

            if (playerHearts == 0)
                StartCoroutine(WaitAndUpdateHeartsDisplay());
            else 
                UpdateHeartsDisplay();
        }
        else if (originalUserInput.Equals(originalTargetString))
        {
            player.PlayerAttack();
            enemyHearts--;
            currentScore += 10;
            AssignNewTargetString();
        }

        CheckGameOver();
    }

    void UpdateUI()
    {
        // Functia UpdateUI actualizeaza interfata utilizatorului
        // Aceasta actualizeaza textul input-ului utilizatorului, textul tinta si scorul
        UpdateUserInputText();
        UpdateTargetText();
        UpdateScoreUI();
    }

    void UpdateUserInputText()
    {
        // Functia UpdateUserInputText actualizeaza textul input-ului utilizatorului
        // Aceasta coloreaza caracterele corecte in verde si caracterele gresite in rosu
        if (UserInputText == null) return;

        UserInputText.text = GetColoredText(originalUserInput, originalTargetString, originalUserInput.Length);
    }

    void UpdateTargetText()
    {
        // Functia UpdateTargetText actualizeaza textul tinta
        // Aceasta coloreaza caracterele corecte in verde si caracterele gresite in rosu
        if (TargetText == null) return;

        TargetText.text = GetColoredText(originalTargetString, originalUserInput, originalUserInput.Length, true);
    }

    string GetColoredText(string mainText, string compareText, int compareLength, bool isTarget = false)
    {
        // Functia GetColoredText returneaza un text colorat
        // Aceasta coloreaza caracterele corecte in verde si caracterele gresite in rosu
        StringBuilder coloredText = new StringBuilder();
        for (int i = 0; i < mainText.Length; i++)
        {
            if (i < compareLength)
            {
                if (mainText[i] == compareText[i])
                    coloredText.Append("<color=green>").Append(mainText[i]).Append("</color>");
                else if (!isTarget || (isTarget && i < compareText.Length && mainText[i] != compareText[i]))
                    coloredText.Append("<color=red>").Append(mainText[i]).Append("</color>");
                else
                    coloredText.Append(mainText[i]);
            }
            else
            {
                coloredText.Append(mainText[i]);
            }
        }
        return coloredText.ToString();
    }

    void AssignNewTargetString()
    {
        // Functia AssignNewTargetString atribuie un nou text tinta
        // Aceasta reseteaza input-ul utilizatorului si alege un text tinta aleatoriu dintr-o lista
        // Apoi se actualizeaza interfata utilizatorului
        originalUserInput = "";
        originalTargetString = targetStrings[Random.Range(0, targetStrings.Count)];
        UpdateUI();
    }

    IEnumerator WaitAndUpdateHeartsDisplay()
    {
        yield return new WaitForSeconds(0.4f);
        UpdateHeartsDisplay();
    }

    IEnumerator ShowEndGameUI(bool playerWon)
    {
        // Functia ShowEndGameUI afiseaza interfata de sfarsit de joc
        // Aceasta afiseaza imaginea de castig sau de pierdere, actualizeaza scorul si asteapta o scurta perioada de timp
        isGameOver = true;
        player.SetContinueButtonActive(false);
        yield return new WaitForSeconds(0.75f);

        TextFiles.CheckAndUpdateHighScore(currentScore);
        highScore = TextFiles.ReadHighScore();
        UpdateScoreUI();
        
        WinImage.gameObject.SetActive(playerWon);
        LoseImage.gameObject.SetActive(!playerWon);
    }

    void CheckGameOver()
    {
        // Functia CheckGameOver verifica daca jocul s-a terminat
        // Aceasta verifica daca jucatorul sau inamicul nu mai are vieti
        // Daca jocul s-a terminat, se afiseaza interfata de sfarsit de joc
        if (playerHearts <= 0 || enemyHearts <= 0)
        {
            StartCoroutine(ShowEndGameUI(enemyHearts <= 0));
        }
    }

    void UpdateScoreUI()
    {
        // Functia UpdateScoreUI actualizeaza interfata scorului
        // Aceasta afiseaza scorul curent si scorul maxim
        if (Current_Score != null)
            Current_Score.text = $"<b><color=white><size=30>Scor            : {currentScore}</size></color></b>";
        if (High_Score != null)
            High_Score.text = $"<b><color=white><size=30>Scor Maxim: {highScore}</size></color></b>";
    }

}
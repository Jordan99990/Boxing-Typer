using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject Enemy; // Referință către obiectul inamic
    public GameObject pauseMenuUI; // Referință către meniul de pauză
    public GameObject TextBackground; // Referință către fundalul textului
    public GameObject ContinueButton; // Referință către butonul de continuare
    public Color backgroundColor = new Color(1f, 1f, 1f, 0.5f); // Culoarea de fundal a textului
    public Color borderColor = Color.black; // Culoarea marginii textului
    public float borderWidth = 5f; // Lățimea marginii textului

    private Animator PlayerAnimator; // Animator pentru jucător
    private Animator EnemyAnimator; // Animator pentru inamic
    private bool isPaused; // Indicator dacă jocul este în pauză sau nu

    void Start()
    {
        Time.timeScale = 1f; // Setează viteza de joc la normal
        pauseMenuUI.SetActive(false); // Dezactivează meniul de pauză
        isPaused = false; // Setează indicatorul de pauză la fals
        PlayerAnimator = GetComponent<Animator>(); // Obține animatorul jucătorului
        EnemyAnimator = Enemy.GetComponent<Animator>(); // Obține animatorul inamicului
        StyleTextBackground(); // Stilizează fundalul textului
        TextBackground.SetActive(true); // Activează fundalul textului
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Dezactivează meniul de pauză
        Time.timeScale = 1f; // Setează viteza de joc la normal
        isPaused = false; // Setează indicatorul de pauză la fals
    }

    void Pause() // Player.cs
    {
        pauseMenuUI.SetActive(true); // Activează meniul de pauză
        Time.timeScale = 0f; // Setează viteza de joc la zero (pauză)
        isPaused = true; // Setează indicatorul de pauză la adevărat
    }

    public void Quit()
    {
        Application.Quit(); // Închide aplicația
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game"); // Încarcă scena "Game"
    }

    public void LoadDemo()
    {
        SceneManager.LoadScene("Demo"); // Încarcă scena "Demo"
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume(); // Dacă jocul este în pauză, reia jocul
            else
                Pause(); // Altfel, pune jocul în pauză
        }
    }

    public void PlayerAttack()
    {
        PlayerAnimator?.SetTrigger("PunchTrigger"); // Activează animația de atac al jucătorului
    }

    public bool IsGamePaused()
    {
        return isPaused; // Returnează valoarea indicatorului de pauză
    }

    public void EnemyAttack()
    {
        EnemyAnimator?.SetTrigger("PunchTrigger"); // Activează animația de atac al inamicului
    }

    void StyleTextBackground()
    {
        Image bgImage = TextBackground.GetComponent<Image>() ?? TextBackground.AddComponent<Image>(); // Obține componenta Image a fundalului textului sau adaugă una nouă dacă nu există
        bgImage.color = backgroundColor; // Setează culoarea de fundal a textului
        bgImage.type = Image.Type.Sliced; // Setează tipul de imagine al fundalului textului ca "Sliced"
    }

    public void SetContinueButtonActive(bool isActive)
    {
        ContinueButton?.SetActive(isActive); // Activează sau dezactivează butonul de continuare în funcție de valoarea parametrului
    }
}

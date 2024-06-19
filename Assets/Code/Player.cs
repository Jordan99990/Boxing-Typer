using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject Enemy;

    private Animator PlayerAnimator;
    private Animator EnemyAnimator;
    

    public GameObject pauseMenuUI;
    private bool isPaused;
    public GameObject TextBackground;

    public Color backgroundColor = new Color(1f, 1f, 1f, 0.5f); // Light white with some transparency
    public Color borderColor = Color.black; // Black border color
    public float borderWidth = 5f;
    public Sprite roundedCornerSprite; 

    void Start()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        isPaused = false;

        PlayerAnimator = this.GetComponent<Animator>();
        EnemyAnimator = Enemy.GetComponent<Animator>();

        StyleTextBackground();
        TextBackground.SetActive(true);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadDemo()
    {
        SceneManager.LoadScene("Demo");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void PlayerAttack()
    {
        if (PlayerAnimator != null)
        {
            PlayerAnimator.SetTrigger("PunchTrigger");
        }

    }

    public void EnemyAttack()
    {
        if (EnemyAnimator != null)
        {
            EnemyAnimator.SetTrigger("PunchTrigger");
        }
    }

    void StyleTextBackground()
    {
        Image bgImage = TextBackground.GetComponent<Image>();
        if (bgImage == null)
        {
            bgImage = TextBackground.AddComponent<Image>();
        }

        bgImage.color = backgroundColor; 

        if (roundedCornerSprite != null)
        {
            bgImage.sprite = roundedCornerSprite;
            bgImage.type = Image.Type.Sliced;
        }
    }
}
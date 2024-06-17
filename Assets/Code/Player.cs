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
    public Text TextComponent;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        isPaused = false;

        PlayerAnimator = this.GetComponent<Animator>();
        EnemyAnimator = Enemy.GetComponent<Animator>();

        TextBackground.SetActive(false);
        TextComponent.text = "";
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

    // Update is called once per frame
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
        if (PlayerAnimator != null)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                PlayerAnimator.SetTrigger("PunchTrigger");
            }
        }
        if (EnemyAnimator != null)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                EnemyAnimator.SetTrigger("PunchTrigger");
            }
        }
    }
}

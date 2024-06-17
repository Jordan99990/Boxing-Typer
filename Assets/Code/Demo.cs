using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Demo : MonoBehaviour
{
    public Transform entityTransform;
    public Animator entityAnimator;
    public GameObject pauseMenuUI;
    private bool isPaused;

    void Start()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void TranslateX(int x)
    {
        entityTransform.transform.position += new Vector3(0.1f * x, 0.0f, 0.0f);
    }
    public void TranslateY(int y)
    {
        entityTransform.transform.position += new Vector3(0.0f, 0.1f * y, 0.0f);
    }
    public void TranslateZ(int z)
    {
        entityTransform.transform.position += new Vector3(0.0f, 0.0f, 0.1f * z);
    }

    public void RotateX(int x)
    {
        entityTransform.transform.Rotate(5.0f * x, 0.0f, 0.0f);
    }
    public void RotateY(int y)
    {

        entityTransform.transform.Rotate(0.0f, 5.0f * y, 0.0f);
    }
    public void RotateZ(int z)
    {

        entityTransform.transform.Rotate(0.0f, 0.0f, 5.0f * z);
    }
    public void RotateCenter(int c)
    {
        entityTransform.RotateAround(Vector3.zero, Vector3.up, 5.0f * c);
    }

    public void ScaleX(int x)
    {
        entityTransform.transform.localScale += new Vector3(0.1f * x, 0.0f, 0.0f);
    }
    public void ScaleY(int y)
    {

        entityTransform.transform.localScale += new Vector3(0.0f, 0.1f * y, 0.0f);
    }
    public void ScaleZ(int z)
    {

        entityTransform.transform.localScale += new Vector3(0.0f, 0.0f, 0.1f * z);
    }
    public void ScaleAll(int a)
    {

        entityTransform.transform.localScale += new Vector3(0.1f * a, 0.1f * a, 0.1f * a);
    }

    public void Punch()
    {
        entityAnimator.SetTrigger("PunchTrigger");
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
}

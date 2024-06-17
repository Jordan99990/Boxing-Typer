using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Transform cameraTransform;
    public Animator blueAnimator;
    public Animator redAnimator;
    public float degrees;

    void Start()
    {
        Time.timeScale = 1f;
        degrees = cameraTransform.transform.rotation.y;
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

    public void RotateCamera()
    {
        cameraTransform.RotateAround(Vector3.zero, Vector3.up, 5.0f * Time.deltaTime);
        degrees += cameraTransform.transform.rotation.y;
    }

    void FixedUpdate()
    {
        RotateCamera();
        if ((int)degrees % 50 == 0)
        {
            if((int)degrees % 100 == 0)
            {
                blueAnimator.SetTrigger("PunchTrigger");
            }
            else
            {
                redAnimator.SetTrigger("PunchTrigger");
            }
        }
    }
}

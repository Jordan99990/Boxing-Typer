using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public GameObject Enemy;

    private Animator PlayerAnimator;
    private Animator EnemyAnimator;

    public GameObject TextBackground;
    public Text TextComponent;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = this.GetComponent<Animator>();
        EnemyAnimator = Enemy.GetComponent<Animator>();

        TextBackground.SetActive(false);
        TextComponent.text = "";
    }

    // Update is called once per frame
    void Update()
    {
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

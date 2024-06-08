using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Enemy;

    private Animator PlayerAnimator;
    private Animator EnemyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = this.GetComponent<Animator>();
        EnemyAnimator = Enemy.GetComponent<Animator>();
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

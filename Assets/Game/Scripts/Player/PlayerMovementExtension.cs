using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementExtension : PlayerMovement
{
    public bool canCharacterDash;
    private PlayerAnimatorExtension playerAnimator;

    public float defaultWalkSpeed;
    public float runSpeed;


    void Start()
    {
        playerAnimator = GetComponentInChildren<PlayerAnimatorExtension>();
        walkSpeed = defaultWalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (GameManager.instance.currentState == GameManager.GameStates.LevelStart)
        {
            canCharacterDash = false;
            playerAnimator.Walk();
            Move(new Vector3(1, 0, 0) * walkSpeed);
           
        }

        if (GameManager.instance.currentState == GameManager.GameStates.LevelActive)
        {
            canCharacterDash = true;
        }


        if (canCharacterDash)
        {
            GetComponent<PlayerDash>().enabled = true;
        }
        else
        {
            GetComponent<PlayerDash>().enabled = false;
        }

        if (walkSpeed > 3)
        {
            playerAnimator.Run();
        }
    }
}

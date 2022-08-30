using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatExtension : PlayerCombat
{
    private PlayerAnimatorExtension playerAnimator;
    public DamageObject[] specialAttackData;
  

   void Start()
    {
        playerAnimator = GetComponentInChildren<PlayerAnimatorExtension>();
    }

   public override void CombatInputEvent(string action)
    {
        base.CombatInputEvent(action);


    }

    public override void Hit(DamageObject d)
    {
        base.Hit(d);
        playerAnimator.ImpactPause();
        if (HitKnockDownCount >= HitKnockDownThreshold)
        {
            playerAnimator.ImpactPause();
        }

        //getting hit while being in the air also causes a knockdown
        if (!GetComponent<PlayerMovement>().playerIsGrounded())
        {
            playerAnimator.ImpactPause();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatExtension : PlayerCombat
{
    private PlayerAnimatorExtension playerAnimator;
    public DamageObject[] specialAttackData;

    [HideInInspector]
    public int attackPointValue;
    public int healDivisor;
 

    //Points Not Used In Main Game
    public delegate void PointsEventHandler(int points);
    public static event PointsEventHandler PointsEvent;

    public delegate void HealBarHandler();
    public static event HealBarHandler HealBarEvent;

    void Start()
    {
        playerAnimator = GetComponentInChildren<PlayerAnimatorExtension>();
      
    }

   public override void CombatInputEvent(string action)
    {
        base.CombatInputEvent(action);

        if (action == "Heal" && UIHealMeter.instance.isBarFull == true)
        {
          
                healDivisor = UIHealMeter.instance.healingBarDivisor;
                UIHealMeter.instance.healMeterFill.fillAmount = 0;
                GetComponent<HealthSystem>().AddHealth(CalculateHealAmount(healDivisor));
              
            
           
        } else if (UIHealMeter.instance.isBarFull == false)
        {

        }
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

    public override void CheckForHit()
    {
        base.CheckForHit();
        //GetAttackPointValue();
        if (targetHit)
        {
            if (HealBarEvent != null) HealBarEvent();

            //Not Used In Main Game --- Save this for a mini game
            if (PointsEvent != null) PointsEvent(GetAttackPointValue());

        }
    }

    //Not Used In Main Game
    private int GetAttackPointValue()
    {
        if (attackNum == 0)
        {
            attackPointValue = 50;
        }
        if (attackNum == 1)
        {
            attackPointValue = 100;
        }

        if (attackNum == 2)
        {
            attackPointValue = 150;
        }
        return attackPointValue;
    }

    private int CalculateHealAmount(int divisor)
    {
        
        int healAmount = GetComponent<HealthSystem>().MaxHp / divisor;
        return healAmount;
    }

}

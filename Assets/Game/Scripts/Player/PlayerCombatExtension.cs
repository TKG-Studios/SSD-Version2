using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerCombatExtension : PlayerCombat
{
    private PlayerAnimatorExtension playerAnimator;
    public DamageObject[] specialAttackData;

    [HideInInspector]
    public int attackPointValue;
 

    //Points Not Used In Main Game
    public delegate void PointsEventHandler(int points);
    public static event PointsEventHandler PointsEvent;

    public delegate void SpecialBarHandler();
    public static event SpecialBarHandler SpecialBarEvent;

    void Start()
    {
        playerAnimator = GetComponentInChildren<PlayerAnimatorExtension>();
      
    }

    public override void CombatInputEvent(string action)
    {
        base.CombatInputEvent(action);

        if (action == "Special" && UISpecialMeter.instance.isBarFull == true)
        {
                Special();
                UISpecialMeter.instance.specialMeterFill.fillAmount = 0;
                Special();



        } else if (UISpecialMeter.instance.isBarFull == false)
        {

        }
    }

    protected override void DealDamageToEnemy(GameObject enemy)
    {
        base.DealDamageToEnemy(enemy);

        DamageObject d = new DamageObject(0, gameObject);
        if (playerState.currentState == PLAYERSTATE.SPECIAL)
        {
            d = specialAttackData[attackNum];
        }
        d.inflictor = gameObject;

        //subsctract health from enemy
        HealthSystem hs = enemy.GetComponent<HealthSystem>();
        if (hs != null)
        {
            hs.SubstractHealth(d.damage);
        }

        enemy.GetComponent<EnemyAI>().Hit(d);
    }


   private void Special()
    {
      playerState.SetState(PLAYERSTATE.SPECIAL);
      playerAnimator.SpecialAttack();
    }

    public void SpecialKnockDown()
    {
        attackNum = 0;
        if (playerState.currentState == PLAYERSTATE.SPECIAL)
        {
            attackNum = 1;
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

    protected override float getAttackRange()
    {
        base.getAttackRange();
        if (playerState.currentState == PLAYERSTATE.PUNCH && attackNum <= PunchAttackData.Length)
        {
            return PunchAttackData[attackNum].range;
        }
        else if (playerState.currentState == PLAYERSTATE.KICK && attackNum <= KickAttackData.Length)
        {
            return KickAttackData[attackNum].range;
        }
        else if (jumpKickActive)
        {
            return JumpKickData.range;
        }

        else if (playerState.currentState == PLAYERSTATE.SPECIAL && attackNum <= specialAttackData.Length)
        {
            return specialAttackData[attackNum].range;
        }
        else
        {
            return 0f;
        } 
    }

    public override void CheckForHit()
    {
        base.CheckForHit();
        //GetAttackPointValue();
        if (targetHit)
        {
            if (playerState.currentState == PLAYERSTATE.KICK || playerState.currentState == PLAYERSTATE.PUNCH || playerState.currentState == PLAYERSTATE.JUMPKICK)
            {
                if (SpecialBarEvent != null) SpecialBarEvent();
            }

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


}

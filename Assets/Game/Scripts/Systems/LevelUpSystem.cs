using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
    public static LevelUpSystem instance;

    public delegate int OnLevelChange(int level);
    public static event OnLevelChange onLevelChange;


    [HideInInspector]
    public int currentLevel = 1;
    [HideInInspector]
    public float xPToNext = 100f;
    [HideInInspector]
    public float currentXP;
    public float xPMultiplier = 1.04f;
    public float xPAdditive = 10f;


    private void Start()
    {
        instance = this;
      
    }


    public float AddEXP(float xp)
    {
        currentXP += xp;
        float xpAddedToNext;
        if (currentXP >= xPToNext)
        {
            xpAddedToNext = (currentXP - xPToNext);
            currentLevel++;
            UpgradePunchAttackStrength(1);
            UpgradeKickAttackStrength(1);
            UpgradeJumpKickAttackStrength(1);
            currentXP = (0 + xpAddedToNext);
            xPToNext = SetNextXP(xPToNext);
        }
        return currentXP;

    }

    public float SetNextXP(float xp)
    {
        xp = (xPToNext * xPMultiplier) + xPAdditive;
        if (onLevelChange != null) onLevelChange (currentLevel);
        return xp;
    }

    public void UpgradePunchAttackStrength(int attackDamage)
    {

        DamageObject[] punchAttacks = GetComponent<PlayerCombat>().PunchAttackData;

        foreach (DamageObject punchAttack in punchAttacks)
        {
            punchAttack.damage += attackDamage;
        }
    }

    public void UpgradeKickAttackStrength(int attackDamage)
    {
        DamageObject[] kickAttacks = GetComponent<PlayerCombat>().KickAttackData;

        foreach (DamageObject kickAttack in kickAttacks)
        {
           kickAttack.damage += attackDamage;
        }
    }


    public void UpgradeJumpKickAttackStrength(int attackDamage)
    {
        DamageObject jumpKickAttack = GetComponent<PlayerCombat>().JumpKickData;

        jumpKickAttack.damage += attackDamage;
    }


}

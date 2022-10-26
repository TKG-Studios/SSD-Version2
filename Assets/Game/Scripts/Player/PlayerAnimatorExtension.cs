using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerAnimatorExtension : PlayerAnimator
{
	private PlayerMovementExtension playerMovement;
    public float waitTime;
    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovementExtension>();
    }
    public override void ShowDustEffect()
    {
        base.ShowDustEffect();
        StartCoroutine(ImpactPause());
    }

    public void SpecialAttack(int id)
    {
        animator.SetTrigger("SpecialAttack" + id);
 
    }

    public void SpecialKnockDown()
    {
        int attackNum = 1;
        SpecialAttack(attackNum);
        transform.parent.GetComponent<PlayerCombatExtension>().Special(attackNum);
       
    }

    public IEnumerator ImpactPause()
    {
        playerMovement.walkSpeed = 0;
		yield return new WaitForSeconds(waitTime);
        playerMovement.walkSpeed = 3f;
    }


}

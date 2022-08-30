using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public IEnumerator ImpactPause()
    {
        playerMovement.walkSpeed = 0;
		yield return new WaitForSeconds(waitTime);
        playerMovement.walkSpeed = 3f;
    }


}

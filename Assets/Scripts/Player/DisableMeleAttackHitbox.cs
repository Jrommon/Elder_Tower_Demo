using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMeleAttackHitbox : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        animator.GetComponent<PlayerMovement>().DisableMeleAttackHitbox();
        animator.GetComponent<PlayerMovement>().EnableMagic();
    }
}

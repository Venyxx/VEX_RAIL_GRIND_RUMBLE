using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAtkAnimEnd : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      //  PlayerAttack.instance.spinEffect.SetActive(true);
      if(!PlayerAttack.instance.spinEffect)
        {
            PlayerAttack.instance.spinEffect.SetActive(true);
        }
      if (!PlayerAttack.instance.Leftleg.enabled)
        {
            PlayerAttack.instance.Leftleg.enabled = false;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     

        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        PlayerAttack.instance.Anim.ResetTrigger("HAttackEnd1");
        PlayerAttack.instance.Anim.ResetTrigger("HAttackEnd2");
        PlayerAttack.instance.Anim.ResetTrigger("HAttackEnd3");
        PlayerAttack.instance.IsHeavyAttacking = false;
        PlayerAttack.instance.Leftleg.enabled = false;
        PlayerAttack.instance.spinEffect.SetActive(false);
        if (PlayerAttack.instance.spins > 0)
        { PlayerAttack.instance.spins = PlayerAttack.instance.spins - 1; }



    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

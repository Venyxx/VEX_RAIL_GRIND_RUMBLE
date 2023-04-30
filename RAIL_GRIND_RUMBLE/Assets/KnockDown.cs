using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDown : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAttack.instance.Righthand.enabled = false;
        PlayerAttack.instance.Lefthand.enabled = false;
        PlayerAttack.instance.Rightleg.enabled = false;
        PlayerAttack.instance.Leftleg.enabled = false;
        PlayerAttack.instance.Weapon.enabled = false;
        PlayerAttack.instance.spinEffect.SetActive(false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAttack.instance.Righthand.enabled = false;
        PlayerAttack.instance.Lefthand.enabled = false;
        PlayerAttack.instance.Rightleg.enabled = false;
        PlayerAttack.instance.Leftleg.enabled = false;
        PlayerAttack.instance.Weapon.enabled = false;
        PlayerAttack.instance.spinEffect.SetActive(false);

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

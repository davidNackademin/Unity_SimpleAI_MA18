using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepStateBehaviour : StateMachineBehaviour
{
    int sleepHash = Animator.StringToHash("sleep");
    int distanceHash = Animator.StringToHash("playerDistance");
    float timeToSleep;
    Transform playerTransform;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        timeToSleep = Random.Range(3f, 7f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeToSleep -= Time.deltaTime;

        if (timeToSleep <= 0)
        {
            animator.SetBool(sleepHash, false);
        }

        Vector2 current = animator.transform.position;
        Vector2 player = playerTransform.position;

        float distance = Vector2.Distance(current, player);
        animator.SetFloat(distanceHash, distance);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(sleepHash, false);
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

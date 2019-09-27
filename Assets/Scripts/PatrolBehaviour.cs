using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    public float speed = 3.0f;
    public GameObject patrolPointsPrefab;
    Transform patrolPoints;
    int randomPointIndex;
    int distanceHash = Animator.StringToHash("playerDistance");
    int sleepHash = Animator.StringToHash("sleep");
    Transform playerTransform;
    float timeStillAwake;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (patrolPoints == null)
        {
            patrolPoints = Instantiate(patrolPointsPrefab).transform;
        }

        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        randomPointIndex = Random.Range(0, patrolPoints.childCount);

        timeStillAwake = Random.Range(3f, 6f);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 current = animator.transform.position;
        Vector2 target = patrolPoints.GetChild(randomPointIndex).position;
        Vector2 player = playerTransform.position;

        float playerDistance = Vector2.Distance(current, player);
        animator.SetFloat(distanceHash, playerDistance);

        if (Vector2.Distance(current, target) > 0.1f)
        {
            animator.transform.position = Vector2.MoveTowards(current, target, speed * Time.deltaTime);
        } else
        {
            randomPointIndex = Random.Range(0, patrolPoints.childCount);
        }

        timeStillAwake -= Time.deltaTime;

        if (timeStillAwake <= 0)
        {
            animator.SetBool(sleepHash, true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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

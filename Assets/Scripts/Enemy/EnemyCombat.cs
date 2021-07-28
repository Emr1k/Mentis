using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{
    public float walkDistance = 8f  ;
    public float attackDistance = 1.4f;
    public float turnSpeed = 5f;
    public float attackRate = 1f;
    public float radius;
    public float damageCount;
    public LayerMask playerLayer;

    float currentAttackTime;

    Transform targetPoint;
    Animator anim;
    NavMeshAgent agent;
    private void Awake()
    {
        targetPoint = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        MoveAndAttack();
    }

    void MoveAndAttack()
    {
        float distance = Vector3.Distance(transform.position, targetPoint.position);
        if (distance > walkDistance)
        {

            if (agent.remainingDistance >= agent.stoppingDistance)
            {
                agent.isStopped = false;
                agent.speed = 7f;
                anim.SetBool("Walk", true);
                agent.SetDestination(targetPoint.position);
            }
            else
            {
                agent.isStopped = true;
                agent.speed = 0f;
                anim.SetBool("Walk", false);
            }
        }
        else
        {
            if (distance > attackDistance + 0.15f)
            {
                if (!anim.IsInTransition(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    agent.isStopped = false;
                    agent.speed = 7f;
                    anim.SetBool("Walk", true);
                    agent.SetDestination(targetPoint.position);
                }
            }
            else if (distance <= attackDistance)
            {
                agent.isStopped = true;
                anim.SetBool("Walk", false);
                agent.speed = 0f;
                Vector3 targetPosition = new Vector3(targetPoint.position.x, transform.position.y, targetPoint.position.z);

                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(targetPosition-transform.position),
                    turnSpeed * Time.deltaTime);

                if(currentAttackTime >= attackRate){
                    anim.SetTrigger("Attack"); 
                    currentAttackTime = 0f;
                    Collider[] hits = Physics.OverlapSphere(transform.position, radius, playerLayer);

                    foreach (Collider enemy in hits)
                    {
                        enemy.GetComponentInChildren<PlayerCombat>().TakeDamage(damageCount);
                    }
                }
                else { currentAttackTime += Time.deltaTime; }
            }

        }
      
    }
}

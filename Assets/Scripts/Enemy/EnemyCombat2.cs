using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat2 : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    public LayerMask whatIsPlayer;
    Animator anim;

    //Attack
    public float timeBetweenAttacks;
    [HideInInspector]
    public bool alreadyAttacked;
    public float radius;
    public float damageCount;
    public float turnSpeed;
    public float attackRange;
    public Vector3 sphereOffset;
    [HideInInspector]
    public bool playerInAttackRange;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position + sphereOffset, attackRange, whatIsPlayer);

        if (!playerInAttackRange) { ChasePlayer(); }
        else { AttackPlayer(); }
    }

    private void ChasePlayer()
    { 
        agent.SetDestination(player.position);
        agent.speed = 8f;
        anim.SetBool("Walk", true);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position),
                turnSpeed * Time.deltaTime);


            anim.SetTrigger("Attack");
            anim.SetBool("Walk", false);

            Collider[] hits = Physics.OverlapSphere(transform.position, radius, whatIsPlayer);

            foreach (Collider enemy in hits)
            {
                enemy.GetComponentInChildren<PlayerCombat>().TakeDamage(damageCount);
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false; 
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + sphereOffset, radius);
    }
}

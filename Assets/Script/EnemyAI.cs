using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public enum attackWays
    {
        longRangedAttack,
        closeRangedAttack,
        rangedAOEAttack
    };
    [Header("AI Generic Stats")]
    public float maxHealth = 100f;
    public float health = 100f;
    public float damage = 10f;
    public new string name = "Enemy Ironbot";
    public GameObject loot1;
    public GameObject loot2;

    [Space]
    [Header("AI References")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public GameObject deathEffect;
    public GameObject deathBody;
    public Slider healthBar;

    [Space]
    [Header("Attacking Object References")]
    public attackWays attackType;
    public GameObject projectile;
    public int killXP = 150;

    [Space]
    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private UIManager uiManager;
    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        healthBar.value = health;
        healthBar.maxValue = maxHealth;
        player = FindObjectOfType<PlayerManager>().gameObject.transform;
        agent = GetComponent<NavMeshAgent>();
        uiManager.SpawnEnemyCompassTrackingPoint(gameObject);
    }
    private void Awake()
    {
        player = FindObjectOfType<PlayerManager>().gameObject.transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint,-transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //Making sure enemy does not move or moves slowly
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            //Attack code here
            if(attackType == attackWays.longRangedAttack)
            {
                //Debug.Log("Damage Orb Launched from Enemy " + gameObject.name);
                GameObject rb = Instantiate(projectile, transform.position, Quaternion.identity);
                rb.GetComponent<DamageSphere>().damage = damage;
                rb.GetComponent<Rigidbody>().AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.GetComponent<Rigidbody>().AddForce(transform.up * 2.5f, ForceMode.Impulse);
                Destroy(rb, 10f);
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public int TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) {
            Invoke(nameof(DestroyEnemy), .05f);
            return killXP;
        }
        return killXP / 10;
    }
    private void DestroyEnemy()
    {
        if(deathEffect != null)
        {
            GameObject temp = Instantiate(deathEffect, transform.position, transform.rotation);
            temp.GetComponent<ParticleSystem>().Play();
            Destroy(temp, 2f);
        }
        if(loot1 != null || loot2 != null)
        {
            int tempRandom = Random.Range(0, 100);
            if(tempRandom < 39)
            {
                int tempRandomLoot = Random.Range(0, 100);
                if (tempRandomLoot < 49)
                {
                    GameObject temp = Instantiate(loot1, transform.position, transform.rotation);
                    temp.GetComponent<Rigidbody>().AddExplosionForce(2f, transform.position, 4f);
                }
                else
                {
                    GameObject temp = Instantiate(loot2, transform.position, transform.rotation);
                    temp.GetComponent<Rigidbody>().AddExplosionForce(2f, transform.position, 4f);
                }
            }
        }
        Destroy(gameObject);
    }
    void Update()
    {
        if(player == null) Destroy(gameObject);
        healthBar.value = Mathf.Lerp(healthBar.value, health, Time.deltaTime * 5f);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

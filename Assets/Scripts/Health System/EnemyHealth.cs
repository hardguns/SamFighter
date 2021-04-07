using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int enemyMaxHealth = 1000;
    public int enemyCurrentHealth = 500;
    public Animator animator;
    private NavMeshAgent agent;
    private CapsuleCollider playerCollider;
    public bool isAlive = true;

    public event Action<float> OnHealthPctChanged = delegate { };
    //public bool isDeathAnimTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isAlive", isAlive);
        agent = GetComponent<NavMeshAgent>();
        playerCollider = GetComponent<CapsuleCollider>();
        LogDamage();
    }

    // Update is called once per frame
    void Update()
    {

        //if (enemyCurrentHealth <= 0 && isAlive)
        //{
        //    agent.enabled = false;
        //    animator.SetTrigger("enemyDeath");

        //    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        //    {
        //        isAlive = false;
        //        animator.SetBool("isAlive", isAlive);
        //        if (!isAlive && !isDeathAnimTriggered)
        //        {
        //            isDeathAnimTriggered = true;
        //            //StartCoroutine(KillEnemy());
        //            Debug.Log("Morir");
        //            //Destroy(gameObject);
        //        }
        //    }
        //}

    }

    public void TakeDamage(int damageAmount)
    {
        enemyCurrentHealth -= damageAmount;

        float currentHealthPct = (float)enemyCurrentHealth / (float)enemyMaxHealth;
        OnHealthPctChanged(currentHealthPct);

        if (enemyCurrentHealth <= 0)
        {
            enemyCurrentHealth = 0;
            if (isAlive)
            {
                animator.SetTrigger("enemyDeath");
                Debug.Log("La maté :v");
                GameController.instance.currentEnemyCount--;
                playerCollider.enabled = false;
            }
            agent.enabled = false;
            isAlive = false;
            StartCoroutine(KillEnemy(animator.GetCurrentAnimatorStateInfo(0).length + 1f));
            //Destroy(gameObject);
        }

        LogDamage();
    }

    private IEnumerator KillEnemy(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        Destroy(gameObject);
    }

    void LogDamage()
    {
        Debug.Log(enemyCurrentHealth);
    }
}

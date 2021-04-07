using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int enemyDamage;
    public bool isAttackTriggered;
    private EnemyFollow enemyFollow;
    // Start is called before the first frame update
    void Start()
    {
        enemyFollow = GetComponentInParent<EnemyFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(enemyFollow.player.transform.position, transform.position);

        if (distance < 1.2f)
        {
            enemyFollow.animator.SetBool("enemyAttack", true);
        }
        else
        {
            enemyFollow.animator.SetBool("enemyAttack", false);
        }
    }

    public void AttackEnd()
    {
        enemyFollow.player.GetComponent<PlayerHealth>().TakeDamage(enemyDamage);
    }
}

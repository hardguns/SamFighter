using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int objectDamage;
    public bool isAttackTriggered;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !isAttackTriggered)
        {
            isAttackTriggered = true;
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(objectDamage);
        }
    }
}

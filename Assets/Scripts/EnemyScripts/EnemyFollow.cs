using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    private Rigidbody rig;
    NavMeshAgent agent;
    private bool isCelebrating;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Pos player: " + player.transform.position);
        //Debug.Log("Pos enemy: " + transform.position);
        //rig.MovePosition(player.transform.position - transform.position);
        if (player.GetComponent<PlayerController>().isAlive)
        {
            if (GetComponent<EnemyHealth>().isAlive)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Sword Attack"))
                {
                    agent.destination = player.transform.position;
                }
            }
        }
        else if (!isCelebrating)
        {
            isCelebrating = true;
            animator.SetTrigger("winEnemy");
            //agent.destination = transform.position;
            agent.enabled = false;
        }
    }
}

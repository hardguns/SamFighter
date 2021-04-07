using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public float liveTime = 4f;
    public bool isDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectsWithTag("Player")[0];
        
        //// Aim spell in player's direction.
        transform.rotation = player.transform.rotation;

        Destroy(gameObject, liveTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No speed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            isDestroyed = true;
            speed = 0;

            Destroy(gameObject);
        }
    }
}

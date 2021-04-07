using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToKey : MonoBehaviour
{
    public float maximumLength;
    //public GameObject player;
    private Ray rayKey;
    private Vector3 direction;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var hAxis = Input.GetAxis("Horizontal");
        var yAxis = Input.GetAxis("Vertical");

        Vector3 vector = new Vector3(hAxis, 0, yAxis);
        RaycastHit hit;
        Vector3 keyPos = new Vector3(transform.position.x, 10f, transform.position.z);
        rayKey = new Ray(keyPos, vector);
        Physics.Raycast(rayKey.origin, rayKey.direction, out hit, maximumLength);

        //if (Physics.Raycast(rayKey.origin, rayKey.direction, out hit, maximumLength))
        //{
        //    //RotateToKeyDirection(gameObject, hit.point);
        //}
    }

    //void RotateToKeyDirection(GameObject obj, Vector3 destination)
    //{
    //    //direction = destination - obj.transform.position;
    //    //rotation = Quaternion.LookRotation(direction);
    //    //obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    //}
}

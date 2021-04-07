using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerObject;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - playerObject.transform.position;
    }

    // LateUpdate is called after Update methods
    void LateUpdate()
    {
        Vector3 newPos = playerObject.transform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (!playerObject.GetComponent<PlayerController>().isAlive)
        {
            LookAtPlayer = true;
        }

        if (LookAtPlayer)
        {
            transform.LookAt(playerObject.transform);
        }
    }
}

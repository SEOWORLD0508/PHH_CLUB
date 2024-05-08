using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    Transform target;

    [SerializeField]
    float moveSpeed;

    // Update is called once per frame
    void LateUpdate()
    {

        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed*Time.deltaTime);
    }
}

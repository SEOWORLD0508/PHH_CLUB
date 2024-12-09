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

        /*transform.position = target.position + new Vector3(0, 0, -10);*/
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -10), moveSpeed*Time.deltaTime);



    }
}

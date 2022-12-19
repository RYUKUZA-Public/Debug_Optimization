using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.forward;
        else if (Input.GetKey(KeyCode.S))
            transform.position += Vector3.back;
        else if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.left;
        else if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right;
    }
}

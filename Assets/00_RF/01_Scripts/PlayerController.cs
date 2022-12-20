using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어 이동 속도
    [SerializeField]
    private float _speed = 10f;
    
    private void Update()
    {
        // TransformDirection : 로컬 -> 월드로
        // InverseTransformDirection : 월드 -> 로컬

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), .2f );
            transform.position += Vector3.forward * (Time.deltaTime * this._speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), .2f );
            transform.position += Vector3.back * (Time.deltaTime * this._speed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), .2f );
            transform.position += Vector3.left * (Time.deltaTime * this._speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), .2f );
            transform.position += Vector3.right * (Time.deltaTime * this._speed);
        }
    }
}

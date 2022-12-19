using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    
    void Update()
    {
        // TransformDirection : 로컬 -> 월드로
        // InverseTransformDirection : 월드 -> 로컬
        
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * (Time.deltaTime * this._speed));
        else if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * (Time.deltaTime * this._speed));
        else if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * (Time.deltaTime * this._speed));
        else if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * (Time.deltaTime * this._speed));
    }
}

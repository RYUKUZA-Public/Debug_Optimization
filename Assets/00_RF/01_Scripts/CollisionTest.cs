using System;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hello Collision");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Hello Trigger : {other.gameObject.name}");
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.magenta, 1.0f);

            // 6번과 7 레이어 취득 (비트시프트)
            //int mask = (1 << 6) | (1 << 7);
            // 비트시프트가 아닌 Unity에 준비 된 LayerMask를 이용하여 취득
            LayerMask mask = LayerMask.GetMask("Monster") |
                             LayerMask.GetMask("Wall");
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, mask))
            {
                Debug.Log($"Raycast Camera {hit.collider.gameObject.name}");
            }
        }
    }
}

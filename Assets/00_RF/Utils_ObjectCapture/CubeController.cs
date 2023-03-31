using System;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float rotationSpeed = 50.0f;
    public float scaleSpeed = 0.1f;
    public float maxScale = 2.0f;
    public float minScale = 0.5f;

    private Vector3 lastMousePosition;
    private bool isDragging = false;

    private Transform obj;
    private void Start()
    {
        if (transform.childCount > 0)
        {
            obj = transform.GetChild(0);
        }
    }
    
    void Update()
    {
        if (transform.childCount == 0)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            delta.y = -delta.y;
            delta.x = -delta.x;

            obj.transform.Rotate(Vector3.up, delta.x * rotationSpeed * Time.deltaTime, Space.World);
            obj.transform.Rotate(Vector3.right, -delta.y * rotationSpeed * Time.deltaTime, Space.World);

            lastMousePosition = Input.mousePosition;
        }
        
        // 마우스 휠로 크기 조절 처리
        float wheelDelta = Input.GetAxis("Mouse ScrollWheel");
        if (wheelDelta != 0.0f)
        {
            float scaleDelta = wheelDelta * scaleSpeed;
            obj.transform.localScale = Vector3.ClampMagnitude(obj.transform.localScale + new Vector3(scaleDelta, scaleDelta, scaleDelta), maxScale);
            obj.transform.localScale = Vector3.Max(obj.transform.localScale, new Vector3(minScale, minScale, minScale));
        }
    }
}
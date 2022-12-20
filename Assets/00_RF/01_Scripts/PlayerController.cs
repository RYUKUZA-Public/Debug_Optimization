using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어 이동 속도
    [SerializeField]
    private float _speed = 10f;

    private void Start()
    {
        // 구독신청
        // InputManager에서 키입력을 감지 했을 경우 OnKeyboard를 실행
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
    }

    private void OnKeyboard()
    {
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

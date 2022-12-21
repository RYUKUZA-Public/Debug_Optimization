using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어 이동 속도
    [SerializeField]
    private float _speed = 10f;
    // 목적지 이동 여부 (마우스)
    private bool _moveToDest = false;
    // 목적지 위치
    private Vector3 _destPos;

    private void Start()
    {
        // 구독신청
        // InputManager에서 키입력을 감지 했을 경우 OnKeyboard를 실행
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    private void Update()
    {
        if (_moveToDest)
        {
            // 방향 벡터
            Vector3 dir = _destPos - transform.position;
            
            // 작은값이라 가정 (도착??)
            // float - float 의 경우에는 오차 범위기 생기기 때문
            if (dir.magnitude < 0.0001f)
            {
                _moveToDest = false;
            }
            // 도작 전
            else
            {
                // 이동하는 값이 남은 거리보다 작다는 것을 체크 하지 않으면 캐릭터의 움직임 버그가 발생한다.
                // 때문에 Clamp를 이용해 계산이 필요하다.
                float moveDist = Mathf.Clamp(Time.deltaTime * _speed, 0, dir.magnitude);
                // 이동
                transform.position += dir.normalized * moveDist;
                // 플레이어가 목표를 바라봄
                //transform.LookAt(_destPos);
                // 플레이어가 목표를 바라봄 (부드럽게 회전)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            }
        }
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

        // 마우스 이벤트만 감지 하기 위해서
        // 키보드 이벤트에서는 false
        _moveToDest = false;
    }

    private void OnMouseClicked(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Click)
            return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.magenta, 1.0f);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Wall")))
        {
            // 목적지를 저장후 플레이어가 이동해야 함
            // 목적지 벡터를 취득
            _destPos = hit.point;
            // 마우스 이동 true
            _moveToDest = true;
        }
    }
}

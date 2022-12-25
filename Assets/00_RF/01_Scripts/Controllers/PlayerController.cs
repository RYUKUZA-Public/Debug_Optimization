using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어 이동 속도
    [SerializeField]
    private float _speed = 10f;
    // 목적지 이동 여부 (마우스)
    //private bool _moveToDest = false;
    // 목적지 위치
    private Vector3 _destPos;
    /// <summary>
    /// 플레이어 상태
    /// </summary>
    private enum PlayerState { Moving, Die, Idle}
    /// <summary>
    /// 플레이어 상태
    /// </summary>
    private PlayerState _state = PlayerState.Idle;

    private void Start()
    {
        // 구독신청
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        //Managers.Resource.Instantiate("UI/Popup/UI_Button");

        for (int i = 0; i < 8; i++)
        {
            UI_Button ui = Managers.UI.ShowPopupUI<UI_Button>();
        }
        
        //Managers.UI.ClosePopupUI(ui);
    }
    
    private void Update()
    {
        switch (_state)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Die:
                UpdateDie();
                break;
        }
    }
    
    private void OnMouseClicked(Define.MouseEvent evt)
    {
        
        if (_state == PlayerState.Die)
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
            _state = PlayerState.Moving;
        }
    }

    private void UpdateIdle()
    {
        // 애니메이션
        Animator anime = GetComponent<Animator>();
        anime.SetFloat("Speed", 0f);
    }
    
    private void UpdateMoving()
    {
        // 방향 벡터
        Vector3 dir = _destPos - transform.position;
            
        // 작은값이라 가정 (도착??)
        // float - float 의 경우에는 오차 범위기 생기기 때문
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
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
        
        // 이동 애니메이션
        Animator anime = GetComponent<Animator>();
        anime.SetFloat("Speed", _speed);
    }

    private void UpdateDie()
    {
    }
    
    
}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// 카메라 모드
    /// </summary>
    [SerializeField]
    private Define.CameraMode _mode = Define.CameraMode.QuarterView;
    /// <summary>
    /// //(0, 6, -5) 카메라의 위치 세팅에 따름
    /// </summary>
    [SerializeField]
    private Vector3 _delta = new Vector3(0, 6f, -5f);
    /// <summary>
    /// 플레이어
    /// </summary>
    [SerializeField] private GameObject _player = null;

    private void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuarterView)
        {
            // 벽을 확인
            RaycastHit hit;
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                // 여기서 카메라를 이동시킴
                // 충돌한 좌표 - 플레이의 포지션 = 방향벡터 .magnitude = 방향텍터의 크기
                float dist = (hit.point - _player.transform.position).magnitude * .8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                // 카메라 포지션 (플레이어 기준 + 델타값)
                transform.position = _player.transform.position + this._delta;
                // 로테이션은 LookAt을 사용하여 플레이어를 바라보게 한다.
                transform.LookAt(_player.transform);
            }
        }
    }
    
    private void SetQuarterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}

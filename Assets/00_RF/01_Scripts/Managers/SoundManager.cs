using UnityEngine;

public class SoundManager
{
    // mp3플레이어 -> AudioSource (소리의 근원)
    // mp3 -> AudioClip
    // 귀 -> AudioListener
    
    /// <summary>
    /// Define에 설정한 사운드 타입
    /// </summary>
    private AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        // 사운드 관리용 오브젝트 찾고 없다면 생성
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject() { name = "@Sound" };
            Object.DontDestroyOnLoad(root);
            
            string[] soundTypeNames = System.Enum.GetNames(typeof(Define.Sound));
            // Define의 MaxCount 제거 때문에 -1
            for (int i = 0; i < soundTypeNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundTypeNames[i] };
                // 해당 soundTyp의 AudioSource 등록
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            
            // BGM은 루프 한다
            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    /// <summary>
    /// 재생
    /// </summary>
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        // 경로에 Sounds가 없다면 붙여 주자
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";
        
        // BGM
        if (type == Define.Sound.Bgm)
        {
            // 해당 클립을 로드
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"오디오 클립이 없습니다. {audioClip}");
                return;
            }
            
            // Bgm 오디오 소스
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            
            // 다른 BGM이 재생중 이라면 정지 한다.
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        // Effect
        else
        {
            // 해당 클립을 로드
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"오디오 클립이 없습니다. {audioClip}");
                return;
            }
            
            // Effect 오디오 소스
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            // 한번 재생
            audioSource.PlayOneShot(audioClip);
        }
    }
}
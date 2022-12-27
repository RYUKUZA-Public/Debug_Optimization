using System.Collections.Generic;
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
    /// 경로와 클립 캐싱
    /// </summary>
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

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
    /// 재생 path 버전
    /// </summary>
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }
    
    /// <summary>
    /// TODO. 재생 AudioClip 버전
    /// </summary>
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;
        
        // BGM
        if (type == Define.Sound.Bgm)
        {
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
            // Effect 오디오 소스
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            // 한번 재생
            audioSource.PlayOneShot(audioClip);
        }
    }
    
    /// <summary>
    /// 오디오 클립을 가져 오거나 추가
    /// </summary>
    private AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        // 경로에 Sounds가 없다면 붙여 주자
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";
        
        AudioClip audioClip = null;
        
        // BGM 
        if (type == Define.Sound.Bgm)
            // 해당 클립을 로드
            audioClip = Managers.Resource.Load<AudioClip>(path);
        // Effect
        else
        {
            // 찾고자 하는 오디오 클립이 있는지 검색
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                // 없다면 로드 후 추가
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }
        
        if (audioClip == null)
            Debug.Log($"오디오 클립이 없습니다. {audioClip}");
        
        return audioClip;
    }
    
    /// <summary>
    /// 클리어
    /// 본 스크립트는 Dontdestroyonloadd이기 때문에
    /// 메모리가 무한이 쌓이게 된다.
    /// 때문에 주의해서 타이밍에 마춰 Clear를 관리 해야 한다.
    /// </summary>
    public void Clear()
    {
        _audioClips.Clear();

        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
    }
}
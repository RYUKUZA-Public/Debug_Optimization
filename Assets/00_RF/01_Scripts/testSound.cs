using UnityEngine;

public class testSound : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioClip audioClip2;
    
    private int i = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        i++;

        if (i % 2 == 0)
            Managers.Sound.Play(audioClip, Define.Sound.Bgm);
        else
            Managers.Sound.Play(audioClip2, Define.Sound.Bgm);
    }
}

using UnityEngine;

public class testSound : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Managers.Sound.Play(Define.Sound.Effect, "UnityChan/univ0001");
        
        Managers.Sound.Play(Define.Sound.Effect, "UnityChan/univ0002");
    }
}

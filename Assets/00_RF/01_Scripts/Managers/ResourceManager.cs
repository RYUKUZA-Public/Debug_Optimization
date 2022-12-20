using UnityEngine;

public class ResourceManager
{
    // 로드
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    // 생성
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if (prefab == null)
        {
            Debug.Log($"Prefab 로드 실패 {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    // 파괴
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;
        
        Object.Destroy(go);
    }
}

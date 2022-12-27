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
        // 1. 오리지널도 이미 가지고 있을 경우 바로 사용
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if (prefab == null)
        {
            Debug.Log($"Prefab 로드 실패 {path}");
            return null;
        }
        
        // 2. 혹시 풀링 된 오브젝트가 있다면 재사용
        GameObject go = Object.Instantiate(prefab, parent);
        // (Clone) 텍스트 제거
        go.name = prefab.name;
        return go;
    }

    // 파괴
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;
        // 3. 풀링이 필요한경우 풀링 매니저에게 처리를 부탁
        
        Object.Destroy(go);
    }
}

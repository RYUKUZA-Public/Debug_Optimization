using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    #region [Pool]
    /// <summary>
    /// 풀
    /// </summary>
    class Pool
    {
        /// <summary>
        /// 오리지널 데이터
        /// </summary>
        public GameObject Original { get; private set; }
        /// <summary>
        /// 부모가 될 Root
        /// </summary>
        public Transform Root { get; set; }
        /// <summary>
        /// Pool을 담을 스택
        /// </summary>
        private Stack<Poolable> _poolStack = new Stack<Poolable>();

        /// <summary>
        /// 초기화
        /// </summary>
        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            // 루트로 사용할 오브젝트를 생성
            Root = new GameObject().transform;
            // 데이터에 따라 이름 설정
            Root.name = $"{original.name}_Root";
            
            // 카운트 만큼 생성 후 Push
            for (int i = 0; i < count; i++)
                Push(Create());
        }

        /// <summary>
        /// 생성
        /// </summary>
        public Poolable Create()
        {
            // 오브젝트 생성 (클론)
            GameObject go = Object.Instantiate<GameObject>(Original);
            // 이름 정리
            go.name = Original.name;
            // Poolable형식으로 리턴
            return go.GetOrAddComponent<Poolable>();
        }
        
        /// <summary>
        /// Push 반환
        /// Stack에 추가및 반환시 필요 작업
        /// </summary>
        public void Push (Poolable poolable)
        {
            if (poolable == null)
                return;
            
            // 비활성화 상태
            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;
            // 스택에 추가
            _poolStack.Push(poolable);
        }

        /// <summary>
        /// Pop 꺼내기
        /// </summary>
        public Poolable Pop(Transform parent)
        {
            Poolable poolable;
            
            // 하나라도 대기중
            if (_poolStack.Count > 0)
                // 꺼내기
                poolable = _poolStack.Pop();
            // 없다면
            else
                // 생성
                poolable = Create();
            
            // 활성화 상태
            poolable.gameObject.SetActive(true);

            // DontDestroyOnLoad 해제용
            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;
            
            poolable.transform.parent = parent;
            poolable.isUsing = true;
            
            return poolable;
        }
    }
    #endregion
    
    // PoolManager는 여러가지 pool을 가지고 있다.
    // 그 pool 목록을 딕셔너리로 관리
    private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();
    private Transform _root;
    
    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    /// <summary>
    /// Pool 생성
    /// </summary>
    private void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;
        
        // 딕셔너리에 추가
        _pools.Add(original.name, pool);
    }

    /// <summary>
    /// 반환
    /// </summary>
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        if (_pools.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pools[name].Push(poolable);
    }
    
    /// <summary>
    /// 꺼내기
    /// </summary>
    public Poolable Pop(GameObject original, Transform parent = null)
    {
        // 처음 만들경우 데이터가 없기 때문에 체크 후 생성이 필요하다.
        if (_pools.ContainsKey(original.name) == false)
            CreatePool(original);
        
        return _pools[original.name].Pop(parent);
    }

    /// <summary>
    /// 원본 취득
    /// </summary>
    public GameObject GetOriginal(string name)
    {
        if (_pools.ContainsKey(name) == false)
            return null;
        
        return _pools[name].Original;
    }

    /// <summary>
    /// 클리어
    /// </summary>
    public void Clear()
    {
        foreach (Transform child in _root)
        {
            GameObject.Destroy(child.gameObject);
        }
        
        _pools.Clear();
    }
}

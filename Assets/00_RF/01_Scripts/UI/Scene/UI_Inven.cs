using UnityEngine;

public class UI_Inven : UI_Scene
{
    private enum GameObjects
    {
        GridPanel
    }

    private void Start() => Init();

    /// <summary>
    /// 초기화
    /// </summary>
    public override void Init()
    {
        base.Init();
        // 바인딩
        Bind<GameObject>(typeof(GameObjects));
        // gridPanel 취득
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        // 아이템 데이터 삭제
        foreach (Transform transform in gridPanel.transform)
            Managers.Resource.Destroy(transform.gameObject);

        // TODO. 테스트 8개 생성
        // 아이템 데이터 생성
        for (int i = 0; i < 8; i++)
        {
            // 아이템 Prefab 생성
            GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item");
            // 그리드 자식으로 등록
            item.transform.SetParent(gridPanel.transform);
            // 체크
            UI_Inven_Item invenItem = Util.GetOrAddComponent<UI_Inven_Item>(item);
            // 아이템 데이터 셋팅
            invenItem.SetInfo($"집판검{i}번");
        }
    }
}

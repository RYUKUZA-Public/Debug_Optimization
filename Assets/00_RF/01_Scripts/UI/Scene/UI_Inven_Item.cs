using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
    private enum GameObjects
    {
        ItemIcon,
        ItemNameText
    }

    /// <summary>
    /// 아이템 이름
    /// </summary>
    private string _name;
    
    private void Start() => Init();

    /// <summary>
    /// 초기화
    /// </summary>
    public override void Init()
    {
        // 바인딩
        Bind<GameObject>(typeof(GameObjects));
        // ItemNameText 취득
        GameObject item = Get<GameObject>((int)GameObjects.ItemNameText);
        // 텍스트 변경
        item.GetComponent<Text>().text = _name;
        
        // 클릭 이벤트 등록 (버튼)
        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((data) =>
        {
            Debug.Log($"{item.GetComponent<Text>().text} 아이템 클릭");
        });
    }
    
    /// <summary>
    /// 데이터 셋팅
    /// </summary>
    public void SetInfo(string name)
    {
        _name = name;
    }
}

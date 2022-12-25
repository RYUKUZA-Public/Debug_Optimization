using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    
    /// <summary>
    /// enum의 이름은 오브젝트의 이름과 동일 해야 한다.
    /// </summary>
    enum Buttons
    {
        PointButton
    }
    enum Texts
    {
        PointText,
        ScoreText
    }
    enum GameObjects
    {
        TestObject
    }

    enum Images
    {
        TestImage
    }
    
    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        
        //AddUIEvent를 엑스텐션메서드화 하여 바로 사용
        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);
        
        // Test Image의 Handler를 받아 와서 드래그 추가
        GameObject go = GetImage((int)Images.TestImage).gameObject;
        
        AddUIEvent(go, 
            data => { go.transform.position = data.position; }, 
            Define.UIEvent.Drag);
    }
    
    private int _score = 0;

    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"점수 : {_score}";
    }
}

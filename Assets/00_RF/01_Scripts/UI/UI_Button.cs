using UnityEngine;
using UnityEngine.UI;

public class UI_Button : UI_Base
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

        // Text타입의 ScoreText의 text를 변경
        GetText((int)Texts.ScoreText).text = "Bind Test";
        
        // Test Image의 Handler를 받아 와서 드래그 추가
        GameObject go = GetImage((int)Images.TestImage).gameObject;
        UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
        evt.OnDragHandler += data =>
        {
            go.transform.position = data.position;
        };
    }
    
    private int _score = 0;

    public void OnButtonClicked()
    {
        _score++;
    }
}

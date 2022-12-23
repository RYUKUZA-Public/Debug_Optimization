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
    
    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        // Text타입의 ScoreText의 text를 변경
        GetText((int)Texts.ScoreText).text = "Bind Test";
    }
    
    private int _score = 0;

    public void OnButtonClicked()
    {
        _score++;
    }
}

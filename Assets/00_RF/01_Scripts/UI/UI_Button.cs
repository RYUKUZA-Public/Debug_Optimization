using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class UI_Button : MonoBehaviour
{
    /// <summary>
    /// 어떤 타입인지 모르기 때문에 Text, Button 등의 최상위 부모를 사용
    /// 타입을 넣어 주면 해당 타입의 목록을 취득
    /// </summary>
    private Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, Object[]>();

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

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
    }

    /// <summary>
    /// 리플렉션을 이용하여 Type에 enum을 넘겨 준다
    /// </summary>
    private void Bind<T>(Type type) where T : UnityEngine.Object
    {
        // 타입의 이름들을 취득
        string[] names = Enum.GetNames(type);
        // 취득한 수 만큼 배열 생성
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        
        // 해당 자식들을 찾아서 취득
        for (int i = 0; i < names.Length; i++)
            objects[i] = Util.FindChild<T>(gameObject, names[i], true);
        
        // 딕셔너리에 추가
        _objects.Add(typeof(T), objects);
    }

    private int _score = 0;

    public void OnButtonClicked()
    {
        _score++;
    }
}

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
        Get<Text>((int)Texts.ScoreText).text = "Bind Test";
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
        // 딕셔너리에 추가
        _objects.Add(typeof(T), objects);
        
        // 해당 자식들을 찾아서 취득
        for (int i = 0; i < names.Length; i++)
        {
            // GameObject
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            // component
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"로드 실패 : {names[i]}");
        }
    }

    /// <summary>
    /// 디셔너리로 부터 컴포넌트 취득
    /// </summary>
    private T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        // 해당 타입(Button, Text 등)을 넣어 주고 값이 있는지 없는지 체크
        // 값이 업다면 null
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;
        
        // 값이 있다면 취득한 배열의 idx를 찾아서 배포
        // ex) (int)Texts.ScoreText <- ScoreText의 인덱스
        return objects[idx] as T;
    }

    private int _score = 0;

    public void OnButtonClicked()
    {
        _score++;
    }
}

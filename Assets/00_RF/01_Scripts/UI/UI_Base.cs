using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public abstract class UI_Base : MonoBehaviour
{
    /// <summary>
    /// 어떤 타입인지 모르기 때문에 Text, Button 등의 최상위 부모를 사용
    /// 타입을 넣어 주면 해당 타입의 목록을 취득
    /// </summary>
    private Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, Object[]>();

    public abstract void Init();
    
    /// <summary>
    /// 리플렉션을 이용하여 Type에 enum을 넘겨 준다
    /// </summary>
    protected void Bind<T>(Type type) where T : UnityEngine.Object
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
    protected T Get<T>(int idx) where T : UnityEngine.Object
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
    
    protected Text GetText(int idx) => Get<Text>(idx);
    protected Button GetButton(int idx) => Get<Button>(idx);
    // TODO. 아직 Images enum 이 없음
    protected Image GetImage(int idx) => Get<Image>(idx);

    public static void AddUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        // UI_EventHandler을 찾거나 없다면 추가
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        // 이벤트 분기점
        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnPointerClickHandler -= action;
                evt.OnPointerClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
    }
}

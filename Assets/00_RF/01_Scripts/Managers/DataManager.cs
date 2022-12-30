using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Stat> StatsDict { get; private set; } = new Dictionary<int, Stat>();

    public void Init()
    {
        StatsDict = LoadJson<StatData, int, Stat>("StatData").MakeDict();
    }

    private Loader LoadJson<Loader, key, Value>(string path) where Loader : ILoader<key, Value>
    {
        // Json데이터이가 때문에 TextAsset
        // Json데이터를 통으로 로드
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        // 메모리에 로드
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
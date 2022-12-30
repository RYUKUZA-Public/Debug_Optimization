using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    // Json의 데이터 이름과 같이야함
    public int level;
    public int maxHp;
    public int attack;
    public int totalExp;
}

[Serializable]
public class StatData
{
    // Json의 데이터 이름과 같이야함
    public List<Stat> stats = new List<Stat>();
}

public class DataManager
{
    public Dictionary<int, Stat> StatsDict { get; private set; } = new Dictionary<int, Stat>();

    public void Init()
    {
        // Json데이터이가 때문에 TextAsset
        // Json데이터를 통으로 로드
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/StatData");
        // 메모리에 로드
        StatData data = JsonUtility.FromJson<StatData>(textAsset.text);
        
        // List -> Dictionary
        foreach (Stat stat in data.stats)
            StatsDict.Add(stat.level, stat);

    }
}

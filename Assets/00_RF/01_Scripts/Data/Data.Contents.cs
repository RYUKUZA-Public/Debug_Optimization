using System;
using System.Collections.Generic;

#region [Stat]
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
public class StatData : ILoader<int, Stat>
{
    // Json의 데이터 이름과 같이야함
    public List<Stat> stats = new List<Stat>();
    
    public Dictionary<int, Stat> MakeDict()
    {
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
    
        // List -> Dictionary
        foreach (Stat stat in stats)
            dict.Add(stat.level, stat);
        return dict;
    }
}
#endregion
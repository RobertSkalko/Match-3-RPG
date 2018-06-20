using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStat : NameValue
{

    public int minValue;
    public int maxValue;

    public int getPercent()
    {
        return minValue / maxValue * 100;
    }

    public ItemStat(string name, int value) : base(name, value)
    {

    }
}

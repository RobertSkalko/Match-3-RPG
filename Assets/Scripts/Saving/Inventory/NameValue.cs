
using System;

[Serializable]
public class NameValue 
{
    public string Name;
    public int Value;
    public string Desc;

    public NameValue(string Key, int Value)
    {
        this.Name = Key;
        this.Value = Value;
    }
}
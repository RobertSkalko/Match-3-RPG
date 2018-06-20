using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public string name;
    public int amount;
    public int maxAmount;
    public string picture;
    public string rarity;
    public string desc;
    public int level;
    public string type;

    public List<NameValue> ItemStats = new List<NameValue>();
    public List<NameValue> GemStats = new List<NameValue>();

    public bool isEmpty()
    {
        return name == null || name.Length < 1;
    }

    // Basic constructor without parameters
    public Item()
    {
   
    }
    // Basic constructor 
    public Item(string name)
    {
        amount = 1;
        maxAmount = 1;
        this.name = name;
        picture = "Unknown";
    }

    // Main constructor with basic parameters
    public Item(string itemName, string itemPicture)
    {
        amount = 1;
        maxAmount = 1;
        name = itemName;
        picture = itemPicture;
    }
}
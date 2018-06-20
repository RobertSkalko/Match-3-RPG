using System;
using System.Collections.Generic;

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

    public static Item clone(Item cloned)
    {
        Item item = new Item();

        item.amount = cloned.amount;
        item.desc = cloned.desc;
        item.GemStats = cloned.GemStats;
        item.ItemStats = cloned.ItemStats;
        item.level = cloned.level;
        item.name = cloned.name;
        item.type = cloned.type;
        item.picture = cloned.picture;
        item.rarity = cloned.rarity;
        item.maxAmount = cloned.maxAmount;

        return item;
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
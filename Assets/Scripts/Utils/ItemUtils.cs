﻿using System.Collections.Generic;
using UnityEngine;

public class ItemUtils
{
    public static string getNameOfItemSlot(string name)
    {
        string[] parts = name.Split(':');

        return parts[0];
    }

    public static int getNumberOfItemSlot(string name)
    {
        string[] parts = name.Split(':');

        return int.Parse(parts[1]);
    }

    public static void deleteItem(string name)
    {
        if (getSlotByID(name).itemInSlot == null || getSlotByID(name).itemInSlot.isEmpty())
        {
            Debug.Log("No item in slot, nothing to delete!");

            return;
        }

        Debug.Log("Deleted item: " + getSlotByID(name).itemInSlot.name);


        getSlotByID(name).itemInSlot = null;


    }

    public static Slot getSlotByID(string ID)
    {
        string bagName = getNameOfItemSlot(ID);
        int slotNumber = getNumberOfItemSlot(ID);

        return Save.file.player.inventory.getBag(bagName).Slots[slotNumber];
    }

    public static Item getItemByID(string ID)
    {
        return getSlotByID(ID).itemInSlot;
    }

    public static void sellItem(string ID)
    {
        Save.file.player.giveGold(getValue(getSlotByID(ID).itemInSlot));

        transferItemToRecentlySold(ID);
    }

    private static int getValue(Item item)
    {
        return item.amount * item.level;
    }

    private static void transferItemToRecentlySold(string ID)
    {
        Slot[] recentlySold = Save.file.player.inventory.getBag(BagNames.RecentlySold).Slots;

        List<Slot> list = new List<Slot>();
        list.AddRange(recentlySold);

        list.Insert(0, getSlotByID(ID));

        list.RemoveAt(list.Count);

        recentlySold = list.ToArray();

        Debug.Log("Item sent into recently sold");
    }
}
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopulateBags : MonoBehaviour
{
    private void Start()
    {
        Container Bank1 = Save.file.player.inventory.getBag(Names.Bags.Bank1);
        Container Bag1 = Save.file.player.inventory.getBag(Names.Bags.Bag1);

        for (int i = 0; i < Bank1.Slots.Length; i++)
        {
            string name = Bank1.name + ":" + i;

            GameObject newSlot = new GameObject(name);

            newSlot.AddComponent<aSlotData>();
        }

        for (int i = 0; i < Bag1.Slots.Length; i++)
        {
            string name = Bag1.name + ":" + i;

            GameObject newSlot = new GameObject(name);

            newSlot.AddComponent<aSlotData>();
        }
    }
}
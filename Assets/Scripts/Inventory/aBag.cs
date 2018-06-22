using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class aBag : MonoBehaviour
{
    private void Start()
    {
        Container Bank1 = Save.file.player.inventory.getBag(Names.Bags.Bank1);

        for (int i = 0; i < Bank1.Slots.Length; i++)
        {
            string name = Bank1.name + ":" + i;

            GameObject newSlot = Instantiate(Resources.Load("Prefabs\\ItemSlot")) as GameObject;

            newSlot.name = name;
        }
    }
}
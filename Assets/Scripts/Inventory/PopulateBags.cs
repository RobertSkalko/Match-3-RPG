using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopulateBags : MonoBehaviour
{
    private void Start()
    {
      
        for (int i = 0; i < Save.file.player.inventory.getBag("Bank1").Slots.Length; i++)
        {

            string name = Save.file.player.inventory.getBag("Bank1").name + ":" + i;

            GameObject newSlot = new GameObject(name);

            newSlot.AddComponent<AttachItem>();


        }
    }
    }
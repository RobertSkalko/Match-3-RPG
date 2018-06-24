using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class aContainer : MonoBehaviour
{
    [SerializeField]
    private string bagName;

    public string BagName

    {
        get { return bagName; }

        set { bagName = value; generateItemSlots(); }
    }

    public void changeBagTo(string newBagName)
    {
        BagName = newBagName;
    }

    private void Start()
    {
        if (this.transform.tag.Equals("Bank"))
        {
            bagName = "Bank1";
        }
        else if (this.transform.tag.Equals("Bag"))
        {
            bagName = "Bag1";
        }

        generateItemSlots();
    }

    private void generateItemSlots()
    {
        Container Bag = Save.file.player.inventory.getBag(bagName);

        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < Bag.Slots.Length; i++)
        {
            string name = Bag.name + ":" + i;

            GameObject newSlot = Instantiate(Prefabs.ItemSlot, this.gameObject.transform);

            newSlot.name = name;
        }
    }
}
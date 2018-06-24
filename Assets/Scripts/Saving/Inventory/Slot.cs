using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Slot
{
    public string type;

    [SerializeField]
    private Item ItemInSlot;

    public Item item
    {
        get { return ItemInSlot; }
        set { ItemInSlot = value; if (Game.saveIsLoaded()) Save.SaveTheGame(); }
    }

    public Slot(string type)
    {
        this.type = type;
    }

    public static Slot clone(Slot cloned)
    {
        Slot slot = new Slot("");

        slot.ItemInSlot = cloned.ItemInSlot;
        slot.type = cloned.type;

        return slot;
    }

    public void delete()
    {
        this.item = null;
    }

    public class Types
    {
        //public static string BankSlot = "BankSlot";
        public static string ItemSlot = "ItemSlot";

        //public static string BagSlot = "BagSlot";

        public class Gear
        {
            public static string Weapon = "Weapon";
            public static string Bodyarmor = "Bodyarmor";
            public static string Gloves = "Gloves";
            public static string Leggings = "Leggings";
            public static string Boots = "Boots";
            public static string Firstsocket = "Firstsocket";
            public static string Secondsocket = "Secondsocket";
            public static string Thirdsocket = "ThirdSocket";
        }
    }

    public void delete(GameObject obj)
    {
        item = null;

        updateImage(obj);
    }

    public void setItem(GameObject obj, Item item)
    {
        this.item = item;

        updateImage(obj);
    }

    public void updateImage(GameObject Obj)
    {
        if (Obj != null && Obj.GetComponent<RawImage>() != null)
        {
            if (ItemInSlot.isEmpty())
            {
                Obj.GetComponent<RawImage>().texture = null;
            }
            else
            {
                Texture2D img = Resources.Load<Texture2D>("unknown");

                Obj.GetComponent<RawImage>().texture = img;
            }
        }
    }
}
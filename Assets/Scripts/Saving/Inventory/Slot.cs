using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Slot  {

     

    public string type;

    [SerializeField]
    private Item ItemInSlot;
    public Item itemInSlot
    {
        get { return ItemInSlot; }
        set { ItemInSlot = value; if (Game.saveIsLoaded)Save.SaveTheGame();}
    }
     

    public Slot(string type)
    {
        this.type = type;
    }

    public class Types
    {

        public static string BankSlot = "BankSlot";
        public static string ItemSlot = "ItemSlot";
        public static string BagSlot = "BagSlot";

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


}

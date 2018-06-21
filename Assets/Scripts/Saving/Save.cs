using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Save
{
    public static Save file = new Save();

    public static List<Save> AllSaves = new List<Save>();

    public static string path = Application.persistentDataPath + "/Saves/";

    public static string getSavePath()
    {
        return path + file.name + ".save";
    }

    public static void SaveTheGame()
    {
        Debug.Log("Saving game");

        Directory.CreateDirectory(path);

        File.WriteAllText(getSavePath(), JsonUtility.ToJson(file, true));
    }

    public static void LoadTheGame(string name)
    {
        foreach (Save save in AllSaves)
        {
            if (save.name.Equals(name))
            {
                StartTheGame(save);
            }
        }
    }

    private static void StartTheGame(Save save)
    {
        file = save;

        Debug.Log("Loading Game: " + file.name);

        SceneManager.LoadScene("Bank");

        Game.saveIsLoaded = true;
    }

    public static void NewGame()
    {
        Save.file.player.inventory.getBag("Bank1").Slots[0].itemInSlot = new Item("First ITEM");
        Save.file.player.inventory.getBag("Bank1").Slots[1].itemInSlot = new Item("Second ITEM");
        Save.file.player.inventory.getBag("Bank1").Slots[2].itemInSlot = new Item("Third ITEM");

        SaveTheGame();

        Debug.Log("Game Started");

        StartTheGame(file);
    }

    //public int SaveFileNumber = 1;

    public string name = "";

    public GameSettings GameSetting = new GameSettings();
    public Player player = new Player();

    [Serializable]
    public class GameSettings
    {
        public bool HARDCORE = false;
        public int DIFFICULTY = 2;
        public int ITEM_RANDOMNESS = 2;
        public int DROPRATES = 2;
        public int PROGRESSION = 2;
        public int DEATH_PENALTY = 2;
        public int GOLD_DROPRATES = 2;

        public float getGoldDropsMult()
        {
            float[] rates = { 4, 2, 1, 0.5F, 0.2F };

            return rates[file.GameSetting.GOLD_DROPRATES];
        }

        public float getDropratesMult()
        {
            float[] rates = { 4, 2, 1, 0.5F, 0.2F };

            return rates[file.GameSetting.DROPRATES];
        }

        public float getDifficultyMult()
        {
            float[] rates = { 4, 2, 1, 0.5F, 0.2F };

            return rates[file.GameSetting.DIFFICULTY];
        }
    }

    [Serializable]
    public class Player
    {
        public int level = 1;
        public int exp = 0;
        public int gold = 0;

        public List<Character> Chars = new List<Character>(new Character[50]);
        public Inventory inventory = new Inventory();

        public void giveGold(int amount)
        {
            file.player.gold = +(int)(amount * file.GameSetting.getGoldDropsMult());
        }
    }

    [Serializable]
    public class Character
    {
        public Gear gear = new Gear();

        public string name = "";
        public bool inBattle;
        public bool selectedForBattle;
    }

    [Serializable]
    public class Inventory
    {
        public List<Container> Bags = new List<Container>();

        public Container getBag(string name)
        {
            foreach (Container bag in Save.file.player.inventory.Bags)
            {
                if (bag.name.Equals(name))
                {
                    return bag;
                }
            }
            throw new Exception("Bag not found!");
        }

        public Inventory()
        {
            Bags.Add(new Container(Names.Bags.Bank1, 72));
            Bags.Add(new Container(Names.Bags.Bag1, 20));
            Bags.Add(new Container(Names.Bags.Bag2, 20));
            Bags.Add(new Container(Names.Bags.Bag3, 20));
            Bags.Add(new Container(Names.Bags.Bag4, 20));
            Bags.Add(new Container(Names.Bags.RecentlySold, 50));
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;

public class NewGameButtons : MonoBehaviour {


    public void OnDifficultyChanged(int x)
    {

        Save.file.GameSetting.DIFFICULTY = x;

    }

    public void OnDroprateChanged(int x)
    {

        Save.file.GameSetting.DROPRATES = x;
    }

    public void OnHardcoreChanged(bool x)
    {

        Save.file.GameSetting.HARDCORE = x;

    }

    public void OnItemRandomnessChanged(int x)
    {

        Save.file.GameSetting.ITEM_RANDOMNESS = x;

    }
    public void OnGoldDropratesChanged(int x)
    {

        Save.file.GameSetting.GOLD_DROPRATES = x;

    }
    public void OnProgressionChanged(int x)
    {

        Save.file.GameSetting.PROGRESSION = x;

    }

    public void OnDeathPenaltyChanged(int x)
    {

        Save.file.GameSetting.DEATH_PENALTY = x;

    }
    public void OnNameEndEdit(string x)
    {

        if (IsValidFileName(x)){

            Save.file.name = x;
        }

    }

    public void StartGame()
    {
       
      if (IsValidFileName(Save.file.name))
        {
            Save.NewGame();
        }

      
    }
    bool IsValidFileName(string name)
    {

        if (name.Length > 0 && name.Length < 20)
        {
            if (!Regex.IsMatch(
                name, string.Format("[{0}]", Regex.Escape(new string(Path.GetInvalidFileNameChars())))))
            {
                bool uniqueName = true;

                foreach (Save save in Save.AllSaves)
                {          
                    if (save.name.Equals(name))
                    {
                        uniqueName = false;
                    }
                }
                if (uniqueName)
                {
                    return true;
                }

            }
        }

        Debug.Log("Name is invalid or not unique!");

        return false;
    }

}

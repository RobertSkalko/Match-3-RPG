using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadAllSaves : MonoBehaviour {

	// Use this for initialization
	void Start () {

        foreach (string file in Directory.GetFiles(Save.path))
        {

            Save.AllSaves.Add(JsonUtility.FromJson<Save>(File.ReadAllText(file)));

        }

    }	

}

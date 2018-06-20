using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadSaveFile : MonoBehaviour {

	// Use this for initialization
	void Start () {

        
        GameObject container = GameObject.Find("SaveFiles");   


        for (int i = 0; i < Save.AllSaves.Count; i++)
        {
            string name = Save.AllSaves[i].name;

            GameObject newFile = new GameObject(name);

            newFile.transform.SetParent(container.transform);

            //
            GameObject text = new GameObject("Text");
            text.AddComponent<Text>().text = newFile.name;
            text.layer = 5;

            text.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 44);
            text.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);

            text.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.GetComponent<Text>().color = Color.black;
            text.GetComponent<Text>().alignment = TextAnchor.LowerCenter;
            text.GetComponent<Text>().fontSize = 20;
            
            text.transform.SetParent(newFile.transform);
            //

            newFile.layer = 5;
                             
            newFile.AddComponent<Image>();
            newFile.AddComponent<Button>();
            newFile.tag = "Save File";

            newFile.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 80);
            newFile.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 350);

            newFile.transform.localScale = new Vector3(1f, 1f, 1f);


            Button butt = newFile.GetComponent<Button>();

            butt.onClick.AddListener(() => {
                Debug.Log("Loading Save: " + name);

                Save.LoadTheGame(name);

            });


        }


    }

    // Update is called once per frame
    void Update () {
		
	}
}

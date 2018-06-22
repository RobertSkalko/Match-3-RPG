using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class aBagChange : MonoBehaviour, IPointerClickHandler
{
    private aContainer bagScript;

    private void Start()
    {
        bagScript = GameObject.FindGameObjectWithTag("Bag").GetComponent<aContainer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Changing Bag to: " + name);

            bagScript.changeBagTo(name);
        }
    }
}
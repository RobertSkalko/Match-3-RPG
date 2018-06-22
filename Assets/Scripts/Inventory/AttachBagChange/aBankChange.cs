using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class aBankChange : MonoBehaviour, IPointerClickHandler
{
    private aContainer bankScript;

    private void Start()
    {
        bankScript = GameObject.FindGameObjectWithTag("Bank").GetComponent<aContainer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Changing Bank to: " + name);

            bankScript.changeBagTo(name);
        }
    }
}
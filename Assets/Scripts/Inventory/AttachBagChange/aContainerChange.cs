using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class aContainerChange : MonoBehaviour, IPointerClickHandler
{
    private aContainer bagScript;

    public string bagName;
    public GameObject container;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)

        {
            bagScript = container.GetComponent<aContainer>();

            Debug.Log("Changing Bag to: " + bagName);

            bagScript.changeBagTo(bagName);
        }
    }
}
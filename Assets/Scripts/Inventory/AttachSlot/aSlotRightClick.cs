using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class aSlotRightClick : MonoBehaviour, IPointerClickHandler
{
    private aSlotData SlotData;

    private void Start()
    {
        SlotData = this.gameObject.GetComponent<aSlotData>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Mouse Button Clicked on: " + name);
        }
    }
}
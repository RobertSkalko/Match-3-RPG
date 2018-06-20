using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopulateBags : MonoBehaviour
{
    private void Start()
    {
        GameObject container = GameObject.Find("Bankslots");

        for (int i = 0; i < Save.file.player.inventory.getBag("Bank1").Slots.Length; i++)
        {
            
            string name = Save.file.player.inventory.getBag("Bank1").name + ":" + i;

            GameObject newSlot = new GameObject(name);

            newSlot.layer = 5;

            newSlot.transform.SetParent(container.transform);

            newSlot.AddComponent<Image>();
            newSlot.AddComponent<EventTrigger>();

            newSlot.transform.localScale = new Vector3(1f, 1f, 1f);


            EventTrigger eventTrigger = newSlot.GetComponent<EventTrigger>();

            // onclick
            EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
            pointerDownEntry.eventID = EventTriggerType.PointerDown;
            pointerDownEntry.callback.AddListener((data) => { OnRightButtonDown((PointerEventData)data); });
            eventTrigger.triggers.Add(pointerDownEntry);
            // on hover
            EventTrigger.Entry pointerOver = new EventTrigger.Entry();
            pointerOver.eventID = EventTriggerType.PointerEnter;
            pointerOver.callback.AddListener((data) => { OnPointerOver((PointerEventData)data); });
            eventTrigger.triggers.Add(pointerOver);
            // on exit hover
            EventTrigger.Entry pointerOverExit = new EventTrigger.Entry();
            pointerOverExit.eventID = EventTriggerType.PointerEnter;
            pointerOverExit.callback.AddListener((data) => { OnPointerOverExit((PointerEventData)data); });
            eventTrigger.triggers.Add(pointerOverExit);
            // begin drag
            EventTrigger.Entry beginDrag = new EventTrigger.Entry();
            beginDrag.eventID = EventTriggerType.BeginDrag;
            beginDrag.callback.AddListener((data) => { OnBeginDrag((PointerEventData)data); });
            eventTrigger.triggers.Add(beginDrag);
            // end drag
            EventTrigger.Entry endDrag = new EventTrigger.Entry();
            endDrag.eventID = EventTriggerType.EndDrag;
            endDrag.callback.AddListener((data) => { OnEndDrag((PointerEventData)data); });
            eventTrigger.triggers.Add(endDrag);
        }
    }

    private void OnEndDrag(PointerEventData data)
    {
        string name = data.pointerCurrentRaycast.gameObject.name;

    }

    private void OnBeginDrag(PointerEventData data)
    {
        string name = data.pointerCurrentRaycast.gameObject.name;

    }

    private void OnPointerOverExit(PointerEventData data)
    {
        string name = data.pointerCurrentRaycast.gameObject.name;

    }

    private void OnPointerOver(PointerEventData data)
    {
        string name = data.pointerCurrentRaycast.gameObject.name;

    }

    private void OnRightButtonDown(PointerEventData data)
    {
        string name = data.pointerCurrentRaycast.gameObject.name;
        
        Debug.Log("You clicked an inventory slot: " + name);

        ItemUtils.deleteItem(name);

    }
}
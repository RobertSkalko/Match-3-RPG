using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttachItem : MonoBehaviour
{
    private bool initialized = false;
    public string Name;
    private GameObject Container;
    private GameObject Obj;

    public Slot Slot;

    public void updateFields()
    {
        Container = GameObject.Find("Bankslots");
        Obj = this.transform.gameObject;
        Name = Obj.name;
    }

    private void changeImage()
    {
        Debug.Log("Check if img can be changed");

        if (Obj != null && Obj.GetComponent<RawImage>() != null)
        {
            Debug.Log("Changing img");

            Texture2D img = Resources.Load<Texture2D>("unknown");

            Obj.GetComponent<RawImage>().texture = img;
        }
    }

    private void changeItem()
    {
        changeImage();
    }

    // Use this for initialization
    private void Start()
    {
        initialized = true;

        updateFields();

        Slot = ItemUtils.getSlotByID(Name);

        Obj.tag = "ItemSlot";

        Obj.layer = 5;

        Obj.transform.SetParent(Container.transform);

        Obj.AddComponent<RawImage>();

        Obj.AddComponent<EventTrigger>();

        Obj.transform.localScale = new Vector3(1f, 1f, 1f);

        EventTrigger eventTrigger = Obj.GetComponent<EventTrigger>();

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

    private void swapItemsOnDrag()
    {
        Debug.Log("Swapped items");

        Slot saved = Slot.clone(Game.FirstDraggedSlot);

        Game.FirstDraggedSlot.itemInSlot = Game.SecondDraggedSlot.itemInSlot;

        Game.SecondDraggedSlot.itemInSlot = saved.itemInSlot;
    }

    private void OnEndDrag(PointerEventData data)
    {
        GameObject obj = data.pointerCurrentRaycast.gameObject;

        if (obj.GetComponent<AttachItem>() != null)
        {
            Game.SecondDraggedSlot = obj.GetComponent<AttachItem>().Slot;

            swapItemsOnDrag();
        }
    }

    private void OnBeginDrag(PointerEventData data)
    {
        Game.FirstDraggedSlot = Slot;

        Game.SecondDraggedSlot = null;
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

        //Slot.itemInSlot = null;

        //ItemUtils.deleteItem(name);
    }
}
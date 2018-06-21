using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class aSlotData : MonoBehaviour
{
    public string Name;
    public GameObject Container;
    public GameObject CurrentObj;
    public bool hovered;
    public static GameObject canvas;
    public GameObject hoverBox;

    [SerializeField]
    private Slot slot;

    public Slot Slot
    {
        get { return slot; }
        set { slot = value; }
    }

    public void updateFields()
    {
        canvas = GameObject.Find("Canvas");
        CurrentObj = this.transform.gameObject;
        Name = CurrentObj.name;
        Container = GameObject.Find(ItemUtils.getNameOfItemSlot(Name));
    }

    // Use this for initialization
    private void Start()
    {
        updateFields();

        Slot = ItemUtils.getSlotByID(Name);

        CurrentObj.layer = 5;

        CurrentObj.transform.SetParent(Container.transform);

        CurrentObj.AddComponent<RawImage>();

        // this is REQUIRED for drag events to work!
        CurrentObj.AddComponent<EventTrigger>();

        CurrentObj.transform.localScale = new Vector3(1f, 1f, 1f);

        Slot.updateImage(CurrentObj);

        CurrentObj.AddComponent<aSlotDragEvents>();
        CurrentObj.AddComponent<aSlotOnHover>();
        CurrentObj.AddComponent<aSlotRightClick>();
    }

    /*
    public void OnPointerDown(PointerEventData data)
    {
        string name = data.pointerCurrentRaycast.gameObject.name;

        //Debug.Log("You clicked an inventory slot: " + name);
    }
    */
}
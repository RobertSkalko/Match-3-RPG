using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttachItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IEndDragHandler, IBeginDragHandler, IPointerExitHandler
{
    private bool sentinel = true;
    private bool initialized = false;
    public string Name;
    private GameObject Container;
    private GameObject CurrentObj;
    private GameObject canvas;
    public static bool hovered;

    public static GameObject hoverBox;

    [SerializeField]
    private Slot slot;

    public Slot Slot
    {
        get { return slot; }
        set { slot = value; }
    }

    public void updateFields()
    {
        Container = GameObject.Find("Bankslots");
        CurrentObj = this.transform.gameObject;
        Name = CurrentObj.name;
        canvas = GameObject.Find("Canvas");
    }

    // Use this for initialization
    private void Start()
    {
        initialized = true;

        updateFields();

        Slot = ItemUtils.getSlotByID(Name);

        CurrentObj.tag = "ItemSlot";

        CurrentObj.layer = 5;

        CurrentObj.transform.SetParent(Container.transform);

        CurrentObj.AddComponent<RawImage>();

        CurrentObj.AddComponent<EventTrigger>();

        CurrentObj.transform.localScale = new Vector3(1f, 1f, 1f);

        Slot.updateImage(CurrentObj);
    }

    private void swapItemsOnDrag(GameObject SecondDraggedObj)
    {
        Debug.Log("Swapped items");

        Slot saved = Slot.clone(Game.FirstDraggedSlot);

        Game.FirstDraggedSlot.setItem(CurrentObj, Game.SecondDraggedSlot.itemInSlot);

        Game.SecondDraggedSlot.setItem(SecondDraggedObj, saved.itemInSlot);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (slot == null || slot.itemInSlot.isEmpty())
        {
            return;
        }
        if (!hovered)
        {
            if (hoverBox != null)
            {
                Destroy(hoverBox);
            }

            hovered = true;

            hoverBox = new GameObject("hoverBox");
            GameObject hoverInfo = new GameObject("hoverInfo");

            // ignores raycast
            hoverBox.layer = 2;
            hoverInfo.layer = 2;

            hoverInfo.transform.parent = hoverBox.transform;
            hoverBox.transform.parent = canvas.transform;

            Text info = hoverInfo.AddComponent<Text>();
            info.text = slot.itemInSlot.name;
            info.font = Game.font;
            info.raycastTarget = false;

            Image img = hoverBox.AddComponent<Image>();
            img.color = Color.gray;
            img.raycastTarget = false;

            Vector2 pos = data.position;
            pos.x = pos.x + 50;

            hoverBox.transform.position = pos;
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        GameObject obj = data.pointerCurrentRaycast.gameObject;

        if (hoverBox != null) Destroy(hoverBox);

        AttachItem.hovered = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        string name = data.pointerCurrentRaycast.gameObject.name;

        Debug.Log("You clicked an inventory slot: " + name);
    }

    public void OnEndDrag(PointerEventData data)
    {
        GameObject SecondDraggedObj = data.pointerCurrentRaycast.gameObject;

        if (SecondDraggedObj.GetComponent<AttachItem>() != null)
        {
            Game.SecondDraggedSlot = SecondDraggedObj.GetComponent<AttachItem>().Slot;

            swapItemsOnDrag(SecondDraggedObj);
        }
    }

    public void OnBeginDrag(PointerEventData data)
    {
        Game.FirstDraggedSlot = Slot;

        Game.SecondDraggedSlot = null;
    }
}
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttachItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IEndDragHandler, IBeginDragHandler, IPointerExitHandler
{
    public string Name;
    private GameObject Container;
    private GameObject CurrentObj;
    public static bool hovered;
    private GameObject canvas;

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

            setPositionOfTooltip(data);
        }
    }

    private void setPositionOfTooltip(PointerEventData data)
    {
        Vector2 mousePos = data.position;
        int objWidth = (int)(CurrentObj.GetComponent<RectTransform>().rect.width * 1.5);

        bool isLeftSideOfScreen = true;

        if (mousePos.x > Screen.width / 2) { isLeftSideOfScreen = false; }

        int pushElementLeftOrRight = 0;

        if (isLeftSideOfScreen)
        {
            pushElementLeftOrRight = objWidth;
        }
        else
        {
            pushElementLeftOrRight = -objWidth;
        }

        Vector2 slotPos = CurrentObj.transform.position;
        slotPos.x = slotPos.x + pushElementLeftOrRight;

        hoverBox.transform.position = slotPos;
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

        //Debug.Log("You clicked an inventory slot: " + name);
    }

    public void OnEndDrag(PointerEventData data)
    {
        GameObject SecondDraggedObj = data.pointerCurrentRaycast.gameObject;

        if (SecondDraggedObj.GetComponent<AttachItem>() != null)
        {
            Game.SecondDraggedSlot = SecondDraggedObj.GetComponent<AttachItem>().Slot;

            swapItemsOnDrag(CurrentObj, SecondDraggedObj);
        }
    }

    public void OnBeginDrag(PointerEventData data)
    {
        Game.FirstDraggedSlot = Slot;

        Game.SecondDraggedSlot = null;
    }

    private void swapItemsOnDrag(GameObject beginDrag, GameObject endDrag)
    {
        Debug.Log("Swapped items");

        Slot saved = Slot.clone(Game.FirstDraggedSlot);

        Game.FirstDraggedSlot.setItem(beginDrag, Game.SecondDraggedSlot.itemInSlot);

        Game.SecondDraggedSlot.setItem(endDrag, saved.itemInSlot);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class aSlotDragEvents : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    private aSlotData SlotData;

    public static GameObject draggedItem;                                      // Item that is dragged now
    public static GameObject icon;                                                  // Icon of dragged item
    public static DragAndDropCell sourceCell;                                       // From this cell dragged item is
    public static GameObject canvas;

    private void Start()
    {
        SlotData = this.gameObject.GetComponent<aSlotData>();
        canvas = GameObject.Find("Canvas");
    }

    public void OnEndDrag(PointerEventData data)
    {
        GameObject SecondDraggedObj = data.pointerCurrentRaycast.gameObject;

        if (SecondDraggedObj.GetComponent<aSlotData>() != null)
        {
            Game.SecondDraggedSlot = SecondDraggedObj.GetComponent<aSlotData>().Slot;

            swapItemsOnDrag(SlotData.CurrentObj, SecondDraggedObj);
        }

        ResetConditions();
    }

    public void OnBeginDrag(PointerEventData data)
    {
        Game.FirstDraggedSlot = SlotData.Slot;

        Game.SecondDraggedSlot = null;

        summonDragIcon();
    }

    private void summonDragIcon()
    {
        GameObject sourceCell = this.gameObject;                                             // Remember source cell
        draggedItem = this.gameObject;                                                     // Set as dragged item
                                                                                           // Create item's icon
        icon = new GameObject();
        icon.transform.SetParent(canvas.transform);
        icon.name = "Icon";
        RawImage myImage = sourceCell.GetComponent<RawImage>();                         // Disable icon's raycast for correct drop handling
        RawImage iconImage = icon.AddComponent<RawImage>();
        iconImage.raycastTarget = false;
        iconImage.texture = myImage.texture;
        RectTransform iconRect = icon.GetComponent<RectTransform>();
        // Set icon's dimensions
        RectTransform myRect = GetComponent<RectTransform>();
        iconRect.pivot = new Vector2(0.5f, 0.5f);
        iconRect.anchorMin = new Vector2(0.5f, 0.5f);
        iconRect.anchorMax = new Vector2(0.5f, 0.5f);
        iconRect.sizeDelta = new Vector2(myRect.rect.width, myRect.rect.height);
    }

    private void ResetConditions()
    {
        if (icon != null)
        {
            Destroy(icon);                                                          // Destroy icon on item drop
        }

        draggedItem = null;
        icon = null;
        sourceCell = null;
    }

    private void swapItemsOnDrag(GameObject beginDrag, GameObject endDrag)
    {
        Debug.Log("Swapped items");

        Slot saved = Slot.clone(Game.FirstDraggedSlot);

        Game.FirstDraggedSlot.setItem(beginDrag, Game.SecondDraggedSlot.itemInSlot);

        Game.SecondDraggedSlot.setItem(endDrag, saved.itemInSlot);
    }

    public void OnDrag(PointerEventData data)
    {
        if (icon != null)
        {
            icon.transform.position = Input.mousePosition;                          // Item's icon follows to cursor in screen pixels
        }
    }
}
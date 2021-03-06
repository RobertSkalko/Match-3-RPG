﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class aSlotDragEvents : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    private aSlotData SlotData;

    public static GameObject dragIcon;
    public static GameObject canvas;

    private void Start()
    {
        SlotData = this.gameObject.GetComponent<aSlotData>();
        canvas = GameObject.Find("Canvas");
    }

    public void OnEndDrag(PointerEventData data)
    {
        // the frame object is the one who picks up the raycast so you have to get the parent..
        GameObject SecondDraggedObj = data.pointerCurrentRaycast.gameObject.transform.parent.gameObject;

        if (SecondDraggedObj.GetComponent<aSlotData>() != null)
        {
            Game.SecondDraggedSlot = SecondDraggedObj.GetComponent<aSlotData>().Slot;

            swapItemsOnDrag(SlotData.CurrentObj, SecondDraggedObj);
        }

        destroyDragIcon();
    }

    public void OnBeginDrag(PointerEventData data)
    {
        if (SlotData.tooltip != null) Destroy(SlotData.tooltip);

        Game.FirstDraggedSlot = SlotData.Slot;

        Game.SecondDraggedSlot = null;

        if (!SlotData.Slot.item.isEmpty())
        {
            summonDragIcon();
        }
    }

    private void summonDragIcon()
    {
        GameObject sourceCell = this.gameObject;

        dragIcon = new GameObject();
        dragIcon.transform.SetParent(canvas.transform, false);
        dragIcon.name = "Icon";
        RawImage itemImg = sourceCell.GetComponent<RawImage>();
        RawImage dragIconImg = dragIcon.AddComponent<RawImage>();
        dragIconImg.raycastTarget = false;
        dragIconImg.texture = itemImg.texture;
        RectTransform iconRect = dragIcon.GetComponent<RectTransform>();
        // Set icon's dimensions
        RectTransform myRect = GetComponent<RectTransform>();
        iconRect.pivot = new Vector2(0.5f, 0.5f);
        iconRect.anchorMin = new Vector2(0.5f, 0.5f);
        iconRect.anchorMax = new Vector2(0.5f, 0.5f);
        iconRect.sizeDelta = new Vector2(myRect.rect.width, myRect.rect.height);
    }

    private void destroyDragIcon()
    {
        if (dragIcon != null)
        {
            Destroy(dragIcon);
        }
    }

    private void swapItemsOnDrag(GameObject beginDrag, GameObject endDrag)
    {
        Debug.Log("Swapped items");

        Slot saved = Slot.clone(Game.FirstDraggedSlot);

        Game.FirstDraggedSlot.setItem(beginDrag, Game.SecondDraggedSlot.item);

        Game.SecondDraggedSlot.setItem(endDrag, saved.item);
    }

    public void OnDrag(PointerEventData data)
    {
        if (dragIcon != null)
        {
            dragIcon.transform.position = Input.mousePosition;
        }
    }
}
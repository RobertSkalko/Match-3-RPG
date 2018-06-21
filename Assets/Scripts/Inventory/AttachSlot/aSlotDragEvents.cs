using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class aSlotDragEvents : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    private aSlotData SlotData;

    private void Start()
    {
        SlotData = this.gameObject.GetComponent<aSlotData>();
    }

    public void OnEndDrag(PointerEventData data)
    {
        GameObject SecondDraggedObj = data.pointerCurrentRaycast.gameObject;

        if (SecondDraggedObj.GetComponent<aSlotData>() != null)
        {
            Game.SecondDraggedSlot = SecondDraggedObj.GetComponent<aSlotData>().Slot;

            swapItemsOnDrag(SlotData.CurrentObj, SecondDraggedObj);
        }
    }

    public void OnBeginDrag(PointerEventData data)
    {
        Game.FirstDraggedSlot = SlotData.Slot;

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
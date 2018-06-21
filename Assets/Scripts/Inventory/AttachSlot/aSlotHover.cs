using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class aSlotOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private aSlotData SlotData;

    private void Start()
    {
        SlotData = this.gameObject.GetComponent<aSlotData>();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (SlotData.Slot == null || SlotData.Slot.itemInSlot.isEmpty())
        {
            return;
        }
        if (!SlotData.hovered)
        {
            if (SlotData.hoverBox != null)
            {
                Destroy(SlotData.hoverBox);
            }

            SlotData.hovered = true;

            SlotData.hoverBox = new GameObject("hoverBox");
            GameObject hoverInfo = new GameObject("hoverInfo");

            // ignores raycast
            SlotData.hoverBox.layer = 2;
            hoverInfo.layer = 2;

            hoverInfo.transform.parent = SlotData.hoverBox.transform;
            SlotData.hoverBox.transform.parent = aSlotData.canvas.transform;

            Text info = hoverInfo.AddComponent<Text>();
            info.text = SlotData.Slot.itemInSlot.name;
            info.font = Game.font;
            info.raycastTarget = false;

            Image img = SlotData.hoverBox.AddComponent<Image>();
            img.color = Color.gray;
            img.raycastTarget = false;

            setPositionOfTooltip(data);
        }
    }

    private void setPositionOfTooltip(PointerEventData data)
    {
        Vector2 mousePos = data.position;
        int objWidth = (int)(SlotData.CurrentObj.GetComponent<RectTransform>().rect.width * 1.5);

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

        Vector2 slotPos = SlotData.CurrentObj.transform.position;
        slotPos.x = slotPos.x + pushElementLeftOrRight;

        SlotData.hoverBox.transform.position = slotPos;
    }

    public void OnPointerExit(PointerEventData data)
    {
        GameObject obj = data.pointerCurrentRaycast.gameObject;

        if (SlotData.hoverBox != null) Destroy(SlotData.hoverBox);

        SlotData.hovered = false;
    }
}
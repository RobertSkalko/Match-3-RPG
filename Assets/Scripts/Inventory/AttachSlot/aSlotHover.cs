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
            if (SlotData.tooltip != null)
            {
                Destroy(SlotData.tooltip);
            }

            SlotData.hovered = true;

            SlotData.tooltip = Instantiate(Prefabs.ItemTooltip);

            SlotData.tooltip.transform.SetParent(aSlotData.canvas.transform);

            setFields(SlotData.tooltip);

            setPositionOfTooltip(data);
        }
    }

    private void setFields(GameObject tooltip)
    {
        GameObject viewport = tooltip.transform.Find("Viewport").gameObject;
        GameObject contents = viewport.transform.Find("Contents").gameObject;
        GameObject name = contents.transform.Find("Name").gameObject;
        GameObject stats = contents.transform.Find("Stats").gameObject;
        GameObject desc = contents.transform.Find("Desc").gameObject;

        name.GetComponent<Text>().text = SlotData.Slot.itemInSlot.name;
    }

    private void setPositionOfTooltip(PointerEventData data)
    {
        Vector2 mousePos = data.position;
        int objWidth = (int)(SlotData.CurrentObj.GetComponent<RectTransform>().rect.width / 2 + SlotData.tooltip.GetComponent<RectTransform>().rect.width / 2);

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

        SlotData.tooltip.transform.position = slotPos;
    }

    public void OnPointerExit(PointerEventData data)
    {
        GameObject obj = data.pointerCurrentRaycast.gameObject;

        if (SlotData.tooltip != null) Destroy(SlotData.tooltip);

        SlotData.hovered = false;
    }
}
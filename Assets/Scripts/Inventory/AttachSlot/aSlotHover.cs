using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class aSlotOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private aSlotData SlotData;
    private GameObject tooltipObj;

    private void Start()
    {
        SlotData = this.gameObject.GetComponent<aSlotData>();
        tooltipObj = SlotData.tooltip;
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

            SlotData.tooltip.transform.SetParent(aSlotData.canvas.transform, false);

            setFields(SlotData.tooltip);

            setXPositionOfTooltip(data);

            setYPositionOfTooltip(data);
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

    private void setXPositionOfTooltip(PointerEventData data)
    {
        Vector2 mousePos = data.position;

        int slotWidth = (int)(gameObject.GetComponent<RectTransform>().rect.width / 2 * gameObject.GetComponent<RectTransform>().lossyScale.x);
        int tooltipWidth = (int)(SlotData.tooltip.GetComponent<RectTransform>().rect.width / 2 * SlotData.tooltip.GetComponent<RectTransform>().lossyScale.x);
        int offsetWidth = slotWidth + tooltipWidth;

        bool isLeftSideOfScreen = true;

        if (mousePos.x > Screen.width / 2) { isLeftSideOfScreen = false; }

        int pushElementLeftOrRight = 0;

        if (isLeftSideOfScreen)
        {
            pushElementLeftOrRight = offsetWidth;
        }
        else
        {
            pushElementLeftOrRight = -offsetWidth;
        }

        Vector2 tooltipPos = gameObject.transform.position;
        tooltipPos.x = tooltipPos.x + pushElementLeftOrRight;

        SlotData.tooltip.transform.position = tooltipPos;
    }

    private void setYPositionOfTooltip(PointerEventData data)
    {
        int tooltipHeight = (int)(SlotData.tooltip.GetComponent<RectTransform>().rect.height * SlotData.tooltip.GetComponent<RectTransform>().lossyScale.y);

        if (data.position.y > Screen.height / 2)
        {
            if (SlotData.CurrentObj.transform.position.y + tooltipHeight / 2 > Screen.height)
            {
                SlotData.tooltip.transform.position = new Vector2(SlotData.tooltip.transform.position.x, Screen.height - tooltipHeight / 2);
            }
        }
        else
        {
            if (SlotData.CurrentObj.transform.position.y - tooltipHeight / 2 < 0)
            {
                SlotData.tooltip.transform.position = new Vector2(SlotData.tooltip.transform.position.x, 0 + tooltipHeight / 2);
            }
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        GameObject obj = data.pointerCurrentRaycast.gameObject;

        if (!obj.Equals(SlotData.tooltip))
        {
            if (SlotData.tooltip != null) Destroy(SlotData.tooltip);

            SlotData.hovered = false;
        }
    }
}
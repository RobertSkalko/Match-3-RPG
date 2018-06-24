using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class aTooltip : MonoBehaviour
{
    public aSlotData SlotData;

    private GameObject viewportObj;
    private GameObject contentsObj;
    private GameObject nameObj;
    private GameObject statsObj;
    private GameObject descObj;

    private void Start()
    {
        setFields();

        setXPositionOfTooltip();

        setYPositionOfTooltip();
    }

    private void setFields()
    {
        viewportObj = this.gameObject.transform.Find("Viewport").gameObject;
        contentsObj = viewportObj.transform.Find("Contents").gameObject;
        nameObj = contentsObj.transform.Find("Name").gameObject;
        statsObj = contentsObj.transform.Find("Stats").gameObject;
        descObj = contentsObj.transform.Find("Desc").gameObject;

        nameObj.GetComponent<Text>().text = SlotData.Slot.item.name;
    }

    private void setXPositionOfTooltip()
    {
        int slotWidth = (int)(SlotData.CurrentObj.GetComponent<RectTransform>().rect.width / 2 * SlotData.CurrentObj.GetComponent<RectTransform>().lossyScale.x);
        int tooltipWidth = (int)(SlotData.tooltip.GetComponent<RectTransform>().rect.width / 2 * SlotData.tooltip.GetComponent<RectTransform>().lossyScale.x);
        int offsetWidth = slotWidth + tooltipWidth;

        bool isLeftSideOfScreen = true;

        if (Input.mousePosition.x > Screen.width / 2) { isLeftSideOfScreen = false; }

        int pushElementLeftOrRight = 0;

        if (isLeftSideOfScreen)
        {
            pushElementLeftOrRight = offsetWidth;
        }
        else
        {
            pushElementLeftOrRight = -offsetWidth;
        }

        Vector2 tooltipPos = SlotData.CurrentObj.transform.position;
        tooltipPos.x = tooltipPos.x + pushElementLeftOrRight;

        SlotData.tooltip.transform.position = tooltipPos;
    }

    private void setYPositionOfTooltip()
    {
        int tooltipHeight = (int)(SlotData.tooltip.GetComponent<RectTransform>().rect.height * SlotData.tooltip.GetComponent<RectTransform>().lossyScale.y);

        if (Input.mousePosition.y > Screen.height / 2)
        {
            if (SlotData.CurrentObj.transform.position.y + tooltipHeight / 2 > Screen.height)
            {
                SlotData.tooltip.transform.position = new Vector2(SlotData.tooltip.transform.position.x, Screen.height - Screen.height / 50 - tooltipHeight / 2);
            }
        }
        else
        {
            if (SlotData.CurrentObj.transform.position.y - tooltipHeight / 2 < 0)
            {
                SlotData.tooltip.transform.position = new Vector2(SlotData.tooltip.transform.position.x, Screen.height / 50 + tooltipHeight / 2);
            }
        }
    }
}
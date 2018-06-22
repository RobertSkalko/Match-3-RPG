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

            GameObject text = createText();
            SlotData.tooltip = createTooltip(text);

            // setting parents
            text.transform.SetParent(SlotData.tooltip.transform);
            SlotData.tooltip.transform.SetParent(aSlotData.canvas.transform);
            //

            setPositionOfTooltip(data);
        }
    }

    private GameObject createTooltip(GameObject Text)
    {
        GameObject tooltip = new GameObject("Tooltip");

        RectTransform rect = tooltip.AddComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 150);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 350);

        // ignores raycast
        //tooltip.layer = 2;

        tooltip.AddComponent<Mask>();
        ScrollRect scroll = tooltip.AddComponent<ScrollRect>();
        scroll.movementType = ScrollRect.MovementType.Unrestricted;
        scroll.inertia = false;
        scroll.horizontal = false;
        scroll.vertical = true;
        scroll.content = Text.GetComponent<RectTransform>();

        Image img = tooltip.AddComponent<Image>();
        img.color = Color.gray;
        img.raycastTarget = false;

        return tooltip;
    }

    private GameObject createText()
    {
        string teststring = "\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING\nTESTING";

        GameObject text = new GameObject("Text");

        Text info = text.AddComponent<Text>();
        info.text = SlotData.Slot.itemInSlot.name + teststring;

        info.font = Game.font;
        info.raycastTarget = false;
        info.horizontalOverflow = HorizontalWrapMode.Wrap;
        info.verticalOverflow = VerticalWrapMode.Overflow;

        return text;
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
using UnityEngine;
using UnityEngine.EventSystems;

public class aSlotHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private aSlotData SlotData;

    private void Start()
    {
        SlotData = this.gameObject.GetComponent<aSlotData>();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (SlotData.Slot == null || SlotData.Slot.item.isEmpty())
        {
            return;
        }

        if (SlotData.tooltip != null)
        {
            Destroy(SlotData.tooltip);
        }

        summonTooltip();
    }

    private void summonTooltip()
    {
        SlotData.tooltip = Instantiate(Prefabs.ItemTooltip, GameObject.Find("Canvas").transform, false);
        SlotData.tooltip.GetComponent<aTooltip>().SlotData = SlotData;
    }

    public void OnPointerExit(PointerEventData data)
    {
        GameObject obj = data.pointerCurrentRaycast.gameObject;

        if (!obj.Equals(SlotData.tooltip))
        {
            if (SlotData.tooltip != null) Destroy(SlotData.tooltip);
        }
    }
}
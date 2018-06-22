using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class aSlotData : MonoBehaviour
{
    public GameObject Container;
    public GameObject CurrentObj;
    public bool hovered;
    public static GameObject canvas;
    public GameObject tooltip;

    [SerializeField]
    private Slot slot;

    public Slot Slot
    {
        get { return slot; }
        set { slot = value; }
    }

    public void initializeFields()
    {
        canvas = GameObject.Find("Canvas");
        CurrentObj = this.transform.gameObject;
        Container = getContainer();
    }

    private GameObject getContainer()
    {
        if (name.Contains("Bank"))
        {
            return GameObject.FindGameObjectWithTag("Bank");
        }
        else if (name.Contains("Bag"))
        {
            return GameObject.FindGameObjectWithTag("Bag");
        }
        return null;
    }

    // Use this for initialization
    private void Start()
    {
        initializeFields();

        Slot = ItemUtils.getSlotByID(name);

        CurrentObj.transform.SetParent(Container.transform);

        CurrentObj.transform.localScale = new Vector3(1f, 1f, 1f);

        Slot.updateImage(CurrentObj);

        CurrentObj.AddComponent<aSlotDragEvents>();
        CurrentObj.AddComponent<aSlotOnHover>();
        CurrentObj.AddComponent<aSlotRightClick>();
    }
}
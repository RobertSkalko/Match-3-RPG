using UnityEngine;

public class aSlotData : MonoBehaviour
{
    public GameObject CurrentObj;
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
    }

    private void Start()
    {
        initializeFields();

        Slot = ItemUtils.getSlotByID(name);

        Slot.updateImage(CurrentObj);
    }
}
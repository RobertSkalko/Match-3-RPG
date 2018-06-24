using System;

[Serializable]
public class Container
{
    public Slot[] Slots;
    private int maxSize = 5000;
    public string name;

    public Container(string name, int size)
    {
        this.name = name;

        if (size > maxSize)
        {
            size = maxSize;
        }

        Slots = new Slot[size];

        for (var i = 0; i < Slots.Length; i++)
        {
            Slots[i] = new Slot(Slot.Types.ItemSlot);
            Slots[i].item = null;
        }
    }
}
using System;
using System.Collections.Generic;

[Serializable]
public class Gear
{
    public Slot Weapon = new Slot(Slot.Types.Gear.Weapon);
    public Slot Gloves = new Slot(Slot.Types.Gear.Gloves);
    public Slot BodyArmor = new Slot(Slot.Types.Gear.Bodyarmor);
    public Slot Leggings = new Slot(Slot.Types.Gear.Leggings);
    public Slot Boots = new Slot(Slot.Types.Gear.Boots);
    public Slot FirstSocket = new Slot(Slot.Types.Gear.Firstsocket);
    public Slot SecondSocket = new Slot(Slot.Types.Gear.Secondsocket);
    public Slot ThirdSocket = new Slot(Slot.Types.Gear.Thirdsocket);

    public int getStat(string StatName)
    {
        List<Slot> allSlots = new List<Slot>
        {
            Gloves,
            BodyArmor,
            Boots,
            Weapon,
            FirstSocket,
            SecondSocket,
            ThirdSocket
        };

        List<NameValue> allStats = new List<NameValue>();

        for (int i = 0; i < allSlots.Count; i++)
        {
            foreach (NameValue stat in allSlots[i].itemInSlot.ItemStats)
            {
                if (stat == null) continue;
                allStats.Add(stat);
            }

            foreach (NameValue stat in allSlots[i].itemInSlot.GemStats)
            {
                if (stat == null) continue;
                allStats.Add(stat);
            }
        }

        int statValue = 0;

        foreach (NameValue stat in allStats)
        {
            if (stat == null) continue;
            if (stat.Name == StatName)
            {
                statValue += stat.Value;
            }
        }

        return statValue;
    }
}
using UnityEngine;

public class Game
{
    public static bool saveIsLoaded = false;

    public static Slot FirstDraggedSlot;
    public static Slot SecondDraggedSlot;

    public static Font font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
}
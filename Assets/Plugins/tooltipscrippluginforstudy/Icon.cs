using UnityEngine;
using System.Collections;

public enum IconType
{
    GameObject = 1,
    Skill = 2,
    Item = 3
}

public class Icon
{
    IconType icon_type;
    Texture2D icon_base_container = null;
    Texture2D icon_container_object = null;
    Rect dimensions;
    string icon_name;

    bool activeIcon = false;

    RaycastHit target;

    public Icon(IconType type, Texture2D icon, Rect dimens, string name)
    {
        icon_type = type;

        if (icon != null)
            icon_container_object = icon;

        dimensions = dimens;
        icon_name = name;
    }

    public Icon(IconType type, Texture2D icon_base, Texture2D icon, Rect dimens, string name)
    {
        icon_type = type;

        if (icon_base != null)
            icon_base_container = icon_base;

        if (icon != null)
            icon_container_object = icon;

        dimensions = dimens;
        icon_name = name;
    }

    public bool ActiveHover()
    {
        activeIcon = this.dimensions.Contains(Event.current.mousePosition);
        return activeIcon;
    }

    public bool ActiveHover(Ray ray)
    {
        return Physics.Raycast(ray, out target);
    }

    public bool isActiveToggled()
    {
        return activeIcon;
    }

    public void DrawIcon()
    {
        if (!icon_type.Equals(IconType.GameObject))
        {
            if (icon_base_container != null)
                GUI.DrawTexture(dimensions, icon_base_container);

            if (icon_container_object != null)
                GUI.DrawTexture(dimensions, icon_container_object);
        }
    }

    public RaycastHit getTargetData()
    {
        return target;
    }

    public Rect getIconDimensions()
    {
        return dimensions;
    }

    public IconType getIconType()
    {
        return icon_type;
    }

    public string Name
    {
        get { return icon_name; }
        set { icon_name = value; }
    }

    public override string ToString()
    {
        return icon_name;
    }
}

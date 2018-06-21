using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour
{
    private ToolTipHandler tooltip_handler;
    private ArrayList icons = new ArrayList();
    private bool Generated = false;
    private bool IconPriority_GUI = false;
    private bool IconPriority_Transform = false;

    private Transform previouslyHovered = null;

    public Texture2D IconBase;
    public Texture2D TooltipBGTexture;
    public Font TooltipFont;
    public bool TooltipMouseFollow = false;
    public int[] TooltipFontSizes = new int[3];
    public Color[] colors;

    private void Start()
    {
        tooltip_handler = new ToolTipHandler(TooltipBGTexture, TooltipFont, TooltipFontSizes, colors);

        // Create Initial Icons here
        Icon main_hand = new Icon(IconType.Item, IconBase,
            (Texture2D)Resources.Load("ObjectDetails/Items/staffoflight"),
            new Rect(300, 260, 32, 32), "Staff of Light");

        Icon off_hand = new Icon(IconType.Item, IconBase,
            (Texture2D)Resources.Load("ObjectDetails/Items/swordoftruth"),
            new Rect(300, 300, 32, 32), "Sword of Truth");

        Icon chest_piece = new Icon(IconType.Item, IconBase,
            (Texture2D)Resources.Load("ObjectDetails/Items/robeofpower"),
            new Rect(300, 340, 32, 32), "Robe of Power");

        Icon skill_one = new Icon(IconType.Skill, IconBase,
            (Texture2D)Resources.Load("ObjectDetails/Skills/healoflight"),
            new Rect(300, 380, 32, 32), "Heal of Light");

        // Only need one Icon with IconType.GameObject
        Icon objecthandler_tooltip = new Icon(IconType.GameObject, null, new Rect(), "");

        // Add icons to the icons list
        icons.Add(main_hand);
        icons.Add(off_hand);
        icons.Add(chest_piece);
        icons.Add(skill_one);
        icons.Add(objecthandler_tooltip);
    }

    private void OnGUI()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        foreach (Icon icon in icons)
        {
            icon.DrawIcon();

            // active mouse hover
            if (icon.Name != "" && (icon.getIconType() != IconType.GameObject && icon.ActiveHover()))
            {
                // if hovering over a GameObject and now hovering over a GUI Icon
                if (IconPriority_Transform && !IconPriority_GUI)
                {
                    IconPriority_GUI = true;
                    IconPriority_Transform = false;
                    previouslyHovered = null;
                    Generated = false;
                }

                if (!Generated)
                {
                    tooltip_handler.CreateNewTooltip(icon.getIconDimensions(), icon.getIconType(), icon.ToString());
                    IconPriority_GUI = true;
                    Generated = true;
                }

                if (icon.ToString() != tooltip_handler.ActiveTooltip())
                {
                    Generated = false;
                }

                tooltip_handler.RenderTooltipGUI();
            }
            else if (icon.getIconType() == IconType.GameObject && icon.ActiveHover(ray))
            {
                if (!IconPriority_GUI && icon.getTargetData().transform.name.Contains("Object"))
                {
                    ObjectHandler obj_handler = icon.getTargetData().transform.GetComponent<ObjectHandler>();
                    icon.Name = obj_handler.Object_Name;

                    if (!Generated)
                    {
                        if (!TooltipMouseFollow)
                            tooltip_handler.CreateNewTooltip(new Rect(Camera.main.WorldToScreenPoint(icon.getTargetData().transform.position + (icon.getTargetData().transform.localScale / 2)).x, Screen.height - Camera.main.WorldToScreenPoint(icon.getTargetData().transform.position).y, icon.getIconDimensions().width, icon.getIconDimensions().height), icon.getIconType(), icon.ToString());
                        else
                            tooltip_handler.CreateNewTooltip(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, icon.getIconDimensions().width, icon.getIconDimensions().height), icon.getIconType(), icon.ToString());

                        previouslyHovered = obj_handler.transform;

                        IconPriority_Transform = true;
                        Generated = true;
                    }
                    else if (Generated && (previouslyHovered == null || !previouslyHovered.Equals(obj_handler.transform)))
                        Generated = false;
                    else if (TooltipMouseFollow && previouslyHovered.Equals(obj_handler.transform))
                    {
                        tooltip_handler.setActiveDetailsDimensionPosition(15 + Input.mousePosition.x, Screen.height - Input.mousePosition.y);
                    }

                    tooltip_handler.RenderTooltipGUI();
                }
            }

            if (icon.ToString() == tooltip_handler.ActiveTooltip())
            {
                if (!icon.ActiveHover())
                    IconPriority_GUI = false;
            }
        }
    }
}
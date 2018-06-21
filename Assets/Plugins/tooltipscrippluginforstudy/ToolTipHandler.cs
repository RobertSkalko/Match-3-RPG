using UnityEngine;
using System.Collections.Generic;
using System.IO;

struct TooltipDetails
{
    private string name;
    private Color name_color;
    private int id;
    private string[] stats;
    private string[] details;
    private Color[] stats_colors;
    private Color[] details_colors;
    private Rect dimensions;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public Color NameColor
    {
        get { return name_color; }
        set { name_color = value; }
    }

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public string[] StatisticalDetails
    {
        get { return stats; }
        set { stats = value; }
    }

    public string[] DescriptiveDetails
    {
        get { return details; }
        set { details = value; }
    }

    public Color[] StatisticalColors
    {
        get { return stats_colors; }
        set { stats_colors = value; }
    }

    public Color[] DescriptiveColors
    {
        get { return details_colors; }
        set { details_colors = value; }
    }

    public Rect Dimensions
    {
        get { return dimensions; }
        set { dimensions = value; }
    }
}

public class ToolTipHandler
{
    Tooltip active_tooltip;
    IconType previousIconType;

    TextAsset objectInfoFile;
    Texture2D tooltip_background;
    Font tooltipFont = new Font();
    int[] font_Sizes = new int[3];
    Color[] ColorValues = { Color.gray, Color.white, Color.green, Color.cyan, Color.magenta };

    public ToolTipHandler(Texture2D tooltip_bg, Font tooltip_font, int[] fontSizes, Color[] colors)
    {
        if (tooltip_font != null)
            tooltipFont = tooltip_font;

        font_Sizes = fontSizes;

        if (colors.Length > 0)
            ColorValues = colors;

        if (tooltip_bg == null)
        {
            Texture2D temp = new Texture2D(1, 1);
            temp.SetPixel(0, 0, Color.black);
            temp.Apply();

            tooltip_background = temp;
        }
        else
            tooltip_background = tooltip_bg;
    }

    // used to retrieve details from a specific file with the associated type and name
    TooltipDetails RetrieveDetails(IconType type, string name)
    {
        TooltipDetails details = new TooltipDetails();

        details.Name = name;

        Stream stream;
        StreamReader reader;

        if (type.Equals(IconType.Item) && previousIconType != IconType.Item)
        {
            objectInfoFile = (TextAsset)Resources.Load("ObjectDetails/item_info");
            previousIconType = IconType.Item;
        }
        else if (type.Equals(IconType.Skill) && previousIconType != IconType.Skill)
        {
            objectInfoFile = (TextAsset)Resources.Load("ObjectDetails/skill_info");
            previousIconType = IconType.Skill;
        }
        else if (type.Equals(IconType.GameObject) && previousIconType != IconType.GameObject)
        {
            objectInfoFile = (TextAsset)Resources.Load("ObjectDetails/object_info");
            previousIconType = IconType.GameObject;
        }

        try
        {
            stream = new MemoryStream(objectInfoFile.bytes);
            reader = new StreamReader(stream);

            string desired = "[" + determineIconToken(type) + "]" + name;
            string read_in = "";

            while (!read_in.Equals(desired))
            {
                string data = reader.ReadLine();

                if (data.Length >= desired.Length)
                    read_in = data.Substring(0, desired.Length);
                else
                    read_in = "NULL";

                if (read_in.Equals(desired))
                {
                    int extra_char_counter = 0;

                    for (int i = desired.Length; i < data.Length; i++)
                    {
                        if (data[i] != '[')
                            extra_char_counter++;
                        else
                            break;
                    }

                    details.NameColor = determineNameColor(data.Substring(desired.Length + extra_char_counter, data.Length - (desired.Length + extra_char_counter)));

                    data = reader.ReadLine();
                    details.ID = int.Parse(data.Substring("[id]".Length, data.Length - "[id]".Length));

                    string[] CombinedDetails = determineDetails(reader);
                    List<Color> colors = new List<Color>();

                    details.StatisticalDetails = placeDetails(CombinedDetails, 's', colors);
                    details.StatisticalColors = colors.ToArray();
                    colors.Clear();

                    details.DescriptiveDetails = placeDetails(CombinedDetails, 'd', colors);
                    details.DescriptiveColors = colors.ToArray();
                    colors.Clear();

                    break;
                }
            }

            reader.Close();
            stream.Close();
        }
        catch (IOException ex) {
            Debug.Log(ex.Message);
        }

        return details;
    }

    private string[] placeDetails(string[] combined_details, char type, List<Color> colors)
    {
        List<string> sorted_type = new List<string>();

        for (int i = 0; i < combined_details.Length; i++)
        {
            string readin_detail = "";
            string readin_detail_color = "";
            int token_counter = 0;

            for (int j = 0; j < combined_details[i].Length; j++)
            {
                if (combined_details[i][j].Equals('['))
                    token_counter++;

                if (token_counter < 2)
                    readin_detail += combined_details[i][j];

                if (token_counter >= 2)
                    readin_detail_color += combined_details[i][j];
            }

            if (readin_detail[1].Equals(type))
            {
                sorted_type.Add(readin_detail.Substring(("[" + type + "]").Length));

                if (readin_detail_color != "")
                    colors.Add(determineNameColor(readin_detail_color));
                else
                    colors.Add(Color.white);
            }
        }

        return sorted_type.ToArray();
    }

    private string[] determineDetails(StreamReader reader)
    {
        List<string> detail_list = new List<string>();
        string read_in = "NULL";

        while (read_in != "" || read_in != null)
        {
            read_in = reader.ReadLine();

            if (read_in == "" || read_in == null)
            {
                break;
            }

            detail_list.Add(read_in);
        }

        return detail_list.ToArray();
    }

    private string determineIconToken(IconType type)
    {
        string token = "";

        if (type.Equals(IconType.Item))
            token = "it";
        else if (type.Equals(IconType.Skill))
            token = "sk";
        else if (type.Equals(IconType.GameObject))
            token = "ob";

        return token;
    }

    private Color determineNameColor(string color_data)
    {
        Color determined_color = Color.white;
        string color_value = "";

        for (int i = 0; i < color_data.Length; i++)
        {
            if (color_data[i] != '[' && color_data[i] != ']')
                color_value += color_data[i];
        }

        for (int i = 0; i < ColorValues.Length; i++)
        {
            if (color_value.Equals(i.ToString()))
                determined_color = ColorValues[i];
        }

        return determined_color;
    }

    public void RenderTooltipGUI()
    {
        active_tooltip.DrawToolTipWindow();
    }

    public void CreateNewTooltip(Rect base_dimens, IconType type, string name)
    {
        try
        {
            active_tooltip = new Tooltip(tooltip_background, tooltipFont, font_Sizes, base_dimens, type, RetrieveDetails(type, name));
        }
        catch (System.NullReferenceException ex)
        {
            Debug.Log("Could Not Locate: " + name + " In: " + objectInfoFile.name);
            Debug.Log(ex.Message);
        }
    }

    public string ActiveTooltip()
    {
        if (active_tooltip != null)
            return active_tooltip.ToString();
        else
            return "NULL";
    }

    public Rect getActiveDetailsDimensions()
    {
        return active_tooltip.getDetailDimensions();
    }

    public void setActiveDetailsDimensionPosition(float x, float y)
    {
        active_tooltip.setDimensionPosition(x, y);
    }

    private class Tooltip
    {
        IconType type;
        TooltipDetails details;
        GUIStyle tooltip_bubbleStyle = new GUIStyle();
        GUIStyle name_style = new GUIStyle();
        GUIStyle stat_style = new GUIStyle();
        GUIStyle detail_style = new GUIStyle();

        public Tooltip(Texture2D tooltip_texture, Font font_style, int[] font_sizes, Rect base_dimens, IconType tooltip_type, TooltipDetails tooltip_details)
        {
            type = tooltip_type;
            details = tooltip_details;

            tooltip_bubbleStyle.normal.background = tooltip_texture;

            // Base GUIStyle Font settings here
            name_style.normal.textColor = details.NameColor;
            name_style.font = font_style;
            name_style.fontSize = font_sizes[0];

            stat_style.font = font_style;
            stat_style.fontSize = font_sizes[1];

            detail_style.font = font_style;
            detail_style.fontSize = font_sizes[2];
            // End Base Settings

            details.Dimensions = new Rect(base_dimens.x + base_dimens.width, base_dimens.y, name_style.CalcSize(new GUIContent(details.Name)).x, name_style.CalcSize(new GUIContent(details.Name)).y);

            Rect determinedDimensions = determineDimensions(details);

            if (details.Dimensions.x + determinedDimensions.width > Screen.width)
                details.Dimensions = new Rect(details.Dimensions.x - (determinedDimensions.width + base_dimens.width), details.Dimensions.y, details.Dimensions.width, details.Dimensions.height);

            if (details.Dimensions.y + determinedDimensions.height > Screen.height)
                details.Dimensions = new Rect(details.Dimensions.x, details.Dimensions.y - ((details.Dimensions.y + determinedDimensions.height) - Screen.height), details.Dimensions.width, details.Dimensions.height);
        }

        private Rect determineDimensions(TooltipDetails details)
        {
            Rect determined_dimens = new Rect();

            if (determined_dimens.width < name_style.CalcSize(new GUIContent(details.Name)).x)
                determined_dimens.width = name_style.CalcSize(new GUIContent(details.Name)).x;

            determined_dimens.height += name_style.CalcSize(new GUIContent(details.Name)).y;

            for (int i = 0; i < details.StatisticalDetails.Length; i++)
            {
                if (determined_dimens.width < stat_style.CalcSize(new GUIContent(details.StatisticalDetails[i])).x)
                    determined_dimens.width = stat_style.CalcSize(new GUIContent(details.StatisticalDetails[i])).x;

                determined_dimens.height += stat_style.CalcSize(new GUIContent(details.StatisticalDetails[i])).y;
            }

            for (int i = 0; i < details.DescriptiveDetails.Length; i++)
            {
                if (determined_dimens.width < detail_style.CalcSize(new GUIContent(details.DescriptiveDetails[i])).x)
                    determined_dimens.width = detail_style.CalcSize(new GUIContent(details.DescriptiveDetails[i])).x;

                determined_dimens.height += detail_style.CalcSize(new GUIContent(details.DescriptiveDetails[i])).y;
            }

            determined_dimens.x = details.Dimensions.x;
            determined_dimens.y = details.Dimensions.y;

            return determined_dimens;
        }

        public void DrawToolTipWindow()
        {
            GUI.Box(determineDimensions(details), "", tooltip_bubbleStyle);
            GUI.Box(details.Dimensions, this.ToString(), name_style);

            for (int i = 0; i < details.StatisticalDetails.Length; i++)
            {
                GUIStyle determined_stat_style = new GUIStyle();
                determined_stat_style.font = stat_style.font;
                determined_stat_style.fontSize = stat_style.fontSize;
                determined_stat_style.normal.textColor = details.StatisticalColors[i];

                GUI.Box(new Rect(details.Dimensions.x, details.Dimensions.height + details.Dimensions.y + (i * determined_stat_style.CalcSize(new GUIContent(details.StatisticalDetails[i])).y),
                                determined_stat_style.CalcSize(new GUIContent(details.StatisticalDetails[i])).x,
                                determined_stat_style.CalcSize(new GUIContent(details.StatisticalDetails[i])).y), details.StatisticalDetails[i], determined_stat_style);
            }

            for (int i = 0; i < details.DescriptiveDetails.Length; i++)
            {
                GUIStyle determined_detail_style = new GUIStyle();
                determined_detail_style.font = detail_style.font;
                determined_detail_style.fontSize = detail_style.fontSize;
                determined_detail_style.normal.textColor = details.DescriptiveColors[i];

                if (details.StatisticalDetails.Length > 0)
                {
                    GUI.Box(new Rect(details.Dimensions.x, details.Dimensions.height + (details.StatisticalDetails.Length * stat_style.CalcSize(new GUIContent(details.StatisticalDetails[i])).y) + details.Dimensions.y + (i * determined_detail_style.CalcSize(new GUIContent(details.DescriptiveDetails[i])).y),
                                    determined_detail_style.CalcSize(new GUIContent(details.DescriptiveDetails[i])).x,
                                    determined_detail_style.CalcSize(new GUIContent(details.DescriptiveDetails[i])).y), details.DescriptiveDetails[i], determined_detail_style);
                }
                else
                {
                    GUI.Box(new Rect(details.Dimensions.x, details.Dimensions.height + details.Dimensions.y + (i * determined_detail_style.CalcSize(new GUIContent(details.DescriptiveDetails[i])).y),
                                    determined_detail_style.CalcSize(new GUIContent(details.DescriptiveDetails[i])).x,
                                    determined_detail_style.CalcSize(new GUIContent(details.DescriptiveDetails[i])).y), details.DescriptiveDetails[i], determined_detail_style);
                }
            }
        }

        public Rect getDetailDimensions()
        {
            return details.Dimensions;
        }

        public void setDimensionPosition(float x, float y)
        {
            details.Dimensions = new Rect(x, y, details.Dimensions.width, details.Dimensions.height);
        }

        public IconType getType()
        {
            return type;
        }

        public override string ToString()
        {
            return details.Name;
        }
    }
}

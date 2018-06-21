using UnityEngine;
using System.Collections;

public class ObjectHandler : MonoBehaviour 
{
    public string ObjectName;

    public string Object_Name
    {
        get { return ObjectName; }
        set { ObjectName = value; }
    }
}
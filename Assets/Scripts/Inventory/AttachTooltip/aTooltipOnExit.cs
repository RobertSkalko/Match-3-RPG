using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class aTooltipOnExit : MonoBehaviour, IPointerExitHandler
{
    public void OnPointerExit(PointerEventData data)
    {
        //GameObject obj = data.pointerCurrentRaycast.gameObject;

        Destroy(gameObject);
    }
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class textHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Text text;
    Color startColor;

    void Start()
    {
        text = GetComponent<Text>();
        startColor = text.color;
    }

    public void OnPointerEnter(PointerEventData p)
    {
        text.color = Color.blue;
    }
    public void OnPointerExit(PointerEventData p)
    {
        text.color = startColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverHighlight : MonoBehaviour
{
    public Color startColor;

    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    void OnMouseEnter()
    {
        startColor = renderer.material.color;
        renderer.material.color = Color.yellow;
    }
    void OnMouseExit()
    {
        renderer.material.color = startColor;
    }
}

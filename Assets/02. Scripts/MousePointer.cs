using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    public SpriteRenderer imageRenderer;

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = worldPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMoving : MonoBehaviour
{
    public float speed = 5f;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {

        rectTransform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}

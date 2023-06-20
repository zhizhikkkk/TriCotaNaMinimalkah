using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AntMoving : MonoBehaviour
{
    
    public float speed = 5f;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
       
        rectTransform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}

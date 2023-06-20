using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodUpal : MonoBehaviour
{
    
    [SerializeField] private Image plate;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "food")
        {
            Destroy(collision.gameObject);
            plate.GetComponent<CollectFood>().Oshibka();
        }
    }
}

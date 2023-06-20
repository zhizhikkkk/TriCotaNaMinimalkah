using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateMove : MonoBehaviour
{
    private bool move = false;
    private int napravlenie = 1;
    
    [SerializeField] private float speed = 5f;



    

    private void FixedUpdate()
    {
        if (move == true)
        {
            gameObject.transform.Translate(transform.right*speed * napravlenie *Time.deltaTime);
        }
    }

    public void MoveCar(bool _move)
    {
        move = _move;
    }

    public void CheckNApravlenie(int x)
    {
        napravlenie = x;
    }
}

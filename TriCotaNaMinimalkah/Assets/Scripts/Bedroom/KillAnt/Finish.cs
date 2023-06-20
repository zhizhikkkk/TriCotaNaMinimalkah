using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject killua;

    private void Update()
    {
        if (scoreText.text == "10")
        {
            spawner.SetActive(false);
            killua.GetComponent<KilluaMoving>().KilluaFinish();
        }
    }
}

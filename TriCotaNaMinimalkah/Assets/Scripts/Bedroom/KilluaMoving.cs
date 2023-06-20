using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using UnityEngine.UI;

public class KilluaMoving : MonoBehaviour
{
    Sequence seq = DOTween.Sequence();
    [SerializeField] private Image speechImage;
    [SerializeField] private Image speechImage2;
    [SerializeField] private Image speechImage3;
    [SerializeField] private Text speechText;
    [SerializeField] private GameObject tracks;
    [SerializeField] private GameObject spawner;
    
    
    private void Start()
    {
        seq.Append(gameObject.transform.DOMoveX(5f, 1));
        StartCoroutine(ReturnKillua());
    }

    private void Update()
    {
        if (tracks.transform.childCount == 0)
        {
            KilluaFinish();

        }

    }
    public void KilluaFinish()
    {
        seq.Append(gameObject.transform.DOMoveX(5f, 1));
        speechImage2.gameObject.SetActive(true);
        spawner.SetActive(false);
    }

    public void KilluaAfterLoss()
    {
        seq.Append(gameObject.transform.DOMoveX(5f, 1));
        speechImage3.gameObject.SetActive(true);
        spawner.SetActive(false);
    }


    IEnumerator ReturnKillua()

    {
        yield return new WaitForSeconds(2f);
        speechImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        seq.Append(gameObject.transform.DOMoveX(15f, 1));
        
        speechImage.gameObject.SetActive(false);
        spawner.SetActive(true);

    }
}

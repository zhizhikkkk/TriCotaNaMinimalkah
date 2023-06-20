using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class WashVegetables : MonoBehaviour
{
    [SerializeField] private bool defaultInitialPos;
    [HideIf("defaultInitialPos")][SerializeField] private Vector2 initialpos;
    [SerializeField] private Canvas Canvas_Drag;
    [SerializeField] private float indentY = 10f;
    [SerializeField] private float indentX = 10f;
    private Transform defaultParent;
    [SerializeField] private float duration;
    [SerializeField] private float jumpPower;
    [SerializeField] private int sibling;
    [SerializeField] private bool useDragParent;

    [ShowIf("useDragParent")] [SerializeField] private Transform dragParent;

    [SerializeField] private bool washing;
    [ShowIf("washing")] [SerializeField] private ParticleSystem particle;
    [ShowIf("washing")] [SerializeField] private Image cleanVeg;
    private RectTransform rect;
    [ShowIf("washing")] [SerializeField] private string waterTag;
    [ShowIf("washing")] [SerializeField] private bool show;

    [ShowIf("washing")] [ShowIf("show")] [SerializeField] private GameObject shadow;


    [ShowIf("washing")] [SerializeField] private Image cleanShadow;


    [SerializeField] private bool slicing;
    [ShowIf("slicing")][SerializeField] private Image knife;
    [ShowIf("slicing")][SerializeField] private Image board;
    [ShowIf("slicing")][SerializeField] private Image shadowOnBoard;

    [ShowIf("slicing")][SerializeField] private Sprite slicedSprite;
    [ShowIf("slicing")][SerializeField] private GameObject slicesInSalad;

    [SerializeField] private bool spicing;
    [ShowIf("spicing")][SerializeField] private Image spiceShadow;



    void Start()
    {
        if (defaultInitialPos)
        {
            initialpos = gameObject.GetComponent<RectTransform>().anchoredPosition;
        }
        rect = gameObject.GetComponent<RectTransform>();
        defaultParent = gameObject.transform.parent;
    }

    private void Update()
    {
        if (gameObject.transform.parent.name == "Spices")
        {
            if(Math.Round(this.transform.position.x, 2) == Math.Round(initialpos.x / 100 - 0.43f, 2))
                spiceShadow.gameObject.SetActive(true);
            else
                spiceShadow.gameObject.SetActive(false);
        }
    }
    public void OnDrag()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas_Drag.transform as RectTransform, Input.mousePosition, Canvas_Drag.worldCamera, out pos);
        pos.y += indentY;
        pos.x += indentX;
        gameObject.transform.position = Canvas_Drag.transform.TransformPoint(pos);
        if (useDragParent)
        {
            gameObject.transform.SetParent(dragParent);
        }
        gameObject.transform.SetAsLastSibling();

        
    }
    public void OnDrop()
    {
        if (useDragParent)
        {
            gameObject.transform.SetParent(defaultParent);
        }
        rect.anchoredPosition = initialpos;
        rect.SetSiblingIndex(sibling);
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pot"))
        {
            var seq = DOTween.Sequence();
            seq.Append(gameObject.transform.DOMoveX(3f, 1f));
            seq.Join(gameObject.transform.DOMoveY(-0.5f, 1f));
            seq.Append(knife.rectTransform.DOMoveX(3.5f, 0.2f));
            seq.Join(knife.rectTransform.DOMoveY(0.5f, 0.2f));
            seq.Append(knife.rectTransform.DOMoveY(-0.5f, 0.2f));
            seq.Join(knife.rectTransform.DOMoveX(4f, 0.2f));
            seq.Append(knife.rectTransform.DOMoveY(0.5f, 0.2f));
            seq.Append(knife.rectTransform.DOMoveY(-0.5f, 0.2f));
            seq.Append(knife.rectTransform.DOMoveY(0.5f, 0.2f));
            seq.Append(knife.rectTransform.DOMoveY(-2.5f, 0.2f));
            seq.Join(knife.rectTransform.DOMoveX(1f, 0.2f));
            seq.Append(gameObject.transform.DOMoveX(7f, 0.5f));
            seq.OnComplete(SwapSprite);
        }

        if (collision.CompareTag("plate") && gameObject.CompareTag("spice"))
        {
            var seq = DOTween.Sequence();
            seq.Append(gameObject.transform.DOMoveX(4f, 1f));
            seq.Join(gameObject.transform.DOMoveY(0f, 1f));
            seq.Join(gameObject.transform.DOMoveY(-1f, 0.5f));
            seq.Append(gameObject.transform.DORotate(new Vector3(0, 0, 75), 0.5f, RotateMode.FastBeyond360));
            seq.Join(gameObject.transform.DOMoveX(4f, 0.1f));
            seq.Append(gameObject.transform.DORotate(new Vector3(0, 0, 50), 0.5f, RotateMode.FastBeyond360));
            seq.Append(gameObject.transform.DORotate(new Vector3(0, 0, 75), 0.5f, RotateMode.FastBeyond360));
            seq.Join(gameObject.transform.DOMoveX(3.7f, 0.3f));
            seq.Append(gameObject.transform.DORotate(new Vector3(0, 0, 50), 0.5f, RotateMode.FastBeyond360));
            seq.Append(gameObject.transform.DORotate(new Vector3(0, 0, 75), 0.5f, RotateMode.FastBeyond360));
            seq.Join(gameObject.transform.DOMoveX(3.4f, 0.3f));
            seq.Append(gameObject.transform.DORotate(new Vector3(0, 0, 50), 0.5f, RotateMode.FastBeyond360));
            seq.Append(gameObject.transform.DOMove(new Vector2(initialpos.x / 100 - 0.43f, initialpos.y / 100 - 0.43f), 1f));
            seq.Join(gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.5f, RotateMode.FastBeyond360));
        }
        if (particle.gameObject.activeSelf)
        {
            if (collision.CompareTag(waterTag))
            {
                if (gameObject.GetComponent<BoxCollider2D>() != null)
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }

                if (gameObject.GetComponent<EventTrigger>() != null)
                {
                    gameObject.GetComponent<EventTrigger>().enabled = false;
                }
                RectTransform shadowRect = shadow.GetComponent<RectTransform>();
                gameObject.transform.SetParent(shadow.transform.parent);
                var seq = DOTween.Sequence();
                seq.Append(rect.DOJumpAnchorPos(shadowRect.anchoredPosition, jumpPower, 1, duration));
                seq.Join(rect.DORotate(shadowRect.rotation.eulerAngles, duration, RotateMode.FastBeyond360));
                seq.Join(rect.DORotate(shadowRect.rotation.eulerAngles, duration, RotateMode.FastBeyond360));
                seq.OnComplete(CleanVegetableJump);
            }
        }
    }

    private void SwapSprite()
    {
        gameObject.GetComponent<Image>().sprite = slicedSprite;

        StartCoroutine(MakeSalad());
        //StopAllCoroutines();
    }

    IEnumerator MakeSalad()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
        slicesInSalad.gameObject.SetActive(true);
    }
    private void CleanVegetableJump()
    {
        Destroy(gameObject);
        cleanVeg.gameObject.SetActive(true);

        var seq = DOTween.Sequence();
        seq.Append(cleanVeg.rectTransform.DOJumpAnchorPos(cleanShadow.rectTransform.anchoredPosition, jumpPower, 1, duration));
        seq.Join(cleanVeg.rectTransform.DORotate(cleanShadow.rectTransform.rotation.eulerAngles, duration, RotateMode.FastBeyond360));
    }
}

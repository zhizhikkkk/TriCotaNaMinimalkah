using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private bool defaultInitialPos;
    [HideIf("defaultInitialPos")]
    [SerializeField] private Vector2 initialpos;
    RectTransform rect;
    [SerializeField] private float indentY = 10f;
    [SerializeField] private float indentX = 10f;
    [SerializeField] private Canvas Canvas_Drag;
    Transform defaultParent;
    [SerializeField] private string mainTag;

    [SerializeField] private bool show;
    [ShowIf("show")]
    [SerializeField] private GameObject shadow;
    [ShowIf("show")]
    [SerializeField] private float duration;
    [ShowIf("show")]
    [SerializeField] private float jumpPower;
    [ShowIf("show")]
    [SerializeField] private bool useDragParent;
    [ShowIf("show")]
    [ShowIf("useDragParent")]
    [SerializeField] private Transform dragParent;

    [ShowIf("show")] public GameObject wools;

    [SerializeField] private Image rag;


    [Title("BasketMoveEvent", titleAlignment: TitleAlignments.Centered)]
    [SerializeField] private GameObject basket;
    [SerializeField] private GameObject basketFront;
    [SerializeField] private GameObject basketBack;
    [SerializeField] private GameObject closedWindow;
    [SerializeField] private GameObject correctShadow;

    private bool checkCor = true;
    [Title("Triggered Event", titleAlignment: TitleAlignments.Centered)]
    [SerializeField] private UnityEvent triggeredEvent;




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
        basketFront.transform.SetAsLastSibling();
        if (closedWindow.activeSelf && correctShadow.activeSelf && basket.transform.childCount == 9)
        {
            var seq = DOTween.Sequence();
            seq.Append(basketBack.transform.DOMoveX(-6f, 3));
            seq.Join(basketFront.transform.DOMoveX(-6f, 3));
        }

        if (basket.transform.childCount == 14)
        {
            if (checkCor)
            {
                StartCoroutine(BasketMove(1));
                checkCor = false;
            }
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

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggeredEvent.Invoke();
        if (collision.tag == mainTag)
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
            seq.Join(rect.DOSizeDelta(shadowRect.sizeDelta, duration));
            seq.Append(rect.DOScale(shadowRect.localScale, duration));
        }
    }
    IEnumerator BasketMove(float duration)
    {
        yield return new WaitForSeconds(duration);
        var aqq = DOTween.Sequence();
        aqq.Append(basket.transform.DOMoveX(-15f, 3));
        aqq.Join(rag.transform.DOMoveX(7, 1f));
        wools.SetActive(false);
    }


}

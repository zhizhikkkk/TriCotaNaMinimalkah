using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;
//using UnityEditor.VersionControl;

public class Brush : MonoBehaviour
{
    [SerializeField] private bool defaultInitialPos;
    [HideIf("defaultInitialPos")]
    [SerializeField] private Vector2 initialpos;
    RectTransform rect;

    [SerializeField] private float indentY = 10f;
    [SerializeField] private float indentX = 10f;
    [SerializeField] private Canvas Canvas_Drag;
    [SerializeField] private string triggerName;
    [SerializeField] private GameObject tracks;
    [SerializeField] private GameObject killua;
    [SerializeField] private GameObject finish;

    [Title("Triggered Event", titleAlignment: TitleAlignments.Centered)]
    public UnityEvent triggeredEvent;

    private float time = 5f;
    private float count;
    void Start()
    {
       
        if (defaultInitialPos)
        {
            initialpos = gameObject.GetComponent<RectTransform>().anchoredPosition;
            initialpos.x += 144;
            rect = gameObject.GetComponent<RectTransform>();
        }

    }
    private void Update()
    {
       
        if (gameObject.transform.position.x > 0)
        {
            if (tracks.transform.childCount>0)
            {
                if (time <= 0f)
                {
                    time = 5f;
                    triggeredEvent.Invoke();
                }
                time -= Time.deltaTime;
            }
        }
        
        if (tracks.transform.childCount == 0)
        {
            
            var aqq = DOTween.Sequence();
            aqq.Append(gameObject.transform.DOMoveX(-80, 1f));

            aqq.OnComplete(Complete);

        }
    }

    private void Complete()
    {
        
       // killua.GetComponent<KilluaMoving>().KilluaFinish();
       finish.GetComponent<ChangeScene>().SceneChange("KillAnts");
    }
    public void OnDrag()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas_Drag.transform as RectTransform, Input.mousePosition, Canvas_Drag.worldCamera, out pos);
        pos.y += indentY;
        pos.x += indentX;
        gameObject.transform.position = Canvas_Drag.transform.TransformPoint(pos);
    }
    public void OnDrop()
    {
        rect.anchoredPosition = initialpos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == triggerName)
        {
            count = collision.gameObject.GetComponent<Image>().color.a;
            if (count > 0.2f)
            {
                count -= 0.1f;
            }
            else
            {
                count = 0;
                Destroy(collision.gameObject);
            }
        }
        collision.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, count);
    }
}


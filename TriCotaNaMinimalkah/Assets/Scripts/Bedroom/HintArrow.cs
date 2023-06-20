using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

public class HintArrow : MonoBehaviour
{
    [SerializeField] private RectTransform finger;
    [SerializeField] private float duration;
    [SerializeField] private List<HelpArrow> help;
    bool check = true;
    Sequence seq;
    [SerializeField] private GameObject gameManager;


   
    public void CallArrow(string index)
    {
       

        for (int x = 0; x < help.Count; x++)
        {
            if (x == 0)
            {
                if (gameManager.GetComponent<SwapChairs>().chairCorrected)
                {
                    help[0] = null;
                }
            }
            if (index == help[x].index)
            {
                if (help[x].obj != null && help[x].obj.gameObject.activeInHierarchy)
                {

                    finger.anchoredPosition = help[x].obj.anchoredPosition + help[x].startDistance;
                    Vector2 startPos = finger.anchoredPosition;
                    Vector2 endPos = finger.anchoredPosition + help[x].indent;
                    seq = DOTween.Sequence();
                    seq.Append(finger.GetComponent<Image>().DOFade(1f, 0.3f));
                    seq.Append(finger.DOAnchorPos(endPos, duration / 4, false));
                    seq.Append(finger.DOAnchorPos(startPos, duration / 4, false));
                    seq.Append(finger.DOAnchorPos(endPos, duration / 4, false));
                    seq.Append(finger.DOAnchorPos(startPos, duration / 4, false));
                    seq.Append(finger.GetComponent<Image>().DOFade(0f, 0.3f)).OnComplete(() => check = true);
                    //break;
                }
            }
        }
    }

    public void OffArrow()
    {
        seq.Complete();
        for (int x = 0; x < help.Count; x++)
        {
            help[x].seq.Complete();
        }
    }
}

[System.Serializable]
public class HelpArrow
{
    [BoxGroup("Object", false)]
    public string index;
    [BoxGroup("Object", false)]
    public RectTransform obj;
    [BoxGroup("Object", false)]
    public Vector2 startDistance;
    [BoxGroup("Object", false)]
    public Vector2 indent;
    [BoxGroup("Object", false)]
    public bool anotherFinger;
    [BoxGroup("Object", false)]
    [ShowIf("anotherFinger")]
    public RectTransform finger;
    [HideInInspector]
    public Sequence seq;
}
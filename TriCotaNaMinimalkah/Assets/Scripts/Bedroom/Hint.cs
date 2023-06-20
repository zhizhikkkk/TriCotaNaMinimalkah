using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float time;
    [PropertySpace(SpaceBefore = 0, SpaceAfter = 10)]
    [HideIf("checkByPosition")]
    [SerializeField] private bool checkByNull;
    [PropertySpace(SpaceBefore = 0, SpaceAfter = 10)]
    [HideIf("checkByNull")]
    [SerializeField] private bool checkByPosition;
    [HideIf("usePointer")]
    [SerializeField] private bool useFinger;
    [HideIf("useFinger")]
    [SerializeField] private bool usePointer;
    [ShowIf("useFinger")]
    [SerializeField] private RectTransform finger;
    [ShowIf("usePointer")]
    [SerializeField] private RectTransform fingerPointer;
    [HideIf("useFinger")]
    [HideIf("usePointer")]
    [SerializeField] private Color32 clr;
    [HideIf("usePointer")]
    [SerializeField] private List<Help> help;
    [ShowIf("usePointer")]
    [SerializeField] private List<HelpPointer> helpPointer;
    [HideInInspector]
    [SerializeField] private float fixtimer;
    [HideInInspector]
    [SerializeField] private bool check = true;
    Sequence seq;
    [SerializeField] private GameObject gameManager;
    private void Start()
    {
        fixtimer = time;
    }
    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = fixtimer;
            if (usePointer)
            {
                HelpPointer();
            }
            else
            {
                Help();
            }
        }
    }
    public void PauseTimer()
    {
        time = fixtimer;
    }
    public void thisGameObjectNull()
    {
        GameObject thisObject = EventSystem.current.currentSelectedGameObject;
        for (int x = 0; x < helpPointer.Count; x++)
        {
            if (helpPointer[x].obj != null)
            {
                if (helpPointer[x].obj.gameObject == thisObject.gameObject)
                {
                    helpPointer[x].obj = null;
                    break;
                }
            }
        }
    }

    public void ThisGameObjectNullPublic(GameObject thisObject)
    {
        for (int x = 0; x < help.Count; x++)
        {
            if (help[x].from != null)
            {
                if (help[x].from.gameObject == thisObject.gameObject)
                {
                    help[x].from = null;
                    break;
                }
            }
        }
    }


    public void Help()
    {
        if (gameManager.GetComponent<CloseWindow>().windowClosed &&
            gameManager.GetComponent<SwapChairs>().chairCorrected)
        {
            if (gameObject.activeInHierarchy)
            {
                if (check)
                {
                    time = fixtimer;
                    for (int x = 0; x < help.Count; x++)
                    {
                        if (help[x].from != null && help[x].to != null)
                        {
                            if (help[x].from.gameObject.activeInHierarchy)
                            {
                                check = false;
                                if (useFinger)
                                {
                                    if (help[x].parentPos)
                                    {
                                        finger.anchoredPosition = help[x].from.anchoredPosition +
                                                                  help[x].from.gameObject.transform.parent
                                                                      .GetComponent<RectTransform>().anchoredPosition +
                                                                  help[x].startDistance;
                                    }
                                    else
                                    {
                                        finger.anchoredPosition = help[x].from.anchoredPosition + help[x].startDistance;
                                    }

                                    Vector2 vec_shadow = help[x].to.anchoredPosition + help[x].to.gameObject.transform
                                        .parent.GetComponent<RectTransform>().anchoredPosition;
                                    seq = DOTween.Sequence();
                                    seq.Append(finger.GetComponent<Image>().DOFade(1f, 0.5f));
                                    if (help[x].Default)
                                    {
                                        seq.Append(
                                            finger.DOAnchorPos(vec_shadow + help[x].endDistance, duration, false));
                                    }

                                    if (help[x].Jump)
                                    {
                                        seq.Append(finger.DOJumpAnchorPos(vec_shadow + help[x].endDistance,
                                            help[x].power, 1, duration));
                                    }

                                    seq.Append(finger.GetComponent<Image>().DOFade(0f, 0.5f))
                                        .OnComplete(() => check = true);
                                    break;
                                }
                                else
                                {
                                    RectTransform rectObj = Instantiate(help[x].from, help[x].from.transform.parent);
                                    foreach (var comp in rectObj.GetComponents<Component>())
                                    {
                                        if (!(comp is Image) && !(comp is RectTransform) && !(comp is CanvasRenderer))
                                        {
                                            Destroy(comp);
                                        }
                                    }

                                    rectObj.GetComponent<Image>().color = clr;
                                    Vector2 vec_shadow = help[x].to.anchoredPosition + help[x].to.gameObject.transform
                                        .parent.GetComponent<RectTransform>().anchoredPosition;
                                    seq = DOTween.Sequence();
                                    if (help[x].Default)
                                    {
                                        seq.Append(rectObj.DOAnchorPos(vec_shadow + help[x].endDistance, duration,
                                            false));
                                    }

                                    if (help[x].Jump)
                                    {
                                        seq.Append(rectObj.DOJumpAnchorPos(vec_shadow + help[x].endDistance,
                                            help[x].power, 1, duration));
                                    }

                                    seq.Append(rectObj.GetComponent<Image>().DOFade(0f, 0.3f));
                                    seq.OnComplete(() => check = true);
                                    Destroy(rectObj.gameObject, duration + 0.3f);
                                    break;
                                }
                            }
                        }

                    }
                }
            }
        }
    }

    public void HelpPointer()
    {
        if (check)
        {
            check = false;
            time = fixtimer;
            for (int x = 0; x < helpPointer.Count; x++)
            {
                if (checkByNull)
                {
                    if (helpPointer[x].obj != null)
                    {
                        fingerPointer.transform.SetParent(helpPointer[x].obj.parent);
                        fingerPointer.anchoredPosition = helpPointer[x].obj.anchoredPosition + helpPointer[x].startDistance;
                        Vector2 startPos = fingerPointer.anchoredPosition;
                        Vector2 endPos = fingerPointer.anchoredPosition + helpPointer[x].indent;
                        seq = DOTween.Sequence();
                        seq.Append(fingerPointer.GetComponent<Image>().DOFade(1f, 0.5f));
                        seq.Append(fingerPointer.DOAnchorPos(endPos, duration / 4, false));
                        seq.Append(fingerPointer.DOAnchorPos(startPos, duration / 4, false));
                        seq.Append(fingerPointer.DOAnchorPos(endPos, duration / 4, false));
                        seq.Append(fingerPointer.DOAnchorPos(startPos, duration / 4, false));
                        seq.Append(fingerPointer.GetComponent<Image>().DOFade(0f, 0.5f)).OnComplete(() => check = true);
                        break;
                    }
                }
            }
        }
    }

    public void OffHint()
    {
        seq.Complete();
    }

}
[System.Serializable]
public class Help
{
    [BoxGroup("Object", false)]
    public RectTransform from;
    [BoxGroup("Object", false)]
    public Vector2 startDistance;
    [BoxGroup("Object", false)]
    public RectTransform to;
    [BoxGroup("Object", false)]
    public Vector2 endDistance;
    [BoxGroup("Object", false)]
    [HideIf("Jump")]
    public bool Default;
    [BoxGroup("Object", false)]
    [HideIf("Default")]
    public bool Jump;
    [BoxGroup("Object", false)]
    [ShowIf("Jump")]
    public float power;
    [BoxGroup("Object", false)]
    public bool parentPos;

}

[System.Serializable]
public class HelpPointer
{
    [BoxGroup("Object", false)]
    public RectTransform obj;
    [BoxGroup("Object", false)]
    public Vector2 startDistance;
    [BoxGroup("Object", false)]
    public Vector2 indent;
}


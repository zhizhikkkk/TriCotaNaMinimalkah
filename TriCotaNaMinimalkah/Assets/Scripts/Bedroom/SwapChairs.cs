using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwapChairs : MonoBehaviour
{
    CompositeDisposable disposables;
    [SerializeField] private Button chair;
    [SerializeField] private GameObject incorrectShadow;
    [SerializeField] private GameObject correctShadow;
    [SerializeField] private GameObject incorrectMirror;
    [SerializeField] private GameObject correctMirror;
    [SerializeField] public bool chairCorrected;
    private WaitForSeconds halfSecond = new WaitForSeconds(0.5f);

    [Title("Triggered Event", titleAlignment: TitleAlignments.Centered)]
    public UnityEvent triggeredEvent;

    private float time = 5f;

    void OnEnable()
    {
        disposables = new CompositeDisposable();
        chair.OnBeginDragAsObservable().Subscribe(_ => OnBeginDrag(chair.GetInstanceID())).AddTo(disposables);
        chair.OnDragAsObservable().Subscribe(_ => OnDrag(chair.GetInstanceID())).AddTo(disposables);
    }
    void OnDisable()
    {
        disposables.Dispose();
    }
    void OnDrag(int instanceID)
    {

    }

    private void Update()
    {
        if (!chairCorrected)
        {
            if (time <= 0f)
            {
                triggeredEvent.Invoke();
                time = 5f;
            }
            time -= Time.deltaTime;
        }
    }
    IEnumerator ChairCorrecting()
    {
        chair.transform.DORotate(new Vector3(0f, 0f, 90f), 1f);
        chair.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-156f, -78f), 1f);
        incorrectShadow.GetComponent<Image>().DOFade(0f, 1f);
        yield return halfSecond;
        correctShadow.SetActive(true);
        correctShadow.GetComponent<Image>().DOFade(1f, 0.5f);
        yield return halfSecond;
        incorrectShadow.gameObject.SetActive(false);
        chair.GetComponent<Image>().raycastTarget = false;
        chairCorrected = true;

        incorrectMirror.SetActive(false);
        correctMirror.SetActive(true);
    }
    private void OnBeginDrag(int instanceID)
    {
        if (instanceID == chair.GetInstanceID())
            StartCoroutine(ChairCorrecting());
    }
}

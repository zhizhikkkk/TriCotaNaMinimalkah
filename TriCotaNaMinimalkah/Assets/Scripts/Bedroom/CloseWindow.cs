using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class CloseWindow : MonoBehaviour
{
    CompositeDisposable disposables;
    [SerializeField] private Button openWindow;
    [SerializeField] private Button closedWindow;
    [SerializeField] public bool windowClosed;

    [Title("Triggered Event", titleAlignment: TitleAlignments.Centered)]
    public UnityEvent triggeredEvent;

    private float time = 5f;
    private void Update()
    {
        if (!windowClosed)
        {
            if (time <= 0f)
            {
                triggeredEvent.Invoke();
                time = 5f;
            }
            time -= Time.deltaTime;

        }
    }
    void OnEnable()
    {
        disposables = new CompositeDisposable();
        openWindow.OnClickAsObservable().Subscribe(_ => OnClick(openWindow.GetInstanceID())).AddTo(disposables);
        closedWindow.OnClickAsObservable().Subscribe(_ => OnClick(closedWindow.GetInstanceID())).AddTo(disposables);
    }
    void OnClick(int instanceID)
    {
        if (instanceID == openWindow.GetInstanceID())
        {
            openWindow.gameObject.SetActive(false);
            closedWindow.gameObject.SetActive(true);
            windowClosed = true;
        }
        else if (instanceID == closedWindow.GetInstanceID())
        {
            closedWindow.gameObject.SetActive(false);
            openWindow.gameObject.SetActive(true);
        }
    }
}

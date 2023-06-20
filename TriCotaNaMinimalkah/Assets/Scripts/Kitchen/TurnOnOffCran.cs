using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOnOffCran : MonoBehaviour
{

    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Image crane;

    private void Awake()
    {
        particleSystem = new ParticleSystem();
    }
    public void TurnOnOff()
    {
        crane.transform.DORotate(new Vector3(0f, 0f, 359f), 2f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
        particleSystem = GetComponent<ParticleSystem>();
        bool curState = particleSystem.gameObject.activeSelf;
        if(!curState)
        {
            crane.transform.DORotate(new Vector3(0f, 0f, 359f), 2f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
        }
        else
        {
            crane.transform.DORotate(new Vector3(0f, 0f, -359f), 2f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
        }
        
        particleSystem.gameObject.SetActive(!curState);
      
    }
}

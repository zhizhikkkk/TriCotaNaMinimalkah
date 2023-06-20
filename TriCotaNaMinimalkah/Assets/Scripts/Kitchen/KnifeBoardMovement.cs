using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class KnifeBoardMovement : MonoBehaviour
{
    [SerializeField] private GameObject cleanVegs;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Image knife;
    [SerializeField] private Image board;
    [SerializeField] private Image plate;
    [SerializeField] private GameObject salad;
    [SerializeField] private Image spoon;
    private bool knifeBoard = false;
    private int count;
    private int cntOfChildInSalad;
    
    private void Update()
    {
        
        count = cleanVegs.transform.Cast<Transform>()
            .Count(child => child.gameObject.activeSelf);
        if (!knifeBoard)
        {
            if (count == 6 )
            {
                var seq = DOTween.Sequence();
                seq.Append(knife.rectTransform.DOMoveX(1f, 1f));
                seq.Join(board.rectTransform.DOMoveX(3f, 1f));
                seq.Append(plate.rectTransform.DOMoveX(7f, 1f));
                particle.GetComponent<TurnOnOffCran>().TurnOnOff();
                knifeBoard = true;
            }
        }
        cntOfChildInSalad = salad.transform.Cast<Transform>()
            .Count(child => child.gameObject.activeSelf);

        if (cntOfChildInSalad == 6)
        {
            var seq = DOTween.Sequence();
            seq.Append(knife.rectTransform.DOMoveY(-10f, 1f));
            seq.Join(board.rectTransform.DOMoveY(-10f, 1f));
            seq.Join(plate.rectTransform.DOMoveX(3f, 1f));
            seq.Join(salad.transform.DOMoveX(-4f, 1f));
            seq.Join(spoon.rectTransform.DOMoveX(6f, 1f));
        }

    }
}

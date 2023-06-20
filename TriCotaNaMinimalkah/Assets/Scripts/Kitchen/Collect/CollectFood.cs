using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class CollectFood : MonoBehaviour
{
    [SerializeField] private List<Image> lives;
    [SerializeField] private Text cntOfFoodText;
    [SerializeField] private GameObject killua;
    [SerializeField] private Button restartBtn;
    [SerializeField] private GameObject finish;
    private int cntOfDeath=0;
    private int cntOfFood = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "food")
        {
            Destroy(collision.gameObject);
            cntOfFood++;
            cntOfFoodText.text=cntOfFood.ToString();
            if (cntOfFood == 10)
            {
                Debug.Log("WIN");
                killua.GetComponent<KilluaMoving>().KilluaFinish();
                StartCoroutine(ChangeSceneCor());

            }
        }

        if (collision.tag == "ugol")
        {

            
            Destroy(collision.gameObject);
            
            
            Oshibka();
        }
    }

    public void Oshibka()
    {
        
        Destroy(lives[cntOfDeath].gameObject);
        cntOfDeath++;
        CheckGameOver();
    }
    public void CheckGameOver()
    {
        
        if (cntOfDeath >= 3)
        {
            
            killua.GetComponent<KilluaMoving>().KilluaAfterLoss();
            restartBtn.gameObject.SetActive(true);
        }
    }
    public int GetCntOfDeath()
    {
        return cntOfDeath;
    }
    public void SetCntOfDeath()
    {
         cntOfDeath++;
    }

    IEnumerator ChangeSceneCor()
    {
        yield return new WaitForSeconds(3f);
        finish.GetComponent<ChangeScene>().SceneChange("Kitchen");
    }
}

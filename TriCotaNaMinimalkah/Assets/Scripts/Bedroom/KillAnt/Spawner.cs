using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;


public class Spawner : MonoBehaviour
{
    private Collider2D spawnArea;

    [SerializeField] private GameObject[] antPrefabs;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private float minSpawnDelay = 3f;
    [SerializeField] private float maxSpawnDelay = 6f;

    [SerializeField] private float minAngle = -15f;
    [SerializeField] private float maxAngle = 15f;
    
    [SerializeField] private float maxLifeTime = 3f;

    [SerializeField] private int maxCountAnt = 15;
    [SerializeField] private GameObject killua;
    [SerializeField] private Button restartBtn;
    private int cnt;
    private void Awake()
    {
        
        spawnArea = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
       
        while (enabled && cnt<=15)
        {
            cnt++;
            if (cnt == 14)
            {
                killua.GetComponent<KilluaMoving>().KilluaAfterLoss();
                restartBtn.gameObject.SetActive(true);
            }
            GameObject prefab = antPrefabs[Random.Range(0, antPrefabs.Length)];
            Vector2 position = new Vector2();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            Quaternion rotation = Quaternion.Euler(0f,0f,Random.Range(minAngle,maxAngle));
            GameObject ant = Instantiate(prefab, position, rotation);
            ant.transform.SetParent(mainCanvas.transform);
            Destroy(ant,maxLifeTime);
            
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            
        }
    }
}

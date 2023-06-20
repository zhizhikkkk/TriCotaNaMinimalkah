using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = System.Numerics.Vector3;

public class FoodSpawner : MonoBehaviour
{
    private Collider2D spawnArea;

    [SerializeField] private Image[] foodPrefabs;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private float minSpawnDelay = 3f;
    [SerializeField] private float maxSpawnDelay = 6f;

    [SerializeField] private float minAngle = -15f;
    [SerializeField] private float maxAngle = 15f;

    [SerializeField] private float maxLifeTime = 3f;

    
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

        while (enabled )
        {
            
            Image prefab = foodPrefabs[Random.Range(0, foodPrefabs.Length)];
            Vector2 position = new Vector2();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));
            Image food = Instantiate(prefab, position, rotation);
            food.transform.SetParent(mainCanvas.transform);
            Vector3 newScale = new Vector3(1f, 1f, 1f);
            food.gameObject.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
            Destroy(food, maxLifeTime);
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

        }
    }

}

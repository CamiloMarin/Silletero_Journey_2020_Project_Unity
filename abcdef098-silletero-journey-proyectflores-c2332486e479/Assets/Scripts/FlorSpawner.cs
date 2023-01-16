using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Flower;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;
    private void Start()
    {
        StartCoroutine(SpawnFlower());
    }

    private IEnumerator SpawnFlower()
    {
        while (true)
        {
            Vector3 randomSpawnPosition = new Vector3(spawnPos.position.x,0.5f,Random.Range(10,-11));
            yield return new WaitForSeconds(Random.Range(minSeparationTime, maxSeparationTime));
            Instantiate(Flower, randomSpawnPosition, Quaternion.identity);
            
            
        }
        
    }   
}

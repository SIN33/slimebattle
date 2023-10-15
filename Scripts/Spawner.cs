using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private GameObject enemyPrefab;

    void Start()
    {
        StartCoroutine(SpawnLoop());       
    }

    private IEnumerator SpawnLoop()
    {
        while(true)
        {
            var distanceVector = new Vector3(10, 0);

            var spawnPosFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;

            var spawnPos = playerStatus.transform.position + spawnPosFromPlayer;

            NavMeshHit navMeshHit;
            if(NavMesh.SamplePosition(spawnPos, out navMeshHit, 10, NavMesh.AllAreas))
            {
                Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(10);


            if(playerStatus.Life <= 0)
            {
                break;
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

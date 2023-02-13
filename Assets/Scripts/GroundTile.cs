using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject coinPrefab2;
    [SerializeField] GameObject coinPrefab3;
    [SerializeField] GameObject coinPrefab4;

    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject obstaclePrefab2;
    [SerializeField] GameObject tallObstaclePrefab;

    [SerializeField] GameObject wall;
    Vector3 spawnLeft;
    Vector3 spawnRight;
    
    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }

    public void SpawnWall()
    {
            Transform spawnLeft = transform.GetChild(5).transform;
            Instantiate(wall, spawnLeft.position, Quaternion.identity, transform);

            Transform spawnRight = transform.GetChild(6).transform;
            Instantiate(wall, spawnRight.position, Quaternion.identity, transform);
    }

    public void SpawnObstacle()
    {
        //Ver qual obstaculo vai nascer
        GameObject obstacleToSpawn;
        int random = Random.Range(0, 10);
        if(random < 2)
            obstacleToSpawn = tallObstaclePrefab;
        else if(random < 6)
            obstacleToSpawn = obstaclePrefab;
        else
            obstacleToSpawn = obstaclePrefab2;

        //Escolher o ponto de spawn
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        //
        Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, transform);
    }


    public void SpawnCoins()
    {
        //Ver qual obstaculo vai nascer
        GameObject coinToSpawn;

        int coinsToSpawn = Random.Range(1, 4);

        for(int i = 0; i < coinsToSpawn; i++)
        {
            int randomCoin = Random.Range(0, 12);
            if(randomCoin < 3)
                coinToSpawn = coinPrefab;
            else if(randomCoin < 6)
                coinToSpawn = coinPrefab2;
            else if(randomCoin < 9)
                coinToSpawn = coinPrefab3;
            else
                coinToSpawn = coinPrefab4;

            GameObject temp = Instantiate(coinToSpawn, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.x, collider.bounds.max.y),
            Random.Range(collider.bounds.min.x, collider.bounds.max.z)
        );
        if(point != collider.ClosestPoint(point)) {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}

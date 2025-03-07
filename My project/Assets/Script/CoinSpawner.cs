using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinCount = 50;
    public float spawnHeight = 1f;
    public float maxSlopeAngle = 10f;
    private Terrain terrain;

    void Start()
    {
        terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            
            return;
        }

        RemoveOldCoins();
        SpawnCoins();
    }

    void SpawnCoins()
{
    int spawned = 0;
    int maxAttempts = coinCount * 5;
    int attempts = 0;

    float terrainMinX = terrain.transform.position.x;
    float terrainMaxX = terrainMinX + terrain.terrainData.size.x;
    float terrainMinZ = terrain.transform.position.z;
    float terrainMaxZ = terrainMinZ + terrain.terrainData.size.z;

    while (spawned < coinCount && attempts < maxAttempts)
    {
        attempts++;

        float randomX = Random.Range(terrainMinX, terrainMaxX);
        float randomZ = Random.Range(terrainMinZ, terrainMaxZ);
        float terrainY = terrain.SampleHeight(new Vector3(randomX, 0, randomZ));

        Vector3 spawnPosition = new Vector3(randomX, terrainY + spawnHeight, randomZ);

        if (CheckSlope(spawnPosition))
        {
            Quaternion coinRotation = Quaternion.Euler(90f, 0f, 0f); // Поворот монеты
            Instantiate(coinPrefab, spawnPosition, coinRotation);
            spawned++;
        }
    }
}


    bool CheckSlope(Vector3 position)
    {
        Vector3 terrainNormal = terrain.terrainData.GetInterpolatedNormal(
            (position.x - terrain.transform.position.x) / terrain.terrainData.size.x,
            (position.z - terrain.transform.position.z) / terrain.terrainData.size.z);

        float slopeAngle = Vector3.Angle(Vector3.up, terrainNormal);
        return slopeAngle <= maxSlopeAngle;
    }

    void RemoveOldCoins()
    {
        GameObject[] existingCoins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in existingCoins)
        {
            Destroy(coin);
        }
    }
}

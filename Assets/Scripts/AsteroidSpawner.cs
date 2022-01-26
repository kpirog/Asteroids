using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime = 3.0f;
    [SerializeField] private float spawnOffset = 15.0f;
    [SerializeField] private int spawnAmount = 1;
    
    
    [SerializeField] private Asteroid asteroidPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnTime, spawnTime);
    }


    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnPosition = Random.insideUnitCircle.normalized * spawnOffset;
            Vector3 position = transform.position + spawnPosition;
            
            Asteroid asteroid = Instantiate(asteroidPrefab, position, Quaternion.identity);
            asteroid.size = UnityEngine.Random.Range(asteroid.minSize, asteroid.maxSize);

            asteroid.SetTrajectory(-spawnPosition);
        }
    }
}

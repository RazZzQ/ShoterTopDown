using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo a instanciar
    public float spawnDelay = 10f; // Tiempo entre cada spawn
    private float nextSpawnTime = 0f; // Tiempo del próximo spawn

    void Update()
    {
        // Si ha pasado suficiente tiempo desde el último spawn
        if (Time.time >= nextSpawnTime)
        {
            // Instanciar un enemigo en la posición del spawner
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            // Actualizar el tiempo del próximo spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}

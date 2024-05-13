using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public float speed = 3f;
    public int damage = 10;

    public GameObject diamondPrefab;

    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Movimiento hacia la posición del jugador
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Instanciar diamantes
        Instantiate(diamondPrefab, transform.position, Quaternion.identity);

        // Aquí puedes agregar efectos de muerte, como partículas o sonidos.
        Destroy(gameObject);

        // Aumentar la puntuación
        FindObjectOfType<ScoreManager>().AddScore(10); // Aquí puedes ajustar la cantidad de puntos   
    }
}

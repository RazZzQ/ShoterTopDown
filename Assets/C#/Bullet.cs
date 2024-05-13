using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifeTime = 2f; // Tiempo de vida de la bala

    private float lifeTimer;

    void Start()
    {
        lifeTimer = lifeTime;
    }

    void Update()
    {
        // Mueve la bala en su dirección actual
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Reduce el temporizador de vida de la bala
        lifeTimer -= Time.deltaTime;

        // Si el temporizador llega a cero o menos, destruye la bala
        if (lifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

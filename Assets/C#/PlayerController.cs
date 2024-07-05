using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public int maxHealth = 100; // Vida m�xima del jugador
    private int currentHealth; // Vida actual del jugador

    public GameObject bulletPrefab;
    public Transform firePoint;
    public TMP_Text healthText; // Referencia al TextMeshPro para mostrar la vida del jugador
    public SubmitShooterTopDown SubmitShooter;

    private Rigidbody2D rb;
    private Vector2 mousePosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    void Update()
    {
        // Obt�n la posici�n del rat�n en el mundo
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Rotaci�n del jugador hacia el rat�n
        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        // Movimiento
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized * speed;
        rb.velocity = movement;

        // Disparo
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instanciar la bala con la rotaci�n actual del jugador
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.transform.rotation = Quaternion.Euler(0, 0, rb.rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Obtener el componente Enemy del GameObject con el que ha colisionado
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // Aplicar da�o al jugador basado en el da�o del enemigo
            TakeDamage(enemy.damage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // Actualiza la UI de vida, reproduce efectos de da�o, etc.

        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthText();
    }

    void Die()
    {
        Time.timeScale = 0.0f;
        SubmitShooter.gameObject.SetActive(true);
    }

    void UpdateHealthText()
    {
        // Actualiza el TextMeshPro de la vida del jugador en UI
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }
}

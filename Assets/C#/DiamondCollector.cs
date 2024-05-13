using UnityEngine;

public class DiamondCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Diamond"))
        {
            // Recolectar el diamante
            CollectDiamond(other.gameObject);
        }
    }

    void CollectDiamond(GameObject diamond)
    {
        // Destruir el diamante
        Destroy(diamond);

        // Agregar monedas al jugador
        FindObjectOfType<ScoreManager>().AddCoins(50); // Ajusta la cantidad de monedas según sea necesario
    }
}

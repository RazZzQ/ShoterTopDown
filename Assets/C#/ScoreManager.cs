using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Referencia al TextMeshPro para la puntuación en UI
    public TMP_Text coinText; // Referencia al TextMeshPro para las monedas en UI
    private int score = 0; // Puntuación actual del jugador
    private int coins = 0; // Cantidad de monedas recolectadas

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    public void MultiplyPoints(int multiplier)
    {
        score *= multiplier;
        UpdateUI();
    }

    void UpdateUI()
    {
        // Actualiza el TextMeshPro de la puntuación y de las monedas en UI
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (coinText != null)
        {
            coinText.text = "Coins: " + coins;
        }
    }
}

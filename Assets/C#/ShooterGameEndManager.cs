using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena
using UnityEngine.Networking;
using TMPro;

public class ShooterGameEndManager : MonoBehaviour
{
    public GameObject endGamePanel; // Panel que se muestra al final del juego
    public Text endGameScoreText;   // Texto en el panel final que muestra el puntaje
    public Text endGameCoinsText;   // Texto en el panel final que muestra las monedas
    public InputField playerNameInput; // Campo de entrada para el nombre del jugador
    public Button submitButton;     // Botón para enviar los datos

    private ScoreManager scoreManager; // Referencia al ScoreManager

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        endGamePanel.SetActive(false); // Asegúrate de que el panel esté desactivado al inicio
    }

    public void EndGame()
    {
        int finalScore = scoreManager.GetScore();
        int finalCoins = scoreManager.GetCoins();
        endGamePanel.SetActive(true); // Activar el panel al finalizar el juego
        endGameScoreText.text = "Score: " + finalScore.ToString();
        endGameCoinsText.text = "Coins: " + finalCoins.ToString();
    }

    public void SubmitScore()
    {
        string playerName = playerNameInput.text;
        int finalScore = scoreManager.GetScore();
        int finalCoins = scoreManager.GetCoins();
        StartCoroutine(SendScoreToDatabase(playerName, finalScore, finalCoins));
    }

    private IEnumerator SendScoreToDatabase(string playerName, int score, int coins)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", playerName);
        form.AddField("score", score);
        form.AddField("coins", coins);

        UnityWebRequest www = UnityWebRequest.Post("http://tuservidor.com/api/submit_score.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            RestartGame(); // Reiniciar el juego después de enviar el puntaje
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }
}

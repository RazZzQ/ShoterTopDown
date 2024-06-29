using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena
using UnityEngine.Networking;

public class ShooterGameEndManager : MonoBehaviour
{
    public GameObject endGamePanel; // Panel que se muestra al final del juego
    public Text endGameScoreText;   // Texto en el panel final que muestra el puntaje
    public Text endGameCoinsText;   // Texto en el panel final que muestra las monedas
    public InputField playerNameInput; // Campo de entrada para el nombre del jugador
    public Button submitButton;     // Botón para enviar los datos

    private ScoreManager scoreManager; // Referencia al ScoreManager
    private bool scoreSubmitted = false; // Estado del envío del puntaje

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        endGamePanel.SetActive(false); // Asegúrate de que el panel esté desactivado al inicio
        submitButton.onClick.AddListener(SubmitScore); // Añadir el listener al botón
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
        if (!scoreSubmitted) // Verificar si el puntaje no ha sido enviado aún
        {
            string playerName = playerNameInput.text;
            int finalScore = scoreManager.GetScore();
            int finalCoins = scoreManager.GetCoins();
            StartCoroutine(SendScoreToDatabase(playerName, finalScore, finalCoins));
            scoreSubmitted = true; // Marcar como enviado después de iniciar la corutina
        }
    }

    private IEnumerator SendScoreToDatabase(string playerName, int score, int coins)
    {
        string jsonData = JsonUtility.ToJson(new ScoreData(playerName, score, coins));
        UnityWebRequest www = new UnityWebRequest("http://localhost/update_score_vj3.php", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score submitted successfully.");
            RestartGame(); // Reiniciar el juego después de enviar el puntaje
        }
        else
        {
            Debug.LogError("Error submitting score: " + www.error);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }

    [System.Serializable]
    private class ScoreData
    {
        public string username;
        public int score;
        public int coins;

        public ScoreData(string username, int score, int coins)
        {
            this.username = username;
            this.score = score;
            this.coins = coins;
        }
    }
}

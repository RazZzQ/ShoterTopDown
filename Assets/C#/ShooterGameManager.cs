using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ShooterGameManager : MonoBehaviour
{
    private string url = "http://localhost/update_shooter_game_score.php";

    public void SendShooterGameData(string username, int score, int coins)
    {
        StartCoroutine(SendShooterGameDataCoroutine(username, score, coins));
    }

    private IEnumerator SendShooterGameDataCoroutine(string username, int score, int coins)
    {
        ShooterGameData shooterGameData = new ShooterGameData
        {
            username = username,
            score = score,
            coins = coins
        };

        string jsonString = JsonUtility.ToJson(shooterGameData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }
}

[System.Serializable]
public class ShooterGameData
{
    public string username;
    public int score;
    public int coins;
}

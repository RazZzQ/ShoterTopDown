using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SubmitShooterTopDown : MonoBehaviour
{
    public InputField nameInput;
    public int enemiesKilled;
    public int coinsCollected;
    public string postURL = "http://localhost/submit_shootertopdown_score.php";
    public ScoreManager scoreManager;
    public void Submit()
    {
        StartCoroutine(PostScore(nameInput.text, enemiesKilled, coinsCollected));
    }

    IEnumerator PostScore(string playerName, int enemiesKilled, int coinsCollected)
    {
        ShooterTopDownScore score = new ShooterTopDownScore();
        score.name = playerName;
        enemiesKilled = scoreManager.GetScore();
        score.enemies_killed = enemiesKilled;
        coinsCollected = scoreManager.GetCoins();
        score.coins_collected = coinsCollected;

        string json = JsonUtility.ToJson(score);

        using (UnityWebRequest www = new UnityWebRequest(postURL, "POST"))
        {
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Score submitted successfully");
            }
        }
    }
}
[System.Serializable]
public class ShooterTopDownScore
{
    public string name;
    public int enemies_killed;
    public int coins_collected;
}
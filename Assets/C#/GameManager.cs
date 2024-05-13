using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    private bool isMultiplyingPoints = false;
    private float pointMultiplierDuration = 20f;
    private float elapsedTime = 0f;
    private int multiplier = 2;

    public void RestartScene()
    {
        // Reiniciar el juego
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void SpawnEnemies()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    public void MultiplyPoints()
    {
        if (!isMultiplyingPoints)
        {
            isMultiplyingPoints = true;
            elapsedTime = 0f;
            StartCoroutine(MultiplyPointsCoroutine());
        }
    }

    private IEnumerator MultiplyPointsCoroutine()
    {
        while (elapsedTime < pointMultiplierDuration)
        {
            FindObjectOfType<ScoreManager>().MultiplyPoints(multiplier);

            yield return null;
            elapsedTime += Time.deltaTime;
        }

        isMultiplyingPoints = false;
    }
}

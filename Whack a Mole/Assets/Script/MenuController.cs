using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;

    void Start()
    {
        // Load best score from PlayerPrefs
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best Score: " + bestScore;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}

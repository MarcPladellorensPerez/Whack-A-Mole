using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI timerText;
    public GameObject molePrefab;
    public GameObject bombPrefab;
    public Transform[] spawnPoints;
    public AudioSource audioSource;
    public AudioClip moleDestroyedSound;
    public AudioClip bombDestroyedSound;

    private int bestScore;
    private int currentScore;
    private float timer = 60f;
    private bool gameActive = false;

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best Score: " + bestScore;
        currentScore = 0;
        currentScoreText.text = "Current Score: " + currentScore;
        timerText.text = "Time: " + timer;
        StartCoroutine(SpawnMolesAndBombs());
    }

    void Update()
    {
        if (gameActive)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timer);
            if (timer <= 0)
            {
                EndGame();
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    HandleTouch(touch.position);
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                HandleTouch(Input.mousePosition);
            }
        }
    }

    void HandleTouch(Vector3 touchPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Mole"))
            {
                hit.collider.GetComponent<Mole>().OnMouseDown();
            }
            else if (hit.collider.CompareTag("Bomb"))
            {
                hit.collider.GetComponent<Bomb>().OnMouseDown();
            }
        }
    }

IEnumerator SpawnMolesAndBombs()
{
    gameActive = true;
    while (gameActive)
    {
        int numItems = Random.Range(2,6);
        for (int i = 0; i < numItems; i++)
        {
            Debug.Log("Spawn"+ i);
            
           

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject prefabToInstantiate = Random.value < 0.8f ? molePrefab : bombPrefab;
            GameObject instance = Instantiate(prefabToInstantiate, spawnPoint.position, Quaternion.identity, spawnPoint);
            
            instance.transform.localScale = new Vector3(1.5f, 1.5f, 1f);

            Animator animator = instance.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("Appear", true);
            }
        }
float spawnDelay = Random.Range(2f, 5f);
         yield return new WaitForSeconds(spawnDelay);
    }
}


    public void IncrementScore()
    {
        currentScore++;
        currentScoreText.text = "Current Score: " + currentScore;
    }

    public void DecrementScore()
    {
        currentScore--;
        currentScoreText.text = "Current Score: " + currentScore;
    }

    public void MoleDestroyed()
    {
        audioSource.PlayOneShot(moleDestroyedSound);
    }

    public void BombDestroyed()
    {
        audioSource.PlayOneShot(bombDestroyedSound);
    }

    void EndGame()
    {
        gameActive = false;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        SceneManager.LoadScene("MenuScene");
    }
}

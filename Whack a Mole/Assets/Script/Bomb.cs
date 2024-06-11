using UnityEngine;

public class Bomb : MonoBehaviour
{
    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void OnMouseDown()
    {
        gameController.DecrementScore();
        gameController.BombDestroyed();
        Destroy(gameObject);
    }
}

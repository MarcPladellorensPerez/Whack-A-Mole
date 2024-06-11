using UnityEngine;

public class Mole : MonoBehaviour
{
    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void OnMouseDown()
    {
        gameController.IncrementScore();
        gameController.MoleDestroyed();
        Destroy(gameObject);
    }
}

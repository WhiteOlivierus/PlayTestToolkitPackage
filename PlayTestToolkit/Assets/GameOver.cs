using UnityEngine;

public class GameOver : MonoBehaviour
{
    public bool gameOver = false;

    private void Update()
    {
        if (Input.GetKeyDown(""))
            gameOver = true;
    }
}

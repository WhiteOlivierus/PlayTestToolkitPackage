using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void Update() => QuitGame();

    public void QuitGame()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        Application.Quit();
    }
}

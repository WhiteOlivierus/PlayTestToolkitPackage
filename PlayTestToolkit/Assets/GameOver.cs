using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        Application.Quit();
    }
}

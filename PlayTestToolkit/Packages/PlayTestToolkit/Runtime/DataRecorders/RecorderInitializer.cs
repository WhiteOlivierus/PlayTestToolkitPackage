using PlayTestToolkit.Runtime;
using UnityEngine;

public class RecorderInitializer : MonoBehaviour
{
    private void Awake()
    {
        RecorderManager manager = RecorderManager.Instance;
    }
}

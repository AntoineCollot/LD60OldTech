using UnityEngine;

public class EnableVSync : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 1;
    }
}

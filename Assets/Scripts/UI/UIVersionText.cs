using TMPro;
using UnityEngine;

namespace UI
{
    public class UIVersionText : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("Game Version: " + Application.version.ToString());
            GetComponent<TextMeshProUGUI>().text = $"v{Application.version.ToString()}";
        }
    }
}
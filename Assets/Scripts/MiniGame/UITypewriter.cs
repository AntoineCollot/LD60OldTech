using System.Collections;
using TMPro;
using UnityEngine;

public class UITypewriter : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] int fps;
    float CharacterInterval => 1f / fps;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayText(string str)
    {
        StartCoroutine(Typewrite(str));
    }

    public void Clear()
    {
        text.text = "";
    }

    IEnumerator Typewrite(string str)
    {
        text.text = str;

        for (int i = 0; i <= str.Length; i++)
        {
            text.maxVisibleCharacters = i;

            yield return new WaitForSeconds(CharacterInterval);
        }
    }
}

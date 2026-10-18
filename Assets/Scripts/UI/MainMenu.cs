using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    CanvasGroup group;
    bool isFading;
    [SerializeField] float fadetime = 2;
    [SerializeField] GameObject panel;

    void Start()
    {
        group = panel.GetComponent<CanvasGroup>();
        panel.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed && Time.time > 3 && !isFading)
            StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        isFading = true;

        float t = 0;

        while(t<1)
        {
            t += Time.deltaTime / fadetime;

            group.alpha = Curves.QuadEaseInOut(1, 0, Mathf.Clamp01(t));

            yield return null;
        }

        panel.SetActive(false);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    CanvasGroup group;
    bool isFading;
    [SerializeField] float fadetime = 2;
    [SerializeField] GameObject panel;
    public bool IsActive => panel.activeSelf;

    public static MainMenu Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        group = panel.GetComponent<CanvasGroup>();
        panel.gameObject.SetActive(true);
    }

    void Update()
    {
#if !UNITY_EDITOR
        if (Mouse.current.leftButton.isPressed && Time.time > 1 && !isFading)
            StartCoroutine(FadeAway());
#else
        if (Mouse.current.leftButton.isPressed)
            panel.SetActive(false);
#endif
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

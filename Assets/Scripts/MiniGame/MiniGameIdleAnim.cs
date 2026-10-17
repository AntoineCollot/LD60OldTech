using System.Collections;
using UnityEngine;

public class MiniGameIdleAnim : MonoBehaviour
{
    [SerializeField] Sprite idleSprite;
    [SerializeField] int fps;
    Sprite baseSprite;
    SpriteRenderer rend;

    float animTimeInterval => 1f / fps;

    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        baseSprite = rend.sprite;
    }

    private void OnEnable()
    {
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        while(true)
        {
            rend.sprite = baseSprite;
            yield return new WaitForSeconds(animTimeInterval);

            rend.sprite = idleSprite;
            yield return new WaitForSeconds(animTimeInterval);
        }
    }
}

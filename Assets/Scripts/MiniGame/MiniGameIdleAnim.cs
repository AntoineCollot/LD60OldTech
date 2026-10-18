using System.Collections;
using UnityEngine;

public class MiniGameIdleAnim : MonoBehaviour
{
    [SerializeField] protected Sprite idleSprite;
    [SerializeField] protected int fps;
    protected Sprite baseSprite;
    protected SpriteRenderer rend;

    protected float animTimeInterval => 1f / fps;

    protected virtual Sprite AnimSprite => idleSprite;

    protected void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        baseSprite = rend.sprite;
    }

    protected void OnEnable()
    {
        StartCoroutine(Anim());
    }

    protected IEnumerator Anim()
    {
        while (true)
        {
            if (!CardProcessor.Instance.isPaused)
                rend.sprite = baseSprite;
            yield return new WaitForSeconds(animTimeInterval);

            if (!CardProcessor.Instance.isPaused)
                rend.sprite = AnimSprite;
            yield return new WaitForSeconds(animTimeInterval);
        }
    }
}

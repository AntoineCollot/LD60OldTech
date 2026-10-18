using UnityEngine;

public class CardDeck : Card
{
    public override void OnSelect(Vector2 uv)
    {
        CardManager.Instance.ToggleMode();
    }

    public override void OnHoverStay(Vector2 uv)
    {
    }

    public override void OnRightClick()
    {
    }
}

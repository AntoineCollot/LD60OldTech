using UnityEngine;

public class CardDeck : Card
{
    public override void OnSelect(Vector2 uv)
    {
        if (MainMenu.Instance.IsActive)
            return;
        CardManager.Instance.ToggleMode();
        SFXManager.PlaySound(GlobalSFX.CardViewModeChanged);
    }

    public override void OnHoverStay(Vector2 uv)
    {
    }

    public override void OnRightClick()
    {
    }
}

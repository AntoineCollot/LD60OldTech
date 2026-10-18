using UnityEngine;

namespace MiniGame
{
    public class MiniGameNotepad : MiniGameEntity
    {
        [TextArea(7,15), SerializeField] string text;
        bool isDisplayed;

        protected override void OnGameReset()
        {
            base.OnGameReset();
            isDisplayed = false;
        }

        protected override void OnGameLateUpdate(MiniGameInput input)
        {
            if(MiniGamePlayer.Instance.currentCoords == currentCoords && !isDisplayed)
            {
                isDisplayed = true;
                MiniGameNotepadDisplay.Instance.Display(text);
            }
            else if(MiniGamePlayer.Instance.currentCoords != currentCoords && isDisplayed)
            {
                isDisplayed = false;
                MiniGameNotepadDisplay.Instance.Hide();
            }
        }
    }
}

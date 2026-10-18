using TMPro;
using UnityEngine;

namespace MiniGame
{
    public class MiniGameNotepadDisplay : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] TextMeshProUGUI text;
        public static MiniGameNotepadDisplay Instance;

        void Awake()
        {
            Instance = this;
            panel.SetActive(false);
        }

        private void Start()
        {
            MiniGameController.onGameReset += OnGameReset;
        }

        private void OnDestroy()
        {
            MiniGameController.onGameReset -= OnGameReset;
        }

        private void OnGameReset()
        {
            Hide();
        }

        public void Display(string notepadInput)
        {
            panel.SetActive(true);
            text.text = notepadInput;
        }

        public void Hide()
        {
            panel.SetActive(false);
        }
    }
}

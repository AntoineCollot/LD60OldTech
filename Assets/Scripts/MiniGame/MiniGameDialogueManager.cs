using MiniGame;
using TMPro;
using UnityEngine;

namespace MinigGame
{
    public class MiniGameDialogueManager : MonoBehaviour
    {
        [SerializeField] GameObject dialoguePanel;
        [SerializeField] UITypewriter greetingsTypewriter;
        [SerializeField] TextMeshProUGUI playerInputText;
        [SerializeField] UITypewriter reactionTypewriter;

        string playerInputString;
        const int MAX_PLAYER_STRING = 8;

        MiniGameTalkable currentTalkable;
        public bool IsDialogueOn => currentTalkable != null;
        bool hasReactionBeenPerformed;

        public static MiniGameDialogueManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            MiniGameController.onMiniGameUpdate += OnMiniGameUpdate;
        }

        private void OnDisable()
        {
            MiniGameController.onMiniGameUpdate -= OnMiniGameUpdate;
        }

        void Start()
        {
            dialoguePanel.SetActive(false);
        }

        private void OnMiniGameUpdate(MiniGameInput input)
        {
            switch (input.command)
            {
                case MiniGameCommand.Talk:
                    Vector2Int playerCoords = MiniGamePlayer.Instance.currentCoords;
                    TryEnteringDialogue(playerCoords);
                    return;
                case MiniGameCommand.Leave:
                    LeaveDialogue();
                    return;
            }

            if (IsDialogueOn && !hasReactionBeenPerformed)
                AddPlayerLetterInput(input.letter);
        }

        bool TryEnteringDialogue(Vector2Int fromCoords)
        {
            //Check if has someone nearby
            if (!MiniGameMap.Instance.TryGetAdjacentEntity(fromCoords, out MiniGameEntity adjacent))
                return false;

            //Check if talkable
            if (!adjacent.TryGetComponent(out MiniGameTalkable talkable))
                return false;

            EnterDialgoue(talkable);
            return true;
        }

        void EnterDialgoue(MiniGameTalkable talkable)
        {
            //Clear
            playerInputString = "";
            hasReactionBeenPerformed = false;

            currentTalkable = talkable;

            //Display the dialogue panel
            reactionTypewriter.Clear();
            greetingsTypewriter.Clear();
            playerInputText.text = "";
            dialoguePanel.SetActive(true);

            greetingsTypewriter.DisplayText(talkable.dialogueData.greetings);
        }

        public void LeaveDialogue()
        {
            currentTalkable.LeavingDialogue(hasReactionBeenPerformed);

            currentTalkable = null;
            playerInputString = "";
            hasReactionBeenPerformed = false;
            dialoguePanel.SetActive(false);
        }

        void AddPlayerLetterInput(char letter)
        {
            playerInputText.enabled = true;

            //Max reached
            if (playerInputString != null && playerInputString.Length >= MAX_PLAYER_STRING)
                return;

            //Add letter
            playerInputString += letter;
            playerInputText.text = "> " + playerInputString;

            if (currentTalkable.IsPlayerInputCorrect(playerInputString))
            {
                DisplayReaction();
            }
        }

        void DisplayReaction()
        {
            hasReactionBeenPerformed = true;
            reactionTypewriter.DisplayText(currentTalkable.dialogueData.reaction);
        }
    }
}

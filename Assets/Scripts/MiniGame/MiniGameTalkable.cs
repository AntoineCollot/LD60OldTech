using UnityEngine;

namespace MiniGame
{
    public class MiniGameTalkable : MonoBehaviour
    {
        public DialogueData dialogueData;

        public enum ReactionCommand { None, MoveUp, MoveRight, MoveDown, MoveLeft}
        public ReactionCommand reactionCommand;

        public bool IsPlayerInputCorrect(string str)
        {
            return dialogueData.listenFor.ToUpper() == str.ToUpper();
        }

        public void LeavingDialogue(bool hasReactionBeenPerformed)
        {
            if(hasReactionBeenPerformed)
                PerformReactionCommand(reactionCommand);
        }

        void PerformReactionCommand(ReactionCommand command)
        {
            MiniGameEntity entity = GetComponent<MiniGameEntity>();
            switch (command)
            {
                case ReactionCommand.MoveUp:
                    entity.Move(Direction.Up);
                    break;
                case ReactionCommand.MoveRight:
                    entity.Move(Direction.Right);
                    break;
                case ReactionCommand.MoveDown:
                    entity.Move(Direction.Down);
                    break;
                case ReactionCommand.MoveLeft:
                    entity.Move(Direction.Left);
                    break;
                default:
                    break;
            }
        }
    }

    [System.Serializable]
    public struct DialogueData
    {
        public string greetings;
        public string listenFor;
        public string reaction;
    }
}

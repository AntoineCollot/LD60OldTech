using MinigGame;
using UnityEngine;

namespace MiniGame
{
    public class MiniGamePlayer : MiniGameEntity
    {
        public static MiniGamePlayer Instance;

        override  protected void Awake()
        {
#if !UNITY_EDITOR //Reset pos
            ResetPos();
#endif

            base.Awake();
            Instance = this;
        }

        protected override void OnGameUpdate(MiniGameInput input)
        {
            //Don't move during dialogues
            if (MiniGameDialogueManager.Instance.IsDialogueOn)
                return;

            if (IsCommandMovement(input.command, out Direction direction))
            {
                bool hasMoved = Move(direction);
                if(hasMoved)
                {
                    MiniGameController.Instance.MarkInputAsUsed();
                    SFXManager.PlaySound(GlobalSFX.MiniGameMove);
                }
            }
        }

        [ContextMenu("ResetPos")]
        public void ResetPos()
        {
            transform.position = Vector2.one * 0.5f;
        }

        bool IsCommandMovement(MiniGameCommand cmd, out Direction direction)
        {
            switch (cmd)
            {
                case MiniGameCommand.MoveUp:
                    direction = Direction.Up;
                    break;
                case MiniGameCommand.MoveRight:
                    direction = Direction.Right;
                    break;
                case MiniGameCommand.MoveDown:
                    direction = Direction.Down;
                    break;
                case MiniGameCommand.MoveLeft:
                    direction = Direction.Left;
                    break;
                default:
                    direction = Direction.Up;
                    return false;
            }

            return true;
        }

        protected override void TriedToMoveIntoWall()
        {
            MiniGameController.Instance.ThrowError();
        }
    }
}
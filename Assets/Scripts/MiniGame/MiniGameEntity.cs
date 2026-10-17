using UnityEngine;

namespace MiniGame
{
    public abstract class MiniGameEntity : MonoBehaviour
    {
        public Vector2Int currentCoords;
        public Direction lastMoveDirection { get; private set; }

        protected virtual void Awake()
        {
            currentCoords = transform.position.ToCoords();
            UpdatePosition();
        }

        protected virtual void OnEnable()
        {
            MiniGameController.onMiniGameUpdate += OnGameUpdate;
            MiniGameMap.Instance.Register(this);
        }

        protected virtual void OnDisable()
        {
            MiniGameController.onMiniGameUpdate -= OnGameUpdate;
            MiniGameMap.Instance.Deregister(this);
        }

        protected virtual void OnGameUpdate(MiniGameInput input)
        {
        }

        public void Move(Direction direction)
        {
            Vector2Int displacement;
            switch (direction)
            {
                case Direction.Up:
                default:
                    displacement = Vector2Int.up;
                    break;
                case Direction.Right:
                    displacement = Vector2Int.right;
                    break;
                case Direction.Down:
                    displacement = Vector2Int.down;
                    break;
                case Direction.Left:
                    displacement = Vector2Int.left;
                    break;
            }

            Vector2Int targetCoords = currentCoords + displacement;

            //Check if move possible
            if (!CanMoveToCoords(targetCoords))
                return;

            //Effective move
            lastMoveDirection = direction;
            currentCoords = targetCoords;
            UpdatePosition();
        }

        protected void UpdatePosition()
        {
            transform.position = (Vector2)currentCoords + Vector2.one * 0.5f;
        }

        protected bool CanMoveToCoords(Vector2Int coords)
        {
            return !MiniGameMap.Instance.HasAnyObstacle(coords);
        }
    }
}

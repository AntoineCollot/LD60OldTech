using System;
using UnityEngine;

namespace MiniGame
{
    public abstract class MiniGameEntity : MonoBehaviour
    {
        public Vector2Int originalCoords;
        public Vector2Int currentCoords;
        public Direction lastMoveDirection { get; private set; }
        public bool isObstacle = true;

        protected virtual void Awake()
        {
            currentCoords = transform.position.ToCoords();
            originalCoords = currentCoords;
            UpdatePosition();

            MiniGameController.onGameReset += OnGameReset;
        }

        protected void OnDestroy()
        {
            MiniGameController.onGameReset -= OnGameReset;
        }

        protected virtual void OnEnable()
        {
            MiniGameController.onMiniGameUpdate += OnGameUpdate;
            MiniGameController.onMiniGameLateUpdate += OnGameLateUpdate;
            if (isObstacle)
                MiniGameMap.Instance.Register(this);
        }

        protected virtual void OnDisable()
        {
            MiniGameController.onMiniGameUpdate -= OnGameUpdate;
            MiniGameController.onMiniGameLateUpdate -= OnGameLateUpdate;
            if (isObstacle)
                MiniGameMap.Instance.Deregister(this);
        }

        protected virtual void OnGameReset()
        {
            currentCoords = originalCoords;
            UpdatePosition();
        }

        protected virtual void OnGameUpdate(MiniGameInput input)
        {
        }

        protected virtual void OnGameLateUpdate(MiniGameInput input)
        {
        }

        protected virtual void TriedToMoveIntoWall()
        {
        }

        public bool Move(Direction direction)
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
            {
                TriedToMoveIntoWall();
                return false;
            }

            //Effective move
            lastMoveDirection = direction;
            currentCoords = targetCoords;
            UpdatePosition();

            return true;
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

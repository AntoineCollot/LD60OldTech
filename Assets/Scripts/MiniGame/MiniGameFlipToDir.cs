using UnityEngine;

namespace MiniGame
{
    public class MiniGameFlipToDir : MonoBehaviour
    {
        MiniGameEntity entity;
        SpriteRenderer rend;

        void Start()
        {
            entity = GetComponentInParent<MiniGameEntity>();
            rend = GetComponent<SpriteRenderer>();
        }

        void LateUpdate()
        {
            switch (entity.lastMoveDirection)
            {
                case Direction.Right:
                    rend.flipX = true;
                    break;
                case Direction.Left:
                    rend.flipX = false;
                    break;
                default:
                    break;
            }
        }
    }
}

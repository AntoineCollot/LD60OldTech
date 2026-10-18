using UnityEngine;

namespace MiniGame
{
    public class MiniGameHeart : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.position = (MiniGamePlayer.Instance.currentCoords + Vector2Int.up).ToCenterOfTile();
        }
    }
}

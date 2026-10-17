using UnityEngine;

namespace MiniGame
{
    public class MiniGameCamController : MonoBehaviour
    {

        void Update()
        {
            MoveToCoords(MiniGamePlayer.Instance.currentCoords);
        }

        void MoveToCoords(Vector2Int coords)
        {
            //Convert into level coords
            coords = GetLevelCoords(coords);
            SetCameraAtLevelCorods(coords);
        }

        Vector2Int GetLevelCoords(Vector2Int coords)
        {
            //Move coords from -4 to 3 -> 0 to 7
            coords.x += MiniGameMap.LEVEL_SIZE / 2;
            coords.y += MiniGameMap.LEVEL_SIZE / 2;

            if (coords.x < 0)
                coords.x -= MiniGameMap.LEVEL_SIZE - 1;
            if (coords.y < 0)
                coords.y -= MiniGameMap.LEVEL_SIZE - 1;

            coords.x /= MiniGameMap.LEVEL_SIZE;
            coords.y /= MiniGameMap.LEVEL_SIZE;

            return coords;
        }

        void SetCameraAtLevelCorods(Vector2Int levelCoords)
        {
            transform.position = (Vector2)levelCoords * MiniGameMap.LEVEL_SIZE;
            transform.position -= Vector3.forward; //Move back one in z
        }
    }
}

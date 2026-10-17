using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MiniGame
{
    [DefaultExecutionOrder(-10)] // Before entities
    public class MiniGameMap : MonoBehaviour
    {
        public const int LEVEL_SIZE = 6;

        Tilemap tilemap;
        public static MiniGameMap Instance;

        HashSet<MiniGameEntity> entities;

        void Awake()
        {
            Instance = this;
            tilemap = GetComponentInChildren<Tilemap>();
            entities = new();
        }

        public bool HasAnyObstacle(Vector2Int coords)
        {
            return HasTile(coords) || HasEntity(coords);
        }

        public bool HasEntity(Vector2Int coords)
        {
            foreach (MiniGameEntity entity in entities)
            {
                if (entity.currentCoords == coords)
                    return true;
            }

            return false;
        }

        public bool HasTile(Vector2Int coords)
        {
            Vector3Int gridPosition = tilemap.WorldToCell(coords.ToCenterOfTile());
            return tilemap.HasTile(gridPosition);
        }

        public void Register(MiniGameEntity entity)
        {
            entities.Add(entity);
        }

        public void Deregister(MiniGameEntity entity)
        {
            entities.Remove(entity);
        }

        public bool TryGetAdjacentEntity(Vector2Int coords, out MiniGameEntity adjacentEntity)
        {
            adjacentEntity = null;
            //Vector2Int[] adjacents = new Vector2Int[4];
            //adjacents[0] = coords + Vector2Int.up;
            //adjacents[1] = coords + Vector2Int.right;
            //adjacents[2] = coords + Vector2Int.down;
            //adjacents[3] = coords + Vector2Int.left;

            foreach (MiniGameEntity entity in entities)
            {
                if (coords.GridDistance(in entity.currentCoords) != 1)
                    continue;

                adjacentEntity = entity;
                return true;
            }

            return false;
        }
    }
}

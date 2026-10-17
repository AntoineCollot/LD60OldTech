using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector2Int ToCoords(this Vector3 v)
    {
        return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
    }

    public static Vector2 ToCenterOfTile(this Vector2Int v)
    {
        return (Vector2)v + Vector2.one * 0.5f;
    }

    public static Vector2Int ToDirection(this Vector2Int v)
    {
        if (Mathf.Abs(v.x) > Mathf.Abs(v.y))
        {
            if (v.x > 0)
                return Vector2Int.right;
            else
                return Vector2Int.left;
        }
        else
        {
            if (v.y > 0)
                return Vector2Int.up;
            else
                return Vector2Int.down;
        }
    }

    public static Direction ToDirectionEnum(this Vector2Int v)
    {
        if (Mathf.Abs(v.x) > Mathf.Abs(v.y))
        {
            if (v.x > 0)
                return Direction.Right;
            else
                return Direction.Left;
        }
        else
        {
            if (v.y > 0)
                return Direction.Up;
            else
                return Direction.Down;
        }
    }

    public static int GridLength(this Vector2Int v)
    {
        return Mathf.Abs(v.x) + Mathf.Abs(v.y);
    }

    public static int GridDistance(this Vector2Int v, in Vector2Int other)
    {
        return Mathf.Abs(v.x - other.x) + Mathf.Abs(v.y - other.y);
    }

    /// <summary>
    /// Distance when allowing diagonal movements
    /// </summary>
    public static int DiagonalDistance(this Vector2Int v, in Vector2Int other)
    {
        return Mathf.Max(Mathf.Abs(v.x - other.x), Mathf.Abs(v.y - other.y));
    }

    public static bool IsAdjacent(this Vector2Int v, in Vector2Int other)
    {
        return v.GridDistance(in other) == 1;
    }

    public static bool IsAligned(this Vector2Int v, in Vector2Int other)
    {
        return v.x == other.x || v.y == other.y;
    }

    /// <summary>
    /// Get the rotation in degree given by looking at a grid pos.
    /// </summary>
    public static float LookRotation(this Vector2Int fromGridPos, Vector2Int lookAtGridPos, bool forceInLine = false)
    {
        Vector2Int dir = lookAtGridPos - fromGridPos;

        //If we shall force in line, make the dir one component only (remove smallest between x and y)
        if (forceInLine)
        {
            dir = dir.ToDirection();
        }
        Quaternion targetRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.y));
        return targetRot.eulerAngles.y;
    }

    public static Direction To2DDirectionEnum(this Vector3 v)
    {
        if (Mathf.Abs(v.x) > Mathf.Abs(v.z))
        {
            if (v.x > 0)
                return Direction.Right;
            else
                return Direction.Left;
        }
        else
        {
            if (v.z > 0)
                return Direction.Up;
            else
                return Direction.Down;
        }
    }

    /// <summary>
    /// From a rotation, get the closest gridpos we are looking at.
    /// </summary>
    public static Vector2Int GetLookedGridPos(this Vector2Int gridPos, float rotation)
    {
        rotation /= 90;
        int dir = Mathf.RoundToInt(rotation) % 4;
        switch (dir)
        {
            case 0:
            default:
                return gridPos + Vector2Int.up;
            case 1:
                return gridPos + Vector2Int.right;
            case 2:
                return gridPos + Vector2Int.down;
            case 3:
                return gridPos + Vector2Int.left;
        }
    }

    public static Vector2 Rotate(this Vector2 vec, float angle)
    {
        float newAngle = Mathf.Atan2(vec.y, vec.x) + angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));
    }


    public static Vector3 FlatRotate(this Vector3 vec, float angle)
    {
        float newAngle = Mathf.Atan2(vec.z, vec.x) + angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(newAngle),0, Mathf.Sin(newAngle));
    }
}

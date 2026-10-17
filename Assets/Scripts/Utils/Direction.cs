using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}

static class DirectionExtensions
{
    public static Vector2Int ToVector(this Direction dir)
    {
        switch (dir)
        {
            default: return Vector2Int.up;
            case Direction.Right: return Vector2Int.right;
            case Direction.Down: return Vector2Int.down;
            case Direction.Left: return Vector2Int.left;
        }
    }

    public static Direction PerpendicularClockwise(this Direction dir)
    {
       return (Direction)(((int)dir + 1)%4);
    }

    public static Direction PerpendicularCounterClockwise(this Direction dir)
    {
        int dirId = (int)dir - 1;
        if(dirId<0)
            return Direction.Left;
        return (Direction)(dirId);
    }

    public static Direction Inverse(this Direction dir)
    {
        return (Direction)(((int)dir + 2) % 4);
    }
}
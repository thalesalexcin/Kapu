using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller2D : RaycastController 
{
    public CollisionInfo Collisions;

    public void Move(Vector2 velocity)
    {
        UpdateRaycastOrigins();
        Collisions.Reset();
        Collisions.Velocity = velocity;

        _HandleMove();
    }

    private void _HandleMove()
    {
        HorizontalCollisions();
        if (Collisions.Velocity.y != 0)
            VerticalCollisions();

        transform.Translate(Collisions.Velocity);
    }

    private void VerticalCollisions()
    {
        float rayLength = Mathf.Abs(Collisions.Velocity.y) + SkinWidth;
        float directionY = Mathf.Sign(Collisions.Velocity.y);

        for (int i = 0; i < VerticalRayCount; i++)
        {
            _CalculateBottomCollisions(directionY, rayLength, i);
            _CalculateTopCollisions(directionY, rayLength, i);
        }
    }

    private void _CalculateBottomCollisions(float directionY, float rayLength, int i)
    {
        Vector2 rayOrigin = Origins.bottomLeft + Vector2.right * (VerticalRaySpacing * i + Collisions.Velocity.x);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, CollisionMask);

        Debug.DrawRay(rayOrigin, Vector2.down * rayLength, Color.red);

        if (hit)
        {
            var zero = (hit.distance - SkinWidth) * directionY;
            Collisions.Velocity.y = Mathf.Max(zero, Collisions.Velocity.y);
            Collisions.Below = true;
        }
    }

    private void _CalculateTopCollisions(float directionY, float rayLength, int i)
    {
        Vector2 rayOrigin = Origins.topLeft + Vector2.right * (VerticalRaySpacing * i + Collisions.Velocity.x);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, CollisionMask);

        Debug.DrawRay(rayOrigin, Vector2.down * rayLength, Color.red);

        if (hit)
        {
            var zero = (hit.distance - SkinWidth) * directionY;
            Collisions.Velocity.y = Mathf.Min(zero, Collisions.Velocity.y);
            Collisions.Above = true;
        }
    }

    private void HorizontalCollisions()
    {
        float rayLength = Mathf.Abs(Collisions.Velocity.y) + SkinWidth;
        float directionX = Mathf.Sign(Collisions.Velocity.x);

        for (int i = 0; i < HorizontalRayCount; i++)
            _CalculateRightCollisions(rayLength, directionX, i);
    }

    private void _CalculateRightCollisions(float rayLength, float directionX, int i)
    {
        Vector2 rayOrigin = Origins.bottomRight + Vector2.up * (HorizontalRaySpacing * i);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, rayLength, CollisionMask);

        Debug.DrawRay(rayOrigin, Vector2.right * rayLength, Color.red);

        if (hit)
        {
            var zero = (hit.distance - SkinWidth) * directionX;
            Collisions.Velocity.x = Mathf.Min(zero, Collisions.Velocity.x);
            Collisions.Right = true;
        }
    }
}

public struct CollisionInfo
{
    public bool Above, Below;
    public bool Left, Right;
    public Vector3 Velocity;

    public void Reset()
    {
        Above = Below = false;
        Left = Right = false;
    }
}

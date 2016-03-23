using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class RaycastController : MonoBehaviour
{
    public const float SkinWidth = .015f;

    public LayerMask CollisionMask;
    public int HorizontalRayCount = 4;
    public int VerticalRayCount = 4;

    public BoxCollider2D Collider { get; set; }
    public float HorizontalRaySpacing { get; set; }
    public float VerticalRaySpacing { get; set; }

    [HideInInspector]
    public RaycastOrigins Origins;

    public virtual void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = Collider.bounds;
        bounds.Expand(SkinWidth * -2);

        Origins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        Origins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        Origins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        Origins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpacing()
    {
        Bounds bounds = Collider.bounds;
        bounds.Expand(SkinWidth * -2);

        HorizontalRayCount = Mathf.Clamp(HorizontalRayCount, 2, int.MaxValue);
        VerticalRayCount = Mathf.Clamp(VerticalRayCount, 2, int.MaxValue);

        HorizontalRaySpacing = bounds.size.y / (HorizontalRayCount - 1);
        VerticalRaySpacing = bounds.size.x / (VerticalRayCount - 1);
    }

    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}

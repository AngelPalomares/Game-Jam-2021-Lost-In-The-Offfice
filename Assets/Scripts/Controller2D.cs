using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    private const float SKIN_WIDTH = 0.015f;

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    private Rigidbody2D rb;

    private float _horizontalRaySpacing;
    private float _verticalRaySpacing;

    public float maxSlopeAngle = 65;




    private new BoxCollider2D collider;
    [SerializeField] private RaycastOrigins raycastOrigins;
    [SerializeField]
    public CollisionInfo collisions;

    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private LayerMask collisionMaskMoveable;
    [SerializeField] private LayerMask collisionMaskMoveableDown;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        UpdateRaycastOrigins();
        CalculateRaySpacing();

    }

    public void Fly(Vector2 velocity)
    {
        transform.Translate(velocity, Space.Self);
    }

    public void Update()
    {
        if (rb.velocity.y < +5)
        {
           // rb.velocity = new Vector2(rb.velocity.x, -5);
        }          

    }
    public void Move(Vector2 velocity)
    {
        UpdateRaycastOrigins();
        CalculateRaySpacing();
        collisions.Reset();
        collisions.VelocityOld = velocity;


        if (velocity.y <0)
        {
            DescendSlope(ref velocity);
        }

        if (Math.Abs(velocity.x) > 0)
        {
            HorizontalCollisions(ref velocity);
        }
        if (Math.Abs(velocity.y) > 0)
        {
            VerticalCollisions(ref velocity);
        }


        transform.Translate(velocity, Space.Self);
    }


    private void HorizontalCollisions(ref Vector2 velocity)
    {
        var directionX = Mathf.Sign(velocity.x);
        var rayLength = Mathf.Abs(velocity.x) + SKIN_WIDTH;

        for (var i = 0; i < horizontalRayCount; i++)
        {
            var rayOrigin = (directionX == -1) ? raycastOrigins.BottomLeft : raycastOrigins.BottomRight;
            rayOrigin += (Vector2)transform.up * (_horizontalRaySpacing * i);
            var hit = Physics2D.Raycast(rayOrigin, transform.right * directionX, rayLength, collisionMask);


            Debug.DrawRay(rayOrigin, transform.right * directionX * rayLength, Color.blue);

            if (hit)
            {

                var slopeAngle = Vector2.Angle(hit.normal, transform.up);

                if (i == 0 && slopeAngle <= maxSlopeAngle)
                {
                    if (collisions.DescendingSlope)
                    {
                        collisions.DescendingSlope = false;
                        velocity = collisions.VelocityOld;
                    }
                    float distanceToSlopeStart = 0;
                    if (slopeAngle != collisions.SlopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - SKIN_WIDTH;
                        velocity.x -= distanceToSlopeStart * directionX;
                    }

                    ClimbSlope(ref velocity, slopeAngle);
                    velocity.x += distanceToSlopeStart * directionX;
                }

                if (!collisions.ClimbingSlope || slopeAngle > maxSlopeAngle)
                {

                    velocity.x = (hit.distance - SKIN_WIDTH) * directionX;
                    rayLength = hit.distance;

                    if (collisions.ClimbingSlope)
                    {
                        velocity.y = Mathf.Tan(collisions.SlopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    collisions.Left = directionX == -1;
                    collisions.Right = directionX == 1;
                }
            }
        }
    }

    private void VerticalCollisions(ref Vector2 velocity)
    {
        var directionY = Mathf.Sign(velocity.y);
        var rayLength = Mathf.Abs(velocity.y) + SKIN_WIDTH;

        for (var i = 0; i < verticalRayCount; i++)
        {
            var rayOrigin = (directionY == -1) ? raycastOrigins.BottomLeft : raycastOrigins.TopLeft;
            rayOrigin += (Vector2)transform.right * (_verticalRaySpacing * i + velocity.x);
            var hit = directionY>0?Physics2D.Raycast(rayOrigin, transform.up * directionY, rayLength, collisionMaskMoveable) : Physics2D.Raycast(rayOrigin, transform.up * directionY, rayLength, collisionMaskMoveableDown);;


            Debug.DrawRay(rayOrigin, transform.up * directionY * rayLength, Color.red);

            if (hit)
            {
                velocity.y = (hit.distance - SKIN_WIDTH) * directionY;
                rayLength = hit.distance;


                if (collisions.ClimbingSlope)
                {
                    velocity.x = velocity.y / Mathf.Tan(collisions.SlopeAngle* Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }

                collisions.Above = directionY == 1;
                collisions.Below = directionY == -1;
            }
        }

        if (collisions.ClimbingSlope)
        {
            var dirX = Mathf.Sign(velocity.x);
            rayLength = Mathf.Abs(velocity.x) + SKIN_WIDTH;
            var rayOrigin = ((dirX == -1) ? raycastOrigins.BottomLeft : raycastOrigins.BottomRight) + (Vector2)transform.up *velocity.y;

            var hit = Physics2D.Raycast(rayOrigin, transform.right * dirX, rayLength, collisionMask);
            if (hit)
            {
                var slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (slopeAngle != collisions.SlopeAngle)
                {
                    velocity.x = (hit.distance - SKIN_WIDTH) * dirX;
                    collisions.SlopeAngle = slopeAngle;
                }
            }
        }
    }


    private void ClimbSlope(ref Vector2 velocity, float slopeAngle)
    {
        var moveDistance = Mathf.Abs(velocity.x);
        var climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
        if (velocity.y <= climbVelocityY)
        {
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);


            collisions.Below = true;
            collisions.ClimbingSlope = true;
            collisions.SlopeAngle = slopeAngle;
        }
    }

    private void DescendSlope(ref Vector2 velocity)
    {
        var dirX = Mathf.Sign(velocity.x);
        var rayOrigin = ((dirX == 1) ? raycastOrigins.BottomLeft : raycastOrigins.BottomRight);


        var hit = Physics2D.Raycast(rayOrigin, -transform.up, Mathf.Infinity, collisionMask);

        if (hit)
        {
            var slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (Math.Abs(slopeAngle) > 0 && slopeAngle <= maxSlopeAngle)
            {
                if (Mathf.Sign(hit.normal.x) == dirX)
                {
                    if (hit.distance - SKIN_WIDTH <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
                    {
                        var moveDist = Mathf.Abs(velocity.x);
                        var descendVelY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDist;
                        velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDist * Mathf.Sign(velocity.x);
                        velocity.y -= descendVelY;

                        collisions.SlopeAngle = slopeAngle;
                        collisions.Below = true;
                        collisions.DescendingSlope = true;
                    }
                }
            }
        }

    }

    private void UpdateRaycastOrigins()
    {
        var top = collider.offset.y + (collider.size.y / 2f)-SKIN_WIDTH;
        var bottom = collider.offset.y - (collider.size.y / 2f)+SKIN_WIDTH;
        var left = collider.offset.x - (collider.size.x / 2f)+SKIN_WIDTH;
        var right = collider.offset.x + (collider.size.x / 2f)-SKIN_WIDTH;

        raycastOrigins.TopLeft.x = left;
        raycastOrigins.TopLeft.y = top;

        raycastOrigins.TopRight.x = right;
        raycastOrigins.TopRight.y = top;

        raycastOrigins.BottomLeft.x = left;
        raycastOrigins.BottomLeft.y = bottom;

        raycastOrigins.BottomRight.x = right;
        raycastOrigins.BottomRight.y = bottom;

        raycastOrigins.TopLeft = transform.TransformPoint(raycastOrigins.TopLeft);
        raycastOrigins.TopRight = transform.TransformPoint(raycastOrigins.TopRight);
        raycastOrigins.BottomLeft = transform.TransformPoint(raycastOrigins.BottomLeft);
        raycastOrigins.BottomRight = transform.TransformPoint(raycastOrigins.BottomRight);



        //Bounds bounds = collider.bounds;
        //bounds.Expand(skinWidth * -2);

        //raycastOrigins.BottomLeft = transform.TransformPoint(new Vector2(bounds.min.x, bounds.min.y));
        //raycastOrigins.BottomRight = transform.TransformPoint(new Vector2(bounds.max.x, bounds.min.y));
        //raycastOrigins.TopLeft = transform.TransformPoint(new Vector2(bounds.min.x, bounds.max.y));
        //raycastOrigins.TopRight = transform.TransformPoint(new Vector2(bounds.max.x, bounds.max.y));

    }

    private void CalculateRaySpacing()
    {
        var bounds = collider.bounds;
        bounds.Expand(SKIN_WIDTH * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        _horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        _verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }


    private struct RaycastOrigins
    {
        public Vector2 TopLeft;
        public Vector2 TopRight;
        public Vector2 BottomLeft;
        public Vector2 BottomRight;
    }

    public struct CollisionInfo
    {
        public bool Above, Below;
        public bool Left, Right;

        public bool ClimbingSlope;
        public bool DescendingSlope;
        public float SlopeAngle, SlopeAngleOld;
        public Vector2 VelocityOld;


        public void Reset()
        {
            Left = Right = false;
            Above = Below = false;
            ClimbingSlope = false;
            DescendingSlope = false;
            SlopeAngleOld = SlopeAngle;
            SlopeAngle = 0;
        }
    }
}
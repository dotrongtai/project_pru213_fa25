using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [Header("Boss Settings")]
    public float moveSpeed = 3f;          // Tốc độ di chuyển
    public bool moveRight = true;         // Hướng ban đầu

    [Header("Ground Check")]
    public Transform wallCheck;           // Vị trí kiểm tra va chạm tường
    public float wallCheckDistance = 0.5f;
    public LayerMask wallLayer;           // Layer của tường (gán trong Inspector)

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        CheckWallCollision();
    }

    private void Move()
    {
        float direction = moveRight ? 1 : -1;
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    private void CheckWallCollision()
    {
        // Bắn 1 tia Raycast theo hướng di chuyển để phát hiện tường
        Vector2 dir = moveRight ? Vector2.right : Vector2.left;
        RaycastHit2D hitWall = Physics2D.Raycast(wallCheck.position, dir, wallCheckDistance, wallLayer);

        if (hitWall.collider != null)
        {
            Flip();
        }
    }

    private void Flip()
    {
        moveRight = !moveRight;
        sprite.flipX = !sprite.flipX; // Lật hình boss
    }

    private void OnDrawGizmosSelected()
    {
        // Vẽ đường ray để dễ debug
        if (wallCheck != null)
        {
            Gizmos.color = Color.red;
            Vector2 dir = moveRight ? Vector2.right : Vector2.left;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)dir * wallCheckDistance);
        }
    }
}

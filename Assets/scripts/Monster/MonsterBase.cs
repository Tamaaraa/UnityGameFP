using System;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    Transform player;
    private float timer;
    private Animator animator;
    MonsterStats monster;

    BoxCollider2D boxCollider;
    Vector2 colliderSize;

    public LayerMask obstacleMask;
    public float detectionDistance = 3f;

    // direction monster takes around an obstacle
    private Vector2 detourDirection = Vector2.zero;
    private float detourTimer = 0f;

    // How long monsters will try to detour around obstacles
    private float detourDuration = 0.5f;
    private PlayerStats playerStats;

    protected virtual void Start()
    {
        monster = GetComponent<MonsterStats>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
        playerStats = FindObjectOfType<PlayerStats>();
        timer = monster.currentAttackSpeed;
        // Get boxcollider size and scale it up slightly to avoid getting stuck on obstacles
        boxCollider = GetComponent<BoxCollider2D>();
        colliderSize = Vector2.Scale(boxCollider.size, transform.localScale) + (Vector2.one * 0.2f);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Attack();
        }

        Vector2 dirToPlayer = (player.position - transform.position).normalized;

        if (IsPathClear(dirToPlayer))
        {
            detourDirection = Vector2.zero;
            detourTimer = 0f;

            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                monster.currentSpeed * Time.deltaTime
            );
        }
        else
        {
            if (detourDirection == Vector2.zero || detourTimer <= 0f)
            {
                detourDirection = FindBestRoute(dirToPlayer);
                detourTimer = detourDuration;
            }

            transform.position += (Vector3)(
                monster.currentSpeed * Time.deltaTime * detourDirection
            );
            detourTimer -= Time.deltaTime;
        }

        Vector2 movementDirection = (player.transform.position - transform.position).normalized;
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
    }

    protected virtual void Attack()
    {
        timer = monster.currentAttackSpeed;
        if (Vector2.Distance(transform.position, player.position) <= 1.5f)
        {
            playerStats.TakeDamage(monster.currentDamage);
        }
    }

    bool IsPathClear(Vector2 direction)
    // Check if the path in the given direction is clear or if there are obstacles
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position,
            colliderSize,
            0f,
            direction,
            detectionDistance,
            obstacleMask
        );
        return hit.collider == null;
    }

    Vector2 FindBestRoute(Vector2 dir)
    {
        Vector2 bestAngle = Vector2.zero;
        float shortestDistance = Mathf.Infinity;
        float[] angles = { 10f, -10f, 20f, -20f, 40f, -40f, 65f, -65f, 90f, -90f, 110f, -110f };
        foreach (float angle in angles)
        {
            // Rotate directions, check if the path is clear and if it is, calculate distance to player, and choose the best
            Vector2 newDir = Quaternion.Euler(0, 0, angle) * dir;
            if (IsPathClear(newDir))
            {
                float distanceToPlayer = Vector2.Distance(
                    transform.position + (Vector3)newDir,
                    player.position
                );
                if (distanceToPlayer < shortestDistance)
                {
                    shortestDistance = distanceToPlayer;
                    bestAngle = newDir;
                }
            }
        }
        if (bestAngle == Vector2.zero)
        {
            return dir;
        }
        return bestAngle;
    }
}

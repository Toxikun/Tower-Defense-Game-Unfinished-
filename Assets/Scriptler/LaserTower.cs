using UnityEngine;

public class LaserTower : MonoBehaviour
{
    [Header("Attack Settings")]
    public float range = 15f;                  // Tower range
    public float attackCooldown = 1.5f;        // Attack cooldown (seconds)
    public Transform laserStartPoint;          // Start point of the laser
    public LineRenderer laserLine;              // Line renderer for the laser effect
    public float rotationSpeed = 10f;

    [Header("Damage Settings")]
    public float initialDamage = 0.1f;         // Initial damage of the laser
    public float damageIncreasePerHit = 0.05f; // Damage increase per hit
    public float damageIncreaseCooldown = 1.0f; // Cooldown for damage increase
    private float currentDamage;                // Current damage of the laser
    private Transform target;                   // Current target
    private float cooldownTimer = 0f;
    private float damageIncreaseTimer = 0f;    // Timer for damage increase

    void Start()
    {
        currentDamage = initialDamage; // Initialize current damage
        laserLine.enabled = false; // Start with laser disabled
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
        damageIncreaseTimer += Time.deltaTime;

        if (target == null)
        {
            FindNearestEnemy();
        }

        if (target != null)
        {
            RotateTowards(target);
            if (cooldownTimer >= attackCooldown)
            {
                Shoot();
                cooldownTimer = 0f;
            }
        }
        else
        {
            // Disable the laser if no target is found
            laserLine.enabled = false;
        }
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemyTag");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void RotateTowards(Transform targetTransform)
    {
        // Rotate towards the target
        Vector3 direction = targetTransform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        // Enable the laser and set its start and end points
        laserLine.enabled = true;
        laserLine.SetPosition(0, laserStartPoint.position);
        laserLine.SetPosition(1, target.position);

        // Deal damage to the target
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(currentDamage); // Inflict current damage

            // Increase damage after the cooldown has elapsed
            if (damageIncreaseTimer >= damageIncreaseCooldown)
            {
                currentDamage += damageIncreasePerHit; // Increase damage for next hit
                damageIncreaseTimer = 0f; // Reset the cooldown timer
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Show range in scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

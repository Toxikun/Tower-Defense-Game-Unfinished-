using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Saldýrý Ayarlarý")]
    public float range = 15f;                  // Kule menzili
    public float attackCooldown = 1.5f;        // Saldýrý aralýðý (saniye)
    public GameObject bulletPrefab;            // Mermi prefab'ý
    public Transform firePoint;                // Mermilerin çýkýþ noktasý
    public float rotationSpeed = 10f;

    [Header("Referanslar")]
    public string enemyTag = "Enemy";           // Düþmanlarýn tag'ý
    public Transform partToRotate;              // Döndürülmesi gereken kýsým (örn: kule baþý)

    private float cooldownTimer = 0f;
    private Transform target;
    

    void Update()
    {
        cooldownTimer += Time.deltaTime;

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
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
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
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        // Mermiyi instantiate et
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Sahne görünümünde menzili göstermek için
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

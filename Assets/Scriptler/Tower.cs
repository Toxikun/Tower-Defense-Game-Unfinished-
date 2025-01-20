using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Sald�r� Ayarlar�")]
    public float range = 15f;                  // Kule menzili
    public float attackCooldown = 1.5f;        // Sald�r� aral��� (saniye)
    public GameObject bulletPrefab;            // Mermi prefab'�
    public Transform firePoint;                // Mermilerin ��k�� noktas�
    public float rotationSpeed = 10f;

    [Header("Referanslar")]
    public string enemyTag = "Enemy";           // D��manlar�n tag'�
    public Transform partToRotate;              // D�nd�r�lmesi gereken k�s�m (�rn: kule ba��)

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
        // Sahne g�r�n�m�nde menzili g�stermek i�in
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

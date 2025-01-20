using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    private int wavepointIndex = 0;
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        // Rigidbody ve Collider bileþenlerinin olduðundan emin ol
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Enemy objesinde Rigidbody eksik!");
        }

        // Yerçekimini devre dýþý býrak
        rb.useGravity = false;

        // Düþmanýn saðlýðýný baþlat
        currentHealth = maxHealth;

        // Ýlk waypoint hedefini ayarla
        target = Waypointler.points[0];
    }

    void Update()
    {
        // Hedef waypoint'e doðru hareket et
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // Hedefe ulaþýldýðýnda bir sonraki waypoint'e geç
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    // Hasar alýndýðýnda çaðrýlan fonksiyon
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Düþmaný yok eden fonksiyon
    void Die()
    {
        Destroy(gameObject);  // Düþmaný yok et
    }

    // Bir sonraki waypoint'e geç
    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypointler.points.Length - 1)
        {
            // Son waypoint'e ulaþýldýðýnda düþmaný yok et
            Destroy(gameObject);
            return;
        }

        // Bir sonraki waypoint'e geç
        wavepointIndex++;
        target = Waypointler.points[wavepointIndex];
    }
}

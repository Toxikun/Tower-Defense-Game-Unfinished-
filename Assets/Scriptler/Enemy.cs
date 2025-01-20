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
        // Rigidbody ve Collider bile�enlerinin oldu�undan emin ol
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Enemy objesinde Rigidbody eksik!");
        }

        // Yer�ekimini devre d��� b�rak
        rb.useGravity = false;

        // D��man�n sa�l���n� ba�lat
        currentHealth = maxHealth;

        // �lk waypoint hedefini ayarla
        target = Waypointler.points[0];
    }

    void Update()
    {
        // Hedef waypoint'e do�ru hareket et
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // Hedefe ula��ld���nda bir sonraki waypoint'e ge�
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    // Hasar al�nd���nda �a�r�lan fonksiyon
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // D��man� yok eden fonksiyon
    void Die()
    {
        Destroy(gameObject);  // D��man� yok et
    }

    // Bir sonraki waypoint'e ge�
    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypointler.points.Length - 1)
        {
            // Son waypoint'e ula��ld���nda d��man� yok et
            Destroy(gameObject);
            return;
        }

        // Bir sonraki waypoint'e ge�
        wavepointIndex++;
        target = Waypointler.points[wavepointIndex];
    }
}

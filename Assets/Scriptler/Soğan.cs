using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soğan : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    private int wavepointIndex;
    public float damage = 200f;
    public float health = 1000f;

    void Start()
    {
        // Rigidbody bileşenini al ve yerçekimini devre dışı bırak
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        // İlk hedef noktasını ayarla
        target = ReverseWaypoints.reversePoints[0];
    }

    void Update()
    {
        // Hedefe doğru hareket et
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // Hedef noktaya ulaşıldığında bir sonraki noktaya geç
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            getNextReversePoint();
        }

        // Soğan'ın canı sıfıra ulaşınca yok et
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void getNextReversePoint()
    {
        if (wavepointIndex >= ReverseWaypoints.reversePoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = ReverseWaypoints.reversePoints[wavepointIndex];
    }

    // 3D çarpışma kontrolü
    void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("Düşmanla çarpışma algılandı!");
            if (health <= damage)
            {
                damage = health;
            }
            enemy.TakeDamage(damage);  // Düşmana hasar ver
            health -= damage;  // Soğan'ın canı da verilen hasar kadar azalıyor

            if (health <= 0)
            {
                Destroy(gameObject);  // Soğan'ı yok et
            }
        }
        else
        {
            Debug.Log("Çarpışma ama Enemy değil!");
        }
    }

}

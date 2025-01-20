using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;              // Merminin hýzý
    public float lifeTime = 5f;            // Merminin ömrü (saniye)
    public float damage = 20f;             // Merminin vereceði hasar

    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Start()
    {
        // Belirli bir sürenin ardýndan mermiyi yok et
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);  // Hedef yoksa mermiyi yok et
            return;
        }

        // Hedefe doðru hareket et
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);  // Hedefe hasar ver
        }

        // Mermiyi yok et
        Destroy(gameObject);
    }
}

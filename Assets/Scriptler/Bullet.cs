using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;              // Merminin h�z�
    public float lifeTime = 5f;            // Merminin �mr� (saniye)
    public float damage = 20f;             // Merminin verece�i hasar

    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Start()
    {
        // Belirli bir s�renin ard�ndan mermiyi yok et
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);  // Hedef yoksa mermiyi yok et
            return;
        }

        // Hedefe do�ru hareket et
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

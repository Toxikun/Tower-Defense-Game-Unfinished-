using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoTower : MonoBehaviour

{
    public float upHeight = 5f;  // Kule ne kadar y�kselecek
    public float upSpeed = 2f;     // Y�kselme ve d��me h�z�
    public float downSpeed = 20f;
    public float damageRadius = 100f; // Hasar alan�
    public float damage = 50f;      // Verilecek hasar
    public LayerMask enemyLayer;    // D��man katman�

    private Vector3 originalPosition;  // Kule ba�lang�� pozisyonu

    void Start()
    {
        originalPosition = transform.position;  // �lk pozisyonu kaydet
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            // Yukar� ��kma
            Vector3 targetPosition = originalPosition + new Vector3(0, upHeight, 0);
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, upSpeed * Time.deltaTime);
                yield return null;
            }

            // K�sa bir bekleme
            yield return new WaitForSeconds(0.5f);

            // Yere vurma
            while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, downSpeed * Time.deltaTime);
                yield return null;
            }

            // Hasar verme
            Collider[] enemiesHit = Physics.OverlapSphere(originalPosition, damageRadius, enemyLayer);
            foreach (Collider enemy in enemiesHit)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage); // D��man�n hasar alma fonksiyonu
            }

            // K�sa bir bekleme
            yield return new WaitForSeconds(2f);  // Kule tekrar sald�rmadan �nce bekleme s�resi
        }
    }


    // Kapsama alan�n� g�rmek i�in
    void OnDrawGizmosSelected()
    { 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(originalPosition, damageRadius);
    }
}


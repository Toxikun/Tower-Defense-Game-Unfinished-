using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoTower : MonoBehaviour

{
    public float upHeight = 5f;  // Kule ne kadar yükselecek
    public float upSpeed = 2f;     // Yükselme ve düþme hýzý
    public float downSpeed = 20f;
    public float damageRadius = 100f; // Hasar alaný
    public float damage = 50f;      // Verilecek hasar
    public LayerMask enemyLayer;    // Düþman katmaný

    private Vector3 originalPosition;  // Kule baþlangýç pozisyonu

    void Start()
    {
        originalPosition = transform.position;  // Ýlk pozisyonu kaydet
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            // Yukarý çýkma
            Vector3 targetPosition = originalPosition + new Vector3(0, upHeight, 0);
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, upSpeed * Time.deltaTime);
                yield return null;
            }

            // Kýsa bir bekleme
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
                enemy.GetComponent<Enemy>().TakeDamage(damage); // Düþmanýn hasar alma fonksiyonu
            }

            // Kýsa bir bekleme
            yield return new WaitForSeconds(2f);  // Kule tekrar saldýrmadan önce bekleme süresi
        }
    }


    // Kapsama alanýný görmek için
    void OnDrawGizmosSelected()
    { 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(originalPosition, damageRadius);
    }
}


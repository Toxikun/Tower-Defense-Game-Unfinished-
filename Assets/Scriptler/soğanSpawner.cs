using UnityEngine;

public class soğanSpawner : MonoBehaviour
{
    public Transform soğanPrefab;  // Inspector üzerinden ayarlanabilir
    public Transform spawnPoint;    // Inspector üzerinden ayarlanabilir

    private EventManager eventManager; // EventManager referansı

    void Start()
    {
        // EventManager'ı bul
        eventManager = FindObjectOfType<EventManager>();
    }

    public void SpawnSoğan() // Metot ismini büyük harfle başlat
    {
        // Yeni soğan spawnlanmadan önce mevcut soğanı kontrol et
        if (GameObject.FindGameObjectWithTag("Soğan") != null) // Eğer sahnede bir soğan varsa
        {
            // Zaten bir soğan mevcut mesajını ekle
            eventManager.AddEvent("Zaten bir soğan mevcut!"); // Chat'e yazdır
            return; // Yeni soğan çağırma
        }

        // Eğer mevcut soğan yoksa, yeni soğanı spawnla
        Instantiate(soğanPrefab, spawnPoint.position, spawnPoint.rotation);

        // Olay mesajını ekle
        eventManager.AddEvent("Soğan çağırıldı."); // Soğan çağrıldığında mesaj oluştur
    }
}

using UnityEngine;
using UnityEngine.UI; // UI bileþenleri için gerekli

public class EventManager : MonoBehaviour
{
    public Text eventText; // Chat mesajlarýnýn görüntüleneceði UI Text bileþeni
    private string messageLog = ""; // Olay mesajlarýný tutmak için bir string

    void Start()
    {
        if (eventText == null)
        {
            Debug.LogError("EventManager: eventText referansý atanmadý!");
        }
    }

    public void AddEvent(string message)
    {
        // Mesajý log'a ekle
        messageLog += message + "\n";

        // Log'un boyutunu sýnýrlamak (örneðin son 5 mesajý tutma)
        string[] messages = messageLog.Split('\n');
        if (messages.Length > 5) // Sadece son 5 mesajý tut
        {
            messageLog = string.Join("\n", messages, messages.Length - 5, 5);
        }

        // UI Text bileþenine güncellenmiþ mesajlarý ekle
        eventText.text = messageLog;
    }
}

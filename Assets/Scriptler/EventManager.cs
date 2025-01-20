using UnityEngine;
using UnityEngine.UI; // UI bile�enleri i�in gerekli

public class EventManager : MonoBehaviour
{
    public Text eventText; // Chat mesajlar�n�n g�r�nt�lenece�i UI Text bile�eni
    private string messageLog = ""; // Olay mesajlar�n� tutmak i�in bir string

    void Start()
    {
        if (eventText == null)
        {
            Debug.LogError("EventManager: eventText referans� atanmad�!");
        }
    }

    public void AddEvent(string message)
    {
        // Mesaj� log'a ekle
        messageLog += message + "\n";

        // Log'un boyutunu s�n�rlamak (�rne�in son 5 mesaj� tutma)
        string[] messages = messageLog.Split('\n');
        if (messages.Length > 5) // Sadece son 5 mesaj� tut
        {
            messageLog = string.Join("\n", messages, messages.Length - 5, 5);
        }

        // UI Text bile�enine g�ncellenmi� mesajlar� ekle
        eventText.text = messageLog;
    }
}

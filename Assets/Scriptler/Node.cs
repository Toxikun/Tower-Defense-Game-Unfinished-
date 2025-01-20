using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    private GameObject turret;
    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;
    EventManager eventManager; // EventManager referans�

    void Start()
    {
        buildManager = BuildManager.Instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        // EventManager'� bul
        eventManager = FindObjectOfType<EventManager>();
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (buildManager.GetTurretToBuild() == null)
            return;
        if (turret != null)
        {
            Debug.Log("Can't build there!");
            return;
        }

        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position, turretToBuild.transform.rotation);

        // Olay mesaj�n� ekle
        eventManager.AddEvent(turretToBuild.name + " kulesi yerle�tirildi."); // Kulenin ad� ile mesaj olu�tur
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (buildManager.GetTurretToBuild() == null)
            return;
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}

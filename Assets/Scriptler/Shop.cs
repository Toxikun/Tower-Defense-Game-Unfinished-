using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void PurchaseStandartTurret()
    {
        buildManager.setTurretToBuild(buildManager.standardTurretPrefab);
        Debug.Log("Standart turret purchased!");
    }

    public void PurchaseAnotherTurret()
    {
        Debug.Log("Another turret purchased!");
        buildManager.setTurretToBuild(buildManager.anotherTurretPrefab);
    }

    public void purchasePotatoTower()
    {
        Debug.Log("patates");
        buildManager.setTurretToBuild(buildManager.i1otherTurretPrefab);
    }

    
    
}

   
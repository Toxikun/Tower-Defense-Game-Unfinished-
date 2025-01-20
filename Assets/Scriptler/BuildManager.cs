using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.LogError("More than one BuildManager in Scene!");
        }
        Instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject anotherTurretPrefab;
    public GameObject i1otherTurretPrefab;
    public GameObject i2otherTurretPrefab;
    public GameObject i3otherTurretPrefab;

    private GameObject turretToBuild;
    public void setTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;

    }
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
    
}

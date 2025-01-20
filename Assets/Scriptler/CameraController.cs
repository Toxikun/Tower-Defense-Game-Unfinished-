using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera : MonoBehaviour
{
    
    public bool doMovement = true;
    public bool MouseControl = true;
    public float speed = 30f;
    public float borderThickness = 10f;
    public Vector3 mainCamPosition; // Pozisyonu Vector3 olarak tan�ml�yoruz

    public float zoomSpeed = 100f;


    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -10f;
    public float maxY = 10f;
    public float minZ = -30f;
    public float maxZ = 30f;

    private void Start()
    {
        // Kameran�n ba�lang�� pozisyonunu al�yoruz
        mainCamPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Escape tu�una bas�ld���nda hareketi a�/kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        // Hareket kapal�ysa geri d�n
        if (!doMovement)
            return;

        if (Input.GetKeyDown(KeyCode.E)){
            MouseControl=!MouseControl;
        }





        // Klavye ile hareket kontrolleri
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        // Space tu�una bas�ld���nda kameray� ba�lang�� pozisyonuna d�nd�r
        if (Input.GetKey("space"))
        {
            transform.position = mainCamPosition;
        }



        if (MouseControl) { 
        // Fare ile kenar hareketi kontrol�
        if (Input.mousePosition.y >= Screen.height - borderThickness)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.mousePosition.x <= borderThickness)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.mousePosition.y <= borderThickness)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (Input.mousePosition.x >= Screen.width - borderThickness)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        
    }
        // Fare tekerle�i ile zoom in ve zoom out
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            transform.Translate(Vector3.forward * scroll * zoomSpeed * Time.deltaTime);
        }

        Vector3 pos = transform.position;
        pos.y =Mathf.Clamp(pos.y,minY,maxY);
        pos.x = Mathf.Clamp(pos.x,minX,maxX);
        pos.z = Mathf.Clamp(pos.z,minZ,maxZ);
        transform.position = pos;


    }
}

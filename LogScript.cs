using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogScript : MonoBehaviour
{
    [Header("Player Raycast Control")]
    [Space(2)]
    [SerializeField] RaycastHit hit;
    [SerializeField] float rayDistance = 1f;
    [SerializeField] GameObject interactionUI;
    [SerializeField] LayerMask logMask;
    [SerializeField] LayerMask computerMask;
    Ray ray;

    [Space(10)]
    [Header("Computer Screen Control")]
    [Space(2)]
    GameObject computerOff;
    GameObject computerOn;

    [HideInInspector] public string lastLogPickup;
    [SerializeField] List<string> logList = new List<string>();    

    // Update is called once per frame
    void Update()
    {
        LogController(); 
    }

    void LogController()
    {
        // Create a ray from the camera's position in the direction of the mouse cursor for logs and computers. 
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Shoot the ray and check if it hits the collider assigned to the logs.
        if (Physics.Raycast(ray, out hit, rayDistance, logMask))
        {
            interactionUI.SetActive(true);
            // Do something with the hit object
            Debug.Log("Raycast hit object: " + hit.collider.gameObject.name);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                lastLogPickup = hit.collider.gameObject.name;
                Debug.Log("Action button pressed: " + hit.collider.gameObject.name + " collected");
                hit.collider.gameObject.SetActive(false);
                logList.Add(lastLogPickup);
            }
        }
        // Shoot the ray and check if it hits the collider assigned to the computers.
        else if (Physics.Raycast(ray, out hit, rayDistance, computerMask))
        {
            computerOff = hit.collider.gameObject.transform.Find("Display_Monitor_Large_Danger").gameObject;
            computerOn = hit.collider.gameObject.transform.Find("Display_Monitor_Large_Ok").gameObject;

            interactionUI.SetActive(true);
            // Do something with the hit object
            Debug.Log("Raycast hit object: " + hit.collider.gameObject.name);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Action button pressed: " + hit.collider.gameObject.name + " accessed.");
                computerOff.SetActive(false);
                computerOn.SetActive(true);

            }
        }
        else
        {
            interactionUI.SetActive(false);
        }



        Debug.DrawRay(this.transform.position, transform.forward);
    }
}

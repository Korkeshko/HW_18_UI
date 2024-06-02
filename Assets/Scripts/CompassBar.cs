using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CompassBar : MonoBehaviour
{
    [SerializeField] private RectTransform compassBarTransform; 
    [SerializeField] private Transform cameraTransform;
    
    
    [SerializeField] private RectTransform[] NSEWMarkersArray;
    [SerializeField] private RectTransform[] QuestMarkersArray;
    [SerializeField] private Transform[] SphereMarkersArray;
    [SerializeField] Vector2[] NSEWDirection;
     [SerializeField] private float maxDistance = 10f;

    // [SerializeField] private RectTransform northMarkerTransform;
    // [SerializeField] private RectTransform southMarkerTransform;
    // [SerializeField] private RectTransform eastMarkerTransform;
    // [SerializeField] private RectTransform westMarkerTransform;

    // [SerializeField] private RectTransform MarkerBlueTransform; 
    // [SerializeField] private RectTransform MarkerRedTransform; 
    // [SerializeField] private RectTransform MarkerYellowTransform;  

    // [SerializeField] private Transform SphereBlueTransform;
    // [SerializeField] private Transform SphereRedTransform;
    // [SerializeField] private Transform SphereYellowTransform;

    // [SerializeField] Vector2 northDirection = Vector2.up;
    // [SerializeField] Vector2 southDirection = Vector2.down;
    // [SerializeField] Vector2 eastDirection  = Vector2.right;
    // [SerializeField] Vector2 westDirection  = Vector2.left;

    void Awake()
    {
        StartCoroutine(UpdateCompassBar());
    }

    private IEnumerator UpdateCompassBar()
    {
        while (true)
        {
            for (int i = 0; i < NSEWMarkersArray.Length; i++)
            {
                SetNSEWPosition(NSEWMarkersArray[i], NSEWDirection[i]);
            }
            
            for (int i = 0; i < QuestMarkersArray.Length; i++)
            {
                SetMarkerPosition(QuestMarkersArray[i], SphereMarkersArray[i].position);
            }
            yield return null;
        }    
    }

    
    
    
    // // Update is called once per frame
    // void Update()
    // {
    //     for (int i = 0; i < NSEWMarkersArray.Length; i++)
    //     {
    //         SetNSEWPosition(NSEWMarkersArray[i], NSEWDirection[i]);
    //     }
        
    //     for (int i = 0; i < QuestMarkersArray.Length; i++)
    //     {
    //         SetMarkerPosition(QuestMarkersArray[i], SphereMarkersArray[i].position);
    //     }

    //     // SetNSEWPosition(northMarkerTransform, northDirection);
    //     // SetNSEWPosition(southMarkerTransform, southDirection);  
    //     // SetNSEWPosition(eastMarkerTransform, eastDirection);
    //     // SetNSEWPosition(westMarkerTransform, westDirection);

    //     // SetMarkerPosition(northMarkerTransform, Vector3.forward * 100);
    //     // SetMarkerPosition(southMarkerTransform, Vector3.back * 100);
    //     // SetMarkerPosition(eastMarkerTransform, Vector3.right * 100);
    //     // SetMarkerPosition(westMarkerTransform, Vector3.left * 100);
    // }

    private void SetNSEWPosition(RectTransform markerTransform, Vector2 direction)
    {
        float angle = Vector2.SignedAngle(direction, new Vector2(cameraTransform.transform.forward.x, cameraTransform.transform.forward.z));
        float compassPosX = Math.Clamp((angle * 2) / Camera.main.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPosX, 0);
    }
    
    private void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition)
    {
        Vector3 directionToTarget = worldPosition - cameraTransform.position;
        float angle = Vector2.SignedAngle(new Vector2(directionToTarget.x, directionToTarget.z), new Vector2(cameraTransform.transform.forward.x, cameraTransform.transform.forward.z));
        float compassPosX = Math.Clamp((angle * 2) / Camera.main.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPosX, 0);
        

        float distance = Vector3.Distance(new Vector3(cameraTransform.transform.position.x, 0, cameraTransform.transform.position.z), worldPosition);
        float Scale = Mathf.Clamp(1 - (distance/maxDistance), 0.3f, 1f);
        markerTransform.localScale = new Vector3(1, 1, 1) * Scale;
        //markerTransform.transform.position = new Vector3(0, 0, Scale);
    
    }

    // private float GetNSEWAngle(Vector3 direction)
    // {  
    //     // Vector3 northDirection = Vector3.up;
    //     // Vector3 southDirection = -Vector3.up;
    //     //Vector3 westDirection = Vector3.Cross(cameraTransform.forward, Vector3.up);
    //     // Vector3 eastDirection = Vector3.Cross(Vector3.up, cameraTransform.forward);
        
        
    //     float angle = Vector3.Angle(direction, cameraTransform.forward);
    //     Vector3 crossProduct = Vector3.Cross(direction, cameraTransform.forward);
    //     if (crossProduct.y < 0)
    //     {
    //         angle = 360 - angle;
    //     }
    //     return angle;
    // }
}

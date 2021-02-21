using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class RocketManager : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public ARPlaneManager arPlaneManager;
    public GameObject rocketPrefab;
    public Button resetButton;

    private bool rocketCreated = false;
    private GameObject instantiatedRocket;

    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();

    private void Awake()
    {
        resetButton.onClick.RemoveAllListeners();
        resetButton.onClick.AddListener(() =>
        {
            DeleteRocket(instantiatedRocket);
        });
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                if (!rocketCreated)
                {
                    //Rraycast Planes
                    if (arRaycastManager.Raycast(touch.position, arRaycastHits))
                    {
                        var pose = arRaycastHits[0].pose;
                        CreateRocket(pose.position);
                        TogglePlaneDetection(false);
                        return;
                    }
                }
            }
        }
    }

    private void CreateRocket(Vector3 position)
    {
        instantiatedRocket = Instantiate(rocketPrefab, position, Quaternion.identity);
        rocketCreated = true;
        resetButton.gameObject.SetActive(true);
    }

    private void TogglePlaneDetection(bool state)
    {
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(state);
        }
        arPlaneManager.enabled = state;
    }

    private void DeleteRocket(GameObject rocketObject)
    {
        Destroy(rocketObject);
        resetButton.gameObject.SetActive(false);
        rocketCreated = false;
        TogglePlaneDetection(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Rigidbody thisRigiBody;
    private bool activate;

    public int force = 100;

    private void Awake()
    {
        thisRigiBody = GetComponent<Rigidbody>();
        StartCoroutine(LaunchRocket());
    }

    private IEnumerator LaunchRocket()
    {
        yield return new WaitForSeconds(3f);
        activate = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (activate)
        {
            thisRigiBody.AddForce(Vector3.up * Time.deltaTime * force, ForceMode.Acceleration);
        }        
    }
}

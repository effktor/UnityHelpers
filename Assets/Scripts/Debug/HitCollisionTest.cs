using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollisionTest : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void Awake()
    {
    }

    private bool hasDealtDamage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (hasDealtDamage)
            return;

        Debug.Log("GET DAMAGE");
        hasDealtDamage = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (hasDealtDamage)
        {
            hasDealtDamage = false;
        }
    }
}
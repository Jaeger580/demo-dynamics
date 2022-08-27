using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RifleShoot : MonoBehaviour
{
    // Stats for the rifle
    [SerializeField] float damage = 5f;
    [SerializeField] float range = 100f;
    [SerializeField] GameObject muzzle;

    public void Shoot(InputAction.CallbackContext context) 
    {
        RaycastHit hit;
        if (Physics.Raycast(muzzle.transform.position, -muzzle.transform.forward, out hit, range)) 
        {
            Debug.Log(hit.transform.name);
            Debug.DrawRay(muzzle.transform.position, -muzzle.transform.forward * range, Color.red);
        }
    }
}

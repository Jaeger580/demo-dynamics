using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAim : MonoBehaviour
{
    // Script that forced the Assault Rifle to aim where the player is looking.

    [SerializeField]
    GameObject target;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);
    }
}

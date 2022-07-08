using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Player.instance.Grounded = true;
    }
}
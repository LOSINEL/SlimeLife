using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherableRegen : MonoBehaviour
{
    [SerializeField] float regenTime = 5f;
    public GameObject gatherableObject;
    void Start()
    {
        StartCoroutine(RegenObject());
    }
    IEnumerator RegenObject()
    {
        yield return new WaitForSeconds(Random.Range(regenTime * 0.5f, regenTime * 2f));
        Instantiate(gatherableObject, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
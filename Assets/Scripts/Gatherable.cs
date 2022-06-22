using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Gatherable : MonoBehaviour
{
    public int nowHp = 10;
    int maxHp = 10, dropNum = 3;
    public GameObject dropItem;
    float vibrateRange = 0.1f;
    float vibrateTime = 0.25f;
    float dropRange = 0.5f;
    bool attacked = false;
    public int gatherableType = 0;
    enum GatherableType
    {
        Tree, // µµ³¢·Î¸¸ µ¥¹ÌÁö¸¦ ÀÔÀ½
        Mineral, // °î±ªÀÌ~~
        Plant, // ³´~~
        Dirt, // »ð~~
    }
    Vector3 basePos;
    void Start()
    {
        basePos = gameObject.transform.position;
    }
    void Update()
    {
        if(attacked)
        {
            gameObject.transform.position = basePos + new Vector3(Random.Range(-1 * vibrateRange, vibrateRange), 0, Random.Range(-1 * vibrateRange, vibrateRange));
            StartCoroutine(Vibrate());
        }
        if (nowHp <= 0 && gameObject.GetComponent<Renderer>().material.color.a > 0)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, gameObject.GetComponent<Renderer>().material.color.a - Time.deltaTime);
            if (gameObject.GetComponent<Renderer>().material.color.a <= 0)
            {
                Destroy(this.gameObject);
                for(int i=0;i<dropNum;i++)
                {
                    Instantiate(dropItem, new Vector3(gameObject.transform.position.x + Random.Range(-1 * dropRange, dropRange), gameObject.transform.position.y + dropRange, gameObject.transform.position.z + Random.Range(-1 * dropRange, dropRange)), Quaternion.identity);
                }
            }
        }
    }
    IEnumerator Vibrate()
    {
        yield return new WaitForSeconds(vibrateTime);
        attacked = false;
        gameObject.transform.position = basePos;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Tool") && Player.instance.IsAttacking)
        {
            if (gatherableType == other.GetComponent<Tool>().toolType)
            {
                attacked = true;
                nowHp -= Player.instance.Damage;
                Player.instance.PlayAttackSound(gatherableType);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gatherable : MonoBehaviour
{
    public int nowHp = 10, maxHp = 10;
    int dropNum = 3;
    public GameObject dropItem;
    float vibrateRange = 0.1f;
    float vibrateTime = 0.25f;
    float dropRange = 0.5f;
    bool attacked = false;
    public int gatherableType = 0;
    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 1f, 0);
    GameObject hpBar;
    Canvas hpCanvas;
    Image hpbarImage;
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
        SetHpBar();
        basePos = gameObject.transform.position;
    }
    void Update()
    {
        if(attacked)
        {
            gameObject.transform.position = basePos + new Vector3(Random.Range(-1 * vibrateRange, vibrateRange), 0, Random.Range(-1 * vibrateRange, vibrateRange));
            StartCoroutine(Vibrate());
        }
        if (nowHp <= 0)
        {
            switch(gatherableType)
            {
                case (int)GatherableType.Tree:
                    break;
                case (int)GatherableType.Mineral:
                    break;
                case (int)GatherableType.Plant:
                    break;
                case (int)GatherableType.Dirt:
                    break;
            }
            for (int i = 0; i < dropNum; i++)
            {
                Instantiate(dropItem, new Vector3(gameObject.transform.position.x + Random.Range(-1 * dropRange, dropRange), gameObject.transform.position.y + dropRange, gameObject.transform.position.z + Random.Range(-1 * dropRange, dropRange)), Quaternion.identity);
            }
            Destroy(hpBar);
            Destroy(this.gameObject);
        }
    }
    void SetHpBar()
    {
        hpCanvas = GameObject.Find("HP_Canvas").GetComponent<Canvas>();
        hpBar = Instantiate<GameObject>(hpBarPrefab, hpCanvas.transform);
        hpbarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var _hpbar = hpBar.GetComponent<HpBar>();
        _hpbar.targetTransform = this.gameObject.transform;
        _hpbar.offset = hpBarOffset;

        RefreshHpBar();
    }
    void RefreshHpBar()
    {
        hpBar.GetComponent<HpBar>().GetComponentInChildren<Text>().text = nowHp.ToString() + "/" + maxHp.ToString();
        hpbarImage.fillAmount = (float)nowHp / maxHp;
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
                if (nowHp < 0) nowHp = 0;
                SoundManager.instance.PlayerSoundPlay(gatherableType, false);
                RefreshHpBar();
            }
        }
    }
}
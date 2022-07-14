using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gatherable : MonoBehaviour
{
    [SerializeField] int dropNum = 3;
    int nowHp = 10, maxHp = 10;
    public GameObject dropItem;
    float vibrateRange = 0.1f;
    float vibrateTime = 0.25f;
    float dropRange = 0.5f;
    bool attacked = false;
    public int gatherableType = 0;
    public GameObject hpBarPrefab;
    public GameObject readyPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 1f, 0);
    float growTime = 0.05f;
    GameObject hpBar;
    Canvas hpCanvas;
    Image hpbarImage;
    bool needgrow = true;
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
        if (needgrow && gameObject.transform.localScale.x < 1f)
        {
            needgrow = false;
            StartCoroutine(GrowObject());
        }
        if (attacked)
        {
            gameObject.transform.position = basePos + new Vector3(Random.Range(-1 * vibrateRange, vibrateRange), 0, Random.Range(-1 * vibrateRange, vibrateRange));
            StartCoroutine(Vibrate());
        }
        if (nowHp <= 0)
        {
            for (int i = 0; i < dropNum; i++)
            {
                Instantiate(dropItem, new Vector3(gameObject.transform.position.x + Random.Range(-1 * dropRange, dropRange), gameObject.transform.position.y + dropRange, gameObject.transform.position.z + Random.Range(-1 * dropRange, dropRange)), Quaternion.identity);
            }
            Instantiate(readyPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(hpBar.gameObject);
            Destroy(gameObject);
        }
    }
    IEnumerator GrowObject()
    {
        yield return new WaitForSeconds(growTime);
        needgrow = true;
        gameObject.transform.localScale += new Vector3(0.04f, 0.04f, 0.04f);
    }
    void SetHpBar()
    {
        hpCanvas = GameObject.Find("HPCanvas").GetComponent<Canvas>();
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
            if (gatherableType == (int)other.GetComponent<Tool>().toolType - 1 || other.GetComponent<Tool>().toolType == (int)Tool.ToolType.Hand)
            {
                if (Player.instance.Hitable)
                {
                    Player.instance.Hitable = false;
                    attacked = true;
                    nowHp -= Player.instance.ToolDamage;
                    if (nowHp < 0) nowHp = 0;
                    SoundManager.instance.PlayerSoundPlay(gatherableType, false);
                    RefreshHpBar();
                }
            }
        }
    }
}
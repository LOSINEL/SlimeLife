using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    const int levelUpStatPoint = 3;
    const int levelUpHp = 4;
    const int levelUpSp = 4;
    public static Player instance;
    float mvSpd_ = 2.5f, mvSpd = 2.5f;
    float mvSnd = 1f, atkSpd = 1f;
    public int jumpPower = 14;
    bool move_able = true;
    bool jump_able = true;
    bool mvsnd_able = true;
    bool attack_able = true;
    int gold = 0;
    public GameObject tool, weapon;
    bool hitable = true;
    bool isMoving = false, grounded = false, isAttacking = false;
    float gravityScale = 10f;
    int weaponDamage = 1;
    int toolDamage = 1;
    float nowHp = 100f, nowSp = 100f;
    float maxHp = 100f, maxSp = 100f;
    float hpRegen = 1f, spRegen = 1f;
    int statStr = 0, statDex = 0, statVit = 0, statLuk = 0, statPoint = 0, statPointAll = 0, level = 1;
    int bonusStatStr = 0, bonusStatDex = 0, bonusStatVit = 0, bonusStatLuk = 0;
    Rigidbody rigid;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject hpBar;
    [SerializeField] GameObject spBar;
    public enum StatType
    { STR, DEX, VIT, LUK }
    public float HpRegen { get { return hpRegen; } }
    public float SpRegen { get { return spRegen; } }
    public float MvSpd { get { return mvSpd; } }
    public float AtkSpd { get { return atkSpd; } }
    public int Level { get { return level; } }
    public int StatPointAll { get { return statPointAll; } }
    public int StatPoint { get { return statPoint; } }
    public int StatStr { get { return statStr; } }
    public int StatDex { get { return statDex; } }
    public int StatVit { get { return statVit; } }
    public int StatLuk { get { return statLuk; } }
    public int BonusStatStr { get { return bonusStatStr; } }
    public int BonusStatDex { get { return bonusStatDex; } }
    public int BonusStatVit { get { return bonusStatVit; } }
    public int BonusStatLuk { get { return bonusStatLuk; } }
    public float NowHp { get { return nowHp; } }
    public float NowSp { get { return nowSp; } }
    public float MaxHp { get { return maxHp; } }
    public float MaxSp { get { return maxSp; } }
    public int Gold { get { return gold; } }
    public bool Grounded { set { grounded = value; } }
    public bool IsAttacking { get { return isAttacking; } }
    public int WeaponDamage { get { return weaponDamage; } }
    public int ToolDamage { get { return toolDamage; } }
    public bool Hitable { get { return hitable; } set { hitable = value; } }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddGold(10000);
        }
        RefreshHpSp();
        if (jump_able)
        {
            Jump();
        }

        if (move_able)
        {
            Sprint();
            Move();
        }
        if (isMoving)
        {
            if (mvsnd_able)
            {
                mvsnd_able = false;
                // 플레이어 이동 사운드 재생
                SoundManager.instance.PlayerSoundPlay((int)SoundManager.PlayerSoundName.Move, true);
                StartCoroutine(MoveSound());
            }
        }
        if (Input.GetKey(KeyCode.J) && attack_able)
        {
            tool.SetActive(true);
            hitable = true;
            isAttacking = true;
            attack_able = false;
            move_able = false;
            SoundManager.instance.PlayerSoundPlay((int)SoundManager.PlayerSoundName.WeaponSwing, true);
            switch (tool.GetComponent<Tool>().toolType)
            {
                case Tool.ToolType.Hand:
                    break;
                case Tool.ToolType.Axe:
                    tool.GetComponent<Animator>().SetTrigger("ToolAxeAttack");
                    break;
                case Tool.ToolType.PickAxe:
                    tool.GetComponent<Animator>().SetTrigger("ToolPickAxeAttack");
                    break;
                case Tool.ToolType.Scythe:
                    break;
                case Tool.ToolType.Shovel:
                    break;
            }
            StartCoroutine(AttackCoolTime());
            StartCoroutine(AttackCheck());
        }
    }
    void LateUpdate()
    {
        if(grounded) // 바닥에 닿아있을 때
        {
            rigid.velocity = new Vector3(rigid.velocity.x, -1.25f, rigid.velocity.z);
        }
        else // 점프 했을 때
        {
            rigid.velocity += new Vector3(0, -1, 0) * gravityScale * Time.deltaTime;
        }
        isMoving = false;
    }
    private void FixedUpdate()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, gameObject.transform.position + new Vector3(0, 10.5f, -8.5f), Time.deltaTime * 3f);
    }
    IEnumerator AttackCheck()
    {
        yield return new WaitForSeconds(tool.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.01f);
        isAttacking = false;
        move_able = true;
        tool.SetActive(false);
    }
    IEnumerator AttackCoolTime()
    {
        yield return new WaitForSeconds(atkSpd);
        attack_able = true;
    }
    IEnumerator MoveSound()
    {
        yield return new WaitForSeconds(Random.Range(mvSnd * 0.5f, mvSnd * 1.2f));
        mvsnd_able = true;
    }
    public void SetDamage(int dmg, bool isWeapon)
    {
        if (isWeapon)
        {
            weaponDamage = dmg;
        } else
        {
            toolDamage = dmg;
        }
    }
    public void ActiveAll(bool check)
    {
        move_able = jump_able = attack_able = check;
    }
    public void AddGold(int num)
    {
        gold += num;
    }

    public void AddHp(float _hp)
    {
        nowHp += _hp;
        if (nowHp > maxHp)
        {
            nowHp = maxHp;
        }
        if (nowHp <= 0)
        {
            nowHp = 0;
            PlayerDead();
        }
    }

    public void AddSp(float _sp)
    {
        nowSp += _sp;
        if (nowSp > maxSp)
        {
            nowSp = maxSp;
        }
        if (nowSp <= 0)
        {
            nowSp = 0;
        }
    }

    public void AddStat(StatType _statType, int num)
    {
        if (num > statPoint)
        {
            AlertManager.instance.CreateAlertMessage("스탯 포인트가 부족합니다.");
            return;
        }
        switch (_statType)
        {
            case StatType.STR:
                statStr += num;
                statPoint -= num;
                break;
            case StatType.DEX:
                statDex += num;
                statPoint -= num;
                break;
            case StatType.VIT:
                statVit += num;
                statPoint -= num;
                break;
            case StatType.LUK:
                statLuk += num;
                statPoint -= num;
                break;
        }
    }

    public void AddStatPoint(int num)
    {
        statPoint += num;
    }

    public void SetStatPoint(int num)
    {
        statPoint = num;
    }

    public void LevelUp()
    {
        statPoint += levelUpStatPoint;
        statPointAll += levelUpStatPoint;
        maxHp += levelUpHp;
        maxSp += levelUpSp;
        RecoverAll();
    }

    public void RecoverAll()
    {
        nowHp = maxHp;
        nowSp = maxSp;
    }

    public void RefreshHpSp()
    {
        RegenHp(hpRegen);
        RegenSp(spRegen);
        hpBar.GetComponent<Image>().fillAmount = nowHp / maxHp;
        spBar.GetComponent<Image>().fillAmount = nowSp / maxSp;
    }

    public void RegenHp(float _hpRegen)
    {
        AddHp(_hpRegen * Time.deltaTime);
    }

    public void RegenSp(float _spRegen)
    {
        AddSp(_spRegen * Time.deltaTime);
    }

    public void PlayerDead()
    {
        AlertManager.instance.CreateAlertMessage("당신은 사망했습니다.");
    }
    void Sprint()
    {
        // 달리기
        if (Input.GetKey(KeyCode.LeftShift))
        {
            mvSpd = mvSpd_ * 1.5f;
        }
        else
        {
            mvSpd = mvSpd_;
        }
    }
    void Jump()
    {
        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            // 플레이어 점프 사운드 재생
            SoundManager.instance.PlayerSoundPlay((int)SoundManager.PlayerSoundName.Jump, true);
            grounded = false;
        }
    }
    void Move()
    {
        // 8방향 이동
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector3(-1, 0, 1).normalized * mvSpd + new Vector3(0, rigid.velocity.y, 0);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, -45, 0));
            isMoving = true;
            return;
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector3(1, 0, 1).normalized * mvSpd + new Vector3(0, rigid.velocity.y, 0);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 45, 0));
            isMoving = true;
            return;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector3(-1, 0, -1).normalized * mvSpd + new Vector3(0, rigid.velocity.y, 0);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, -135, 0));
            isMoving = true;
            return;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector3(1, 0, -1).normalized * mvSpd + new Vector3(0, rigid.velocity.y, 0);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 135, 0));
            isMoving = true;
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigid.velocity = new Vector3(0, 0, 1) * mvSpd + new Vector3(0, rigid.velocity.y, 0);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            isMoving = true;
            return;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigid.velocity = new Vector3(0, 0, -1) * mvSpd + new Vector3(0, rigid.velocity.y, 0);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector3(-1, 0, 0) * mvSpd + new Vector3(0, rigid.velocity.y, 0);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            isMoving = true;
            return;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector3(1, 0, 0) * mvSpd + new Vector3(0, rigid.velocity.y, 0);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            isMoving = true;
        }
    }
}
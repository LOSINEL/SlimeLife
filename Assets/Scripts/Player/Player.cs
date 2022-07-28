using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float mvspd_ = 2.5f, mvspd = 2.5f;
    public float mvsnd = 1f, atkspd = 1f;
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
    Rigidbody rigid;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject hpBar;
    [SerializeField] GameObject spBar;
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
        RefreshHpSp();
        if (jump_able)
        {
            Jump();
        }
        
        if (move_able && grounded)
        {
            Sprint();
            Move();
        }
        if(isMoving)
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
        rigid.velocity += new Vector3(0, -1, 0) * gravityScale * Time.deltaTime;
        if (!isMoving && grounded) rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
        isMoving = false;
        mainCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 10.5f, gameObject.transform.position.z - 8.5f);
    }
    IEnumerator AttackCheck()
    {
        yield return new WaitForSeconds(tool.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false;
        move_able = true;
        tool.SetActive(false);
    }
    IEnumerator AttackCoolTime()
    {
        yield return new WaitForSeconds(atkspd);
        attack_able = true;
    }
    IEnumerator MoveSound()
    {
        yield return new WaitForSeconds(Random.Range(mvsnd * 0.5f, mvsnd * 1.2f));
        mvsnd_able = true;
    }
    public void SetDamage(int dmg, bool isWeapon)
    {
        if(isWeapon)
        {
            weaponDamage = dmg;
        }else
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

    public void RefreshHpSp()
    {
        RegenHp(0f);
        RegenSp(0f);
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
            mvspd = mvspd_ * 1.5f;
        }
        else
        {
            mvspd = mvspd_;
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
            rigid.velocity = new Vector3(-1, 0, 1).normalized * mvspd;
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, -45, 0));
            isMoving = true;
            return;
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector3(1, 0, 1).normalized * mvspd;
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 45, 0));
            isMoving = true;
            return;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector3(-1, 0, -1).normalized * mvspd;
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, -135, 0));
            isMoving = true;
            return;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector3(1, 0, -1).normalized * mvspd;
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 135, 0));
            isMoving = true;
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigid.velocity = new Vector3(0, 0, 1) * mvspd;
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            isMoving = true;
            return;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigid.velocity = new Vector3(0, 0, -1) * mvspd;
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector3(-1, 0, 0) * mvspd;
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            isMoving = true;
            return;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector3(1, 0, 0) * mvspd;
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            isMoving = true;
        }
    }
}
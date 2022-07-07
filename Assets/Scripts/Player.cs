using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float mvspd_ = 2.5f, mvspd = 2.5f;
    public float mvsnd = 1f, atkspd = 1f;
    public int jumpPower = 14;
    float attackAnimTime = 0.3f;
    bool move_able = true;
    bool jump_able = true;
    bool mvsnd_able = true;
    bool attack_able = true;
    [SerializeField] bool isMoving = false, grounded = false, isAttacking = false;
    [SerializeField] float gravityScale = 10f;
    [SerializeField] GameObject weapon, mainCamera;
    [SerializeField] int damage = 1;
    Rigidbody rigid;
    public bool Grounded { set { grounded = value; } }
    public bool IsAttacking { get { return isAttacking; } }
    public int Damage { get { return damage; } }
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
        if(attack_able)
        {
            if (Input.GetKey(KeyCode.J) && attack_able)
            {
                weapon.SetActive(true);
                isAttacking = true;
                attack_able = false;
                move_able = false;
                SoundManager.instance.PlayerSoundPlay((int)SoundManager.PlayerSoundName.WeaponSwing, true);
                weapon.GetComponent<Animator>().SetTrigger("Attack");
                StartCoroutine(AttackCoolTime());
                StartCoroutine(Attack());
            }
        }
    }
    void LateUpdate()
    {
        rigid.velocity += new Vector3(0, -1, 0) * gravityScale * Time.deltaTime;
        if (!isMoving && grounded) rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
        isMoving = false;
        mainCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 10.5f, gameObject.transform.position.z - 8.5f);
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackAnimTime);
        isAttacking = false;
        move_able = true;
        weapon.SetActive(false);
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
    public void ActiveAll(bool check = true)
    {
        move_able = jump_able = attack_able = check;
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
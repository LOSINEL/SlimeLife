using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const int playerSoundSize = 3;
    const int weaponAttackSoundSize = 4;
    const float doubleInput = 0.7071f;
    public static Player instance;
    public float mvspd_ = 2.5f, mvspd = 2.5f;
    public float mvsnd = 1f, atkspd = 1f;
    public int jumpPower = 100;
    float attackAnimTime = 0.3f;
    bool move_able = true;
    bool jump_able = true;
    bool mvsnd_able = true;
    bool attack_able = true;
    [SerializeField] bool isMoving = false;
    [SerializeField] bool grounded = true;
    [SerializeField] bool isAttacking = false;
    GameObject weapon;
    GameObject mainCamera;
    Rigidbody rigid;
    AudioSource audioSource;
    [SerializeField]AudioClip[] playerAudioClip = new AudioClip[playerSoundSize];
    [SerializeField]AudioClip[] weaponAudioClip = new AudioClip[weaponAttackSoundSize];
    [SerializeField] int damage = 1;
    public bool Grounded { set { grounded = value; } }
    public bool IsAttacking { get { return isAttacking; } }
    public int Damage { get { return damage; } }
    public enum AudioClipName
    {
        Move, Jump, WeaponSwing
    }
    public enum WeaponAttackClipName
    {
        AxeChop, PickAxeChop, ScytheChop, ShovelChop
    }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        weapon = GameObject.Find("Weapon");
        rigid = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        playerAudioClip[(int)AudioClipName.Move] = Resources.Load<AudioClip>("SoundEffect/Player/Slime_Move");
        playerAudioClip[(int)AudioClipName.Jump] = Resources.Load<AudioClip>("SoundEffect/Player/Slime_Jump");
        playerAudioClip[(int)AudioClipName.WeaponSwing] = Resources.Load<AudioClip>("SoundEffect/Weapon/ETC/WeaponSwing");
        weaponAudioClip[(int)WeaponAttackClipName.AxeChop] = Resources.Load<AudioClip>("SoundEffect/Weapon/Axe/Axe_Chop");
        weaponAudioClip[(int)WeaponAttackClipName.PickAxeChop] = Resources.Load<AudioClip>("SoundEffect/Weapon/PickAxe/PickAxe_Chop");
        weaponAudioClip[(int)WeaponAttackClipName.ScytheChop] = Resources.Load<AudioClip>("SoundEffect/Weapon/Scythe/Scythe_Chop");
        weaponAudioClip[(int)WeaponAttackClipName.ShovelChop] = Resources.Load<AudioClip>("SoundEffect/Weapon/Shovel/Shovel_Chop");
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
                audioSource.clip = playerAudioClip[(int)AudioClipName.Move];
                audioSource.Play();
                StartCoroutine(MoveSound());
            }
            isMoving = false;
        }
        if(attack_able)
        {
            if (Input.GetKey(KeyCode.J) && attack_able)
            {
                isAttacking = true;
                attack_able = false;
                move_able = false;
                audioSource.clip = playerAudioClip[(int)AudioClipName.WeaponSwing];
                audioSource.Play();
                weapon.GetComponent<Animator>().SetTrigger("Attack");
                StartCoroutine(AttackCoolTime());
                StartCoroutine(Attack());
            }
        }
    }
    void LateUpdate()
    {
        mainCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 10.5f, gameObject.transform.position.z - 8.5f);
    }
    public void PlayAttackSound(int num)
    {
        audioSource.clip = weaponAudioClip[num];
        audioSource.Play();
    }
    public void PlaySound(int num)
    {
        audioSource.clip = playerAudioClip[num];
        audioSource.Play();
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackAnimTime);
        isAttacking = false;
        move_able = true;
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
            audioSource.clip = playerAudioClip[(int)AudioClipName.Jump];
            audioSource.Play();
            grounded = false;
        }
    }
    void Move()
    {
        // 8방향 이동
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector3(-1 * doubleInput * mvspd, 0, doubleInput * mvspd);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, -45, 0));
            isMoving = true;
            return;
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector3(doubleInput * mvspd, 0, doubleInput * mvspd);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 45, 0));
            isMoving = true;
            return;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector3(-1 * doubleInput * mvspd, 0, -1 * doubleInput * mvspd);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, -135, 0));
            isMoving = true;
            return;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector3(doubleInput * mvspd, 0, -1 * doubleInput * mvspd);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 135, 0));
            isMoving = true;
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigid.velocity = new Vector3(0, 0, mvspd);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigid.velocity = new Vector3(0, 0, -1 * mvspd);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector3(-1 * mvspd, 0, 0);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector3(mvspd, 0, 0);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            isMoving = true;
        }
    }
}
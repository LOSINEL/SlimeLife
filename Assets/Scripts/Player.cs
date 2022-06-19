using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const int soundSize = 2;
    const float doubleInput = 0.7071f;
    public static Player instance;
    public float mvspd_ = 2f, mvspd = 2f;
    public float mvsnd = 1f;
    public int jumpPower = 100;
    bool move_able = true;
    bool jump_able = true;
    bool mvsnd_able = true;
    [SerializeField] bool isMoving = false;
    [SerializeField] bool grounded = true;
    GameObject mainCamera;
    GameObject hand;
    Rigidbody rigid;
    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClip = new AudioClip[soundSize];
    public bool Grounded { set { grounded = value; } }
    enum AudioClipName
    {
        move, jump
    }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        hand = GameObject.FindGameObjectWithTag("Hand");
        rigid = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioClip[(int)AudioClipName.move] = Resources.Load<AudioClip>("SoundEffect/Player/Slime_Move");
        audioClip[(int)AudioClipName.jump] = Resources.Load<AudioClip>("SoundEffect/Player/Slime_Jump");
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
                audioSource.clip = audioClip[(int)AudioClipName.move];
                audioSource.Play();
                StartCoroutine(MoveSound());
            }
            isMoving = false;
        }
    }
    void LateUpdate()
    {
        mainCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 10.5f, gameObject.transform.position.z - 8.5f);
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
            audioSource.clip = audioClip[(int)AudioClipName.jump];
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
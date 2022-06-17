using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const float doubleInput = 0.7071f;
    public static Player instance;
    public float mvspd = 2f, mvspd_;
    public float jumpPower = 175f;
    [SerializeField] bool grounded = true;
    GameObject mainCamera;
    GameObject hand;
    Rigidbody rigid;
    public bool Grounded { set { grounded = value; } }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        hand = GameObject.FindGameObjectWithTag("Hand");
        rigid = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        // 대쉬
        if(Input.GetKey(KeyCode.LeftShift))
        {
            mvspd_ = mvspd * 1.5f;
        }else
        {
            mvspd_ = mvspd;
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rigid.velocity = new Vector3(0, jumpPower * Time.deltaTime, 0);
            grounded = false;
        }

        // 8방향 이동
        if(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.A))
        {
            rigid.transform.Translate(new Vector3(-1 * doubleInput * mvspd_ * Time.deltaTime, 0, doubleInput * mvspd_ * Time.deltaTime), Space.World);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, -45, 0));
            return;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            rigid.transform.Translate(new Vector3(doubleInput * mvspd_ * Time.deltaTime, 0, doubleInput * mvspd_ * Time.deltaTime), Space.World);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 45, 0));
            return;
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            rigid.transform.Translate(new Vector3(-1 * doubleInput * mvspd_ * Time.deltaTime, 0, -1 * doubleInput * mvspd_ * Time.deltaTime), Space.World);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, -135, 0));
            return;
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            rigid.transform.Translate(new Vector3(doubleInput * mvspd_ * Time.deltaTime, 0, -1 * doubleInput * mvspd_ * Time.deltaTime), Space.World);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 135, 0));
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rigid.transform.Translate(new Vector3(0, 0, mvspd_ * Time.deltaTime), Space.World);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            return;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigid.transform.Translate(new Vector3(0, 0, -1 * mvspd_ * Time.deltaTime), Space.World);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            return;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigid.transform.Translate(new Vector3(-1 * mvspd_ * Time.deltaTime, 0, 0), Space.World);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            return;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigid.transform.Translate(new Vector3(mvspd_ * Time.deltaTime, 0, 0), Space.World);
            rigid.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            return;
        }
    }
    void LateUpdate()
    {
        mainCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 14.5f, gameObject.transform.position.z - 12.5f);
    }
}
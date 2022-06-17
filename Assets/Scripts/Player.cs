using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float mvspd = 40f;
    GameObject mainCamera;
    Rigidbody rigid;
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        rigid = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            mvspd = 60f;
        }else
        {
            mvspd = 40f;
        }
        if(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector3(-1, 0, 1) * mvspd * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -45, 0));
            return;
        }
        if(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector3(1, 0, 1) * mvspd * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 45, 0));
            return;
        }
        if(Input.GetKey(KeyCode.S)&&Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector3(-1, 0, -1) * mvspd * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -135, 0));
            return;
        }
        if(Input.GetKey(KeyCode.S)&&Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector3(1, 0, -1) * mvspd * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 135, 0));
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigid.velocity = Vector3.forward * mvspd * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            return;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigid.velocity = Vector3.back * mvspd * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            return;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigid.velocity = Vector3.left * mvspd * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            return;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigid.velocity = Vector3.right * mvspd * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            return;
        }
    }
    void LateUpdate()
    {
        mainCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 14.5f, gameObject.transform.position.z - 12.5f);
    }
}
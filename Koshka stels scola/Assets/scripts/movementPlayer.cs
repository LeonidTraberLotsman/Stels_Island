using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementPlayer : MonoBehaviour
{
    public float speed;
    public float speedCat;
    private Rigidbody Rb;
    private Rigidbody CAtRb;
    public float jump;
    public float cat_jump;
    public Vector3 rotPl;
    public float acselerate;
    public bool amIcat = false;
    bool CanIJump = true;
   
    public enemy_manager manager;

    public Transform camCenter;
    public Transform catCamCenter;

    public GameObject HumanBody;
    public GameObject CatBody;
    void Start()
    {
        Rb = HumanBody.GetComponent<Rigidbody>();
        CAtRb = CatBody.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {

        Rb.AddForce(-Vector3.up * 2000);
        if (Input.GetKey(KeyCode.Space)&& CanIJump)
        {
            CanIJump = false;
            if(amIcat) CAtRb.AddForce(Vector3.up * cat_jump);
            else Rb.AddForce(Vector3.up * jump);
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (amIcat)
            {
                if (CAtRb.velocity.magnitude < acselerate)
                {
                    CatBody.transform.rotation = Quaternion.Euler(0, catCamCenter.rotation.eulerAngles.y, 0);


                    //CatBody.transform.rotation = catCamCenter.rotation;
                    //Quaternion.Euler(catCamCenter.rotation.eulerAngles.x, catCamCenter.rotation.eulerAngles.y, catCamCenter.rotation.eulerAngles.z);
                    //catCamCenter.rotation = Quaternion.Euler(0, 0, 0);
                    CAtRb.AddForce(catCamCenter.forward * speedCat);
                    Debug.Log("cat forward");
                }
            }
            else if (Rb.velocity.magnitude < acselerate)
            {
                Rb.AddForce(camCenter.forward * speed );
                //Debug.Log("human forward");
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (amIcat)
            {
                if (CAtRb.velocity.magnitude < acselerate)
                    CatBody.transform.rotation = Quaternion.Euler(0, catCamCenter.rotation.eulerAngles.y, 0);
                {
                    CAtRb.AddForce(-catCamCenter.forward * speedCat);
                    Debug.Log("cat forward");
                }
            }
            else if (Rb.velocity.magnitude < acselerate)
            {
                Rb.AddForce(-camCenter.forward * speed);
                //Debug.Log("human forward");
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (amIcat)
            {
                if (CAtRb.velocity.magnitude < acselerate)
                {
                    CatBody.transform.rotation = Quaternion.Euler(0, catCamCenter.rotation.eulerAngles.y, 0);
                    CAtRb.AddForce(catCamCenter.right * speedCat);
                    //Debug.Log("cat forward");
                }
            }
            else if (Rb.velocity.magnitude < acselerate)
            {
                Rb.AddForce(camCenter.right * speed);
                //Debug.Log("human forward");
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (amIcat)
            {
                if (CAtRb.velocity.magnitude < acselerate)
                {
                    CatBody.transform.rotation = Quaternion.Euler(0, catCamCenter.rotation.eulerAngles.y, 0);
                    CAtRb.AddForce(-catCamCenter.right * speedCat);
                    Debug.Log("cat forward");
                }
            }
            else if (Rb.velocity.magnitude < acselerate)
            {
                Rb.AddForce(-camCenter.right * speed);
                Debug.Log("human forward");
            }
        }
        //if (Input.GetKey(KeyCode.S))
        //{
        //    if (amIcat)
        //    {
        //        if (Rb.velocity.magnitude < acselerate) Rb.AddForce(-camCenter.forward * speedCat);

        //    }
        //    else
        //    {
        //        if (Rb.velocity.magnitude < acselerate) Rb.AddForce(-camCenter.forward * speed);
        //    }
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    if (amIcat)
        //    {
        //        if (Rb.velocity.magnitude < acselerate) Rb.AddForce(-camCenter.right * speedCat);

        //    }
        //    else
        //    {
        //        if (Rb.velocity.magnitude < acselerate) Rb.AddForce(-camCenter.right * speed);
        //    }
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    if (amIcat)
        //    {
        //        if (Rb.velocity.magnitude < acselerate) Rb.AddForce(camCenter.right * speed);

        //    }
        //    else
        //    {
        //        if (Rb.velocity.magnitude < acselerate) Rb.AddForce(camCenter.right * speed);
        //    }
        //}
        if (Input.GetKey(KeyCode.F))
        {
            if (!amIcat)
            {
                stealth_kill();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            turnInTo(!amIcat);
            
        }

    }

    void stealth_kill()
    {
        float min = 20000;
        basicEnemy victim = null;
        foreach (basicEnemy that_basicEnemy in manager.enemies)
        {
            float dist = Vector3.Distance(transform.position, that_basicEnemy.transform.position);
            if (dist < min)
            {
                min = dist;
                victim = that_basicEnemy;

            }
        }
        victim.GetDamage(6);
    }
    void turnInTo(bool pontencialState)
    {
        CatBody.SetActive(pontencialState);
        HumanBody.SetActive(!pontencialState);
        amIcat = pontencialState;

        if (amIcat) CatBody.transform.position = HumanBody.transform.position;
        else HumanBody.transform.position = CatBody.transform.position;
    }
}
 

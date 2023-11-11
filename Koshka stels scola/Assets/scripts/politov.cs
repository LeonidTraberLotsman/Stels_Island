using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class politov : MonoBehaviour
{
    public Transform Player;
    public Transform pointer;
    public int hp;
    public List<Transform> points;
    public float delta_delta_cof = 5;
    public int invisible = 0;
    public float in_im_find_you = 120;
    public float in_im_kill_you_hahahahaha = 1200;
    public float cadr_minimum = 0;
    
Coroutine my_behavior;


    float mode;
    float cadr_cof = 0;
    public float delta_cof;
    int visibility;
    enum state {
        im_blink,
        im_find_you,
        im_kill_you_hahahahaha
    }
    NavMeshAgent agent;
    // Start is called before the first frame update

    void Start()
    {
        mode = cadr_minimum;
        
        agent = GetComponent<NavMeshAgent>();
        my_behavior= StartCoroutine(PlayerFind());
        StartCoroutine(Enemy_path());

        StopCoroutine(my_behavior);


        //StartCoroutine(ReachPoint(new Vector3(1, 0, 0)));
    }
    void Die()
    {
        Destroy(gameObject);
        
    }
    public void GetDamage(int damage)
    {
        hp = hp - damage;
        if (hp < 1)
        {
            Die();
        }
    }
    bool CanBeSeen(Transform target, int fieldOfView)
    {
        RaycastHit hit;
        
        Vector3 direction = target.position - transform.position;
        
        if (Mathf.Abs(Vector3.Angle(direction, transform.forward)) > fieldOfView)return false;
        if (Physics.Raycast(transform.position,direction,out hit))
        {
            pointer.position = hit.point;
            if (hit.transform == target) return true;
        }
        return false;
    }

    IEnumerator  ReachPoint(Vector3 cel)
    {
        agent.destination = cel;
        while (Vector3.Distance(cel,transform.position)>10)
        {
            yield return null;

        }
        yield return null;
    }
    IEnumerator  PlayerRun()
    {
        
        while (Vector3.Distance(Player.position,transform.position)>1)
        {
            if (CanBeSeen(Player,60))
            {
                agent.destination = Player.position;

            }

            
            yield return null;

        }
        yield return null;
    }
    IEnumerator PlayerFind()
    {
        while (true)
        {
            yield return null;
            if (CanBeSeen(Player, 60))
            {
                mode += cadr_cof;
                cadr_cof += delta_cof;
                
                //yield return PlayerRun();


            }
        }
    }
    IEnumerator Enemy_path()
    {
        
        while (true)
        {
            foreach (Transform that_point in points)
            {
                yield return ReachPoint(that_point.position);
            }

        }
        
    }
   
    

            
        
    
    
    state my_state=state.im_blink;

    // Update is called once per frame
    void Update()
    { 
        if (mode < in_im_find_you)
        {
            my_state=state.im_blink;
        }
        else if( mode > in_im_kill_you_hahahahaha)
        {
            my_state = state.im_kill_you_hahahahaha;
            cadr_cof = 60;
            cadr_minimum = in_im_find_you - 1;
            delta_cof = delta_delta_cof;
        }
        else my_state = state.im_find_you;
        //возможна ошибка. В случае ошибки условие переменная = 0
        if (invisible == 0)
        {
            visibility = 60;
        }
        else
        {
            visibility = 30;
        }
        
        if (CanBeSeen(Player, visibility))
        {
            mode += cadr_cof;
            cadr_cof += delta_cof;
        }
        else
        {
            Debug.Log("-");
        }
        
        if (mode <= cadr_minimum)
        {
            cadr_cof = 1;
        }
        else
        {
            mode--;
        }
    }
}

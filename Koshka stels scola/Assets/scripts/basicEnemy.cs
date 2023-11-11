using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class basicEnemy: MonoBehaviour
{
    public Transform Player;
    public Transform CatPlayer;
    public Transform pointer;
    public int hp;
    public List<Transform> points;
    float delta_delta_cof = 1;
    public float index_delta_delta_cof;
    public int invisible = 0;
    public float trigger_border = 120;
    public float kill_trigger_border = 1200;
    public float cadr_minimum = 0;
    public List<float> player_delta_mod = new List<float>();
    public List<float> range_delta_mod = new List<float>();
    public List<float> cat_delta_mod = new List<float>();
    Coroutine my_behavior;
    public float warning_points;
    float cadr_cof = 0;
    public float delta_cof;
    public int visibility;
    public bool invalid = false;
    
    Vector3 LastSeen = new Vector3();

    public List<BreakDans> ochenvashniedela = new List<BreakDans>();
    public class BreakDans
    {
        public BreakDans()
        {

        }
        public virtual IEnumerator Do(basicEnemy doer)
        {
            yield return null;
        }
    }
    public class Hodba:BreakDans
    {
        Vector3 point;
        public Hodba(Vector3 p)
        {
            point = p;
        }
        public override IEnumerator Do(basicEnemy doer)
        {
            yield return doer.ReachPoint(point);
        }
    }
    public class WaitTask : BreakDans
    {
        float seconds;
        public WaitTask(float s)
        {
            seconds = s;
        }
        public override IEnumerator Do(basicEnemy doer)
        {
            yield return new WaitForSeconds(seconds);
        }
    }
    public enum state
    {
        im_blink,
        im_find_you,
        im_kill_you_hahahahaha
    }
    public state my_state = state.im_blink;
   
    NavMeshAgent agent;
    // Start is called before the first frame update
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    /// <summary>
    /// /
    /// </summary>
    /// start
    /// 

    void Shoot()
    {
        RaycastHit hit;

        Vector3 direction = (Player.position - transform.position) + new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));

        if (Physics.Raycast(transform.position, direction,out hit))
        {
            if (hit.transform == Player.transform)
            {
               
                    Destroy(Player);
               
            }
        }
    }

    void Start()
    {
        ochenvashniedela.Add(new Hodba(new Vector3(0,0,0)));
        ochenvashniedela.Add(new WaitTask(10.0f));
        ochenvashniedela.Add(new Hodba(new Vector3(100,0,0)));
        warning_points = 100;
        warning_points = cadr_minimum;
        agent = GetComponent<NavMeshAgent>();

        my_behavior = StartCoroutine(BlinkStateDoing());
        //my_behavior=StartCoroutine(player_logic());
        //my_behavior =StartCoroutine(PlayerFind());
        //StartCoroutine(Enemy_path());

        //StopCoroutine(my_behavior);


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
        //Debug.Log("raycast");
        Vector3 direction = target.position - transform.position;

        if (Mathf.Abs(Vector3.Angle(direction, transform.forward)) > fieldOfView) return false;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            pointer.position = hit.point;
            if (hit.transform == target) return true;
        }
        return false;
    }
    IEnumerator player_logic()
    {
        while (true)
        {


            if (state.im_kill_you_hahahahaha == my_state)
            {
                Debug.Log("im kill you hahahahahahaha");
                yield return null;

            }
            
            else
            {


                if (CanBeSeen(Player, 60))
                {
                    agent.destination = Player.position;

                }


                yield return null;


                yield return null;

            }
        }
    }
    IEnumerator BlinkStateDoing()
    {
        while (true)
        foreach (BreakDans that_point in ochenvashniedela)
        {
            yield return that_point.Do(this);
        }
        
    }

    IEnumerator FindStateDoing()
    {
        while (true)
        {
            agent.destination = LastSeen;

            //GameObject evil_clone = Instantiate(gameObject);
            //evil_clone.transform.position = LastSeen;

            //yield return new WaitForSeconds(20);

            yield return null;
            Debug.Log("you are noob");
            
        }

    }
    IEnumerator KillStateDoing()
    {

        while (true) 
        {
            Shoot();
            if (!invalid)
            {
                agent.destination = LastSeen;
            }
            yield return null;


        }                                                                                                                                               yield return null; 

    }

    IEnumerator ReachPoint(Vector3 cel)
    {
        agent.destination = cel;
        while (Vector3.Distance(cel, transform.position) > 10)
        {
            yield return null;

        }
        yield return null;
    }
    //IEnumerator PlayerRun()
    //{
    //    while (Vector3.Distance(Player.position, transform.position) > 1)
    //    {
    //        if (CanBeSeen(Player, 60))
    //        {
    //            agent.destination = Player.position;

    //        }


    //        yield return null;

    //    }
    //    yield return null;
    //}
    IEnumerator PlayerFind()
    {
        while (true)
        {
            yield return null;
            if (CanBeSeen(Player, 60))
            {
                LastSeen = Player.transform.position;
                Debug.Log("seen");
                warning_points += cadr_cof;
                cadr_cof += delta_cof*delta_delta_cof;

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







    private void Update()
    {
        //if (CanBeSeen(Player, 60)) Debug.Log("yes");
        //else Debug.Log("nope");
        TrackPlayer();

        //process_state();

    }

    // Update is called once per frame
    //
    //
    /// <summary>
    /// /
    /// </summary>
    /// //
    /// ////
    /// 
    //
    //
    //
    //update (trackPlayer)


    void process_state(state new_state)
    {
        if (my_state == new_state) return;
        my_state = new_state;

        Color new_color = Color.green;
        if (my_state == state.im_find_you) { 
            
            new_color = Color.yellow;
            agent.destination = transform.position;
            if(my_behavior!=null)StopCoroutine(my_behavior);
            my_behavior = StartCoroutine(FindStateDoing());

        }
        else if (my_state == state.im_kill_you_hahahahaha) new_color = Color.red;
        GetComponent<Renderer>().material.color = new_color;
    }
    void TrackPlayer()
    {
        if (warning_points < trigger_border)
        {
            process_state(state.im_blink);
        }
        else if (warning_points > kill_trigger_border)
        {
            process_state(state.im_kill_you_hahahahaha); ;
            cadr_cof = 60;
            cadr_minimum = trigger_border - 1;
            delta_delta_cof = index_delta_delta_cof;
        }
        else process_state(state.im_find_you); 
        //возможна ошибка. В случае ошибки условие переменная = 0
        if (invisible == 0)
        {
            visibility = 60;
        }
        else
        {
            visibility = 30;
        }
        bool is_he_cat = true;
        if (Player.gameObject.active) is_he_cat = false;
        List<float> range_player= player_delta_mod;
        if (is_he_cat){
            //возможна ошибка без copy
            range_player = cat_delta_mod;
        }

        Transform curBody = CatPlayer;
        if (!is_he_cat) curBody = Player;
        
        if (CanBeSeen(curBody, visibility))
        {
            //Debug.Log("seen");
            LastSeen = Player.position;


            float cat_dist = Vector3.Distance(transform.position, curBody.position);
            float range = 0;
            int i = 0;

            while (cat_dist>range)
            {
                range = range_delta_mod[i];
                i++;
            } 
            warning_points += cadr_cof;
            Debug.Log(range_player[i - 1]);
            Debug.Log(cadr_cof);
            cadr_cof += delta_cof*delta_delta_cof*range_player[i-1];
            Debug.Log(cadr_cof);

        }
        else
        {
            if (warning_points <= cadr_minimum)
            {
                cadr_cof = 0;
            }
            else
            {
                warning_points--;
            }
            /////
            //Debug.Log("-");
            //fghfgh
        }

       
    }
}

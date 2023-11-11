using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    Vector3 start_pos;
    public int chooer;
    void Start()
    {
        start_pos = transform.position;
        StartCoroutine(Move());


        ;
    }

    IEnumerator Move()
    {
      
        while (true)
        {
            for (int i = 0; i < 12000; i++)
            {
                transform.position += new Vector3(0, 0, 0.5f);
                yield return null;

            }
            transform.position = start_pos;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

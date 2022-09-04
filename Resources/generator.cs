using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour
{
    double last_time, cur_time;
    public GameObject badminton;
    Vector3 start_pos = new Vector3(0, 2, -6);
    Quaternion start_rotation = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        last_time = Time.time;
        cur_time = Time.time;
        start_rotation.eulerAngles = new Vector3(135, 0, 0);
        //badminton = (GameObject)Resources.Load("badminton");
    }

    // Update is called once per frame
    void Update()
    {
        cur_time = Time.time;
        if (cur_time - last_time > 3)
        {
            last_time = cur_time;
            GameObject new_obj = Instantiate(badminton, start_pos, start_rotation);
            Rigidbody rigid = new_obj.GetComponent<Rigidbody>();
            rigid.velocity = new Vector3(0, 15, 15);
            //Debug.Log("instantiate: ");
        }
    }
}

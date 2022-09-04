using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenPose.Example;
public class racket_controller : MonoBehaviour
{
    [SerializeField] Abcd abcd = null;
    //CharacterController m_controller;
    public Transform m_transform;
    Vector3 ori_rotation, m_rotation, m_pos, rotate_point;
    float m_movSpeed = 3.0f, w_speed = 3.0f;
    int launch_state = 0;
    // Start is called before the first frame update
    void Start()
    {
        //m_controller = this.GetComponent<CharacterController>();
        m_transform = this.transform;
        m_rotation = m_transform.eulerAngles;
        ori_rotation = m_rotation;
        m_pos = m_transform.position;
        rotate_point = new Vector3(0.0f, 1.5f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //float dy = Input.GetAxis("Mouse Y");
        //m_rotation.x -= 2*dy;
        //m_transform.eulerAngles = m_rotation;
        //rotate_point.z -= dy;
        //rotate_point.y += dy;
        if(abcd != null)
        {
            m_pos = abcd.RWristOut;
            rotate_point = abcd.RElbowOut;
        }
        m_transform.eulerAngles = Quaternion.FromToRotation(Vector3.forward, m_pos - rotate_point).eulerAngles;
        /*if (Input.GetKey(KeyCode.Space))
        {
            launch_state = 1;
            //
        }
        if (launch_state == 1)
        {
            if ((ori_rotation.x - m_transform.eulerAngles.x+360)%90 < 89)
            {
                //Debug.Log("ori.x = "+ori_rotation.x);
                m_transform.rotation = Quaternion.RotateTowards(m_transform.rotation, Quaternion.Euler(new Vector3(m_rotation.x-90, m_rotation.y, m_rotation.z)), w_speed);
                //Debug.Log("rotate: x = "+ m_transform.eulerAngles.x);
                //m_rotation.x -= w_speed;
                //m_controller.
                //m_transform.eulerAngles = m_rotation;
            }
            else
            {
                //m_rotation = ori_rotation;
                //m_transform.eulerAngles = m_rotation;
                //Debug.Log("recover");
                m_transform.rotation = Quaternion.RotateTowards(m_transform.rotation, Quaternion.Euler(new Vector3(ori_rotation.x, ori_rotation.y, ori_rotation.z)), 100);
                launch_state = 0;
            }
            
        }*/
        if (Input.GetKey(KeyCode.W))
        {
            m_pos.y += m_movSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            m_pos.y -= m_movSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_pos.z -= m_movSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_pos.z += m_movSpeed * Time.deltaTime;
        }
        // Debug.Log(" m_pos - rotate_point" + (m_pos - rotate_point).x + " " + (m_pos - rotate_point).y + " " + (m_pos - rotate_point).z);
        m_pos = new Vector3(0f, 1f, 2f);
        m_transform.position = m_pos;
    }
    void launch()
    {

    }
}

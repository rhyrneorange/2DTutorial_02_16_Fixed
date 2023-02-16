using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingTest : MonoBehaviour
{
    Camera m_mainCam;
    Ray m_ray;
    RaycastHit m_rayHit;
    GameObject m_selectObj;
    GameObject GetSelectedObject()
    {//��ũ�� ��ǥ�� Ray�� ����������
        m_ray = m_mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(m_ray, out m_rayHit, 100f)) //������ true, �ƴϸ� false
        {
            return m_rayHit.collider.gameObject;
        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //0:��Ŭ��
        {
            m_selectObj = GetSelectedObject();
            if (m_selectObj != null)
            {
                m_selectObj.transform.position += Vector3.back * 2f;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (m_selectObj != null)
            {
                m_selectObj.transform.position += Vector3.forward * 2f;
            }
            m_selectObj = null;
        }
        if(m_selectObj)
            Debug.DrawRay(m_ray.origin, m_ray.direction.normalized * m_rayHit.distance, Color.magenta);
        else
            Debug.DrawRay(m_ray.origin, m_ray.direction.normalized * 100f, Color.green);
    }
}

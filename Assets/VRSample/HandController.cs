using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    IGrabable target = null; //OnTrigger�� ���� ���
    IGrabable grabObj = null; //�����̽��� ������ ���� ���

    void Update()
    {
        if(target != null)  //OnTriggerEnter�� ���� �� �ִ� ����� ���� ���
        {
            if (Input.GetKeyDown(KeyCode.Space))    //�����̽��ٸ� ������, (VR�� ��� ���Ⱑ OVRInput.GetDown(OVRInput.PrimaryHandTrigger) �� ����)
            {
                if (grabObj == null)    //��� �ִ� ����� ���� ��
                {
                    grabObj = target;       //��� �ִ� ����� ���� OnTriggerEnter�� ���� ������� ����
                    target.Grab(transform); //IGrabable�������̽��� Grab�� ȣ���Ͽ� ��´�
                }
                else                    //�̹� ��� �ִ� ����� ���� ��
                {
                    grabObj.Release();      //IGrabable�������̽��� Grab�� ȣ���Ͽ� �����ְ�
                    grabObj = null;         //��� �ִ� ����� ����ش�.
                }
            }
        }


        //�̵� ��//
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * 10f* Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -10f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)         //Ʈ���� �ȿ� ������ �� ȣ�� ��
    {
        Debug.Log("In : " + other.gameObject.name);
        if(other.GetComponent<IGrabable>() != null)     //�浹�� ������Ʈ�� IGrabable�� ������,
        {
            Debug.Log("GrabTarget : " + other.gameObject.name);
            target = other.GetComponent<IGrabable>();   //IGrabable target�� �־��ش�.
        }
    }

    private void OnTriggerExit(Collider other)          //Ʈ���� ������ ����� �� ȣ�� ��
    {
        Debug.Log("Out : " + other.gameObject.name);
        if (other.GetComponent<IGrabable>() != null)    //�浹�� ������Ʈ�� IGrabable�� ������,
        {
            target = null;                              //IGrabable target�� ����ش�.
            Debug.Log("GrabTarget(release) : " + other.gameObject.name);
        }
    }
}

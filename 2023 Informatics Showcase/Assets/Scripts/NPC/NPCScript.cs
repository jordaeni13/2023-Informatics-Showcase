using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public GameObject rangeOb; //빈 오브젝트(Position), 하위 오브젝트받기
    CapsuleCollider rangeCol; //빈 오브젝트(Position)의 CircleCollider2D 가져오기

    private void Awake() //update를 안쓰기 때문에 Start가 안먹힘 == Awake쓰기
    {
        rangeCol = rangeOb.GetComponent<CapsuleCollider>(); // 빈오브젝트의 CircleCollider2D 받아오기
    }

    private void OnMouseDown() // 마우스로 클릭했을때 실행됨, 콜라이더 없으면 안됨
    {
        Vector3 NPCPos = rangeOb.transform.position; //지금 빈 오브젝트의 위치값 가져오기
        Debug.Log("클릭된 오브젝트 : " + gameObject.name);

        GameObject Main = GameObject.Find("NPCMain");
        Main.GetComponent<NPCMainScript>().NPCChatEnter("asdf");


    }
}
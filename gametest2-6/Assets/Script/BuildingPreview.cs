using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPreview : MonoBehaviour {
    //public GameObject Worker; //일꾼
    //public int field_check = 0; //0: 일반건물 1: 농장건설 2: 제재소건설
    //public int judge_build = 0; //이 오브젝트의 건설가능 여부 판단 0:가능 1: 불가능

    MeshRenderer Rend;

    // Use this for initialization
    void Start () {
        Rend = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	//void Update () {
 //       Buildjudgecheck();
 //       ObjectColor();
	//}

 //   void OnTriggerStay(Collider other)
 //   {
 //       if (field_check == 1)
 //       {
 //           if (other.CompareTag("foodtile"))
 //           {
 //               judge_build = 0;
 //           }
 //           else
 //           {
 //               judge_build = 1;
 //           }
 //       }

 //       if (field_check == 2)
 //       {
 //           if (other.CompareTag("woodtile"))
 //           {
 //               judge_build = 0;
 //           }
 //           else
 //           {
 //               judge_build = 1;
 //           }
 //       }

 //       if (field_check == 0)
 //       {
 //           judge_build = 0;
 //           //Debug.Log(other.tag);
 //       }

 //       if (other.CompareTag("watertile"))
 //       {
 //           judge_build = 1;
 //       }

        
 //   }

 //   public void Buildjudgecheck()
 //   {
 //       if(Worker.GetComponent<WorkerScript>().BuildType == 1)//농장건설
 //       {
 //           field_check = 1;
 //       }
 //       else if(Worker.GetComponent<WorkerScript>().BuildType == 3)//제재소건설
 //       {
 //           field_check = 2;
 //       }
 //       else
 //       {
 //           field_check = 0;
 //       }

        
 //   }

 //   public void ObjectColor()
 //   {
 //       if (judge_build == 0)
 //       {
 //           Rend.material.color = Color.white;
 //       }
 //       if (judge_build == 1)
 //       {
 //           Rend.material.color = Color.gray;
 //       }
 //   }

}

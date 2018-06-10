//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class unit : MonoBehaviour {

//    Unitmove Target = null;
//    UnitInfo Info = null;
//    Vector3 Positon;

//    // Use this for initialization
//    void Start () {
		
//	}
	
//	// Update is called once per frame
//	void Update () {
//        Unitaction();
//        Camerawalk();
//    }

//    public void Unitaction()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hitinfo;
            
//            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("playerunit")))
//            {
//                Target = hitinfo.collider.gameObject.GetComponent<Unitmove>();
//                //Info = hitinfo.collider.gameObject.GetComponent<UnitInfo>();
//            }
//            else
//                Target = null;
//        }
//        if (Input.GetMouseButtonDown(1))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hitinfo;

//            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
//            {
//                Positon = hitinfo.point;
//            }
//            Target.TargetPosition = Positon;
//        }
//    }

//    public void Camerawalk()
//    {
//        if (Input.GetKey(KeyCode.UpArrow))//키가 눌렸는지 확인하는 함수
//        {
//            transform.position += transform.forward * Time.deltaTime * 10 +
//                transform.up * Time.deltaTime * 10;
//            //여기서 forward는 파란 화살표가 가리키는 방향이다

//        }
//        if (Input.GetKey(KeyCode.RightArrow))
//        {
//            transform.position += transform.right * Time.deltaTime * 10;
//        }
//        if (Input.GetKey(KeyCode.LeftArrow))
//        {
//            transform.position -= transform.right * Time.deltaTime * 10;
//        }
//        if (Input.GetKey(KeyCode.DownArrow))
//        {
//            transform.position -= transform.forward * Time.deltaTime * 10
//                + transform.up * Time.deltaTime * 10;
//        }
//        if (Input.GetAxis("Mouse ScrollWheel") > 0)
//            transform.position += transform.forward;
//        if (Input.GetAxis("Mouse ScrollWheel") < 0)
//            transform.position -= transform.forward;
//    }
//}

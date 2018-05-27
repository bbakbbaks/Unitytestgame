using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Muiltiunitmove : MonoBehaviour {
    Unitmove Target = null;
    Unitmove Targetenemy = null;
    //UnitBox Targetcmd = null;
    //UnitBox Targetenemycmd = null;
    Vector3 Positon;
    List<Unitmove> m_cTargetlist = new List<Unitmove>();
    //List<UnitBox> m_cTargetcmdlist = new List<UnitBox>();
    float m_fDist = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MultiUnitaction();
        Camerawalk();
    }

    public void MultiUnitaction()
    {
        if (Input.GetMouseButtonDown(0)/*&&Input.GetKey(KeyCode.LeftShift)*/)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;

            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("playerunit")))
            {
                Target = hitinfo.collider.gameObject.GetComponent<Unitmove>();
                m_cTargetlist.Add(Target);
                //Targetcmd = hitinfo.collider.gameObject.GetComponent<UnitBox>();
                //m_cTargetcmdlist.Add(Targetcmd);    
            }
            else
            {
                m_cTargetlist.Clear();
                //Target = null;
                //m_cTargetcmdlist.Clear();
            }
        }
        if (Target != null) {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitinfo;

                if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("enemyunit")))
                {
                    Targetenemy = hitinfo.collider.gameObject.GetComponent<Unitmove>();
                    m_fDist = Vector3.Distance(Target.TargetPosition, Targetenemy.TargetPosition);
                    if (m_fDist < 5)
                        Debug.Log("Detect Enemy");
                }//문제점1 적을 찍어도 적 밑의 지형도 같이 찍히면서 타켓의 포지션이 적의 포지션 근처로
                //바뀌면서 적과의 거리가 무조건 지근거리로 나오게된다.
                //이것을 이너미유닛과 디폴트의 위치를 바꿔주는것만으로도 해결

                if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
                {
                    
                    Positon = hitinfo.point;             
                }
                for (int i = 0; i < m_cTargetlist.Count; i++)
                {
                    m_cTargetlist[i].TargetPosition = Positon;
                }
                //Target.TargetPosition = Positon;
                
            }
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 20), "Dist:" + m_fDist);
    }

    public void Camerawalk()
    {
        if (Input.GetKey(KeyCode.UpArrow))//키가 눌렸는지 확인하는 함수
        {
            transform.position += transform.forward * Time.deltaTime * 10 +
                transform.up * Time.deltaTime * 10;
            //여기서 forward는 파란 화살표가 가리키는 방향이다

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= transform.forward * Time.deltaTime * 10
                + transform.up * Time.deltaTime * 10;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            transform.position += transform.forward;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            transform.position -= transform.forward;
    }
}

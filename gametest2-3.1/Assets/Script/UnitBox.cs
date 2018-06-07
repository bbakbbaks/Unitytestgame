using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBox : MonoBehaviour {
    public UnitManager.eUnit m_eUnit;
    public Vector3 TargetPosition;
    NavMeshAgent nav;//유닛 박스에 직접 네비매시를 입력
    public Unit m_sUnit;
    public UnitHp m_UnitHp;
    float m_fMax; //Hp바 x축의 크기를 나타내는 변수
    float m_fDists; //유닛과 유닛사이의 거리 측정용 변수
    public UnitBox m_enemy = null; //타겟을 저장하는 변수
    public UnitBox m_MyBox; //자기 자신을 저장하는 변수
    public int DetectCheck = 0; //1이면 사거리안, 0이면 밖
    // public int counttest = 0;
    public int m_UnitCommend = 0; //1이면 공격모드, 0이면 스탑모드
    public int SelectCheck = 0; //1이면 유닛선택, 0이면 해제

    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        m_fMax = m_UnitHp.m_cRectTransform.sizeDelta.x;
        InvokeRepeating("Attack", 0, 1);
        //InvokeRepeating("DirectAttack", 0, 1);
    }
	
	// Update is called once per frame
	void Update () {
        nav.SetDestination(TargetPosition);
        //if (DetectCheck == 1)
        //{
        //    StartCoroutine("Attack");
        //}
        //Dead();
        UnitSelect();
        UnitCommend();
        UnitMove();
        Detect();
        //InvokeRepeating("Attack", 0, 1);
        //StartCoroutine("DirectAttack");
        DirectAttack();
    }
    
    public void ChangeHp(float unithp, float unitmaxhp)//HP바의 체력변화
    {
        float HpRatio = unithp / unitmaxhp * m_fMax;
        m_UnitHp.m_cRectTransform.sizeDelta=new Vector3(HpRatio,m_UnitHp.m_cRectTransform.sizeDelta.y);
    }

    public void Detect(/*UnitBox uBox*//*, UnitBox euBox*/)//유닛 탐지
    {
        //m_fDists = Vector3.Distance(uBox.transform.position, euBox.transform.position);
        if (m_MyBox != null)
        {
            Collider[] hitCollider = Physics.OverlapSphere(m_MyBox.transform.position, 6.0f/*, 1 << LayerMask.NameToLayer("enemyunit")*/); //탐지

            if (m_MyBox.tag == "PlayerU" && m_UnitCommend != 0)
            {
                foreach (Collider hit in hitCollider)
                {
                    if (hit.tag == "EnemyU")
                    {
                        //Debug.Log("탐지");
                        m_enemy = hit.gameObject.GetComponent<UnitBox>(); //탐지 한 적개체를 타겟으로 지정
                        m_fDists = Vector3.Distance(m_MyBox.transform.position, m_enemy.transform.position);
                        if (m_fDists > m_MyBox.m_sUnit.Range)
                        {
                            m_MyBox.TargetPosition = m_enemy.transform.position;//적유닛의 위치로이동
                                                                                //Debug.Log("too far");
                            DetectCheck = 0;
                        }
                        if (m_fDists <= m_MyBox.m_sUnit.Range)//사거리 안으로 이동시
                        {
                            m_MyBox.TargetPosition = m_MyBox.transform.position;//현재위치에 멈춤
                                                                                //Attack();
                                                                                //StartCoroutine("Attack");
                            DetectCheck = 1;
                        }
                    }
                }
            }
            //if (m_MyBox.tag == "EnemyU")
            //{
            //    foreach (Collider hit in hitCollider)
            //    {
            //        if (hit.tag == "PlayerU")
            //        {
            //            //Debug.Log("탐지");
            //            m_enemy = hit.gameObject.GetComponent<UnitBox>(); //탐지 한 적개체를 타겟으로 지정
            //            m_fDists = Vector3.Distance(m_MyBox.transform.position, m_enemy.transform.position);
            //            if (m_fDists > m_MyBox.m_sUnit.Range)
            //            {
            //                m_MyBox.TargetPosition = m_enemy.transform.position;//적유닛의 위치로이동
            //                                                                    //Debug.Log("too far");
            //                DetectCheck = 0;
            //            }
            //            if (m_fDists <= m_MyBox.m_sUnit.Range)//사거리 안으로 이동시
            //            {
            //                m_MyBox.TargetPosition = m_MyBox.transform.position;//현재위치에 멈춤
            //                                                                    //Attack();
            //                                                                    //StartCoroutine("Attack");
            //                DetectCheck = 1;
            //            }
            //        }
            //    }
            //}
        }
    }

    public void DirectAttack()
    {
        if (m_MyBox.tag=="PlayerU" && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;

            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("enemyunit")))//타겟 선택 공격
            {
                m_enemy = hitinfo.collider.gameObject.GetComponent<UnitBox>();

                m_fDists = Vector3.Distance(m_MyBox.transform.position, m_enemy.transform.position);
                if (m_fDists <= m_MyBox.m_sUnit.Range)
                {
                    m_MyBox.TargetPosition = m_MyBox.transform.position;
                    if (m_enemy != null)
                    {
                        m_enemy.m_sUnit.Hp = m_enemy.m_sUnit.Hp - m_sUnit.Damage;
                        m_enemy.ChangeHp(m_enemy.m_sUnit.Hp, m_enemy.m_sUnit.MaxHp);
                        //counttest++;
                        if (m_enemy.m_sUnit.Hp <= 0)
                        {
                            Destroy(m_enemy.gameObject);
                            m_enemy = null;
                        }
                    }
                }
                if (m_fDists > m_MyBox.m_sUnit.Range)
                {
                    m_MyBox.TargetPosition = m_enemy.transform.position;
                }
            }
        }
    }
    //어택땅 키와 그냥 이동만 하는 키를 따로 만들어야한다.

    public void Attack()//유닛을 한번때리고 함수가 종료되는것이 아닌 죽일때까지 반복되는듯함
    {
        if (m_enemy != null)
        {
            if (m_UnitCommend == 1 && DetectCheck == 1)
            {
                m_enemy.m_sUnit.Hp = m_enemy.m_sUnit.Hp - m_sUnit.Damage;
                m_enemy.ChangeHp(m_enemy.m_sUnit.Hp, m_enemy.m_sUnit.MaxHp);
                //counttest++;
                if (m_enemy.m_sUnit.Hp <= 0)
                {
                    Destroy(m_enemy.gameObject);
                    m_enemy = null;
                }
            }
        }
    }

    public void UnitCommend()
    {
        if (SelectCheck == 1 && m_MyBox.tag == "PlayerU")
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                m_UnitCommend = 1;
                Debug.Log("attack");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                m_UnitCommend = 0;
                Debug.Log("stop");
            }
        }
    }

    public void UnitSelect()
    {
        if (m_MyBox.tag == "PlayerU" && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("playerunit")))
            {
                m_MyBox = hitinfo.collider.gameObject.GetComponent<UnitBox>();
                m_MyBox.SelectCheck = 1;
                Debug.Log(SelectCheck);
            }
            else if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
            {
                SelectCheck = 0;
                Debug.Log(SelectCheck);
            }
        }
    }

    public void UnitMove()
    {
        if (SelectCheck == 1 && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
            {
                m_UnitCommend = 0;
                TargetPosition = hitinfo.point;
            }
        }
    }
    //public void Dead()
    //{
    //    if (m_MyBox.m_sUnit.Hp <= 0)
    //    {
    //        Destroy(m_MyBox.gameObject);
    //    }
    //}
    //IEnumerator Attack()
    //{
    //    if (m_enemy != null)
    //    {
    //        yield return new WaitForSeconds(1.0f);
    //        m_enemy.m_sUnit.Hp = m_enemy.m_sUnit.Hp - m_sUnit.Damage;
    //        m_enemy.ChangeHp(m_enemy.m_sUnit.Hp, m_enemy.m_sUnit.MaxHp);
    //        if (m_enemy.m_sUnit.Hp <= 0)
    //        {
    //            Destroy(m_enemy.gameObject);
    //            m_enemy = null;
    //        }
    //    }
    //}

}

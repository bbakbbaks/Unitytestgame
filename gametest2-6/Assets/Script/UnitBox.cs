using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UnitBox : MonoBehaviour {
    public UnitManager.eUnit m_eUnit;
    Vector3 TargetPosition;
    NavMeshAgent nav;//유닛 박스에 직접 네비매시를 입력
    public Unit m_sUnit;
    public UnitHp m_UnitHp;
    float m_fMax; //Hp바 x축의 크기를 나타내는 변수
    float m_fDists; //유닛과 유닛사이의 거리 측정용 변수
    public UnitBox m_enemy = null; //타겟을 저장하는 변수
    //public UnitBox m_MyBox; //자기 자신을 저장하는 변수
    public Center m_enemys_target; //좀비가 센터를 타겟으로 정하는 변수
    public Center TargetCenter; //센터의 위치를 가져오기 위한 변수
    public BuildingBox m_TargetofZombie = null;
    public int DetectCheck = 0; //1이면 사거리안 유닛, 0이면 밖, 2면 건물, 3이면 센터
    public int m_UnitCommend = 0; //1이면 공격모드, 0이면 스탑모드
    public int SelectCheck = 0; //1이면 유닛선택, 0이면 해제
    public GameObject HitEffect;
    public GameObject HitEffectpoint;
    public GameObject ShootEffect;
    public GameObject ShootEffectpoint;
    public GameObject HpbarPosition;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        TargetPosition = this.transform.position;
        Debug.Log(this.transform.position);       
        m_fMax = m_UnitHp.m_cRectTransform.sizeDelta.x;
        InvokeRepeating("Attack", 0, 1);
        InvokeRepeating("ZombieAttacktoBuilding", 0, 1);
        //StartPosition();
        //InvokeRepeating("ClockCount", 1, 1);
    }

    void Update()
    {
        nav.SetDestination(TargetPosition);
        UnitSelect();
        UnitCommend();
        UnitMove();
        Detect();
        DirectAttack();
        //TestBuildingAttack();
        //PdBuilding();
        Dead();
        //Build();
        //TimeCounter();
        HpbarPosition.transform.rotation = Quaternion.Euler(0, -180, 0);
    }

    public void ChangeHp(float unithp, float unitmaxhp)//HP바의 체력변화
    {
        float HpRatio = unithp / unitmaxhp * m_fMax;
        m_UnitHp.m_cRectTransform.sizeDelta = new Vector3(HpRatio, m_UnitHp.m_cRectTransform.sizeDelta.y);
    }

    public void Detect(/*UnitBox uBox*//*, UnitBox euBox*/)//유닛 탐지
    {
        if (this != null)
        {
            Collider[] hitCollider = Physics.OverlapSphere(this.transform.position, 6.0f); //탐지

            if (this.tag == "PlayerU" && m_UnitCommend != 0)
            {
                foreach (Collider hit in hitCollider)
                {
                    if (hit.tag == "EnemyU")
                    {
                        m_enemy = hit.gameObject.GetComponent<UnitBox>(); //탐지 한 적개체를 타겟으로 지정
                        m_fDists = Vector3.Distance(this.transform.position, m_enemy.transform.position);
                        if (m_fDists > this.m_sUnit.Range)
                        {
                            this.TargetPosition = m_enemy.transform.position;//적유닛의 위치로이동
                            DetectCheck = 0;
                        }
                        if (m_fDists <= this.m_sUnit.Range)//사거리 안으로 이동시
                        {
                            this.TargetPosition = this.transform.position;//현재위치에 멈춤
                            DetectCheck = 1;
                        }
                    }
                }
            }

            //Collider[] hitZombieCollider = Physics.OverlapSphere(this.transform.position, 100.0f);
            if (this.tag == "EnemyU")
            {
                foreach (Collider hit in hitCollider)
                {
                    if (hit.tag == "PlayerU")
                    {
                        //Debug.Log("탐지");
                        m_enemy = hit.gameObject.GetComponent<UnitBox>(); //탐지 한 적개체를 타겟으로 지정
                        m_fDists = Vector3.Distance(this.transform.position, m_enemy.transform.position);
                        if (m_fDists > this.m_sUnit.Range)
                        {
                            this.TargetPosition = m_enemy.transform.position;//적유닛의 위치로이동
                                                                             //Debug.Log("too far");
                            DetectCheck = 0;
                        }
                        if (m_fDists <= this.m_sUnit.Range)//사거리 안으로 이동시
                        {
                            this.TargetPosition = this.transform.position;//현재위치에 멈춤
                                                                          //Attack();
                                                                          //StartCoroutine("Attack");
                            DetectCheck = 1;
                        }
                    }
                    else if (hit.tag == "PlayerB")
                    {
                        m_TargetofZombie = hit.gameObject.GetComponent<BuildingBox>();
                        m_fDists = Vector3.Distance(this.transform.position, m_TargetofZombie.transform.position);
                        if (m_fDists > this.m_sUnit.Range)
                        {
                            this.TargetPosition = m_TargetofZombie.transform.position;
                        }
                        if (m_fDists <= this.m_sUnit.Range + 1)
                        {
                            this.TargetPosition = this.transform.position;
                            DetectCheck = 2;
                        }
                    }
                    if (hit.tag == "Center")
                    {
                        m_enemys_target = hit.gameObject.GetComponent<Center>();
                        m_fDists = Vector3.Distance(this.transform.position, m_enemys_target.transform.position);
                        if (m_fDists > this.m_sUnit.Range)
                        {
                            this.TargetPosition = m_enemys_target.transform.position;
                            //ZombieDeCheck = 0;
                        }
                        if (m_fDists <= this.m_sUnit.Range + 2)
                        {
                            this.TargetPosition = this.transform.position;
                            //ZombieDeCheck = 1;
                            DetectCheck = 3;
                        }
                    }
                }
            }
        }
    }

    public void DirectAttack()
    {
        if (this.tag == "PlayerU" && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;

            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("enemyunit")))//타겟 선택 공격
            {
                m_enemy = hitinfo.collider.gameObject.GetComponent<UnitBox>();

                m_fDists = Vector3.Distance(this.transform.position, m_enemy.transform.position);
                if (m_fDists <= this.m_sUnit.Range)
                {
                    this.TargetPosition = this.transform.position;
                    this.m_UnitCommend = 1;
                }
                if (m_fDists > this.m_sUnit.Range)
                {
                    this.TargetPosition = m_enemy.transform.position;
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
                this.transform.LookAt(m_enemy.transform);
                if (this.CompareTag("PlayerU"))
                {
                    Instantiate(HitEffect, HitEffectpoint.transform.position, Quaternion.identity);
                    SoundManager.instance.PlaySound();
                }
                Instantiate(ShootEffect, ShootEffectpoint.transform.position, Quaternion.identity);
                m_enemy.ChangeHp(m_enemy.m_sUnit.Hp, m_enemy.m_sUnit.MaxHp);
                if (m_enemy.m_sUnit.Hp <= 0)
                {
                    //Destroy(m_enemy.gameObject);
                    m_enemy = null;
                }
            }
        }
    }

    public void UnitCommend()
    {
        if (SelectCheck == 1 && this.tag == "PlayerU")
        {
            //GameManager.GetInstance().m_cGUIManager.SetScene(GUIManager.eScene.UCOMMEND);
            if (Input.GetKeyDown(KeyCode.A))
            {
                m_UnitCommend = 1;
                //Debug.Log("attack");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                m_UnitCommend = 0;
                this.TargetPosition = this.transform.position;
                //Debug.Log("stop");
            }
        }
    }

    public void UnitSelect()
    {
        if (this.tag == "PlayerU" && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("playerunit")))
            {
                hitinfo.collider.gameObject.GetComponent<UnitBox>().SelectCheck = 1;               
                GameManager.GetInstance().UnitSelectCount++;
            }
            else if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
            {
                SelectCheck = 0;
                GameManager.GetInstance().UnitSelectCount = 0;
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
                this.m_UnitCommend = 0;
                this.TargetPosition = hitinfo.point;
            }
        }

        if(this.tag=="EnemyU" && m_enemy == null)
        {
            this.TargetPosition = TargetCenter.transform.position;
        }
    }

    public void ZombieAttacktoBuilding()
    {
        if (this.tag == "EnemyU")
        {
            if (DetectCheck == 2 && m_TargetofZombie != null)
            {
                m_TargetofZombie.m_Building.Hp = m_TargetofZombie.m_Building.Hp - m_sUnit.Damage;
                //Debug.Log("건물공격중");
                m_TargetofZombie.ChangeHp(m_TargetofZombie.m_Building.Hp, m_TargetofZombie.m_Building.MaxHp);
                if (m_TargetofZombie.m_Building.Hp <= 0)
                {
                    //Destroy(m_TargetofZombie.gameObject);
                    m_TargetofZombie = null;
                }
            }
            if (DetectCheck == 3 && m_enemys_target != null)
            {
                m_enemys_target.m_Center.Hp = m_enemys_target.m_Center.Hp - m_sUnit.Damage;
                //Debug.Log("1");
                m_enemys_target.ChangeHp(m_enemys_target.m_Center.Hp, m_enemys_target.m_Center.MaxHp);
                if (m_enemys_target.m_Center.Hp <= 0)
                {
                    Destroy(m_enemys_target.gameObject);
                    m_enemys_target = null;
                }
            }
        }
    }

    public void Dead()
    {
        if (this.m_sUnit.Hp <= 0)
        {
            if (this.tag == "PlayerU")
            {
                GameManager.GetInstance().NowPopulation--;
            }
            if (this.tag == "EnemyU")
            {
                GameManager.GetInstance().ZombieAmount--;
            }
            Destroy(this.gameObject);
        }
    }
}

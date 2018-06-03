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
    float m_fMax;
    float m_fDists;
    public UnitBox m_enemy = null;

    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        m_fMax = m_UnitHp.m_cRectTransform.sizeDelta.x;
    }
	
	// Update is called once per frame
	void Update () {
        nav.SetDestination(TargetPosition);
        Attack();
    }
    
    public void ChangeHp(float unithp, float unitmaxhp)//HP바의 체력변화
    {
        float HpRatio = unithp / unitmaxhp * m_fMax;
        m_UnitHp.m_cRectTransform.sizeDelta=new Vector3(HpRatio,m_UnitHp.m_cRectTransform.sizeDelta.y);
    }
    //공격 ai 를 만들어야한다.

    public void Detect(UnitBox uBox, UnitBox euBox)
    {
        if (euBox != null)
        {
            m_fDists = Vector3.Distance(uBox.transform.position, euBox.transform.position);
            Collider[] hitCollider = Physics.OverlapSphere(uBox.transform.position, 6.0f, 1 << LayerMask.NameToLayer("enemyunit")); //탐지

            foreach (Collider hit in hitCollider)
            {
                if (hit.tag == "EnemyU")
                {
                    Debug.Log("탐지");
                    m_enemy = euBox;
                    if (m_fDists > uBox.m_sUnit.Range)
                    {
                        Debug.Log("too far");
                    }
                    if (m_fDists <= uBox.m_sUnit.Range)
                    {
                        Attack();
                    }
                }
            }
        }
    }


    public void Attack()
    {
        if (m_enemy != null)
        {
            m_enemy.m_sUnit.Hp = m_enemy.m_sUnit.Hp - m_sUnit.Damage;
            m_enemy.ChangeHp(m_enemy.m_sUnit.Hp, m_enemy.m_sUnit.MaxHp);
            if (m_enemy.m_sUnit.Hp <= 0)
            {
                Destroy(m_enemy.gameObject);
                m_enemy = null;
            }
        }
    }
}

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

    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        m_fMax = m_UnitHp.m_cRectTransform.sizeDelta.x;
    }
	
	// Update is called once per frame
	void Update () {
        nav.SetDestination(TargetPosition);
    }
    
    public void ChangeHp(float unithp, float unitmaxhp)//HP바의 체력변화
    {
        float HpRatio = unithp / unitmaxhp * m_fMax;
        m_UnitHp.m_cRectTransform.sizeDelta=new Vector3(HpRatio,m_UnitHp.m_cRectTransform.sizeDelta.y);
    }
    
}

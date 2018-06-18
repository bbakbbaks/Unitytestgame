using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour {
    public Building m_Center;
    //public GameManager GameM;
    public Transform Regenposition;
    public UnitHp m_CHp;
    //public int CenterCheck = 0;
    float m_fMax;

    // Use this for initialization
    void Start () {
        m_fMax = m_CHp.m_cRectTransform.sizeDelta.x;
        //m_Center = new Building("지휘본부", 500, 0, 0, "imgcenter");
    }
	
	// Update is called once per frame
	void Update () {
        //DestroyCenter();
	}

    public void ChangeHp(float unithp, float unitmaxhp)//HP바의 체력변화
    {
        float HpRatio = unithp / unitmaxhp * m_fMax;
        m_CHp.m_cRectTransform.sizeDelta = new Vector3(HpRatio, m_CHp.m_cRectTransform.sizeDelta.y);
    }

    //public void DestroyCenter()
    //{
    //    if (m_Center.Hp <= 0)
    //        CenterCheck = 1;
    //}
}

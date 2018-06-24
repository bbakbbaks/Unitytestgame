using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBox : MonoBehaviour {
    public BuildingManager.eBuilding m_eBuilding;
    public Building m_Building;
    public UnitHp m_Hp;
    float m_fMax;

    // Use this for initialization
    void Start () {
        m_fMax = m_Hp.m_cRectTransform.sizeDelta.x;
	}
	
	// Update is called once per frame
	void Update () {
        DTbuilding();
	}

    public void ChangeHp(float unithp, float unitmaxhp)//HP바의 체력변화
    {
        float HpRatio = unithp / unitmaxhp * m_fMax;
        m_Hp.m_cRectTransform.sizeDelta = new Vector3(HpRatio, m_Hp.m_cRectTransform.sizeDelta.y);
    }

    public void DTbuilding()
    {
        if (this.m_Building.Hp <= 0) 
        {
            if (this.m_Building.Name == "제재소")
            {
                GameManager.GetInstance().LumCount--;
            }
            if (this.m_Building.Name == "농장")
            {
                GameManager.GetInstance().FarmCount--;
            }
            if (this.m_Building.Name == "집")
            {
                GameManager.GetInstance().HouseCount--;
            }
            Destroy(this.gameObject);
        }
    }
}

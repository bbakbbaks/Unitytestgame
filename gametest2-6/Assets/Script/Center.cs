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
        PdUnit();
	}

    public void ChangeHp(float unithp, float unitmaxhp)//HP바의 체력변화
    {
        float HpRatio = unithp / unitmaxhp * m_fMax;
        m_CHp.m_cRectTransform.sizeDelta = new Vector3(HpRatio, m_CHp.m_cRectTransform.sizeDelta.y);
    }

    public void PdUnit()
    {
        if (Input.GetKeyDown(KeyCode.C) && GameManager.GetInstance().Food >= 10 && GameManager.GetInstance().MaxPopulation > GameManager.GetInstance().NowPopulation)
        {
            GameObject pdWorker = Instantiate(GameManager.GetInstance().G_Worker, this.Regenposition.position, Quaternion.identity);
            GameManager.GetInstance().unitcount++;
            pdWorker.name = "worker";
            GameManager.GetInstance().m_cUnits.Add(pdWorker.GetComponent<UnitBox>());
            GameManager.GetInstance().m_cUnits[GameManager.GetInstance().unitcount].m_sUnit = GameManager.GetInstance().m_cUnitManager.GetUnit(UnitManager.eUnit.Worker);
            //Debug.Log(m_cUnits[unitcount].m_sUnit.Name);
            GameManager.GetInstance().Food -= 10;
            GameManager.GetInstance().NowPopulation++;
        }
    }
}

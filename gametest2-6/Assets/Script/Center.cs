using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour {
    public Building m_Center;
    //public GameManager GameM;
    public Transform Regenposition;
    public UnitHp m_CHp;
    public GameObject Center_UI;
    //public int CenterCheck = 0;
    public int UnitCheck = 0;
    public int SelectCheck = 0; 
    float m_fMax;

    // Use this for initialization
    void Start () {
        m_fMax = m_CHp.m_cRectTransform.sizeDelta.x;
        //m_Center = new Building("지휘본부", 500, 0, 0, "imgcenter");
    }
	
	// Update is called once per frame
	void Update () {
        PdUnit();
        CenterSelect();
        CenterUI();
        UCheck2();
    }

    public void ChangeHp(float unithp, float unitmaxhp)//HP바의 체력변화
    {
        float HpRatio = unithp / unitmaxhp * m_fMax;
        m_CHp.m_cRectTransform.sizeDelta = new Vector3(HpRatio, m_CHp.m_cRectTransform.sizeDelta.y);
    }

    public void CenterSelect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo = new RaycastHit();
        if (Input.GetMouseButtonDown(0))
        {                      
            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Center")))
            {
                hitinfo.collider.gameObject.GetComponent<Center>().SelectCheck = 1;
                //if (hitinfo.collider.tag == "Center")
                //{
                    
                //    this.SelectCheck = 1;
                //}
                //if(hitinfo.collider.tag!="Center" || hitinfo.collider.tag != "Map")
                //{
                //    SelectCheck = 0;
                //}
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            SelectCheck = 0;
        }
    }

    public void UCheck()
    {
        if (GameManager.GetInstance().Food >= 10 && GameManager.GetInstance().MaxPopulation > GameManager.GetInstance().NowPopulation)
        {
            UnitCheck = 1;
        }
    }
    public void UCheck2()
    {
        if (SelectCheck == 1 && Input.GetKeyDown(KeyCode.C) && GameManager.GetInstance().Food >= 10 && GameManager.GetInstance().MaxPopulation > GameManager.GetInstance().NowPopulation)
        {
            UnitCheck = 1;
        }
    }

    public void CenterUI()
    {
        if (SelectCheck == 1)
        {
            Center_UI.SetActive(true);
        }
        else
        {
            Center_UI.SetActive(false);
        }
    }

    public void PdUnit()
    {
        if (this.UnitCheck == 1)
        {
            GameObject pdWorker = Instantiate(GameManager.GetInstance().G_Worker, this.Regenposition.position, Quaternion.identity);
            GameManager.GetInstance().unitcount++;
            pdWorker.name = "worker";
            GameManager.GetInstance().m_cUnits.Add(pdWorker.GetComponent<UnitBox>());
            GameManager.GetInstance().m_cUnits[GameManager.GetInstance().unitcount].m_sUnit = GameManager.GetInstance().m_cUnitManager.GetUnit(UnitManager.eUnit.Worker);
            //Debug.Log(m_cUnits[unitcount].m_sUnit.Name);
            GameManager.GetInstance().Food -= 10;
            GameManager.GetInstance().NowPopulation++;
            UnitCheck = 0;
        }
    }
}

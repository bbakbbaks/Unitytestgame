using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public GameObject FireEffect;
    public GameObject Firepoint1; //체력 75퍼이하일때 불
    bool Fire1check = false;
    public GameObject Firepoint2; //체력 50퍼이하일때 불
    bool Fire2check = false;
    public GameObject Firepoint3; //체력 25퍼이하일때 불
    bool Fire3check = false;
    GameObject Fire1;
    GameObject Fire2;
    GameObject Fire3;
    //public GameObject DestroyEffect;
    //GameObject Destroy1;
    //bool Destroycheck = false;

    // Use this for initialization
    void Start () {
        m_fMax = m_CHp.m_cRectTransform.sizeDelta.x;
        m_Center = new Building("지휘본부", 500, 0, 0, "imgcenter");
    }
	
	// Update is called once per frame
	void Update () {
        PdUnit();
        CenterSelect();
        CenterUI();
        UCheck2();
        Damagedbyzombie();
    }

    public void ChangeHp(float unithp, float unitmaxhp)//HP바의 체력변화
    {
        float HpRatio = unithp / unitmaxhp * m_fMax;
        m_CHp.m_cRectTransform.sizeDelta = new Vector3(HpRatio, m_CHp.m_cRectTransform.sizeDelta.y);
    }

    public void CenterSelect()
    {
        if (!(GameManager.GetInstance().UnitSelectCheck))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo = new RaycastHit();
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false) //UI위가 아닐경우에
                {
                    if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Center")))
                    {
                        hitinfo.collider.gameObject.GetComponent<Center>().SelectCheck = 1;
                        GameManager.GetInstance().BuildingSelectCheck = true;
                    }
                    else
                    {
                        SelectCheck = 0;
                        GameManager.GetInstance().BuildingSelectCheck = false;
                    }
                }
            }
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

    public void Damagedbyzombie()
    {
        float HpRatio = m_Center.Hp / m_Center.MaxHp * 100;
        //Debug.Log(m_Center.MaxHp);
        if (HpRatio <= 75 && !(Fire1check))
        {
            Fire1 = Instantiate(FireEffect, Firepoint1.transform.position, Quaternion.identity);
            Fire1check = true;
        }
        if (HpRatio <= 50 && !(Fire2check))
        {
            Fire2 = Instantiate(FireEffect, Firepoint2.transform.position, Quaternion.identity);
            Fire2check = true;
        }
        if (HpRatio <= 25 && !(Fire3check))
        {
            Fire3 = Instantiate(FireEffect, Firepoint3.transform.position, Quaternion.identity);
            Fire3check = true;
        }
        if (HpRatio <= 0)
        {
            Destroy(this.gameObject);
            Destroy(Fire1);
            Destroy(Fire2);
            Destroy(Fire3);
            //if (!(Destroycheck))
            //{
            //    Destroy1 = Instantiate(DestroyEffect, this.transform.position, Quaternion.identity);
            //    Destroycheck = true;
            //}
            //double Destime = 0.1;
            //Destime -= Time.deltaTime;
            //if (Destime <= 0)
            //{

            //}          
        }
    }
}

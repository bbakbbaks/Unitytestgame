﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Barrack : MonoBehaviour {
    public Transform Regenposition;
    public GameObject Barrack_UI;
    public int SelectCheck = 0;
    public int SoliderCount = -1;
    public GameObject selectcircle;

    // Use this for initialization
    void Start () {
        InvokeRepeating("CountDown", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
        BarrackSelect();
        PdSoliderCount();
        PdUnit();
        BarrackUI();
        //TimeCounter();
        circlecheck();
    }

    public void circlecheck()
    {
        if (SelectCheck == 1)
        {
            selectcircle.SetActive(true);
        }
        else
        {
            selectcircle.SetActive(false);
        }
    }

    public void BarrackSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitinfo;
                if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Barrack")))
                {
                    if (!(GameManager.GetInstance().UnitSelectCheck))
                    {
                        hitinfo.collider.gameObject.GetComponent<Barrack>().SelectCheck = 1;
                        GameManager.GetInstance().BuildingSelectCheck = true;
                    }
                }
                else
                {
                    SelectCheck = 0;
                    GameManager.GetInstance().BuildingSelectCheck = false;
                }
            }
        }
    }

    public void BarrackUI()
    {
        if (SelectCheck == 1)
        {
            Barrack_UI.SetActive(true);
        }
        else
        {
            Barrack_UI.SetActive(false);
        }
    }

    public void PdSoliderCount()
    {
        if (SelectCheck == 1 && Input.GetKeyDown(KeyCode.M) && GameManager.GetInstance().Food >= 25 && 
            GameManager.GetInstance().MaxPopulation > GameManager.GetInstance().NowPopulation)//배럭에서 솔저 생산
        {
            this.SoliderCount = 2;
        }
    }
    public void PdSoliderCount2()
    {
        if (GameManager.GetInstance().Food >= 25 && GameManager.GetInstance().MaxPopulation > GameManager.GetInstance().NowPopulation)
        {
            this.SoliderCount = 2;
        }
    }

    public void CountDown()
    {
        if(this.SoliderCount >= 1)
        {
            this.SoliderCount--;
        }
    }

    public void TimeCounter()
    {
        if (this.SoliderCount > 0)
        {
            GameManager.GetInstance().m_cGUIManager.m_textInfo.text = "군인 생성중..." + this.SoliderCount;
        }
        if (this.SoliderCount < 0)
        {
            GameManager.GetInstance().m_cGUIManager.m_textInfo.text = "";
        }
    }

    public void PdUnit()
    {
        if (this.SoliderCount == 0)//배럭에서 솔저 생산
        {
            GameObject pdSolider = Instantiate(GameManager.GetInstance().G_Solider, this.transform.position, Quaternion.identity);
            pdSolider.name = "solider";
            GameManager.GetInstance().unitcount++;
            GameManager.GetInstance().m_cUnits.Add(pdSolider.GetComponent<UnitBox>());
            GameManager.GetInstance().m_cUnits[GameManager.GetInstance().unitcount].m_sUnit = GameManager.GetInstance().m_cUnitManager.GetUnit(UnitManager.eUnit.Solider);
            //Debug.Log(m_cUnits[unitcount].m_sUnit.Name);
            GameManager.GetInstance().Food -= 25;
            GameManager.GetInstance().NowPopulation++;
            this.SoliderCount = -1;
        }
    }

}

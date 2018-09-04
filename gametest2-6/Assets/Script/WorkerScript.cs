﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WorkerScript : MonoBehaviour
{
    public Vector3 TargetPosition;
    public GameObject test;
    NavMeshAgent nav;//유닛 박스에 직접 네비매시를 입력
    public int SelectCheck = 0; //1이면 유닛선택, 0이면 해제
    public int BuildingNumber = 0; //(미리보기)1: Farm, 2: Barrack, 3: Lumber, 4: House, 5: WallHo, 6: WallVer
    public int BuildClock = -1; //건설 시간
    public int BuildType = 0; //건설할 건물 종류
    public int DestinationCheck = 0; //일꾼의 목적지 도착여부
    Vector3 BuildingPosi;
    GameObject BuildPreview;
    public GameObject Commend_UI;
    public GameObject Build_UI;

    void Start()
    {
        TargetPosition = this.transform.position;
        nav = GetComponent<NavMeshAgent>();
        InvokeRepeating("ClockCount", 1, 1);
    }

    void Update()
    {
        nav.SetDestination(TargetPosition);
        UnitSelect();
        PdBuilding();
        Build();
        TimeCounter();
        UnitMove();
        BuildNumber();
        ButtonActive();
    }

    public void UnitMove()
    {
        if (this.SelectCheck == 1 && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
            {
                this.TargetPosition = hitinfo.point;
            }
        }

    }

    public void UnitSelect()
    {
        if (BuildPreview == null && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("playerunit")))
            {
                if (hitinfo.collider.name == "worker")
                {
                    hitinfo.collider.gameObject.GetComponent<WorkerScript>().SelectCheck = 1;
                }
            }
            //else if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
            //{
            //    SelectCheck = 0;
            //}
        }
    }

    public void ButtonActive()
    {
        if (SelectCheck == 1)
        {
            Commend_UI.SetActive(true);
        }
        else
        {
            Commend_UI.SetActive(false);
        }
    }

    public void BuildButton()
    {
        Build_UI.SetActive(true);
        Commend_UI.SetActive(false);
    }

    public void BackButton()
    {
        Commend_UI.SetActive(true);
        Build_UI.SetActive(false);
    }

    public void BuildNumber()
    {
        if (BuildPreview == null)
        {
            if (Input.GetKeyDown(KeyCode.F) && GameManager.GetInstance().Wood >= 100)
            {
                this.BuildingNumber = 1;
                //this.BuildClock = 5;
            }
            if (Input.GetKeyDown(KeyCode.B) && GameManager.GetInstance().Wood >= 200)
            {
                this.BuildingNumber = 2;
                //this.BuildClock = 5;
            }
            if (Input.GetKeyDown(KeyCode.L) && GameManager.GetInstance().Wood >= 100)
            {
                this.BuildingNumber = 3;
                //this.BuildClock = 5;
            }
            if (Input.GetKeyDown(KeyCode.H) && GameManager.GetInstance().Wood >= 50)
            {
                this.BuildingNumber = 4;
                //this.BuildClock = 5;
            }
            if (Input.GetKey(KeyCode.W) && GameManager.GetInstance().Wood >= 20)
            {
                this.BuildingNumber = 5;
                //this.BuildClock = 1;
            }
            if (Input.GetKey(KeyCode.Q) && GameManager.GetInstance().Wood >= 20)
            {
                this.BuildingNumber = 6;
                //this.BuildClock = 1;
            }
        }
    }

    public void FarmButton()
    {
        this.BuildingNumber = 1;
    }

    public void BarrackButton()
    {
        this.BuildingNumber = 2;
    }

    public void LumberButton()
    {
        this.BuildingNumber = 3;
    }

    public void HouseButton()
    {
        this.BuildingNumber = 4;
    }

    public void WallHoButton()
    {
        this.BuildingNumber = 5;
    }

    public void WallVerButton()
    {
        this.BuildingNumber = 6;
    }

    public void PdBuilding()
    {
        if (this.SelectCheck == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
            {
                BuildingPosi.x = Mathf.Round(hitinfo.point.x);
                //BuildingPosi.y = Mathf.Round(hitinfo.point.y);
                BuildingPosi.z = Mathf.Round(hitinfo.point.z);               
                if (BuildPreview == null)
                {
                    if(this.BuildingNumber == 1)
                    {
                        //BuildPreview = Instantiate(GameManager.GetInstance().G_Farm, BuildingPosi, Quaternion.identity);
                        BuildPreview = Instantiate(test, BuildingPosi, Quaternion.identity);
                        this.BuildType = 1;
                        Debug.Log("1");
                    }
                    if (this.BuildingNumber == 2)
                    {
                        //BuildPreview = Instantiate(GameManager.GetInstance().G_Barrack, BuildingPosi, Quaternion.identity);
                        BuildPreview = Instantiate(test, BuildingPosi, Quaternion.identity);
                        this.BuildType = 2;                       
                    }
                    if (this.BuildingNumber == 3)
                    {
                        //BuildPreview = Instantiate(GameManager.GetInstance().G_Lumber, BuildingPosi, Quaternion.identity);
                        BuildPreview = Instantiate(test, BuildingPosi, Quaternion.identity);
                        this.BuildType = 3;
                        Debug.Log("1");
                    }
                    if (this.BuildingNumber == 4)
                    {
                        //BuildPreview = Instantiate(GameManager.GetInstance().G_House, BuildingPosi, Quaternion.identity);
                        BuildPreview = Instantiate(test, BuildingPosi, Quaternion.identity);
                        this.BuildType = 4;                       
                    }
                    if (this.BuildingNumber == 5)
                    {
                        //BuildPreview = Instantiate(GameManager.GetInstance().G_WallHo, BuildingPosi, Quaternion.identity);
                        BuildPreview = Instantiate(test, BuildingPosi, Quaternion.identity);
                        this.BuildType = 5;                        
                    }
                    if (this.BuildingNumber == 6)
                    {
                        //BuildPreview = Instantiate(GameManager.GetInstance().G_WallVer, BuildingPosi, Quaternion.identity);
                        BuildPreview = Instantiate(test, BuildingPosi, Quaternion.identity);
                        this.BuildType = 6;
                    }
                }
                if (BuildPreview != null)
                {
                    this.BuildingNumber = 0;
                    BuildPreview.transform.position = BuildingPosi;

                    if (Input.GetMouseButtonDown(0))
                    {
                        this.TargetPosition = BuildingPosi;
                        Debug.Log(this.TargetPosition);
                        Debug.Log(this.transform.position);
                        
                    }
                    if (this.transform.position.x == this.TargetPosition.x && this.transform.position.z == this.TargetPosition.z)
                    {
                        this.DestinationCheck = 1;
                        if (this.BuildType == 1)
                        {
                            this.BuildClock = 5;
                        }
                        if (this.BuildType == 2)
                        {
                            this.BuildClock = 5;
                        }
                        if (this.BuildType == 3)
                        {
                            this.BuildClock = 5;
                        }
                        if (this.BuildType == 4)
                        {
                            this.BuildClock = 5;
                        }
                        if (this.BuildType == 5)
                        {
                            this.BuildClock = 1;
                        }
                        if (this.BuildType == 6)
                        {
                            this.BuildClock = 1;
                        }
                        Destroy(BuildPreview);
                        BuildPreview = null;
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        this.BuildType = 0;
                        Destroy(BuildPreview);
                        BuildPreview = null;
                    }
                }
            }
        }
    }

    public void ClockCount()
    {
        if (this.BuildClock > 0)
        {
            this.BuildClock--;
        }
    }

    public void Build()
    {
        if (this.DestinationCheck == 1 && this.BuildClock == 0 && this.BuildType == 1)
        {
            GameObject pdFarm = Instantiate(GameManager.GetInstance().G_Farm, this.transform.position, Quaternion.identity);
            GameManager.GetInstance().BuildingCount++;
            GameManager.GetInstance().m_cBuildings.Add(pdFarm.GetComponent<BuildingBox>());
            GameManager.GetInstance().m_cBuildings[GameManager.GetInstance().BuildingCount].m_Building = GameManager.GetInstance().m_cBuildingManager.GetBuilding(BuildingManager.eBuilding.Farm);
            //Debug.Log(BuildingCount);
            GameManager.GetInstance().FarmCount++;
            GameManager.GetInstance().Wood -= 100;
            this.BuildType = 0;
            this.DestinationCheck = 0;
            this.BuildClock = -1;
        }
        if (this.DestinationCheck == 1 && this.BuildClock == 0 && this.BuildType == 2)
        {
            GameObject pdBarrack = Instantiate(GameManager.GetInstance().G_Barrack, this.transform.position, Quaternion.identity);
            GameManager.GetInstance().BuildingCount++;
            GameManager.GetInstance().m_cBuildings.Add(pdBarrack.GetComponent<BuildingBox>());
            GameManager.GetInstance().m_cBuildings[GameManager.GetInstance().BuildingCount].m_Building = GameManager.GetInstance().m_cBuildingManager.GetBuilding(BuildingManager.eBuilding.Barrack);
            GameManager.GetInstance().Wood -= 200;
            this.BuildType = 0;
            this.DestinationCheck = 0;
            this.BuildClock = -1;
        }
        if (this.DestinationCheck == 1 && this.BuildClock == 0 && this.BuildType == 3)
        {
            GameObject pdLumber = Instantiate(GameManager.GetInstance().G_Lumber, this.transform.position, Quaternion.identity);
            GameManager.GetInstance().BuildingCount++;
            GameManager.GetInstance().m_cBuildings.Add(pdLumber.GetComponent<BuildingBox>());
            GameManager.GetInstance().m_cBuildings[GameManager.GetInstance().BuildingCount].m_Building = GameManager.GetInstance().m_cBuildingManager.GetBuilding(BuildingManager.eBuilding.Lumbermill);
            //Debug.Log(BuildingCount);
            GameManager.GetInstance().LumCount++;
            GameManager.GetInstance().Wood -= 100;
            this.BuildType = 0;
            this.DestinationCheck = 0;
            this.BuildClock = -1;
        }
        if (this.DestinationCheck == 1 && this.BuildClock == 0 && this.BuildType == 4)
        {
            GameObject pdHouse = Instantiate(GameManager.GetInstance().G_House, this.transform.position, Quaternion.identity);
            GameManager.GetInstance().BuildingCount++;
            GameManager.GetInstance().m_cBuildings.Add(pdHouse.GetComponent<BuildingBox>());
            GameManager.GetInstance().m_cBuildings[GameManager.GetInstance().BuildingCount].m_Building = GameManager.GetInstance().m_cBuildingManager.GetBuilding(BuildingManager.eBuilding.House);
            //Debug.Log(BuildingCount);
            GameManager.GetInstance().HouseCount++;
            GameManager.GetInstance().Wood -= 50;
            this.BuildType = 0;
            this.DestinationCheck = 0;
            this.BuildClock = -1;
        }
        if (this.DestinationCheck == 1 && this.BuildClock == 0 && this.BuildType == 5)
        {
            GameObject pdWallHo = Instantiate(GameManager.GetInstance().G_WallHo, this.transform.position, Quaternion.identity);
            GameManager.GetInstance().BuildingCount++;
            GameManager.GetInstance().m_cBuildings.Add(pdWallHo.GetComponent<BuildingBox>());
            GameManager.GetInstance().m_cBuildings[GameManager.GetInstance().BuildingCount].m_Building = GameManager.GetInstance().m_cBuildingManager.GetBuilding(BuildingManager.eBuilding.Wall);
            //Debug.Log(BuildingCount);
            GameManager.GetInstance().Wood -= 20;
            this.BuildType = 0;
            this.DestinationCheck = 0;
            this.BuildClock = -1;
        }
        if (this.DestinationCheck == 1 && this.BuildClock == 0 && this.BuildType == 6)
        {
            GameObject pdWallVer = Instantiate(GameManager.GetInstance().G_WallVer, this.transform.position, Quaternion.identity);
            GameManager.GetInstance().BuildingCount++;
            GameManager.GetInstance().m_cBuildings.Add(pdWallVer.GetComponent<BuildingBox>());
            GameManager.GetInstance().m_cBuildings[GameManager.GetInstance().BuildingCount].m_Building = GameManager.GetInstance().m_cBuildingManager.GetBuilding(BuildingManager.eBuilding.Wall);
            //Debug.Log(BuildingCount);
            GameManager.GetInstance().Wood -= 20;
            this.BuildType = 0;
            this.DestinationCheck = 0;
            this.BuildClock = -1;
        }
    }

    public void TimeCounter()
    {
        if (GameManager.GetInstance().UnitSelectCount == 1)
        {
            if (this.BuildClock > 0)
            {
                GameManager.GetInstance().m_cGUIManager.m_textInfo.text = "건물 생성중..." + this.BuildClock;
            }
            if (this.BuildClock < 0)
            {
                GameManager.GetInstance().m_cGUIManager.m_textInfo.text = "";
            }
        }
        else
        {
            GameManager.GetInstance().m_cGUIManager.m_textInfo.text = "";
        }
    }

    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    Vector3 BuildingPosi; //미리보기(?) 위치
    GameObject BuildPreview; //건물 미리보기
    public GameObject Commend_UI; //일반 UI
    public GameObject Build_UI; //건물 생산 UI
    int buttoncheck = 0; //건물버튼활성화 여부
    int Workerbuild = 0; //건물건설중일때 일꾼 못움직이게 만들기위한 변수
    public GameObject BuildInfo; //건물 건설정보
    public Text m_buildinfotext; //건물 건설정보 텍스트

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
        if (this.SelectCheck == 1 && Input.GetMouseButtonDown(1) && Workerbuild == 0)
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
            if (EventSystem.current.IsPointerOverGameObject() == false) //UI위가 아닐경우에
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
                else
                {
                    SelectCheck = 0;
                }
            }
        }
    }

    public void ButtonActive()
    {
        if (SelectCheck == 1 && buttoncheck == 0)
        {
            Commend_UI.SetActive(true);
        }
        else
        {
            Commend_UI.SetActive(false);
        }

        if (SelectCheck == 1)
        {
            BuildInfo.SetActive(true);
        }
        else
        {
            BuildInfo.SetActive(false);
            Build_UI.SetActive(false);
            buttoncheck = 0;
        }
    }

    public void BuildButton()
    {
        if (SelectCheck == 1)
        {
            Build_UI.SetActive(true);
            buttoncheck = 1;
            Commend_UI.SetActive(false);
        }
    }

    public void BackButton()
    {
        if (SelectCheck == 1)
        {
            Commend_UI.SetActive(true);
            buttoncheck = 0;
            Build_UI.SetActive(false);
        }
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
        if (BuildPreview == null && GameManager.GetInstance().Wood >= 100)
            this.BuildingNumber = 1;
    }

    public void BarrackButton()
    {
        if (BuildPreview == null && GameManager.GetInstance().Wood >= 200)
            this.BuildingNumber = 2;
    }

    public void LumberButton()
    {
        if (BuildPreview == null && GameManager.GetInstance().Wood >= 100)
            this.BuildingNumber = 3;
    }

    public void HouseButton()
    {
        if (BuildPreview == null && GameManager.GetInstance().Wood >= 50)
            this.BuildingNumber = 4;
    }

    public void WallHoButton()
    {
        if (BuildPreview == null && GameManager.GetInstance().Wood >= 20)
            this.BuildingNumber = 5;
    }

    public void WallVerButton()
    {
        if (BuildPreview == null && GameManager.GetInstance().Wood >= 20)
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
                    if (this.transform.position.x == BuildingPosi.x && this.transform.position.z == BuildingPosi.z)
                    {
                        this.DestinationCheck = 1;
                        this.Workerbuild = 1;
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
            this.Workerbuild = 0;
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
            this.Workerbuild = 0;
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
            this.Workerbuild = 0;
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
            this.Workerbuild = 0;
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
            this.Workerbuild = 0;
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
            this.Workerbuild = 0;
        }
    }

    public void TimeCounter()
    {
        if (this.BuildClock > 0)
        {
            m_buildinfotext.text = "건물 생성중..." + this.BuildClock;
        }
        if (this.BuildClock < 0)
        {
            m_buildinfotext.text = "";
        }
    }

    
}

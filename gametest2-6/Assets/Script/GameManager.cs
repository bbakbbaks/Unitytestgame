using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UnitManager m_cUnitManager;
    public List<UnitBox> m_cUnits = new List<UnitBox>();
    public List<UnitBox> m_cEnemies = new List<UnitBox>();
    public BuildingManager m_cBuildingManager;
    public List<BuildingBox> m_cBuildings = new List<BuildingBox>();
    public Barrack m_cBarrack;
    public Center m_cCenter;
    public GameObject G_Solider;
    public GameObject G_Worker;
    public GameObject G_Zombie;
    public GameObject G_Lumber;
    public GameObject G_House;
    public GameObject G_Farm;
    public GameObject G_WallHo;
    public GameObject G_WallVer;
    public Transform Z_Point;
    public int unitcount = -1; //리스트에 추가되는 유닛을 위한 카운트
    public int BuildingCount = -1; //리스트에 추가되는 빌딩을 위한 카운트
    int Zcount = -1; //좀비리스트에 추가되는 좀비를 위한 카운트
    public int ZombieAmount = 0;
    int StartZcount = 0; //한 웨이브에 나오는 좀비의 수에 관련된 변수
    int WaveCount = 0; //웨이브 횟수
    public GUIManager m_cGUIManager;
    public GUIManager.eScene m_eScene;
    public int Food = 20; //식량
    public int Wood = 200; //목재
    public int MaxPopulation = 1; //최대인구수
    public int NowPopulation = 0; //현재인구수
    public int LumCount = 0; //재재소 갯수
    public int FarmCount = 0; //농장 갯수
    public int HouseCount = 0; //집 갯수

    static GameManager m_cInstance;

    static public GameManager GetInstance()
    {
        return m_cInstance;
    }

    void Start()
    {
        m_cInstance = this;
        m_cGUIManager.SetScene(m_eScene);
        //InvokeRepeating("PdEnemy", 1, 1);
        //PdEnemy();
    }

    void Update()
    {
        PdUnit();
        if (WaveCount == 5)
            CancelInvoke("PdEnemy");
        GameCheck();
        //PdBuilding();
        //Decrease();
        m_cGUIManager.m_textResource.text = string.Format("Food: {0}  Wood: {1}  Population: {2} / {3}", Food, Wood, NowPopulation, MaxPopulation);
    }

    public void CreateUnit()
    {
        m_cCenter.m_Center = new Building("지휘본부", 500, 0, 0, "imgcenter");
        m_cUnitManager.m_listunits.Add(new Unit("일꾼", 5, 1, 1, 1, "imgworker"));
        m_cUnitManager.m_listunits.Add(new Unit("군인", 15, 4, 4, 1, "imgsolider"));
        m_cUnitManager.m_listunits.Add(new Unit("좀비", 20, 3, 1, 1, "imgzombie"));
        //m_cBuildingManager.m_listBuildings.Add(new Building("지휘본부", 500, 0, 0, "imgcenter"));
        m_cBuildingManager.m_listBuildings.Add(new Building("제재소", 70, 100, 20, "imglumber"));
        m_cBuildingManager.m_listBuildings.Add(new Building("집", 30, 50, 4, "imghouse"));
        m_cBuildingManager.m_listBuildings.Add(new Building("농장", 70, 100, 5, "imgfarm"));
        m_cBuildingManager.m_listBuildings.Add(new Building("병영", 50, 200, 0, "imgbarrack"));
        m_cBuildingManager.m_listBuildings.Add(new Building("벽", 100, 20, 0, "imgWall"));
    }

    public void PdEnemy()//임시 웨이브 StartZcount + 숫자 로 한웨이브당 나오는 좀비수를 정한다
    {
        for (Zcount = StartZcount; Zcount < StartZcount + 3; Zcount++)
        {
            GameObject pdZombie = Instantiate(G_Zombie, Z_Point.position, Z_Point.rotation);
            m_cEnemies.Add(pdZombie.GetComponent<UnitBox>());
            m_cEnemies[Zcount].m_sUnit = m_cUnitManager.GetUnit(UnitManager.eUnit.Zombie);
            ZombieAmount++;
        }
        StartZcount += 3;
        WaveCount++;
    }

    //public void PdBuilding()
    //{
    //    //if (Input.GetKeyDown(KeyCode.B))
    //    //{
    //    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    //    RaycastHit hitinfo = new RaycastHit();
    //    //    if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
    //    //    {
    //    //        Vector3 posi = hitinfo.point;
    //    //        Instantiate(m_cBarrack, posi, Quaternion.identity);
    //    //        //GameObject pdBarrack = Instantiate(m_cBarrack, posi, Quaternion.identity);
    //    //    }
    //    //}
    //    if (Input.GetKeyDown(KeyCode.F) && Wood >= 100)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hitinfo = new RaycastHit();
    //        if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
    //        {
    //            Vector3 posi = hitinfo.point;
    //            GameObject pdFarm = Instantiate(G_Farm, posi, Quaternion.identity);
    //            BuildingCount++;
    //            m_cBuildings.Add(pdFarm.GetComponent<BuildingBox>());
    //            m_cBuildings[BuildingCount].m_Building = m_cBuildingManager.GetBuilding(BuildingManager.eBuilding.Farm);
    //            //Debug.Log(BuildingCount);
    //            FarmCount++;
    //            Wood -= 100;
    //        }
    //    }
    //    if (Input.GetKeyDown(KeyCode.L) && Wood >= 100)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hitinfo = new RaycastHit();
    //        if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
    //        {
    //            Vector3 posi = hitinfo.point;
    //            GameObject pdLumber = Instantiate(G_Lumber, posi, Quaternion.identity);
    //            BuildingCount++;
    //            m_cBuildings.Add(pdLumber.GetComponent<BuildingBox>());
    //            m_cBuildings[BuildingCount].m_Building = m_cBuildingManager.GetBuilding(BuildingManager.eBuilding.Lumbermill);
    //            //Debug.Log(BuildingCount);
    //            LumCount++;
    //            Wood -= 100;
    //        }
    //    }
    //    if (Input.GetKeyDown(KeyCode.H) && Wood >= 50)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hitinfo = new RaycastHit();
    //        if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
    //        {
    //            Vector3 posi = hitinfo.point;
    //            GameObject pdHouse = Instantiate(G_House, posi, Quaternion.identity);
    //            BuildingCount++;
    //            m_cBuildings.Add(pdHouse.GetComponent<BuildingBox>());
    //            m_cBuildings[BuildingCount].m_Building = m_cBuildingManager.GetBuilding(BuildingManager.eBuilding.House);
    //            //Debug.Log(BuildingCount);
    //            HouseCount++;
    //            Wood -= 50;
    //        }
    //    }
    //    if (Input.GetKeyDown(KeyCode.W) && Wood >= 20)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hitinfo = new RaycastHit();
    //        if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
    //        {
    //            Vector3 posi = hitinfo.point;
    //            GameObject pdHouse = Instantiate(G_WallHo, posi, Quaternion.identity);
    //            BuildingCount++;
    //            m_cBuildings.Add(pdHouse.GetComponent<BuildingBox>());
    //            m_cBuildings[BuildingCount].m_Building = m_cBuildingManager.GetBuilding(BuildingManager.eBuilding.Wall);
    //            //Debug.Log(BuildingCount);
    //            Wood -= 20;
    //        }
    //    }
    //    if (Input.GetKeyDown(KeyCode.Q) && Wood >= 20)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hitinfo = new RaycastHit();
    //        if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
    //        {
    //            Vector3 posi = hitinfo.point;
    //            GameObject pdHouse = Instantiate(G_WallVer, posi, Quaternion.identity);
    //            BuildingCount++;
    //            m_cBuildings.Add(pdHouse.GetComponent<BuildingBox>());
    //            m_cBuildings[BuildingCount].m_Building = m_cBuildingManager.GetBuilding(BuildingManager.eBuilding.Wall);
    //            //Debug.Log(BuildingCount);
    //            Wood -= 20;
    //        }
    //    }
    //}

    public void PdUnit()
    {
        if (Input.GetKeyDown(KeyCode.M) && Food >= 25 && MaxPopulation > NowPopulation)//배럭에서 솔저 생산
        {
            GameObject pdSolider = Instantiate(G_Solider, m_cBarrack.Regenposition.position, Quaternion.identity);
            pdSolider.name = "solider";
            unitcount++;
            m_cUnits.Add(pdSolider.GetComponent<UnitBox>());
            m_cUnits[unitcount].m_sUnit = m_cUnitManager.GetUnit(UnitManager.eUnit.Solider);
            //Debug.Log(m_cUnits[unitcount].m_sUnit.Name);
            Food -= 25;
            NowPopulation++;
        }
        if (Input.GetKeyDown(KeyCode.C) && Food >= 10 && MaxPopulation > NowPopulation)
        {
            GameObject pdWorker = Instantiate(G_Worker, m_cCenter.Regenposition.position, Quaternion.identity);
            unitcount++;
            pdWorker.name = "worker";
            m_cUnits.Add(pdWorker.GetComponent<UnitBox>());
            m_cUnits[unitcount].m_sUnit = m_cUnitManager.GetUnit(UnitManager.eUnit.Worker);
            //Debug.Log(m_cUnits[unitcount].m_sUnit.Name);
            Food -= 10;
            NowPopulation++;
        }
    }

    public void IncreaseRecource()
    {
        Wood += LumCount * 20;
        Food += FarmCount * 5;
        MaxPopulation = 1 + HouseCount * 4;
        //Debug.Log("Wood: " + Wood);
        //Debug.Log("Food: " + Food);
        //Debug.Log("NowPopulation: " + NowPopulation + " / " + "MaxPopulation: " + MaxPopulation);
    }

    public void GameCheck()
    {
        if (m_cCenter == null) //패배
        {
            //Debug.Log("GameOver");
            m_cGUIManager.SetScene(GUIManager.eScene.GAMEOVER);
        }
        if (WaveCount == 5 && ZombieAmount == 0) //승리
        {
            m_cGUIManager.SetScene(GUIManager.eScene.THEEND); 
        }

    }

    public void EventStart()
    {
        m_cGUIManager.SetScene(GUIManager.eScene.PLAY);
        //InvokeRepeating("PdEnemy", 30, 10);
        CreateUnit();
        InvokeRepeating("IncreaseRecource", 0, 1);
        //m_cCenter.DestroyCenter();
    }

    public void EventRetry()
    {
        m_cGUIManager.SetScene(GUIManager.eScene.TITLE);
    }

    public void EventExit()
    {
        Application.Quit();
    }

    public void EventTheEnd()
    {
        m_cGUIManager.SetScene(GUIManager.eScene.THEEND);
    }

    public void EventGameOver()
    {
        m_cGUIManager.SetScene(GUIManager.eScene.GAMEOVER);
    }
}

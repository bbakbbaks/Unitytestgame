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
    public Transform Z_Point;
    public int unitcount = 0;
    int Zcount = -1;
    int StartZcount = 0;
    int WaveCount = 0;
    float m_fDist = 0;
    public GUIManager m_cGUIManager;
    public GUIManager.eScene m_eScene;
    int Food = 0;
    int Wood = 0;
    int Population = 0;

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
    }

    public void PdEnemy()//임시 웨이브. 정식 웨이브 함수를 따로 만들어야한다.
    {
        for (Zcount = StartZcount; Zcount < StartZcount + 3; Zcount++)
        {
            GameObject pdZombie = Instantiate(G_Zombie, Z_Point.position, Z_Point.rotation);
            m_cEnemies.Add(pdZombie.GetComponent<UnitBox>());
            m_cEnemies[Zcount].m_sUnit = m_cUnitManager.GetUnit(UnitManager.eUnit.Zombie);
        }
        StartZcount += 3;
        WaveCount++;
    }

    public void PdBuilding()
    {

    }

    public void PdUnit()
    {
        if (Input.GetKeyDown(KeyCode.M))//배럭에서 솔저 생산
        {
            GameObject pdSolider = Instantiate(G_Solider, m_cBarrack.Regenposition.position, m_cBarrack.Regenposition.rotation);
            pdSolider.name = "solider";
            unitcount++;
            m_cUnits.Add(pdSolider.GetComponent<UnitBox>());
            m_cUnits[unitcount].m_sUnit = m_cUnitManager.GetUnit(UnitManager.eUnit.Solider);
            Debug.Log(m_cUnits[unitcount].m_sUnit.Name);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject pdWorker = Instantiate(G_Worker, m_cCenter.Regenposition.position, m_cCenter.Regenposition.rotation);
            unitcount++;
            pdWorker.name = "worker";
            m_cUnits.Add(pdWorker.GetComponent<UnitBox>());
            m_cUnits[unitcount].m_sUnit = m_cUnitManager.GetUnit(UnitManager.eUnit.Worker);
            Debug.Log(m_cUnits[unitcount].m_sUnit.Name);
        }
    }

    public void GameCheck()
    {
        if (m_cCenter == null)
        {
            //Debug.Log("GameOver");
            m_cGUIManager.SetScene(GUIManager.eScene.GAMEOVER);
        }
    }

    public void EventStart()
    {
        m_cGUIManager.SetScene(GUIManager.eScene.PLAY);
        InvokeRepeating("PdEnemy", 1, 1);
        CreateUnit();
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

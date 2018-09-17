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
    public Center m_cCenter;
    public GameObject G_Solider;
    public GameObject G_Worker;
    public GameObject G_Zombie;
    public GameObject G_Lumber;
    public GameObject G_House;
    public GameObject G_Barrack;
    public GameObject G_Farm;
    public GameObject G_WallHo;
    public GameObject G_WallVer;
    public Barrack m_cBarrack;
    public Transform Z_Point;
    public int unitcount = -1; //리스트에 추가되는 유닛을 위한 카운트
    public int BuildingCount = -1; //리스트에 추가되는 빌딩을 위한 카운트
    int Zcount = -1; //좀비리스트에 추가되는 좀비를 위한 카운트
    public int ZombieAmount = 0; //좀비 수
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
    public int UnitSelectCount = 0; //선택된 유닛의 갯수
    public bool BuildingSelectCheck = false; //건물선택체크
    public bool UnitSelectCheck = false; //유닛선택체크 //건물과 유닛의 중복 체크 방지
    public float Wavetimer = 60;
    int wavetimerstart = 0;

    static GameManager m_cInstance;

    static public GameManager GetInstance()
    {
        return m_cInstance;
    }

    void Start()
    {
        m_cInstance = this;
        m_cGUIManager.SetScene(m_eScene);
        //InvokeRepeating("PdEnemy", 60, 60);
        //PdEnemy();
    }

    void Update()
    {
        //PdUnit();
        if (WaveCount == 5)
            CancelInvoke("PdEnemy");
        GameCheck();
        ResourceText();
        GameInfo2();
        if (wavetimerstart == 1)
        {
            Wavetimer -= Time.deltaTime;
            if (Wavetimer <= 0)
            {
                Wavetimer = 60;
            }
        }
    }

    public void CreateUnit()
    {
        m_cCenter.m_Center = new Building("지휘본부", 500, 0, 0, "imgcenter");
        m_cUnitManager.m_listunits.Add(new Unit("일꾼", 5, 1, 1, 1, "imgworker"));
        m_cUnitManager.m_listunits.Add(new Unit("군인", 15, 4, 4, 1, "imgsolider"));
        m_cUnitManager.m_listunits.Add(new Unit("좀비", 20, 0, 1, 1, "imgzombie"));
        //m_cBuildingManager.m_listBuildings.Add(new Building("지휘본부", 500, 0, 0, "imgcenter"));
        m_cBuildingManager.m_listBuildings.Add(new Building("제재소", 70, 100, 20, "imglumber"));
        m_cBuildingManager.m_listBuildings.Add(new Building("집", 30, 50, 4, "imghouse"));
        m_cBuildingManager.m_listBuildings.Add(new Building("농장", 70, 100, 5, "imgfarm"));
        m_cBuildingManager.m_listBuildings.Add(new Building("병영", 50, 200, 0, "imgbarrack"));
        m_cBuildingManager.m_listBuildings.Add(new Building("벽", 100, 20, 0, "imgWall"));
    }

    public void PdEnemy()//임시 웨이브 StartZcount + 숫자 로 한웨이브당 나오는 좀비수를 정한다
    {
        for (Zcount = StartZcount; Zcount < StartZcount + 10; Zcount++)
        {
            GameObject pdZombie = Instantiate(G_Zombie, Z_Point.position, Z_Point.rotation);
            m_cEnemies.Add(pdZombie.GetComponent<UnitBox>());
            m_cEnemies[Zcount].m_sUnit = m_cUnitManager.GetUnit(UnitManager.eUnit.Zombie);
            ZombieAmount++;
        }
        StartZcount += 10;
        WaveCount++;
    }

    public void IncreaseRecource()
    {
        Wood += LumCount * 20;
        Food += FarmCount * 5;
        MaxPopulation = 1 + HouseCount * 4;
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

    public void ResourceText()
    {
        m_cGUIManager.m_textFood.text = string.Format("{0}", Food);
        m_cGUIManager.m_textWood.text = string.Format("{0}", Wood);
        m_cGUIManager.m_textPopulation.text = string.Format("{0} / {1}", NowPopulation, MaxPopulation);
        if (WaveCount > 0)
        {
            m_cGUIManager.m_textZombieInfo.text = WaveCount + "번째 웨이브" + "\n남은 좀비 수: " + ZombieAmount;
        }
        m_cGUIManager.m_textWaveTimer.text = "다음 웨이브: " + (int)Wavetimer + "초";
    }

    public void EventStart()
    {
        m_cGUIManager.SetScene(GUIManager.eScene.PLAY);
        //InvokeRepeating("PdEnemy", 0, 600);
        CreateUnit();
        InvokeRepeating("IncreaseRecource", 0, 1);
        //m_cCenter.DestroyCenter();
        wavetimerstart = 1;
        
    }

    public void GameInfo()
    {
        m_cGUIManager.SetScene(GUIManager.eScene.GAMEINFO);        
    }
    public void GameInfo2()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            m_cGUIManager.SetScene(GUIManager.eScene.GAMEINFO);
        }
        else if (Input.GetKeyUp(KeyCode.F1))
        {
            m_cGUIManager.SetScene(GUIManager.eScene.PLAY);
        }
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

    public void Tutorial()
    {
        Time.timeScale = 0;
    }
}

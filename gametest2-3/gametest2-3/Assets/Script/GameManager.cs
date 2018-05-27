using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public UnitManager m_cUnitManager;
    //Unitstat m_cUnitstat1;
    //Unitstat m_cUnitstat2;
    //UnitBox m_cUnitbox;
    public List<UnitBox> m_cUnits=new List<UnitBox>();
    public List<UnitBox> m_cEnemies = new List<UnitBox>();
    public BuildingManager m_cBuildingManager;
    public List<BuildingBox> m_cBuildingBoxes;
    public Barrack m_cBarrack;
    public Center m_cCenter;
    public GameObject G_Solider;
    public GameObject G_Worker;
    public GameObject G_Zombie;
    public Transform Z_Point;
    //public Player m_cPlayer;
    int unitcount = -1;
    int Zcount = -1;

    static GameManager m_cInstance;

    static public GameManager GetInstance()
    {
        return m_cInstance;
    }

	// Use this for initialization
	void Start () {
        m_cInstance = this;
        CreateUnit();
        PdEnemy();
        //Debug.Log(m_cUnitManager.GetUnit(UnitManager.eUnit.Worker).Name);
        //Debug.Log(m_cUnitManager.GetUnit(UnitManager.eUnit.Solider).Name);
        //Debug.Log(m_cUnitManager.GetUnit(UnitManager.eUnit.Zombie).Name);
    }
	
	// Update is called once per frame
	void Update () {
        PdUnit();
    }

    public void CreateUnit()
    {
        m_cUnitManager.m_listunits.Add(new Unit("일꾼", 5, 1, 1, 1, "imgworker"));
        m_cUnitManager.m_listunits.Add(new Unit("군인", 15, 4, 4, 1, "imgsolider"));
        m_cUnitManager.m_listunits.Add(new Unit("좀비", 20, 3, 1, 1, "imgzombie"));
    }

    public void PdEnemy()
    {
        for (Zcount = 0; Zcount < 10; Zcount++)
        {
            GameObject pdZombie = Instantiate(G_Zombie, Z_Point.position, Z_Point.rotation);
            m_cEnemies.Add(pdZombie.GetComponent<UnitBox>());
            m_cEnemies[Zcount].m_sUnit = m_cUnitManager.GetUnit(UnitManager.eUnit.Zombie);
        }
    }

    public void PdUnit()
    {
       
        if (Input.GetKeyDown(KeyCode.M))//배럭에서 솔저 생산
        {
            GameObject pdSolider = Instantiate(G_Solider, m_cBarrack.Regenposition.position, m_cBarrack.Regenposition.rotation);
            //GameObject pdSolider = Instantiate(G_O, Regenposition.position, Regenposition.rotation);//게임 오브젝트 만드는 방법
            pdSolider.name = "solider";
            //m_cUnitBoxes.Add(m_cUnitManager.GetUnit(UnitManager.eUnit.Solider));
            //m_cUnitManager.m_listunits.Add(new Unit("군인", 15, 4, 4, 1, "imgsolider"));//M을 누를떄마다 유닛매니저의 리스트에
            ////군인이 추가된다
            //Debug.Log(m_cUnitManager.m_listunits[].Name);
            //pdSolider.GetComponent<Unitstat>()
            //UnitBox unitbox = pdSolider.GetComponent<UnitBox>();
            unitcount++;
            m_cUnits.Add(pdSolider.GetComponent<UnitBox>());
            m_cUnits[unitcount].m_sUnit = m_cUnitManager.GetUnit(UnitManager.eUnit.Solider);
            Debug.Log(m_cUnits[unitcount].m_sUnit.Name + m_cUnits[unitcount].m_sUnit.Hp);

            //m_cUnits.Add(pdSolider));
            //m_cUnitbox.m_cUnit = m_cUnitManager.GetComponent<UnitManager>().GetUnit(UnitManager.eUnit.Solider);
            //Debug.Log(m_cUnitbox.m_cUnit.Hp);

            //Debug.Log(m_cUnits[unitcount].Name);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject pdWorker=Instantiate(G_Worker, m_cCenter.Regenposition.position, m_cCenter.Regenposition.rotation);
            unitcount++;
            pdWorker.name = "worker";
            //m_cUnits.Add(m_cUnitManager.GetUnit(UnitManager.eUnit.Worker));
            //Debug.Log(m_cUnits[unitcount].Name);
            m_cUnits.Add(pdWorker.GetComponent<UnitBox>());
            m_cUnits[unitcount].m_sUnit = m_cUnitManager.GetUnit(UnitManager.eUnit.Worker);
            Debug.Log(m_cUnits[unitcount].m_sUnit.Name + m_cUnits[unitcount].m_sUnit.Hp);
        }
    }





}

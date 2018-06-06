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
    int StartZcount = 0;
    //UnitBox Target = null;
    //UnitBox Targetenemy = null;
    //UnitBox Targetcmd = null;
    //UnitBox Targetenemycmd = null;
    //Vector3 Positon;
    //List<UnitBox> m_cTargetlist = new List<UnitBox>();
    //List<UnitBox> m_cTargetcmdlist = new List<UnitBox>();
    float m_fDist = 0;

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
        //UnitSelect();
        //UnitCommend();
        //UnitMoves();
        //UnitDetect();
        //ZombieAction();
        //StartCoroutine("UnitAttack");
        //StartCoroutine("UnitDetect");
        //InvokeRepeating("UnitDetect", 1, 1);
    }

    public void CreateUnit()
    {
        m_cUnitManager.m_listunits.Add(new Unit("일꾼", 5, 1, 1, 1, "imgworker"));
        m_cUnitManager.m_listunits.Add(new Unit("군인", 15, 4, 4, 1, "imgsolider"));
        m_cUnitManager.m_listunits.Add(new Unit("좀비", 20, 3, 1, 1, "imgzombie"));
    }

    public void PdEnemy()//임시 웨이브. 정식 웨이브 함수를 따로 만들어야한다.
    {
        for (Zcount = StartZcount; Zcount < StartZcount + 2; Zcount++)
        {
            GameObject pdZombie = Instantiate(G_Zombie, Z_Point.position, Z_Point.rotation);
            m_cEnemies.Add(pdZombie.GetComponent<UnitBox>());
            m_cEnemies[Zcount].m_sUnit = m_cUnitManager.GetUnit(UnitManager.eUnit.Zombie);
        }
        StartZcount += 2;
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
            Debug.Log(m_cUnits[unitcount].m_sUnit.Name/* + m_cUnits[unitcount].m_sUnit.Hp*/);

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
            Debug.Log(m_cUnits[unitcount].m_sUnit.Name/* + m_cUnits[unitcount].m_sUnit.Hp*/);
        }
    }

    //public void UnitSelect()
    //{
    //    if (Input.GetMouseButtonDown(0)/*&&Input.GetKey(KeyCode.LeftShift)*/)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hitinfo;

    //        if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("playerunit")))
    //        {
    //            Target = hitinfo.collider.gameObject.GetComponent<UnitBox>();
    //            m_cTargetlist.Add(Target);
    //            Targetcmd = hitinfo.collider.gameObject.GetComponent<UnitBox>();
    //            m_cTargetcmdlist.Add(Targetcmd);
    //        }
    //        else
    //        {
    //            m_cTargetlist.Clear();
    //            Target = null;
    //            m_cTargetcmdlist.Clear();
    //        }
    //    }
    //}

    //public void UnitMoves()
    //{
    //    if (Target != null)
    //    {
    //        if (Input.GetMouseButtonDown(1))
    //        {
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            RaycastHit hitinfo;

    //            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("enemyunit")))//타겟 선택 공격
    //            {
    //                Targetenemy = hitinfo.collider.gameObject.GetComponent<UnitBox>();
    //                for (int i = 0; i < m_cTargetlist.Count; i++)
    //                {
    //                    m_fDist = Vector3.Distance(m_cTargetlist[i].transform.position, Targetenemy.transform.position);//Target.transform.position <= 객체의 현재위치
    //                    m_cTargetlist[i].DirectAttack(Targetenemy);
    //                    if (m_fDist > m_cTargetlist[i].m_sUnit.Range)
    //                    {
    //                        m_cTargetlist[i].TargetPosition = Targetenemy.transform.position;
    //                    }

    //                    if (m_fDist <= m_cTargetlist[i].m_sUnit.Range)
    //                    {
    //                        m_cTargetlist[i].TargetPosition = m_cTargetlist[i].transform.position;
    //                        Debug.Log("Detect Enemy");
    //                        Target.m_sUnit.Attack(Targetenemy.m_sUnit);
    //                        Targetenemy.m_sUnit.Hp = Targetenemy.m_sUnit.Hp - m_cTargetlist[i].m_sUnit.Damage;//임시 공격
    //                        Debug.Log(Targetenemy.m_sUnit.Hp);
    //                        Targetenemy.ChangeHp(Targetenemy.m_sUnit.Hp, Targetenemy.m_sUnit.MaxHp);//Hp바에 체력 표시
    //                    }
    //                    if (Targetenemy.m_sUnit.Hp <= 0)
    //                    {
    //                        Destroy(Targetenemy.gameObject);//게임오브젝트를 파괴시켜서 화면에서 없앤다
    //                    }
    //                    m_cTargetlist[i].Attack();
    //                }
    //            }
    //            문제점1 적을 찍어도 적 밑의 지형도 같이 찍히면서 타켓의 포지션이 적의 포지션 근처로
    //            바뀌면서 적과의 거리가 무조건 지근거리로 나오게된다.
    //            이것을 이너미유닛과 디폴트의 위치를 바꿔주는것만으로도 해결

    //            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Default")))
    //            {
    //                Positon = hitinfo.point;
    //                for (int i = 0; i < m_cTargetlist.Count; i++)
    //                {
    //                    m_cTargetlist[i].TargetPosition = Positon;
    //                }

    //            }
    //            이렇게 else if로 땅클릭을 적용하면 적 유닛 클릭시 적유닛쪽으로 이동을 안함
    //            Target.TargetPosition = Positon;

    //        }
    //    }
    //}

    //public void UnitCommend()
    //{
    //    if (Target != null)
    //    {
    //        if (Input.GetKeyDown(KeyCode.A))
    //        {
    //            for (int i = 0; i < m_cTargetlist.Count; i++)
    //                m_cTargetlist[i].m_UnitCommend = 1;
    //        }
    //        else if(Input.GetKeyDown(KeyCode.S))
    //        {
    //            for (int i = 0; i < m_cTargetlist.Count; i++)
    //                m_cTargetlist[i].m_UnitCommend = 0;
    //        }
    //    }
    //}

    //public void UnitDetect()
    //{
    //    if (Target != null)
    //    {
    //        //if (Input.GetKey(KeyCode.A)/*&&Input.GetMouseButtonDown(1)*/)//A키를 누르고있으면 어택땅
    //        for (int i = 0; i < m_cTargetlist.Count; i++)
    //        {
    //            //Target.Detect(Target);
    //            if(m_cTargetlist[i].m_UnitCommend == 1)
    //            {
    //                m_cTargetlist[i].Detect(/*m_cTargetlist[i]*/);
    //                if(m_cTargetlist[i].DetectCheck == 1)
    //                {
    //                    if (m_cTargetlist[i].m_enemy != null)
    //                    {
    //                        m_cTargetlist[i].m_enemy.m_sUnit.Hp = m_cTargetlist[i].m_enemy.m_sUnit.Hp - m_cTargetlist[i].m_sUnit.Damage;
    //                        m_cTargetlist[i].m_enemy.ChangeHp(m_cTargetlist[i].m_enemy.m_sUnit.Hp, m_cTargetlist[i].m_enemy.m_sUnit.MaxHp);
    //                        if (m_cTargetlist[i].m_enemy.m_sUnit.Hp <= 0)
    //                        {
    //                            Destroy(m_cTargetlist[i].m_enemy.gameObject);
    //                            m_cTargetlist[i].m_enemy = null;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //public void ZombieAction()
    //{
    //    if (m_cEnemies != null)
    //    {
    //        for (int i = 0; i < m_cEnemies.Count; i++)
    //        {
    //            m_cEnemies[i].Detect();
    //            if (m_cEnemies[i].DetectCheck == 1)
    //            {
    //                if (m_cEnemies[i].m_enemy != null)
    //                {
    //                    m_cEnemies[i].m_enemy.m_sUnit.Hp = m_cEnemies[i].m_enemy.m_sUnit.Hp - m_cEnemies[i].m_sUnit.Damage;
    //                    m_cEnemies[i].m_enemy.ChangeHp(m_cEnemies[i].m_enemy.m_sUnit.Hp, m_cEnemies[i].m_enemy.m_sUnit.MaxHp);
    //                    if (m_cEnemies[i].m_enemy.m_sUnit.Hp <= 0)
    //                    {
    //                        Destroy(m_cEnemies[i].m_enemy.gameObject);
    //                        m_cEnemies[i].m_enemy = null;
    //                    }
    //                }
    //            }
    //        } 
    //    }
    //}
    //if (m_enemy != null)
    //    {
    //        m_enemy.m_sUnit.Hp = m_enemy.m_sUnit.Hp - m_sUnit.Damage;
    //        m_enemy.ChangeHp(m_enemy.m_sUnit.Hp, m_enemy.m_sUnit.MaxHp);
    //        //counttest++;
    //        if (m_enemy.m_sUnit.Hp <= 0)
    //        {
    //            Destroy(m_enemy.gameObject);
    //            m_enemy = null;
    //        }
    //    }
    //IEnumerator UnitAttack()
    //{
    //    for (int i = 0; i < m_cTargetlist.Count; i++)
    //    {
    //        if (m_cTargetlist[i].DetectCheck == 1)
    //        {
    //            yield return new WaitForSeconds(1.0f);
    //            m_cTargetlist[i].Attack();
    //            //Debug.Log(m_cTargetlist[i].counttest);
    //        }
    //    }
    //}
    //IEnumerator UnitDetect()
    //{
    //    if (Target != null)
    //    {
    //        if (Input.GetKey(KeyCode.A)/*&&Input.GetMouseButtonDown(1)*/)//A키를 누르고있으면 어택땅
    //        {
    //            //Target.Detect(Target);
    //            for (int i = 0; i < m_cTargetlist.Count; i++)
    //            {
    //                m_cTargetlist[i].Detect(/*m_cTargetlist[i]*/);
    //                if (m_cTargetlist[i].DetectCheck == 1)
    //                {
    //                    yield return new WaitForSeconds(1.0f);
    //                    m_cTargetlist[i].Attack();
    //                }
    //            }
    //        }
    //    }
    //}

    //private void OnGUI()
    //{
    //    GUI.Box(new Rect(0, 0, 100, 20), "Dist:" + m_fDist);
    //}


}

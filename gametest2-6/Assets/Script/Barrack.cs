using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour {
    public Transform Regenposition;
    public int SelectCheck = 0;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        BarrackSelect();
        PdUnit();
	}

    public void BarrackSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, 100.0f, 1 << LayerMask.NameToLayer("Building")))
            {
                hitinfo.collider.gameObject.GetComponent<Barrack>().SelectCheck = 1;
            }
            else
            {
                SelectCheck = 0;
            }
        }
    }

    public void PdUnit()
    {
        if (SelectCheck == 1 && Input.GetKeyDown(KeyCode.M) && GameManager.GetInstance().Food >= 25 && GameManager.GetInstance().MaxPopulation > GameManager.GetInstance().NowPopulation)//배럭에서 솔저 생산
        {
            GameObject pdSolider = Instantiate(GameManager.GetInstance().G_Solider, this.Regenposition.position, Quaternion.identity);
            pdSolider.name = "solider";
            GameManager.GetInstance().unitcount++;
            GameManager.GetInstance().m_cUnits.Add(pdSolider.GetComponent<UnitBox>());
            GameManager.GetInstance().m_cUnits[GameManager.GetInstance().unitcount].m_sUnit = GameManager.GetInstance().m_cUnitManager.GetUnit(UnitManager.eUnit.Solider);
            //Debug.Log(m_cUnits[unitcount].m_sUnit.Name);
            GameManager.GetInstance().Food -= 25;
            GameManager.GetInstance().NowPopulation++;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour {
    public Text m_cName;
    public Text m_cHp;

    public void Set(UnitManager.eUnit unit)
    {
        //Unit cUnit = GameManager.GetInstance().m_cUnitManager.GetUnit(unit);
        //m_cName.text = cUnit.Name;
        //m_cHp.text = string.Format("{0} / {1}",cUnit.Hp,cUnit.MaxHp);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

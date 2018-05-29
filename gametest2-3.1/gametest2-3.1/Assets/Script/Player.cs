using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public List<Unit> m_eUnitlist = new List<Unit>();
    List<BuildingManager.eBuilding> m_eBuildinglist = new List<BuildingManager.eBuilding>();

    int m_nWood;
    int m_nFood;
    int m_nMaxPop;//최대인구수
    int m_nNowPop;//현재인구수

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}

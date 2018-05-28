using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBox : MonoBehaviour {
    public UnitManager.eUnit m_eUnit;
    public Vector3 TargetPosition;
    NavMeshAgent nav;//유닛 박스에 직접 네비매시를 입력
    public Unit m_sUnit;

    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        nav.SetDestination(TargetPosition);
    }
 
    
}

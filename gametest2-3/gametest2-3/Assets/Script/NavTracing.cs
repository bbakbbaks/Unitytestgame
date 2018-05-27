using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTracing : MonoBehaviour
{
    public Transform m_Target; //추적 대상
    public float m_fMinDist = 5; //대상을 추적하는 범위
    NavMeshAgent m_cNavMeshAgent;
    float m_fDist = 0; //대상과의 남은 거리 //디버깅을 위해 멤버변수로 만듦.

    // Use this for initialization
    void Start()
    {
        //네비매쉬를 사용하기 위해 저장해둠.
        m_cNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        TrancingTarget();
    }

    void TrancingTarget()
    {
        Vector3 vTargetPos = m_Target.position; //추적대상 위치
        Vector3 vPos = transform.position; //물체위치

        m_fDist = Vector3.Distance(vTargetPos, vPos);

        if (m_fDist < m_fMinDist) //대상의 추적 최소거리까지 물체를 추적함.
            m_cNavMeshAgent.SetDestination(vTargetPos);
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 20), "Dist:" + m_fDist);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHp : MonoBehaviour {
    public RectTransform m_cRectTransform;

    // Use this for initialization
    void Start () {
        m_cRectTransform = this.gameObject.GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

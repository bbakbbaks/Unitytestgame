using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    string strName;
    int m_nHp;
    int m_nMaxHp;
    int m_nNdResource;
    int m_nPdResource;
    string strImage;

    public string Name { get { return strName; } }
    public int Hp
    {
        set { m_nHp = value; }
        get { return m_nHp; }
    }
    public int MaxHp { get { return m_nMaxHp; } }
    public int NdResource { get { return m_nNdResource; } }
    public int PdResource { get { return m_nPdResource; } }
    public string Image { get { return strImage; } }

    public Building(string name, int hp, int NdRs, int PdRs, string image)
    {
        Set(name, hp, NdRs, PdRs, image);
    }
    public void Set(string name, int hp, int NdRs, int PdRs, string image)
    {
        strName = name;
        m_nHp = hp;
        m_nMaxHp = hp;
        m_nNdResource = NdRs;
        m_nPdResource = PdRs;
        strImage = image;
    }
}

public class BuildingManager : MonoBehaviour {
    public enum eBuilding { Center, Lumbermill, House, Farm, Barrack }
    List<Building> m_listBuildings = new List<Building>();
	// Use this for initialization
	void Start () {
        InitBulilding();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitBulilding()
    {
        m_listBuildings.Add(new Building("지휘본부", 500, 0, 0, "imgcenter"));
        m_listBuildings.Add(new Building("제재소", 70, 100, 20, "imglumber"));
        m_listBuildings.Add(new Building("집", 30, 50, 4, "imghouse"));
        m_listBuildings.Add(new Building("농장", 70, 100, 5, "imgfarm"));
        m_listBuildings.Add(new Building("병영", 50, 200, 0, "imgbarrack"));
    }
}

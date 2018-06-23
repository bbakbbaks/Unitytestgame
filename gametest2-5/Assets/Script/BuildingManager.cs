using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Building
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
        strName = name;
        m_nHp = hp;
        m_nMaxHp = hp;
        m_nNdResource = NdRs;
        m_nPdResource = PdRs;
        strImage = image;
    }

}

public class BuildingManager : MonoBehaviour {
    public enum eBuilding { Lumbermill, House, Farm, Barrack, Wall }
    public List<Building> m_listBuildings = new List<Building>();

    public Building GetBuilding(eBuilding building)
    {
        return m_listBuildings[(int)building];
    }
}

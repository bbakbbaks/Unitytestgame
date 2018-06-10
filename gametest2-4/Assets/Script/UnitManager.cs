using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Unit
{
    string strName;
    int m_nHp;
    int m_nMaxHp;
    int m_nDam;
    int m_nRange;
    int m_nMovespeed;
    string strImage;

    public string Name { get { return strName; } }
    public int Hp
    {
        set { m_nHp = value; }
        get { return m_nHp; }
    }
    public int MaxHp { get { return m_nMaxHp; } }
    public int Damage { get { return m_nDam; } }
    public int Range { get { return m_nRange; } }
    public int Movespeed { get { return m_nMovespeed; } }
    public string Image { get { return strImage; } }

    public Unit(string name, int hp, int dam, int range, int movespeed, string image)
    {
        strName = name;
        m_nHp = hp;
        m_nMaxHp = hp;
        m_nDam = dam;
        m_nRange = range;
        m_nMovespeed = movespeed;
        strImage = image;
    }
}

public class UnitManager : MonoBehaviour {
    public enum eUnit { Worker, Solider, Zombie }
    public List<Unit> m_listunits = new List<Unit>();

    public Unit GetUnit(eUnit unit)
    {
        return m_listunits[(int)unit];
    }

}

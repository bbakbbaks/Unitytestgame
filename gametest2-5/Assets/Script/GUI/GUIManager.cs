using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public List<GameObject> m_listScene;
    public Text m_textResource;
    public enum eScene { TITLE, GAMEOVER, PLAY, THEEND, MAX };
    eScene m_eScene;

    public void SetScene(eScene scene)
    {
        switch (scene)
        {
            case eScene.TITLE:
                break;
            case eScene.GAMEOVER:
                break;
            case eScene.PLAY:
                break;
            case eScene.THEEND:
                break;
        }
        ShowScene(scene);
        m_eScene = scene;
    }

    public void UpdateScene()
    {
        switch (m_eScene)
        {
            case eScene.TITLE:
                break;
            case eScene.GAMEOVER:
                break;
            case eScene.PLAY:
                break;
            case eScene.THEEND:
                break;
        }
    }

    public GameObject GetScene(eScene scene)
    {
        return m_listScene[(int)scene];
    }

    public void ShowScene(eScene scene)
    {
        for (eScene e = 0; e < eScene.MAX; e++)
        {
            if (scene == e)
                m_listScene[(int)e].SetActive(true);
            else
                m_listScene[(int)e].SetActive(false);
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateScene();
    }
}

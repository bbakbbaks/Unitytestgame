using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Muiltiunitmove : MonoBehaviour {
    public Camera camera;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Camerawalk();
    }

    public void Camerawalk()
    {
        if (Input.GetKey(KeyCode.UpArrow))//키가 눌렸는지 확인하는 함수
        {
            transform.position += transform.forward * Time.deltaTime * 10 +
                transform.up * Time.deltaTime * 10;
            //여기서 forward는 파란 화살표가 가리키는 방향이다

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= transform.forward * Time.deltaTime * 10
                + transform.up * Time.deltaTime * 10;
        }
        camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel");
        if (camera.orthographicSize >= 15)
        {
            camera.orthographicSize = 15f;
        }
        if (camera.orthographicSize <= 10)
        {
            camera.orthographicSize = 10f;
        }
        Vector3 limitmap;
        limitmap.x = Mathf.Clamp(transform.position.x, (float)11.5, (float)58.5);
        limitmap.y = Mathf.Clamp(transform.position.y, (float)23.5, (float)48.5);
        limitmap.z = Mathf.Clamp(transform.position.z, (float)-20, (float)14);
        transform.position = limitmap;
    }

    public void GoTutorial()
    {
        SceneManager.LoadScene("tutorial");
    }

    public void GoMain()
    {
        SceneManager.LoadScene("basic");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GoToProfiler : MonoBehaviour
{
    public Controller controller;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("OnClick");
        Debug.Log(controller.layout30);
        controller.WriteLayout30ToFile();
        SceneManager.LoadScene("profile", LoadSceneMode.Single);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class Controller : MonoBehaviour
{
    public string layout30 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ____";
    public string layout30_path;
    private string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ____";
    private TMP_InputField[] input_boxes = new TMP_InputField[3];
    public GameObject not_ready;
    public GameObject ready;

    // Start is called before the first frame update
    void Start()
    {
        layout30_path = Application.dataPath + "/finger/layout30.txt";
        DirectoryUtils.SafeCreateDirectory(Application.dataPath + "/finger");
        if(!File.Exists(layout30_path)){
            File.WriteAllText(layout30_path, "hoge");
        }
        Debug.Log(GameObject.Find("top"));
        Debug.Log(GameObject.Find("top").GetComponent<TMP_InputField>());
        input_boxes[0] = GameObject.Find("top").GetComponent<TMP_InputField>();
        input_boxes[1] = GameObject.Find("middle").GetComponent<TMP_InputField>();
        input_boxes[2] = GameObject.Find("bottom").GetComponent<TMP_InputField>();

        layout30 = LoadLayout30();
        InitializeInputBoxes();
    }

    // Update is called once per frame
    void Update()
    {
        layout30 = JoinInputBoxesIntoLayout30();
        not_ready.SetActive(!CanSortToMatch(layout30));
        ready.SetActive(CanSortToMatch(layout30));
    }

    private string LoadLayout30(){
        string data = File.ReadAllText(layout30_path);
        return CanSortToMatch(data) ? data : "ABCDEFGHIJKLMNOPQRSTUVWXYZ____";
    }

    private void InitializeInputBoxes(){
        for (int i = 0; i < 3; i++){
            input_boxes[i].text = layout30.Substring(10 * i, 10);
        }
    }

    private string JoinInputBoxesIntoLayout30(){
        return input_boxes[0].text + input_boxes[1].text + input_boxes[2].text;
    }

    public bool CanSortToMatch(string input1)
    {
        char[] chars1 = input1.ToCharArray();
        Array.Sort(chars1);
        string sortedInput1 = new string(chars1);

        return string.Equals(sortedInput1, ABC);
    }
     
    public void WriteLayout30ToFile(){
        Debug.Log(layout30_path);
        File.WriteAllText(layout30_path, layout30);
    }

    void ReadWriteSample()
    {
        // 書き込み
        string path = Application.dataPath + "/finger/test.txt";
        File.WriteAllText(path, "hoge");

        // 追記
        File.AppendAllText(path, "fuga");
        Debug.Log("Save at " + path);

        // 読み込み
        string data = File.ReadAllText(path);
        Debug.Log("Data is " + data);
    }
}

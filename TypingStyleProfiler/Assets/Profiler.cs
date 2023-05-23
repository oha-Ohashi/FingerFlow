using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class Profiler : MonoBehaviour
{
    public float time_game_start;
    public string layout30_path;
    public char[] layout30ch;
    private string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ____";
    public GameObject board;
    public GameObject[] keys = new GameObject[30];
    public GameObject original_key;
    
    public TargetsGenerator targets_generator;

    private List<int> targets = new List<int>();
    public List<float> resultL = new List<float>();
    private int disp_phase = 0;
    private int phase_done = 0;
    private int nth_ch = 0;
    private int nth_ch_done = 0;
    public List<int> to_type = new List<int>();
    
    public GameObject beforeStartObject;
    public GameObject afterFinishObject;

    private float timeElapsed;
    // Start is called before the first frame update

    void Start()
    {
        time_game_start = Time.time;
        layout30_path = Application.dataPath + "/finger/layout30.txt";
        layout30ch = LoadLayout30().ToCharArray();
        for(int i = 0; i < 29; i++){
            Instantiate(original_key, board.transform);
        }
        for(int y = 0; y < 3; y++){
            for(int x = 0; x < 10; x++){
                int i = y * 10 + x;
                keys[i] = board.transform.GetChild(i).gameObject;
                RectTransform rt = keys[i].transform as RectTransform;
                rt.localPosition = new Vector3(-540 + 120*x, 120 - 120*y, 0);
                //Debug.Log(layout30ch[i]);
                GameObject ch_obj = keys[i].transform.Find("text").gameObject;
                //Debug.Log(ch_obj);
                TMP_Text ch_tmp = ch_obj.GetComponent<TMP_Text>();
                //Debug.Log(ch_tmp);
                ch_tmp.text = layout30ch[i].ToString();
            }
        }

        targets_generator.SetValidIndices(layout30ch);
        targets = targets_generator.GenerateTargets();

        //StartCoroutine(InclimentDispPhaseCoroutine(1.0f));
        afterFinishObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeElapsed > 1){
            //keys[2].GetComponent<key>().which_color = 1;
            keys[2].GetComponent<key>().next_color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
        }

        timeElapsed += Time.deltaTime;
        //Debug.Log(timeElapsed);

        while(disp_phase < targets.Count && targets[disp_phase] >= 0){
            //StartCoroutine(DispPhaseCoroutine());
            Invoke("DispPhase", nth_ch * 0.5f);
            disp_phase++;
            nth_ch++;
        }
        nth_ch = 0;

        if(disp_phase == 0 ){
            if (Input.GetKeyDown("space"))
            {   
                print("space key was pressed");
                disp_phase++;
                phase_done++;
                nth_ch_done = 0;
                beforeStartObject.SetActive(false);
            }
        }else{
            if(to_type.Count == 0 && nth_ch_done > 0){
            //if(false){
                disp_phase++;
                phase_done++;
                nth_ch_done = 0;
            }
        }

        if(disp_phase >= targets.Count && to_type.Count == 0){
            afterFinishObject.SetActive(true);
        }else{
            afterFinishObject.SetActive(false);
        }
    }

    private string LoadLayout30(){
        string data = File.ReadAllText(layout30_path);
        return CanSortToMatch(data) ? data : "ABCDEFGHIJKLMNOPQRSTUVWXYZ____";
    }

    public bool CanSortToMatch(string input1)
    {
        char[] chars1 = input1.ToCharArray();
        Array.Sort(chars1);
        string sortedInput1 = new string(chars1);

        return string.Equals(sortedInput1, ABC);
    }

    private void DispPhase(){
        //Debug.Log("done:" + phase_done + "tgt" + this.targets[phase_done]);
        keys[this.targets[phase_done]].GetComponent<key>().ChangeWhichColor(nth_ch_done+1);
        to_type.Add(this.targets[phase_done]);
        phase_done++;
        nth_ch_done++;
    }

    public void SaveAndFinish(){
        string res = "";
        Debug.Log("おわり");
        List<float> inserted_resultL = ProcessLists(targets, resultL);
        /*for(int i = 0; i < targets.Count; i++){
            string line = targets[i] + "," + inserted_resultL[i];
            Debug.Log(line);
            res += line + "\n";
        }*/
        res += String.Join(",", targets);
        res += "\n";
        res += String.Join(",", inserted_resultL);
        Debug.Log(res);
        int file_num = (int)Time.time;
        string path = Application.dataPath + "/finger/result/"+file_num.ToString()+".csv";
        DirectoryUtils.SafeCreateDirectory(Application.dataPath + "/finger/result");
        File.WriteAllText(path, res);

        SceneManager.LoadScene("profile", LoadSceneMode.Single);
    }

    public List<float> ProcessLists(List<int> list1, List<float> list2)
    {
        List<float> output = new List<float>();
        float lastFloat = 0f;
        int index = 0;

        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] == -1)
            {
                output.Add(lastFloat);
            }
            else
            {
                if (index < list2.Count)
                {
                    lastFloat = list2[index];
                    output.Add(list2[index]);
                    index++;
                }
            }
        }
        
        return output;
    }
}

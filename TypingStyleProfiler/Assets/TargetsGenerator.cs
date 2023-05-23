using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetsGenerator : MonoBehaviour
{
    private List<int> valid_indices = new List<int>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValidIndices(char[] layout30ch){
        for(int i = 0; i < 30; i++){
            if(layout30ch[i] != '_'){
                valid_indices.Add(i);
            }
        }
        Debug.Log(string.Join(",", valid_indices.Select(n => n.ToString())));
    }
     
    public List<int> GenerateTargetsOld(){
        List<int> res = new List<int>();
        for(int i = 0; i < 10; i++){
            res.Add(valid_indices[Random.Range(0, 26)]);
        }
        Debug.Log("gen: ");
        Debug.Log(string.Join(",", res.Select(n => n.ToString())));
        return res;
    }

    public List<int> GenerateTargetsMock(){
        List<int> res = new List<int>();
        res.Add(-1);
        for(int i = 0; i < 3; i++){
            res.Add(valid_indices[Random.Range(0, 26)]);
        }
        res.Add(-1);
        for(int i = 0; i < 3; i++){
            res.Add(valid_indices[Random.Range(0, 26)]);
        }
        Debug.Log("gen: ");
        Debug.Log(string.Join(",", res.Select(n => n.ToString())));
        return res;
    }

    public List<int> GenerateTargets(){
        List<int> res = new List<int>();
        int repeats;
        for(int i = 0; i < 23; i++){
            res.Add(-1);
            if(i < 2) {
                repeats = 2;
            } else if (i < 13) {
                repeats = 3;
            } else {
                repeats = 4;
            }
            for(int j = 0; j < repeats; j++){
                res.Add(valid_indices[Random.Range(0, 26)]);
            }
        }
        Debug.Log("gen: ");
        Debug.Log(string.Join(",", res.Select(n => n.ToString())));
        return res;
    }


}

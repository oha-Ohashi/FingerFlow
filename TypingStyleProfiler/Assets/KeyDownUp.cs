using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDownUp : MonoBehaviour
{
    public Profiler profiler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){KeyDown('A');}
        if (Input.GetKeyDown(KeyCode.B)){KeyDown('B');}
        if (Input.GetKeyDown(KeyCode.C)){KeyDown('C');}
        if (Input.GetKeyDown(KeyCode.D)){KeyDown('D');}
        if (Input.GetKeyDown(KeyCode.E)){KeyDown('E');}
        if (Input.GetKeyDown(KeyCode.F)){KeyDown('F');}
        if (Input.GetKeyDown(KeyCode.G)){KeyDown('G');}
        if (Input.GetKeyDown(KeyCode.H)){KeyDown('H');}
        if (Input.GetKeyDown(KeyCode.I)){KeyDown('I');}
        if (Input.GetKeyDown(KeyCode.J)){KeyDown('J');}
        if (Input.GetKeyDown(KeyCode.K)){KeyDown('K');}
        if (Input.GetKeyDown(KeyCode.L)){KeyDown('L');}
        if (Input.GetKeyDown(KeyCode.M)){KeyDown('M');}
        if (Input.GetKeyDown(KeyCode.N)){KeyDown('N');}
        if (Input.GetKeyDown(KeyCode.O)){KeyDown('O');}
        if (Input.GetKeyDown(KeyCode.P)){KeyDown('P');}
        if (Input.GetKeyDown(KeyCode.Q)){KeyDown('Q');}
        if (Input.GetKeyDown(KeyCode.R)){KeyDown('R');}
        if (Input.GetKeyDown(KeyCode.S)){KeyDown('S');}
        if (Input.GetKeyDown(KeyCode.T)){KeyDown('T');}
        if (Input.GetKeyDown(KeyCode.U)){KeyDown('U');}
        if (Input.GetKeyDown(KeyCode.V)){KeyDown('V');}
        if (Input.GetKeyDown(KeyCode.W)){KeyDown('W');}
        if (Input.GetKeyDown(KeyCode.X)){KeyDown('X');}
        if (Input.GetKeyDown(KeyCode.Y)){KeyDown('Y');}
        if (Input.GetKeyDown(KeyCode.Z)){KeyDown('Z');}
    }

    private void KeyDown(char ch){
        int i_key = Array.IndexOf(profiler.layout30ch, ch);
        //Debug.Log("down: " + i_key);
        if (profiler.to_type.Count > 0){
            if (profiler.to_type[0] == i_key){
                //Debug.Log("hit!!");
                profiler.resultL.Add(Time.time - profiler.time_game_start);
                profiler.keys[i_key].GetComponent<key>().ChangeWhichColor(0);
                profiler.to_type.RemoveAt(0);
            }
        }
    }

}

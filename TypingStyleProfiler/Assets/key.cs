using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class key : MonoBehaviour
{
    Image img;
    private Color old_color;
    public Color next_color;
    private Color[] colors = new Color[5];
    public int which_color = 0;
    private float timeOut = 0.033f; //30FPS
    private float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = this.transform.Find("img").gameObject;
        img = obj.GetComponent<Image>();
        colors[0] = new Color(37f/256f, 37f/256f, 37f/256f, 1.0f);
        colors[1] = new Color(133f/256f, 0f/256f, 0f/256f, 1.0f);
        colors[2] = new Color(133f/256f, 53f/256f, 0f/256f, 1.0f);
        colors[3] = new Color(133f/256f, 106f/256f, 0f/256f, 1.0f);
        colors[4] = new Color(99f/256f, 133f/256f, 0f/256f, 1.0f);
        img.color = colors[which_color];
    }

    // Update is called once per frame
    void Update()
    {
        //img.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        timeElapsed += Time.deltaTime;
        if(timeElapsed >= timeOut) {
            // Do anything
            Color bet_color = Color.Lerp(
                img.color,
                colors[which_color],
                //colors[which_color], 
                0.2f
            );
            img.color = bet_color;

            timeElapsed = 0.0f;
        }
    }

    public void ChangeWhichColor(int arg){
        this.which_color = arg;
    }
}

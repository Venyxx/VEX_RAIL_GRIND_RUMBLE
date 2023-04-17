using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSitting : MonoBehaviour
{
    Animator anim;
    private float maxTime = 2;
    private float currentTime;
    public float changeTime;
     float var;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        //anim.SetBool("isIdle", true);
        var = Random.Range(0f,6);
        changeTime = Random.Range(2f, 5f);
        float sm = Random.Range(0.01f,1.15f);
        anim.SetFloat("sm", sm);
    }

    // Update is called once per frame
    void Update()
    {

        if (var < 2 && changeTime != 0)
        {
            anim.SetBool("isIdle",true);
            anim.SetBool("isLaughing",false);
            anim.SetBool("isTalking",false);

        }
        else if (var > 5 && changeTime != 0)
        {
            anim.SetBool("isTalking", true);
            anim.SetBool("isLaughing",false);
            anim.SetBool("isIdle",false);
        }  
        else if (var >= 2 && var <= 5)
        {
            anim.SetBool("isLaughing" , true);
            anim.SetBool("isIdle",false);
            anim.SetBool("isTalking",false);
        }

        changeTime -= Time.deltaTime;

        if(changeTime < 0)
        {
            var = Random.Range(0f,6);
            changeTime = Random.Range(2f, 5f);
        }
            
    }
}

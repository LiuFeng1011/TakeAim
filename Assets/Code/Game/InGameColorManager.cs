﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameColorManager{
    private static InGameColorManager instance;

    public static InGameColorManager GetInstance(){
        if(instance == null){
            instance = new InGameColorManager();
            instance.Init();
        }
        return instance;
    }

    float rand,targetRand, updateTime = 0f, maxUpdateTime = 0.1f;

    public Color objColor1, objColor2, bgColor;

    public void Init(){
        rand = Random.Range(0f,3.14f);
        targetRand = rand;
        SetColor();
        //GameObject bg = GameObject.Find("bg");
        //if (bg == null) return;
        //ParticleSystem particle = bg.GetComponent<ParticleSystem>();

        //var shape = particle.shape;
        //shape.box = new Vector3(
        //    InGameManager.GetInstance().GetGameRect().width,
        //    0,InGameManager.GetInstance().GetGameRect().height);

        //Gradient grad = new Gradient();
        //grad.SetKeys(new GradientColorKey[] { new GradientColorKey(c, 0.0f) , new GradientColorKey(c, 0.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f) , new GradientAlphaKey(0.1f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) });
        //var a = particle.colorOverLifetime;
        //a.color = grad;

    }

    public void Update(){
        //rand += Time.deltaTime * (1f / 10f);
        //updateTime += Time.deltaTime;

        //if(updateTime < maxUpdateTime){
        //    return;
        //}
        //updateTime = 0;

        //SetColor(rand);

        if(targetRand != rand){
            rand += (targetRand - rand) * 0.3f;
            SetColor();
        }
    }

    public void SetColor(float val)
    { 
        targetRand = rand + val;
    }
    public void SetColor(){
        
        float h, s, v;
        h = Mathf.Abs(Mathf.Sin(rand));
        s = 0.6f;
        v = 0.8f;

        objColor1   = Color.HSVToRGB(h, s, v);

        bgColor = Color.HSVToRGB(h, 1f, 1f);

        h += 0.33f;
        objColor2   = Color.HSVToRGB(h - (int)h, s, v);



        //Camera.main.backgroundColor = objColor1;

        //InGameManager.GetInstance().inGameUIManager.gamePadManager.scoreslabel.SetColor(objColor2);
    }

    public void Destroy(){
        
    }
}

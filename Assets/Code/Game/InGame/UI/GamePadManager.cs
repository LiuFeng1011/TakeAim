﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadManager : InGameUIBaseLayer {

    public GamePadScoresLabel scoreslabel;
    public GamePadScoresLabel combolabel;

    public Vector3 comboLabelPos;

    int combocount = 0;
    public override void Init()
    {
        base.Init();
        scoreslabel = transform.Find("scores").Find("Label").GetComponent<GamePadScoresLabel>();
        scoreslabel.Init(0);
        Transform comboIcom = transform.Find("TopLeft").Find("ComboIcon");
        combolabel = comboIcom.Find("CountLabel").GetComponent<GamePadScoresLabel>();
        combolabel.Init(combocount);
        combocount = PlayerPrefs.GetInt(GameConst.USERDATANAME_COMBO_COUNT, 0);
        combolabel.SetScores(combocount);

        Camera uicamera = GameObject.Find("UI Root").transform.Find("Camera").GetComponent<Camera>();
        Vector3 screenpos = uicamera.WorldToScreenPoint(comboIcom.position);
        comboLabelPos = GameCommon.ScreenPositionToWorld(InGameManager.GetInstance().gamecamera, screenpos);


        GameObject pausebtn = transform.Find("TopRight").Find("pause").gameObject;
        GameUIEventListener.Get(pausebtn).onClick = PauseBtnCB;

    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
       
	}

    public void PauseBtnCB(GameObject obj){
        InGameManager.GetInstance().Pause();
    }

    public void SetScores(int val,bool isCombo)
    {
        scoreslabel.SetScores(val);
        if(isCombo){
            combocount++;
            PlayerPrefs.SetInt(GameConst.USERDATANAME_COMBO_COUNT, combocount);
            combolabel.SetScores(combocount);
        }
    }

}

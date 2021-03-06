﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCreateObjManager : BaseGameObject {

    float addStepTime = 0, addSawTime = 0f;
    const float MAX_ADDHEIGHT = 4.0f, MIN_ADDHEIGHT = 0.8f;
    const float MAX_ADD_SAW_TIME = 3f,MAX_ADD_STEP_TIME = 7f;

    float lastAddTime = 0, addTime = 1f, addDis = 0;

    int maxCount = 3;

    List<InGameBaseObj> steps = new List<InGameBaseObj>();

    public void Init()
    {
    }

    public void Update()
    {
        AddStepUpdate();
    }

    void AddStepUpdate(){
        lastAddTime += Time.deltaTime;
        if (lastAddTime < addTime) return;

        int count = steps.Count-1;
        for (int i = count; i >= 0; i--){
            if (steps[i] == null) steps.RemoveAt(i);
        }

        if (steps.Count >= maxCount) return;

        Rect gamerect = InGameManager.GetInstance().GetGameRect();

        float y = InGameManager.GetInstance().role.transform.position.y + 2;
        float rand = Random.Range(0f,gamerect.y + gamerect.height - y - 2);

        InGameBaseObj obj = AddItem("InGameStep", y + rand);

        steps.Add(obj);

        lastAddTime = 0;
    }

    InGameBaseObj AddItem(string id, float height){

        InGameBaseObj item = InGameManager.GetInstance().inGameLevelManager.AddObj(id);

        float randScale = 1.8f - InGameManager.GetInstance().gameScale * 1f;

        item.transform.localScale = new Vector3(randScale, randScale, 1);

        Rect gamerect = InGameManager.GetInstance().GetGameRect();
        //add item
        item.transform.position = new Vector3(gamerect.x + Random.Range(0, gamerect.width - 2) + 1,
                                              height + randScale / 2);

        item.Init();
        return item;
    }

    public void Destroy()
    {

    }
}

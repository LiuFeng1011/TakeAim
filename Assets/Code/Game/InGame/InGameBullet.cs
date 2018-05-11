using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBullet : InGameBaseObj {
    public enum BulletState{
        READY,
        FIRE
    }

    BulletState state;

    float speed = 15f;

    private void Awake()
    {
        state = BulletState.READY;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    public override void ObjUpdate () {

        Rect gamerect = InGameManager.GetInstance().GetGameRect();
        if(state == BulletState.READY){
            transform.position = InGameManager.GetInstance().role.transform.position;
        }else{
            transform.position += new Vector3(0, speed * Time.deltaTime);
        }

        if(transform.position.y > gamerect.x + gamerect.height){
            SetDie();
            InGameManager.GetInstance().GameOver();
            return;
        }


        List<InGameBaseObj> objlist = InGameManager.GetInstance().inGameLevelManager.objList;

        for (int i = 0; i < objlist.Count; i++)
        {
            InGameBaseObj obj = objlist[i];
            if (obj.myObjType != InGameBaseObj.ObjType.enemy)
            {
                continue;
            }

            float dis = Vector3.Distance(transform.position, obj.transform.position);
            if(dis < (transform.localScale.x + obj.transform.localScale.x) * 0.4f){
                SetDie();
                obj.SetDie();
                InGameManager.GetInstance().role.AddScores(1, true,obj);
                return;
            }
        }
	}

    public void Fire(){
        state = BulletState.FIRE;

        (new EventCreateEffect(60010010, gameObject, transform.position, 1.0f)).Send();
        //60010010
    }
}

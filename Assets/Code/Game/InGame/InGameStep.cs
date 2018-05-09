using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStep : InGameBaseObj {


	public override void Init()
    {
        base.Init();

        SpriteRenderer sr = transform.Find("icon").GetComponent<SpriteRenderer>();
        sr.sprite = InGameManager.GetInstance().stepSpriteRes;

    }
	
	// Update is called once per frame
	public override void ObjUpdate()
    {
        base.ObjUpdate();

        if(IsDie()){
            return;
        }

        //transform.position -= new Vector3(0, Time.deltaTime*InGameManager.GetInstance().gameSpeed, 0);

        if(transform.position.y < InGameManager.GetInstance().GetGameRect().y + transform.localScale.y / 2 ){
            SetDie();
            //InGameManager.GetInstance().GameOver();
        }

    }

    public override void Die()
    {
        (new EventCreateEffect(60010013, null, transform.position, transform.localScale.x)).Send();
        base.Die();
    }

}

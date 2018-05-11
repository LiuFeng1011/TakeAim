using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStep : InGameBaseObj {
    float baseScale = 0f, showActionTime = 0f, showActionMaxTime = 0.4f;
    public AnimationCurve ac;

    float actionSpeed = 0, actionLeft = 0,actionRight = 0;

	public override void Init()
    {
        base.Init();

        SpriteRenderer sr = transform.Find("icon").GetComponent<SpriteRenderer>();
        sr.sprite = InGameManager.GetInstance().stepSpriteRes;

        baseScale = transform.localScale.x;

        transform.localScale = Vector3.zero;

        float rate = Random.Range(0, 0.5f) * InGameManager.GetInstance().gameScale;

        if (rate > 0.05f){
            actionSpeed = Random.Range(1f, 2f) + 0.2f;
            float width = Random.Range(1f, 3f);
            Rect gamerect = InGameManager.GetInstance().GetGameRect();
            actionLeft = Mathf.Max(transform.position.x - width,gamerect.x + transform.localScale.x / 2);
            actionRight = Mathf.Min(transform.position.x + width, gamerect.x + gamerect.width - transform.localScale.x / 2);
        }

    }

    // Update is called once per frame
    public override void ObjUpdate()
    {
        base.ObjUpdate();

        if (IsDie())
        {
            return;
        }

        //transform.position -= new Vector3(0, Time.deltaTime*InGameManager.GetInstance().gameSpeed, 0);

        if (transform.position.y < InGameManager.GetInstance().GetGameRect().y + transform.localScale.y / 2)
        {
            SetDie();
            //InGameManager.GetInstance().GameOver();
        }

        ActionUpdate();

        if (showActionTime >= showActionMaxTime) return;
        showActionTime += Time.deltaTime;
        float val = ac.Evaluate(Mathf.Min(showActionTime / showActionMaxTime, 1f));
        float scale = baseScale * val;

        transform.localScale = new Vector3(scale, scale, 1f);
    }

    public void ActionUpdate(){
        if (actionSpeed == 0) return;

        transform.position = transform.position + new Vector3(actionSpeed * Time.deltaTime,0,0);

        if((actionSpeed > 0 && transform.position.x > actionRight) || 
           (actionSpeed < 0 && transform.position.x < actionLeft)){
            actionSpeed *= -1;
        }
    }

    public override void Die()
    {
        (new EventCreateEffect(60010013, null, transform.position, transform.localScale.x)).Send();
        base.Die();
    }

}

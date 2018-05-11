using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class InGameRole : InGameBaseObj {

    public int combo = 0,scores = 0;

    public BuffManager buffManager;

    float moveSpeed = 8,addBulletTime = 0f,addBulletMaxTime = 0.2f;

    InGameBullet bullet = null;


    private void Awake()
    {
        EventManager.Register(this,
                       EventID.EVENT_TOUCH_DOWN,
                              EventID.EVENT_TOUCH_MOVE);

        buffManager = new BuffManager();
        buffManager.Init();

    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	public void RoleUpdate () {
        base.ObjUpdate();
        if (!gameObject.activeSelf) return;

        MoveUpdate();
        AddBulletUpdate();
    }

    public void MoveUpdate(){
        if (bullet == null) return;
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        //gamerect.x + Random.Range(0, gamerect.width - 2) + 1
        Rect gamerect = InGameManager.GetInstance().GetGameRect();
        if (moveSpeed > 0 && transform.position.x > gamerect.x + gamerect.width - 1)
        {
            moveSpeed *= -1;
        }
        else if (moveSpeed < 0 && transform.position.x < gamerect.x)
        {
            moveSpeed *= -1;
        }
    }

    public void AddBulletUpdate(){
        if (bullet != null) return;
        addBulletTime += Time.deltaTime;
        if (addBulletTime < addBulletMaxTime) return;

        addBulletTime = 0f;

        GameObject bulletObj = Resources.Load("Prefabs/MapObj/InGameBullet") as GameObject;
        bulletObj = Instantiate(bulletObj);
        InGameBullet b = bulletObj.GetComponent<InGameBullet>();
        bullet = b;
        b.transform.position = transform.position;
        b.transform.localScale = new Vector3(0.5f,0.5f,1f);
        InGameManager.GetInstance().inGameLevelManager.AddObj(b);
    }



    public override void Die(){

        AudioManager.Instance.Play("sound/die");
        // game over
        InGameManager.GetInstance().GameOver();
        combo = 0;
        gameObject.SetActive(false);
        //create efffect
        GameObject effect = Resources.Load("Prefabs/Effect/RoleDieEffect") as GameObject;
        effect = Instantiate(effect);
        effect.transform.position = transform.position;

    }

    public override void HandleEvent(EventData resp)
    {
        if(InGameManager.GetInstance().gameState != enGameState.playing){
            return;
        }
        if (bullet == null) return;
        switch (resp.eid)
        {
            case EventID.EVENT_TOUCH_DOWN:
                EventTouch eve = (EventTouch)resp;
                //TouchToPlane(eve.pos);
                //Fire(GameCommon.ScreenPositionToWorld(eve.pos));
                Vector3 pos = GameCommon.ScreenPositionToWorld(InGameManager.GetInstance().gamecamera,eve.pos);

                if(pos.y > InGameManager.GetInstance().inGameUIManager.gamePadManager.comboLabelPos.y - 1){
                    return;
                }

                bullet.Fire();
                AudioManager.Instance.Play("sound/btn");
                bullet = null;
                break;

        }

    }


    public void AddScores(int score,bool iscombo,InGameBaseObj source)
    {
        int s = score;
        //if(iscombo){
        //    combo++;
        //    scores += combo;
        //    s = combo;
        //}else {
        //    combo = 0;
        //    scores += score;
        //}
        scores += score;
        InGameManager.GetInstance().inGameUIManager.AddScores(source.transform.position, s, scores,iscombo, true);
        //InGameManager.GetInstance().inGameColorManager.SetColor((float)score * (iscombo ? 1f /* ((float)combo * 0.3f)*/ : 0.1f  ));

        //Vector3[] ps = {
        //    source.transform.position,
        //    new Vector3(0,source.transform.position.y + source.transform.position.x /*Random.Range(8f,15f)*/,0),
        //                InGameManager.GetInstance().inGameUIManager.gamePadManager.comboLabelPos -
        //                new Vector3(0,Random.Range(8f,15f),0),
        //                InGameManager.GetInstance().inGameUIManager.gamePadManager.comboLabelPos
        //            };
        //ComboFlyObj.Create(ps);
    }



    public void Revive(){
        gameObject.SetActive(true);

    }

    private void OnDestroy()
    {
        buffManager.Destroy();
        EventManager.Remove(this);
    }
}

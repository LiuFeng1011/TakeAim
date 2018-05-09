using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStepEffect : GameEffect {

    public override void Init(GameEffectData conf)
    {
        base.Init(conf);

        //BrokenEffect
        ParticleSystem chileps = transform.Find("BrokenEffect").GetComponent<ParticleSystem>();

        Material m = chileps.GetComponent<Renderer>().material;

        m.SetTexture("_MainTex",InGameManager.GetInstance().stepSpriteRes.texture);

        ParticleSystem circle = transform.Find("Particle System").GetComponent<ParticleSystem>();

        Material cm = circle.GetComponent<Renderer>().material;

        cm.SetTexture("_MainTex", InGameManager.GetInstance().stepSpriteRes.texture);


    }
	
}

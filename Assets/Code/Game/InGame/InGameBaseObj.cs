using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBaseObj : MSBaseObject {

    bool isdie = false;

    public override void ObjUpdate(){
    }

    public virtual void SetDie(){
        isdie = true;
    }

    public virtual bool IsDie()
    {
        return isdie;
    }

    public virtual void Die(){
        Destroy(gameObject);
    }

}

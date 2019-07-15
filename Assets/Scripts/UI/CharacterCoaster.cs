using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCoaster : MonoBehaviour
{

   public Sprite CharacterSprite
    {
        get { return this.GetComponent<SpriteRenderer>().sprite; }
        set
        {
            this.GetComponent<SpriteRenderer>().sprite = value ;
            this.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        }
    }

    public Vector3 CoasterLocation { set { transform.position = new Vector3(value.x, value.y , -0.5f); }  }


}

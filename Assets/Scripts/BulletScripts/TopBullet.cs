using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBullet : Bullet
{
    private float magicDamage = 20;
    private float armorDamage = 40;

    public float MagicDamage
    {
        get { return magicDamage; }
        set { magicDamage = value; }
    }

    public float ArmorDamage
    {
        get { return armorDamage; }
        set { armorDamage = value; }
    }
}

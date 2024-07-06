using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private int enemyCoinCount;
    private static int count = 0;

    public int EnemyCoinCount
    {
        get { return enemyCoinCount; }
        set { enemyCoinCount = value; }
    }
    public int Count
    {
        get { return count; }
        set { count = value; }
    }

}

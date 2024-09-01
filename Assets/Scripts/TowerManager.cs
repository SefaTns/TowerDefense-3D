using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{//seviye icerisinde tanumlanacak game manager ile birlikte kullanilacak 

    public GameObject[] AvaliableTower; //icersine eklenecek olan kulelerin herbirisi towerbase scriptine sahip olacak

    public void BuyTower(string towerName)
    {
        GameObject towerToBuy = System.Array.Find(AvaliableTower, x=> x.GetComponent<TowerBase>().TowerName == towerName);
        if (towerToBuy!= null)
        {
            TowerBase towerBase = towerToBuy.GetComponent<TowerBase>();
            if (GameManager.instance.SpendCoins(towerBase.Cost))
                PlaceTower(towerToBuy);
            else
              Debug.Log("yeterli coin yok.");
        }
        else
        {
          Debug.LogError("Kule bulunamadi" + towerName);
        }
    }
    
    private void PlaceTower(GameObject tower)
    {
        Vector3 placementPosition = GetPlacementPosition();
        //sahnede kuleyi gorunur yapip pozzisyon ayarliyoruz
        Instantiate(tower, placementPosition, Quaternion.identity);
    }

    private Vector3 GetPlacementPosition()
    {
        return new Vector3(0,0,0); // tiklanilan zeminin prefabi ile degistirilecek
    }

}

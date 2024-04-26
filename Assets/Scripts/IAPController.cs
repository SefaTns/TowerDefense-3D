using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;
using UnityEngine.UI;  
using TMPro;


public class IAPController : MonoBehaviour, IStoreListener
{

    IStoreController controller;
    public string[] product;
    public TextMeshProUGUI coinText;
    public bool delete = true;

    private void Start()
    {
        if (delete)
        {
            PlayerPrefs.DeleteAll();
        }
        if(PlayerPrefs.GetInt("RemoveAds")==1)
        {
            GameObject.Find("Ads").SetActive(false);
            GameObject.Find("RemoveAdsButton").GetComponent<Button>().interactable = false; 
        }
        IAPStart();
    }

    private void IAPStart()
    {
        var module = StandardPurchasingModule.Instance();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

        foreach(string item in product)
        {
            builder.AddProduct(item, ProductType.Consumable);
        }
        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Error :"+error.ToString());
    }
    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("Error :" + message);
    }
    //void OnInitializeFailed(InitializationFailureReason error)

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("Error whille buying"+p.ToString());
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        if (string.Equals(e.purchasedProduct.definition.id, product[0], StringComparison.Ordinal))
        {
            addCoin(100);
            return PurchaseProcessingResult.Complete;
        }
        else if(string.Equals(e.purchasedProduct.definition.id, product[1], StringComparison.Ordinal))
        {
            addCoin(500);
            return PurchaseProcessingResult.Complete;
        }
        else
        {
            return PurchaseProcessingResult.Pending;
        }
    }
    

    private void addCoin(int coin)
    {
        coinText.text = coin.ToString()+" satın alındı.";
    }
    
    private void RemoveAds()
    {
        PlayerPrefs.SetInt("RemoveAds",1);
        GameObject.Find("Ads").SetActive(false);
        GameObject.Find("RemoveAdsButton").GetComponent<Button>().interactable = false;
        Debug.Log("Ads removed");
    }

    public void IAPButton(string id)
    {
        Product product = controller.products.WithID(id);
        if (product != null && product.availableToPurchase)
        {
            Debug.Log("Buying product");
            controller.InitiatePurchase(product); 
        }
        else
        {
            Debug.Log("Product not found");
        }
    }
}

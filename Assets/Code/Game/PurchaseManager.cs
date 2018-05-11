using UnityEngine.Purchasing;
using UnityEngine;
using System;

public class PurchaseManager : MonoBehaviour, IStoreListener

{
    static PurchaseManager instance;
    //public BuyManager buyManager;

    private IStoreController controller;
    private IExtensionProvider m_StoreExtensionProvider;


    public static PurchaseManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        instance = this;

#if IAP
        var module = StandardPurchasingModule.Instance();

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

        builder.AddProduct("TouchColorRemoveADS", ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
#endif

    }



    /// <summary>

    /// Called when Unity IAP is ready to make purchases.

    /// </summary>

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)

    {

        this.controller = controller;
        m_StoreExtensionProvider = extensions;

    }



    /// <summary>

    /// Called when Unity IAP encounters an unrecoverable initialization error.

    ///

    /// Note that this will not be called if Internet is unavailable; Unity IAP

    /// will attempt initialization until it becomes available.

    /// </summary>

    public void OnInitializeFailed(InitializationFailureReason error)

    {

    }



    /// <summary>

    /// Called when a purchase completes.

    ///

    /// May be called at any time after OnInitialized().

    /// </summary>

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        PlayerPrefs.SetInt("noad", 1);
        return PurchaseProcessingResult.Complete;

    }



    /// <summary>

    /// Called when a purchase fails.

    /// </summary>

    public void OnPurchaseFailed(Product item, PurchaseFailureReason r)

    {

    }


    public void DoIapPurchase(Action<bool, string> callback)
    {
        if (controller != null)
        {
            var product = controller.products.WithID("TouchColorRemoveADS");
            if (product != null && product.availableToPurchase)
            {
                //调起支付  
                controller.InitiatePurchase(product);
            }
            else
            {
                callback(false, "no available product");
            }
        }
        else
        {
            callback(false, "m_Controller is null");
        }
    }

    public void RestorePurchases()
    {

#if IAP
        if (Application.platform == RuntimePlatform.IPhonePlayer ||   
            Application.platform == RuntimePlatform.OSXPlayer)  
        {  
            Debug.Log("RestorePurchases started ...");  
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();  
            apple.RestoreTransactions((result) => {  
                //返回一个bool值，如果成功，则会多次调用支付回调，然后根据支付回调中的参数得到商品id，最后做处理(ProcessPurchase)  
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");  
            });  
        }  
        else  
        {  
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);  
        }  
#endif
    }  
}
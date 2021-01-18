using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
using UnityEngine.Purchasing;

public class ShopItems : MonoBehaviour, IStoreListener
{
    public static ShopItems Instance {get; set;}

    //The Unity Purchasing system.
    private static IStoreController m_StoreController;
    // The store-specific Purchasing subsystems.
    private static IExtensionProvider m_StoreExtensionProvider;

    //Create your products..
    private string removeBannerAds = "Remove Banner Ads";
    private string removeVideoAds = "Remove Videos Ads";
    private string removeAllAds = "Remove All Ads";
    private string coins100 = "Coins 100";
    private string coins150 = "Coins 150";
    private string coins200 = "Coins 200";


    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized()) { return;}
        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(removeAllAds, ProductType.NonConsumable);
        builder.AddProduct(removeBannerAds, ProductType.NonConsumable);
        builder.AddProduct(removeVideoAds, ProductType.NonConsumable);
        builder.AddProduct(coins100, ProductType.Consumable);
        builder.AddProduct(coins150, ProductType.Consumable);
        builder.AddProduct(coins200, ProductType.Consumable);

        
        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs args)
    {
        //En las comillas pones el nombre del consumable item que quieres verificar.. 
        if(String.Equals(args.purchasedProduct.definition.id, coins100, StringComparison.Ordinal))
        {
            //Here you code for removing the ads.. 
            Debug.Log("Give the player some 100 coins");
        }

        else if (String.Equals(args.purchasedProduct.definition.id, removeBannerAds, StringComparison.Ordinal))
        {
            Debug.Log("Remove banner Ads has been purchased, remove them now..");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, removeVideoAds, StringComparison.Ordinal))
        {
            Debug.Log("Remove Vides Ads from the game..");
        }

        else if (String.Equals(args.purchasedProduct.definition.id, removeAllAds, StringComparison.Ordinal))
        {
            Debug.Log("Remove all Ads from the game..");
        }

       
        else if (String.Equals(args.purchasedProduct.definition.id, coins150, StringComparison.Ordinal))
        {
            Debug.Log("Coins 150 has been bought");
        }
        else if(String.Equals(args.purchasedProduct.definition.id, coins200, StringComparison.Ordinal))
        {
            Debug.Log("Coins for 200 has been bought");
        }
        else
        {
            Debug.Log("Purchased failed..");
        }

        return PurchaseProcessingResult.Complete;
    }

    public void BuyRemoveAllAds()
    {
        BuyProductID(removeAllAds);
    }

    public void BuyRemoveBannerAds()
    {
        BuyProductID(removeBannerAds);
    }

    public void BuyRemoveVideoAds()
    {
        BuyProductID(removeVideoAds);
    }

    public void BuyCoins100()
    {
        BuyProductID(coins100);
    }

    public void BuyCoins150()
    {
        BuyProductID(coins150);
    }

    public void BuyCoins200()
    {
        BuyProductID(coins200);
    }


    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }



    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
*/


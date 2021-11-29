using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRewardView : MonoBehaviour
{
   [SerializeField] private Image rewardImage;
   [SerializeField] private TextMeshProUGUI rewardValue;
   [SerializeField] private TextMeshProUGUI rewardDay;
   [SerializeField] private GameObject rewardShadow;
   [SerializeField] private GameObject rewardGrab;

   public int RewardValue
   {
      get => Convert.ToInt32(rewardValue.text);
      set => rewardValue.text = value.ToString();
   }

   public EItemType RewardImage
   {
      get
      {
         switch (rewardImage.sprite.name)
         {
            case "coin":
               return EItemType.Coin;
            case "jetpack":
               return EItemType.JetPack;
            default:
               return EItemType.Coin;
         }
      }
      set
      {
         switch (value)
         {
            case EItemType.Coin:
               rewardImage.sprite = Resources.Load<Sprite>("sprites/items/coin");
               break;
            case EItemType.JetPack:
               rewardImage.sprite = Resources.Load<Sprite>("sprites/items/jetpack");
               break;
         }
      }
   }

   public int RewardDay
   {
      get => Convert.ToInt32(rewardDay.text);
      set => rewardDay.text = $"{value} DAY";
   }

   public bool RewardActive
   {
      get => rewardShadow.gameObject.activeSelf;
      set => rewardShadow.SetActive(!value);
   }
   public bool GrabActive
   {
      get => rewardGrab.gameObject.activeSelf;
      set => rewardGrab.SetActive(value);
   }

   public GameObject RewardGrab => rewardGrab;
}

using UnityEngine;

public class UIManagerCricket : MonoBehaviour
{
   public static UIManagerCricket Instance;

   private void Awake()
   {
      if (Instance == null)
         Instance = this;
   }

   public void BowlingButtonPressed()
   {
      AIBowler.Instance.Bowl();
   }
}

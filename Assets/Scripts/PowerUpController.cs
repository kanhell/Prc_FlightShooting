using UnityEngine;

public class PowerUpController : ItemController
{
    PlayerController playerController;

    protected override void ItemGain()
    {
         playerController = base.player.GetComponent<PlayerController>();
        if(playerController.Damage < 3)
        {
            playerController.Damage++;
        }
        if (playerController.Damage >= 3)
        {
            UIManager.instance.ScoreAdd(base.score);
        }
    }
}

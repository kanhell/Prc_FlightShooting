using UnityEngine;

public class BoomController : ItemController
{
    PlayerController playerController;

    protected override void ItemGain()
    {
        playerController = base.player.GetComponent<PlayerController>();
        if(playerController.Boom < 4)
        {
            playerController.Boom++;
            UIManager.instance.BoomCheck(playerController.Boom);  // boom 갯수만큼 ui_Booms 활성화하기
        }
        if (playerController.Boom >= 4)
        {
            UIManager.instance.ScoreAdd(base.score);
        }
    }
}

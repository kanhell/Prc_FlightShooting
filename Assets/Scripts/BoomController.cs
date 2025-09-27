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
            UIManager.instance.BoomCheck(playerController.Boom);  // boom ������ŭ ui_Booms Ȱ��ȭ�ϱ�
        }
        if (playerController.Boom >= 4)
        {
            UIManager.instance.ScoreAdd(base.score);
        }
    }
}

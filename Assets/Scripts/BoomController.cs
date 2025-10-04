using UnityEngine;

public class BoomController : ItemController
{
    PlayerController playerController;

    protected override void ItemGain()
    {
        base.ItemGain();

        playerController = base.player.GetComponent<PlayerController>();
        if(playerController.Boom < 3)
        {
            playerController.Boom++;
            UIManager.instance.BoomCheck(playerController.Boom);  // boom ������ŭ ui_Booms Ȱ��ȭ�ϱ�
        }
        if (playerController.Boom >= 3)
        {
            UIManager.instance.ScoreAdd(base.score);
        }
    }
}

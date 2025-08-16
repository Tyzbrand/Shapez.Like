using UnityEngine;

public class DayCounter : MonoBehaviour
{
    private PlayerVariables player;
    private float counter;
    private OverlaySC overlay;



    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        overlay = ReferenceHolder.instance.inGameOverlay;
    }


    private void Update()
    {
        counter += Time.deltaTime;

        if (counter >= 1f)
        {
            player.seconds++;
            counter -= 1f;
            overlay.UpdateTimeText();
        }

        if (player.seconds >= 60)
        {
            player.minutes++;
            player.seconds -= 60;
            overlay.UpdateTimeText();

        }

        if (player.minutes >= 1 && player.seconds >= 30)
        {
            player.day++;
            player.seconds = 0;
            player.minutes = 0;
            overlay.UpdateDayText();
        }
        
    }


}


using PathFinder.Scripts.GameObject.GUI;
using PathFinder;

public class DialogueBoxManager
{
    public static void UpdateDialogueBoxMessages(Player player, DialogueBox dialogueBox, string currentLocation, bool hasKey1, bool hasKey2, bool visitedTower, bool visitedChest)
    {
        switch (currentLocation)
        {
            case "Spawning":
                dialogueBox.UpdateScenario("Erik is drunk again and has accidentally teleported himself away from home. Help him get home safely");
                break;
            case "CollectBeerInTower":
                if (visitedTower && !hasKey1 && !hasKey2)
                {
                    dialogueBox.UpdateScenario("Erik has picked up another Beer, help him find a dumpster for the empty can");
                }
                else if (visitedTower && hasKey1 && hasKey2)
                {
                    dialogueBox.UpdateScenario("Erik has made it home sound and safe once again");
                }
                else
                {
                    dialogueBox.UpdateScenario("Erik can't get in without his keys");
                }
                break;
            case "Chest":
                dialogueBox.UpdateScenario("uhm,I guess that works too");
                break;
            case " PortalSecondTime ":
                dialogueBox.UpdateScenario("Erik is once again wandering around aimlessly. It looks like he lost his keys");
                break;
            case "PickUpKey1":
                dialogueBox.UpdateScenario("Erik has found a key. I bet he managed to lose his spare key too though");
                break;
            case "PickUpKey2":
                dialogueBox.UpdateScenario("It looks like Erik has somehow managed to retrieve his spare key aswell");
                break;
            default:               
                break;
        }
    }
}

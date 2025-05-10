using UnityEngine;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public class BabySystem : MonoBehaviour
{

    public SoundFXManager SoundFXManager;

    Queue<Item> NextMainItems = new Queue<Item>(); // Liste der gewünschten Main Items 
                                                    // enqueued bei level Start und wenn item erfolgreich abgearbeitet
                                                    // dequeued wenn item erfolgreich abgearbeitet

    Queue<Item> FinishedItems = new Queue<Item>(); // Liste der fertigen Items
                                                    // enqueued wenn item erfolgreich abgearbeitet
                                                    // dequeued bei baby Destruction

    public Item currentMainItem;
    public Item currentHappinessItem;

    // Item Variablen
    List<Item> MainItemsList = new List<Item>(); // Alle gewünschten Main items
    List<Item> PossibleHappinessItemsList = new List<Item>();

    // Happiness Variablen
    float happinessValf = 100f;
    public bool isHappy = true;
    float baseHappinessTimerf = 10f; // 10 sec
    float happinessTimerf = 10f; // current timer für den Timer loop

    int itemDestructionInterval = 3; // 3 sec

    void Start(){
        // fill NextMainItems
        foreach(Item cI in MainItemsList){
            NextMainItems.Enqueue(cI);
        }
        SetupNextMainItem();

        // StartBabyHappinessLoop
    }

    public Item GetCurrentMainItem(){
        return currentMainItem;
    }

    public Item GetCurrentHappinessItem(){
        if(!isHappy){
            Debug.Log("ERROR! Sollte nicht nach happiness Item fragen, wenn baby nicht unhappy ist!");
        }
        return currentHappinessItem;
    }

    private async void HappinessTimerLoop(){
        // hide Happiness item
        isHappy = true;
        happinessTimerf = baseHappinessTimerf;

        while(happinessTimerf > 0){
            await Task.Delay(1000);
            happinessTimerf -= 1f;
        }

        // Timer abgelaufen Init Rage Mode
        isHappy = false;
        RollHappinessItem();
        PlayBabyCrySound();
        StartBabyCryAnimation();
        ItemDestructionTimerLoop();
        // start item Destruction
    }

    private void RollHappinessItem(){
        System.Random rnd = new System.Random();
        int index = rnd.Next(1, PossibleHappinessItemsList.Count);
        currentHappinessItem = PossibleHappinessItemsList[index];
    }

    private void PlayBabyCrySound(){
        // SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.babyCrySound, transform, 1.0f);
    }

    private void StartBabyCryAnimation(){
        Debug.Log("ToDo: Baby Cry Animated");
    }

    private async void ItemDestructionTimerLoop(){
        isHappy = false;

        while(happinessValf >= 0 && !isHappy){
            await Task.Delay(itemDestructionInterval * 1000);
            happinessValf -= 1f;
            DestroyFinishedItem();
        }

        if(happinessValf > 0){
            // enter KALM
            PlayBabyHappySound();
            StartBabySleepAnimation();
            HappinessTimerLoop();
        }else {
            GameOver();
        }
    }

    private void PlayDestroySound(){
        // SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.destroySound, transform, 1.0f);
    }

    private void DestroyFinishedItem(){
        Debug.Log("Todo: Destroy an Item");
        Item lostItem = FinishedItems.Dequeue();
        NextMainItems.Enqueue(lostItem);
    }

    private void PlayBabyHappySound(){
        // SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.babyHappySound, transform, 1.0f);
    }

    private void StartBabySleepAnimation(){
        Debug.Log("ToDo: Baby Sleep Animation");
    }

    private void GameOver(){
        Debug.Log("ToDo: Load GameOver Screen");
    }

    private void PlayGameOverSound(){
        // SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.gameOverSound, transform, 1.0f);
    }

    public Item DeliverItem(Item deliveredItem){

        if(isHappy && deliveredItem == currentMainItem){
            // correct Main Item Delivered

            PlayCorrectItemDeliveredSound();
            FinishedItems.Enqueue(currentMainItem);
            NextMainItems.Enqueue(currentMainItem);

            if(FinishedItems.Count >= MainItemsList.Count){
                PlayWonSound();
                LevelWon();
                return null;
            }

            SetupNextMainItem();

            return null;
        }else if(!isHappy && deliveredItem == currentHappinessItem){
            // correct Happiness Item
            isHappy = true;

            return null;
        }else {
            // incorrect item
            return deliveredItem;
        }
    }

    private void PlayCorrectItemDeliveredSound(){
        // SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.correctItemDeliveredSound, transform, 1.0f);
    }

    private void SetupNextMainItem(){
        currentMainItem = NextMainItems.Dequeue();
    }

    private void LevelWon(){
        Debug.Log("ToDo: Level Won");
    }

    private void PlayWonSound(){
        // SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.wonSound, transform, 1.0f);
    }
}

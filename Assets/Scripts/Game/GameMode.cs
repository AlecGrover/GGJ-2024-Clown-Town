using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct SpawnDay
{
    public int Day;
    public List<Character> Characters;
}

public class GameMode : MonoBehaviour
{
    [SerializeField]
    private List<Card> AllCards;
    
    private InGamePopUpMenu inGameMenu;
    private MenuHandler menuHandler;

    [SerializeField][Range(15, 600)]
    private float secondsPerDay = 180f;
    [SerializeField]
    private TextMeshProUGUI secondsText;
    
    private float secondsElapsed = 0f;
    
    [SerializeField]
    private CardHolder player;
    
    private bool isGameOver = false;
    private bool isGameStarted = false;
    
    [Header("NPC Spawning")]
    public GameObject npcPrefab;
    public List<Character> RandomSpawnCharacters;
    public List<Character> ActiveCharacters;
    public List<SpawnDay> DaySpawns;
    public Vector3 SpawnPosition = Vector3.zero;
    public float SpawnRadius = 10f;

    // Start is called before the first frame update
    void Start()
    {
        inGameMenu = FindObjectOfType<InGamePopUpMenu>();
        menuHandler = FindObjectOfType<MenuHandler>();
        if (secondsText != null) secondsText.text = Mathf.FloorToInt(secondsPerDay).ToString();
        GamePersistence persistence = FindObjectOfType<GamePersistence>();
        if (persistence != null) persistence.Load();
        StartGame();
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isGameStarted) return;
        secondsElapsed += Time.fixedDeltaTime;
        if (secondsText != null) secondsText.text = Mathf.FloorToInt(Mathf.Max(secondsPerDay - secondsElapsed, 0)).ToString();
        if (secondsElapsed >= secondsPerDay)
        {
            isGameStarted = false;
            isGameOver = true;
            if (inGameMenu != null)
            {
                inGameMenu.WinGame();
            }
        }
    }

    public void LoseGame()
    {
        if (!isGameOver)
        {
            if (inGameMenu!= null) inGameMenu.LoseGame();
            isGameOver = true;
            isGameStarted = false;
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        GamePersistence persistence = FindObjectOfType<GamePersistence>();
        if (persistence != null)
        {
            foreach (var liveCharacter in persistence.ActiveNPCs)
            {
                SpawnCharacter(liveCharacter);
            }
        }

        if (inGameMenu != null)
        {
            Debug.Log("Starting Spawns");
            List<SpawnDay> spawnDays = DaySpawns.Where(SD => SD.Day == inGameMenu.GetDayNumber()).Select(sd => sd).ToList();
            if (spawnDays.Count > 0)
            {
                foreach (var spawnDay in spawnDays)
                {
                    Debug.Log("Spawning Day " + spawnDay.Day);
                    foreach (Character character in spawnDay.Characters)
                    {
                        SpawnCharacter(character);
                        Debug.Log("Spawned " + character.name);
                    }
                }
            }
            else
            {
                Debug.Log("No Spawn Set for Day " + inGameMenu.GetDayNumber());
                Debug.Log("Random Spawning");
                int RandIndex = Random.Range(0, RandomSpawnCharacters.Count);
                if (RandomSpawnCharacters.Count > 0)
                {
                    SpawnCharacter(RandomSpawnCharacters[RandIndex]);
                }
            }
        }
        
    }

    public void GetPersistenceData(out int DayNumber, out int Gold, out List<Character> ActiveNPCs)
    {
        if (menuHandler != null)
        {
            DayNumber = menuHandler.GetDay();
            Gold = menuHandler.GetMoney();
        }
        else
        {
            DayNumber = 0;
            Gold = 0;
        }

        ActiveNPCs = ActiveCharacters;
    }

    public void SetPersistenceData(int DayNumber, int Gold)
    {
        if (menuHandler != null)
        {
            menuHandler.SetDay(DayNumber);
            menuHandler.SetMoney(Gold);
            Debug.Log("Day: " + DayNumber + " Gold: " + Gold);
        }
        Debug.Log("Attempted to save Day: " + DayNumber + " Gold: " + Gold + "");
    }

    public void SpawnCharacter(Character newCharacter)
    {
        Debug.Log("Spawning " + newCharacter.name);
        Vector3 randomPosition = Random.insideUnitSphere * SpawnRadius + SpawnPosition;
        randomPosition.y = SpawnPosition.y;
        GameObject npc = Instantiate(npcPrefab, randomPosition, Quaternion.identity);
        if (npc != null && npc.GetComponent<NPC>() != null)
        {
            npc.GetComponent<NPC>().SetCharacter(newCharacter);
        }
    }

    public Card GetRandomCard()
    {
        int index = Random.Range(0, AllCards.Count);
        if (index < AllCards.Count)
        {
            return AllCards[index];
        }

        return null;
    }

    public void AddCardToPlayer(Card cardData)
    {
        if (player != null)
        {
            player.AddToDeck(cardData);
        }
    }
}

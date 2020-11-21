using System;
using System.Collections.Generic;
using System.Linq;

public class GameStatsService
{
    public delegate void ChangeSelectedCharacterEvent(Character character);
    public event ChangeSelectedCharacterEvent onChangeSelectedCharacter;

    private static GameStatsService _serviceInstance;
    private static ExpeditionStateManager _expeditionStateManager;
    private ICollection<Character> _characters;
    private Character _selectedCharacter;
    public GameStats gameStats;
    private List<Item> _expeditionPreparedInventory;

    public ICollection<Character> characters
    {
        get
        {
            return _characters;
        }
    }
    public Character selectedCharacter { 
        get
        {
            return _selectedCharacter;
        }
        set {
            _selectedCharacter = value;
            onChangeSelectedCharacter?.Invoke(_selectedCharacter);
        } 
    }

    public static GameStatsService Instance
    {
        get
        {
            if (_serviceInstance == null)
            {
                _serviceInstance = new GameStatsService();
            }
            return _serviceInstance;
        }
    }

    public static ExpeditionStateManager SceneStateManager
    {
        get
        {
            if (_expeditionStateManager == null)
            {
                _expeditionStateManager = new ExpeditionStateManager();
            }
            return _expeditionStateManager;
        }
    }

    public Character GetCharacterById(int id)
    {
        if (_characters == null)
        {
            throw new ArgumentNullException("Character list was null.");
        }
        return _characters.FirstOrDefault(c => c.id == id);
    }

    public void CompleteExpedition(Item[] items)
    {
        if (items != null)
        {
            // TODO handle other item types
            items.Where(x => x is Ration).ToList().ForEach(item =>
            {
                gameStats.AddItem(item as Ration);
            });
        }
        gameStats.expeditionComplete = true;
    }

    public void SetStartData(ICollection<Character> characters, GameStats gameStats)
    {
        _characters = characters;
        this.gameStats = gameStats;
    }

    public List<Item> GetPreparedInventory()
    {
        _expeditionPreparedInventory = null;
        return _expeditionPreparedInventory;
    }

    public void SetPreparedInventory(List<Item> inventory)
    {
        _expeditionPreparedInventory = inventory;
    }
}

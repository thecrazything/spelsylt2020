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
    private List<Item> _tmpExpeditionCompleteInv;

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
        _tmpExpeditionCompleteInv = new List<Item>();
        if (items != null)
        {
            _tmpExpeditionCompleteInv.AddRange(items);
        }
        gameStats.expeditionComplete = true;
    }

    public void MoveTmpExpiditionInvToHub()
    {
        if (_tmpExpeditionCompleteInv != null)
        {
            gameStats.AddItems(_tmpExpeditionCompleteInv.ToArray());
        }
        _tmpExpeditionCompleteInv = null;
    }

    public Item[] GetTmpInventory()
    {
        if (_tmpExpeditionCompleteInv == null) {
            return new Item[0];
        }

        return _tmpExpeditionCompleteInv.ToArray();
    }

    public void SetStartData(ICollection<Character> characters, GameStats gameStats)
    {
        _characters = characters;
        this.gameStats = gameStats;
    }

    public List<Item> GetPreparedInventory()
    {
        List<Item> temp = _expeditionPreparedInventory;
        _expeditionPreparedInventory = null;
        return temp;
    }

    public void SetPreparedInventory(List<Item> inventory)
    {
        _expeditionPreparedInventory = inventory;
    }
}

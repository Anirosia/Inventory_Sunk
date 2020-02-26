using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Singleton

    public static InventoryManager _instance;

    void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public List<Item> items = new List<Item>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int space = 10;

    public bool inInteractArea;//For TextDisplay

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    
    public bool AddItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(item);

            if(onItemChangedCallBack != null) onItemChangedCallBack.Invoke();

           inInteractArea = false;
        }

        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallBack != null) onItemChangedCallBack.Invoke();
    }
}

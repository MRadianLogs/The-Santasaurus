using System.Collections.Generic;
using UnityEngine;

public class ItemSpriteManager : MonoBehaviour
{
    public static ItemSpriteManager instance;

    [SerializeField] private List<Sprite> spriteImports = null;
    private Dictionary<int, Sprite> itemSprites = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists! Destroying object!");
            Destroy(transform.root.gameObject);
        }

        itemSprites = new Dictionary<int, Sprite>();
        for(int i = 0; i < spriteImports.Count; i++)
        {
            itemSprites.Add(i+1, spriteImports[i]);
        }
    }

    public Dictionary<int, Sprite> GetItemSpritesList()
    {
        return itemSprites;
    }

    public Sprite GetSprite(int spriteNum)
    {
        return itemSprites[spriteNum];
    }
}

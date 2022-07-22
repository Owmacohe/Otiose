using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    Transform leftObjects, rightObjects, leftItems, rightItems, headBone;
    [SerializeField]
    string[] types;
    [SerializeField]
    TextAsset[] descriptions;

    TMP_Text[] texts;
    ItemType[] itemTypes;
    PlayerStats stats;

    class ItemType
    {
        public string typeName;
        public GameObject[] items;
        public string[] names;
        public string[] descriptions;
        
        public ItemType(string typeName, GameObject[] items, string[] names, string[] descriptions)
        {
            this.typeName = typeName;
            this.items = items;
            this.names = names;
            this.descriptions = descriptions;
        }
    }
    
    void Start()
    {
        texts = GetComponentsInChildren<TMP_Text>();
        texts[0].text = "";
        texts[1].text = "";

        stats = FindObjectOfType<PlayerStats>();

        itemTypes = new ItemType[types.Length];

        for (int i = 0; i < types.Length; i++)
        {
            GameObject[] temp = Resources.LoadAll<GameObject>(types[i]);
            
            itemTypes[i] = new ItemType(
                types[i].ToLower().Substring(0, types[i].Length - 1),
                temp, 
                new string[temp.Length], 
                descriptions[i].text.Split('\n')
            );
            
            AddItems(itemTypes[i]);   
        }
    }

    void AddItems(ItemType type)
    {
        GameObject itemObject = Resources.Load<GameObject>("Item");
        Transform currentParent = leftItems;
        Transform currentObjectParent = leftObjects;

        for (int i = 0; i < type.items.Length; i++)
        {
            if (i == type.items.Length / 2)
            {
                currentParent = rightItems;
                currentObjectParent = rightObjects;
            }
            
            type.names[i] = type.items[i].name + " " + type.typeName;

            GameObject temp = Instantiate(itemObject, currentParent);
            temp.GetComponentInChildren<TMP_Text>().text = type.names[i];

            int selectionIndex = i;
            temp.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectItem(type, selectionIndex); });
            
            Instantiate(
                type.items[i], 
                Instantiate(
                    Resources.Load<GameObject>("Object"),
                    currentObjectParent
                ).transform.GetChild(0)
            ).AddComponent<ItemRotator>();
        }
    }

    void SelectItem(ItemType type, int index)
    {
        texts[0].text = type.names[index];
        texts[1].text = type.descriptions[index];

        if (headBone.transform.childCount > 0)
        {
            Destroy(headBone.GetChild(0).gameObject);    
        }

        Transform temp = Instantiate(type.items[index], headBone).transform;
        temp.Rotate(Vector3.up, 180);
        temp.localPosition += Vector3.down;
        
        if (type.typeName.Equals("hat"))
        {
            stats.hatObject = type.items[index];
        }
    }
}
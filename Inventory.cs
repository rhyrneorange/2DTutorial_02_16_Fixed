using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject m_itemSlotPrefab;
    [SerializeField] GameObject m_itemPrefab;
    [SerializeField] UIGrid m_itemSlotGrid;
    [SerializeField] UISprite m_cursorSpr;
    List<ItemSlot> m_itemSlotList = new List<ItemSlot>();
    int m_maxSlot = 24;
    [SerializeField] Sprite[] m_iconSprites;
    [SerializeField] List<ItemData> m_itemDataList = new List<ItemData>();
    Dictionary<ItemType, ItemData> m_itemTable = new Dictionary<ItemType, ItemData>();
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }
    public void HideUI()
    {
        gameObject.SetActive(false);
    }
    public void CreateItem()
    {
        for (int i = 0; i < m_itemSlotList.Count; i++)
        {
            if (m_itemSlotList[i].IsEmpty)
            {
                var item = Instantiate(m_itemPrefab).GetComponent<Item>();
                var type = (ItemType)Random.Range((int)ItemType.Ball, (int)ItemType.Max);
                var data = GetItemData(type);
                ItemInfo itemInfo = new ItemInfo() { itemData = data, count = Random.Range(1, 99) };
                item.SetItem(itemInfo, GetItemIcon(type));
                m_itemSlotList[i].SetSlot(item);
                break;
            }
        }
    }
    public void OnSelectSlot(ItemSlot slot)
    {
        for (int i = 0; i < m_itemSlotList.Count; i++)
        {
            if (m_itemSlotList[i].m_isSelected)
            {
                m_itemSlotList[i].m_isSelected = false;
                break;
            }
        }
        slot.m_isSelected = true;
        if (!m_cursorSpr.enabled)
            m_cursorSpr.enabled = true;
        m_cursorSpr.transform.position = slot.transform.position;
    }
    Sprite GetItemIcon(ItemType type)
    {
        return m_iconSprites[(int)type];
    }
    void SetInventroey()
    {
        for (int i = 0; i < m_maxSlot; i++)
        {
            CreateSlot();
        }
    }
    void CreateSlot()
    {
        var obj = Instantiate(m_itemSlotPrefab);
        obj.transform.SetParent(m_itemSlotGrid.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.localRotation = Quaternion.identity;
        var slot = obj.GetComponent<ItemSlot>();
        slot.InitSlot(this);
        m_itemSlotList.Add(slot);
    }
    public void OnPressItemUse()
    {
        var find = m_itemSlotList.Find(slot => slot.m_isSelected);
        if (find != null)
        {
            find.UseItem();
        }
    }
    void InitItemTable()
    {
        for (int i = 0; i < m_itemDataList.Count; i++)
        {
            m_itemTable.Add(m_itemDataList[i].type, m_itemDataList[i]);
        }
    }
    ItemData GetItemData(ItemType type)
    {
        return m_itemTable[type];
    }

    // Start is called before the first frame update
    void Start()
    {
        SetInventroey();
        InitItemTable();
        m_cursorSpr.enabled = false;
        HideUI();
    }
}

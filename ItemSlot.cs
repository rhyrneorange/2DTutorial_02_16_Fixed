using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    Item m_item;
    Inventory m_inventory;
    public bool m_isSelected;
    public bool IsEmpty { get { return m_item == null; } }
    public void InitSlot(Inventory inventory)
    {
        m_inventory = inventory;
    }
    public void SetSlot(Item item)
    {
        m_item = item;
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.transform.localScale = Vector3.one;
    }
    public void OnSelect()
    {
        m_inventory.OnSelectSlot(this);
        m_isSelected = true;
    }
    public void UseItem()
    {
        if (IsEmpty) return;
        m_item.OnUse();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
}
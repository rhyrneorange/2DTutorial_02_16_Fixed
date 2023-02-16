using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] UI2DSprite m_icon;
    [SerializeField] UILabel m_countLabel;
    ItemInfo m_itemInfo;

    public void SetItem(ItemInfo itemInfo, Sprite icon)
    {
        m_itemInfo = itemInfo;
        m_countLabel.text = m_itemInfo.count.ToString();
        m_icon.sprite2D = icon;
    }
    public void OnUse()
    {
        m_itemInfo.count--;
        m_countLabel.text = m_itemInfo.count.ToString();
        if (m_itemInfo.count <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
}
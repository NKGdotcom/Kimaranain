using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがアイテムを取得しているかどうかを調べる処理
/// </summary>
public class PlayerItem : MonoBehaviour
{
    //コンポーネント参照
    //現在持っているアイテム
    private List<IItem> haveItemsList = new List<IItem>();
    public List<IItem> HaveItemsList => haveItemsList;

    public void GetItem()
    {

    }
}

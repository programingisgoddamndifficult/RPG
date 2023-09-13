using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();//继承自虚函数

        PickUp();
    }
    void PickUp()//物品放入仓库
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickUp = Inventory.instance.Add(item);
        if (wasPickUp) 
        { 
            Destroy(gameObject); 
        }
        
    }
}

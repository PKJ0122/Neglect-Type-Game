using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySwap : MonoBehaviour
{
    InventoryManager iM;

    public int soltId;
    // Start is called before the first frame update
    void Start()
    {
        iM = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        if (!iM.Swords[soltId].swordOn)
            return;
        if (iM.swordInfoWindow.activeSelf)
        {
            if (InventoryManager.clickId == soltId)
            {
                InventoryManager.clickId = -1;
                iM.swordInfoWindow.SetActive(false);
                return;
            }
        }
        InventoryManager.clickId = soltId;
        iM.SetSwordInfo();
    }
    public void OnMouseDrag()
    {
        if (!iM.Swords[soltId].swordOn)
            return;

        iM.mousePoint.GetComponent<SpriteRenderer>().sprite = iM.Swords[soltId].swordImg[iM.Swords[soltId].grade];
        InventoryManager.pick = true;
        InventoryManager.picksoltId = soltId;
        iM.Slots[soltId].SetActive(false);
        iM.mousePoint.SetActive(true);
    }
    public void OnMouseUp()
    {
        if (!InventoryManager.pick)
            return;

        iM.mousePoint.SetActive(false);
        (iM.Swords[soltId].grade, iM.Swords[InventoryManager.picksoltId].grade) = (iM.Swords[InventoryManager.picksoltId].grade,iM.Swords[soltId].grade);
        (iM.Swords[soltId].playerHandSword, iM.Swords[InventoryManager.picksoltId].playerHandSword) = (iM.Swords[InventoryManager.picksoltId].playerHandSword, iM.Swords[soltId].playerHandSword);
        (iM.Swords[soltId].swordOn, iM.Swords[InventoryManager.picksoltId].swordOn) = (iM.Swords[InventoryManager.picksoltId].swordOn, iM.Swords[soltId].swordOn);
        iM.SetSlot();
        InventoryManager.clickId = soltId;
        if (iM.swordInfoWindow.activeSelf)
            iM.SetSwordInfo();
    }
}

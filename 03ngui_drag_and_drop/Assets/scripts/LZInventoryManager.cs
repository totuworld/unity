using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LZInventoryManager : MonoBehaviour {

	public UIAtlas atlas;
	public List<LZItemSlot> equipSlots;
	public List<string> itemSpriteNames;

	void Start()
	{
		Invoke("SetupEquiptSlot", 1.0f);
	}

	void SetupEquiptSlot()
	{
		for(int i=0; i<3;++i)
		{
			LZItemData nItem = new LZItemData();
			nItem.spriteName = itemSpriteNames[i];

			equipSlots[i].SetSlot(nItem);
		}
	}
}

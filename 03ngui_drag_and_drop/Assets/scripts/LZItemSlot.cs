using UnityEngine;
using System.Collections;

public class LZItemSlot : MonoBehaviour {

	UISprite icon;

	LZItemData mItem; //current item

	static LZItemData mDraggedItem; // now dragged item

	/// <summary>
	/// Set slot icon
	/// </summary>
	public void SetSlot(LZItemData itemData=null)
	{
		mItem = itemData;

		if(icon == null)
		{
			icon = transform.GetChild(0).GetComponent<UISprite>();
		}

		if(itemData == null)
		{
			icon.enabled = false;
		}
		else
		{
			icon.enabled = true;
			icon.spriteName = mItem.spriteName;
		}
	}

	public void OnClick()
	{
#if UNITY_EDITOR || UNITY_WEBPLAYER
		Calculate();   
#endif
	}

#if (UNITY_ANDROID||UNITY_IPHONE) && !UNITY_EDITOR
	void OnPress(bool isDown)
	{
		if(isDown)
			Calculate();
	}
	
	void OnDrop (GameObject go)
	{
		Calculate();
	}
#endif

	void Calculate()
	{
		if( mDraggedItem != null )
		{
			// cursor - exist / slot - none		==> drop(equipt)
			if(mItem == null)
			{
				
				mItem = mDraggedItem;
				mDraggedItem = null;
				
				SetSlot(mItem);
				UpdateCursor();
			}
			// cursor - exist / slot - exist	==> replace
			else
			{
				LZItemData tempItem = mDraggedItem;
				mDraggedItem = mItem;
				mItem = tempItem;
				
				SetSlot(mItem);
				UpdateCursor();
			}
		}
		// cursor - none / slot - exist		==> pickup
		else if(mItem != null)
		{
			mDraggedItem = mItem;
			mItem = null;
			
			SetSlot();
			UpdateCursor();
		}
	}

	void UpdateCursor ()
	{
		if (mDraggedItem != null )
		{
			LZCursor.Set(mDraggedItem.spriteName);
		}
		else
		{
			LZCursor.Clear();
		}
	}
}

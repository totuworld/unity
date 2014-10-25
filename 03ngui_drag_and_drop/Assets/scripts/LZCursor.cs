using UnityEngine;

/// <summary>
/// Selectable sprite that follows the mouse.
/// </summary>

[RequireComponent(typeof(UISprite))]
public class LZCursor : MonoBehaviour
{
    static public LZCursor instance;
    
	// Camera used to draw this cursor
	public Camera uiCamera;
	
	Transform mTrans;
	UISprite mSprite;
	
	UIAtlas mAtlas;
	
	void Awake() { instance = this; }
	void OnDestroy() { instance = null; }
	
	/// <summary>
	/// Cache the expected components and starting values.
	/// </summary>
	
	void Start()
	{
		mTrans = transform;
		mSprite = GetComponent<UISprite>();
		
		if (uiCamera == null)
			uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
	}
	
	/// <summary>
	/// Reposition the widget.
	/// </summary>
	
	void Update()
	{
		Vector3 pos = Vector3.zero;
		if( Application.platform == RuntimePlatform.Android ||
		   Application.platform == RuntimePlatform.IPhonePlayer)
		{
			if(Input.touchCount > 0)
			{
				pos = Input.GetTouch(0).position;
			}
			else
			{
				pos = Vector3.zero;
			}
		}
		else
		{ 
			pos = Input.mousePosition; 
		}
		
		
		if (uiCamera != null)
		{
			// Since the screen can be of different than expected size, we want to convert
			// mouse coordinates to view space, then convert that to world position.
			pos.x = Mathf.Clamp01(pos.x / Screen.width);
			pos.y = Mathf.Clamp01(pos.y / Screen.height);
			mTrans.position = uiCamera.ViewportToWorldPoint(pos);
			
			// For pixel-perfect results
			if (uiCamera.isOrthoGraphic)
			{
				Vector3 lp = mTrans.localPosition;
				lp.x = Mathf.Round(lp.x);
				lp.y = Mathf.Round(lp.y);
				mTrans.localPosition = lp;
			}
		}
		else
		{
			// Simple calculation that assumes that the camera is of fixed size
			pos.x -= Screen.width * 0.5f;
			pos.y -= Screen.height * 0.5f;
			pos.x = Mathf.Round(pos.x);
			pos.y = Mathf.Round(pos.y);
			mTrans.localPosition = pos;
		}
	}
	
	/// <summary>
	/// Clear the cursor back to its original value.
	/// </summary>
	
	static public void Clear()
	{
		if (instance != null)
			Set("");
	}
	
	/// <summary>
	/// Override the cursor with the specified sprite.
	/// </summary>
	static public void Set (string spriteName)
	{
		if (instance != null && instance.mSprite)
		{
			if(spriteName == "")
			{
				instance.mSprite.enabled = false;
				return;
			}
			instance.mSprite.enabled = true;
			instance.mSprite.spriteName = spriteName;
			instance.mSprite.MakePixelPerfect();
			instance.Update();
		}
	}
}

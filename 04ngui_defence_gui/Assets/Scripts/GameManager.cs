using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	public static GameManager instance{
		get{
			if(_instance == null){
				GameObject go = GameObject.Find( "GameManager" );
				
				if( go == null )
				{
					go = new GameObject( "GameManager" );
				}
				_instance = go.GetComponent<GameManager>();
				
				if( _instance == null )
				{
					_instance = go.AddComponent<GameManager>();
				}

				DontDestroyOnLoad(go);
			}
			return _instance;
		}
	}

	/// <summary>
	///  지면에 표시할 장애요소.
	/// </summary>
	public List<Sprite> groundSprite = new List<Sprite>();

	/// <summary>
	/// 건물표현에 사용될 요소.
	/// </summary>
	public List<Sprite> buildingSprite = new List<Sprite>();

	public Camera mainCamera;
	public GameObject circleUIRoot;
	GameObject uiFollowTargetObj;
	List<GameObject> uiObjs = new List<GameObject>();

	float screenWidth = 0, screenHeight = 0;
	public UIRoot uiroot;

	void OnEnable()
	{
		if(uiObjs.Count <= 0)
		{
			for(int i=0;i<4;++i)
			{
				uiObjs.Add(circleUIRoot.transform.GetChild(i).gameObject);
			}
		}
		circleUIRoot.SetActive(false);

		screenWidth = Screen.width;
		screenHeight = Screen.height;
	}

	Vector3 calVector;
	void Update()
	{
		if(uiFollowTargetObj != null)
		{
			calVector = mainCamera.WorldToScreenPoint(uiFollowTargetObj.transform.position);
			calVector = new Vector3(calVector.x /screenWidth * uiroot.manualWidth,
			                        calVector.y /screenHeight * uiroot.manualHeight,
			                        0);
			circleUIRoot.transform.localPosition = calVector;
		}
	}

	public void TurnOnEraseMenu(GameObject senderObj)
	{
		uiFollowTargetObj = senderObj;
		uiObjs[0].SetActive(false);
		uiObjs[1].SetActive(true);
		uiObjs[2].SetActive(false);
		uiObjs[3].SetActive(false);

		circleUIRoot.SetActive(true);
	}

	public void TurnOnBuildingMenu(GameObject senderObj)
	{
		uiFollowTargetObj = senderObj;
		uiObjs[0].SetActive(true);
		uiObjs[1].SetActive(true);
		uiObjs[2].SetActive(false);
		uiObjs[3].SetActive(false);

		circleUIRoot.SetActive(true);
	}

	public void TurnOnGroundMenu(GameObject senderObj)
	{
		uiFollowTargetObj = senderObj;
		uiObjs[0].SetActive(false);
		uiObjs[1].SetActive(false);
		uiObjs[2].SetActive(true);
		uiObjs[3].SetActive(true);

		circleUIRoot.SetActive(true);
	}



	public void ClickDestroy()
	{
		TileUnit unit = uiFollowTargetObj.GetComponent<TileUnit>();
		if(unit.groundType != GroundType.normal)
		{
			unit.DestroyObstacle();
		}
		else
		{
			unit.DestoryBuilding();
		}
		TurnOnGroundMenu(uiFollowTargetObj);
	}

	public void ClickMakeBuilding(GameObject sender)
	{
		TileUnit unit = uiFollowTargetObj.GetComponent<TileUnit>();
		if(sender.name == "Door")
		{
			unit.MakeBuilding( buildingSprite[0]);
		}
		else
		{
			unit.MakeBuilding( buildingSprite[1]);
		}
		TurnOnBuildingMenu(uiFollowTargetObj);
	}


}

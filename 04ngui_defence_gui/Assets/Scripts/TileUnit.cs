using UnityEngine;
using System.Collections;

public enum GroundType { normal, rock, tree }
public enum Building { none, Door, Chest }

public class TileUnit : MonoBehaviour {

	public GroundType groundType = GroundType.normal;

	SpriteRenderer groundRender;
	SpriteRenderer buildingRender;

	[HideInInspector]
	public bool isMakedBuilding = false;

	void OnEnable()
	{
		if(groundRender == null)
		{
			groundRender = transform.GetChild(0).GetComponent<SpriteRenderer>();
			buildingRender = transform.GetChild(1).GetComponent<SpriteRenderer>();
		}

		switch(groundType)
		{
		case GroundType.rock:
			groundRender.gameObject.SetActive(true);
			groundRender.sprite = GameManager.instance.groundSprite[0];
			break;
		case GroundType.tree:
			groundRender.gameObject.SetActive(true);
			groundRender.sprite = GameManager.instance.groundSprite[1];
			break;
		case GroundType.normal:
			groundRender.gameObject.SetActive(false);
			break;
		}
	}

	void OnMouseDown()
	{
		if(isMakedBuilding)
		{
			// already maked building
			// destroy or upgrade menu on
			GameManager.instance.TurnOnBuildingMenu(gameObject);
		}
		else if(groundType != GroundType.normal)
		{
			GameManager.instance.TurnOnEraseMenu(gameObject);
		}
		else
		{
			//make menu on
			GameManager.instance.TurnOnGroundMenu(gameObject);
		}
	}

	/// <summary>
	/// 장애물 요소 파괴.
	/// </summary>
	public void DestroyObstacle()
	{
		groundType = GroundType.normal;
		groundRender.gameObject.SetActive(false);
	}

	public void DestoryBuilding()
	{
		isMakedBuilding = false;
		buildingRender.gameObject.SetActive(false);
	}

	public void MakeBuilding(Sprite targetSprite)
	{
		if(isMakedBuilding) return;

		isMakedBuilding = true;
		buildingRender.sprite = targetSprite;
		buildingRender.gameObject.SetActive(true);
	}
}

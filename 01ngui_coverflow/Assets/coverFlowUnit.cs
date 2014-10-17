using UnityEngine;
using System.Collections;

public class coverFlowUnit : MonoBehaviour {

	Transform mTrans;
	/// <summary>
	/// 패널의 값을 알아내기 위한 것.
	/// </summary>
	UIPanel mPanel;
	/// <summary>
	/// UISprite나 UITexture 등이 상속받은 상위 클래스 UIWidget으로 Size를 조절할 때 사용.
	/// </summary>
	UIWidget mWidget;
	//unit의 가로 크기.
	float cellWidth;
	//작아졌을 때 크기.
	float downScale;

	void Start()
	{
		mTrans = transform;
		mPanel = mTrans.parent.parent.GetComponent<UIPanel>();
		mWidget = GetComponent<UIWidget>();

		//초기값 입력.
		cellWidth = 300;
		downScale = 0.35f;
	}
	
	float pos, dist;
	
	void FixedUpdate()
	{
		//중심점과 거리가 얼마나 멀어졌는지 확인.
		pos = mTrans.localPosition.x - mPanel.clipOffset.x;
		dist = Mathf.Clamp(Mathf.Abs(pos), 0f, cellWidth);
		//width값을 조절하여 sprite의 size를 조정. 
		mWidget.width = System.Convert.ToInt32( ((cellWidth - dist * downScale) / cellWidth) * cellWidth );
	}
}

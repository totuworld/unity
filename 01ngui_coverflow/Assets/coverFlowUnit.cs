using UnityEngine;
using System.Collections;

public class coverFlowUnit : MonoBehaviour {

	Transform mTrans;
	UIPanel mPanel;
	UIWidget mWidget;

	float cellWidth;
	float downScale;

	void Start()
	{
		mTrans = transform;
		mPanel = mTrans.parent.parent.GetComponent<UIPanel>();
		mWidget = GetComponent<UIWidget>();
		
		cellWidth = 300;
		downScale = 0.35f;
	}
	
	Vector3 pos, calPos;
	
	void FixedUpdate()
	{
		calPos = new Vector3(mPanel.clipOffset.x, 0, 0);
		pos = mTrans.localPosition - calPos;
		float dist = Mathf.Clamp(Mathf.Abs(pos.x), 0f, cellWidth);

		mWidget.width = System.Convert.ToInt32( ((cellWidth - dist * downScale) / cellWidth) * cellWidth );
	}
}

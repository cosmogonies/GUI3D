using UnityEngine;
using System.Collections;

public class BHV_Motion : MonoBehaviour 
{
	public float Depth=1.0f;
	
	private const float StableThreshold = 0.1f;
	
	public Rect targetScreenBoundary;
	
	public CG_GUI3D.Widgets.Widget SuperObj ;
	
	private Vector3 Position;
	private Vector3 Scale;
	private float Scale_AnimationDelta= 0.99f;
	
	private Vector3 previousMove= new Vector3(float.PositiveInfinity,float.PositiveInfinity,float.PositiveInfinity);
	
	
	void Start () 
	{
		//this.transform.LookAt( UnityEngine.Camera.mainCamera.transform );
	}
	
	
	void Update () 
	{
		if(this.SuperObj.isEnabled)
		{
			Vector2 mousePositionYInverted = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y);			
			
			if( this.targetScreenBoundary.Contains(mousePositionYInverted))
			{
				if(  Input.GetMouseButtonUp(0))
				{	//We hit the button!					
					Debug.Log (this.name+" fired");
									
					if(this.SuperObj.SignalDict.ContainsKey("Pressed"))
						this.SuperObj.SignalDict[ "Pressed" ]();
				}
				else
				{	//On Hover
					this.transform.localScale *= Scale_AnimationDelta;
									
					if(this.transform.localScale.x > this.Scale.x )
						Scale_AnimationDelta = 0.99f;
					if(this.transform.localScale.x < this.Scale.x*0.7f )
						Scale_AnimationDelta = 1.01f;
				}
			}
			else
			{
				this.transform.localScale = this.Scale;	
			}
			
			
			//Container Logic (maybe we have to put that in ANOTHER BHV)
			if( this.targetScreenBoundary.Contains(mousePositionYInverted))
			{
				if(  Input.GetMouseButtonUp(0))
				{	
					Debug.Log (this.name+" toggle");
					if(this.SuperObj.SignalDict.ContainsKey("Toggled"))
						this.SuperObj.SignalDict[ "Toggled" ]();
				}
			}
			
			
			
		}
	}
	
	
	public void Positionize()
	{
		float zDepth = this.Depth;
					
		Vector3 goal = UnityEngine.Camera.mainCamera.ScreenToWorldPoint(new Vector3(targetScreenBoundary.x+targetScreenBoundary.width*0.5f  ,Screen.height - (targetScreenBoundary.y+targetScreenBoundary.height*0.5f),zDepth) );			
		
		this.transform.position = goal;
		this.transform.Translate(0.0f,0.0f,zDepth*0.5f);
		
		this.Position = this.transform.position;				
		
		Vector3 bottomLeftOnWorld = UnityEngine.Camera.mainCamera.ScreenToWorldPoint(new Vector3(targetScreenBoundary.xMin,targetScreenBoundary.yMin,this.Depth) );
		Vector3 bottomRightOnWorld = UnityEngine.Camera.mainCamera.ScreenToWorldPoint(new Vector3(targetScreenBoundary.xMax,targetScreenBoundary.yMin,this.Depth) );
		
		Vector3 UpperLeftOnWorld = UnityEngine.Camera.mainCamera.ScreenToWorldPoint(new Vector3(targetScreenBoundary.xMin,targetScreenBoundary.yMax,this.Depth) );
		Vector3 UpperRightOnWorld = UnityEngine.Camera.mainCamera.ScreenToWorldPoint(new Vector3(targetScreenBoundary.xMax,targetScreenBoundary.yMax,this.Depth) );
						
		float scaleX = (bottomLeftOnWorld - bottomRightOnWorld).magnitude;
		float scaleY = (UpperLeftOnWorld - bottomLeftOnWorld).magnitude;
		
		this.transform.localScale = new Vector3(scaleX,scaleY,this.transform.localScale.z);
		this.Scale = this.transform.localScale;
	}
	
	
	
	void OnGUI()
	{
		//GUI.Box(currentScreenBoundary,"");
		GUI.Box(targetScreenBoundary,this.transform.name);

	}
	
	
	
	
}

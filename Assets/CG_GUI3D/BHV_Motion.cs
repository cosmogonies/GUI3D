using UnityEngine;
using System.Collections;

public class BHV_Motion : MonoBehaviour 
{
	public float Depth=1.0f;
	
	private const float StableThreshold = 0.1f;
	
	//public bool isPositionStable= false;
	//public bool isScaleStable= false;
	
	public Rect targetScreenBoundary;
	public Rect currentScreenBoundary;
	
	public CG_GUI3D.Widgets.Widget SuperObj ;
	
	private Vector3 Position;
	private Vector3 Scale;
	private float Scale_AnimationDelta= 0.99f;
	
	private Vector3 previousMove= new Vector3(float.PositiveInfinity,float.PositiveInfinity,float.PositiveInfinity);
	
	// Use this for initialization
	void Start () 
	{
		//this.transform.LookAt( UnityEngine.Camera.mainCamera.transform );
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(this.SuperObj.isEnabled)
		{
			//Positionize();
			
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
	
	
	
	
	
	//http://answers.unity3d.com/questions/49943/is-there-an-easy-way-to-get-on-screen-render-size.html
	
	public Rect ToScreenSpace(Bounds bounds, Camera camera)
    {
		//Vector3 min = new  Vector3(bounds.min.x, bounds.min.y, 0.0f) + this.transform.position;
		//Vector3 max = new  Vector3(bounds.max.x, bounds.max.y, 0.0f) + this.transform.position;
		Vector3 min = new  Vector3(bounds.min.x, bounds.min.y, 0.0f);
		Vector3 max = new  Vector3(bounds.max.x, bounds.max.y, 0.0f);
		
		Vector3 minOnScreen = camera.WorldToScreenPoint(min);
		Vector3 maxOnScreen = camera.WorldToScreenPoint(max);
		
        //Vector3 origin = camera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.min.y, 0.0f));
        //Vector3 extents = camera.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.max.y, 0.0f));
 		
		//Debug.Log (origin);
		//Debug.Log (extents);
        
		//wrong: could retrun bad-roiented rect (negative heigt)
		return new Rect(minOnScreen.x, Screen.height - minOnScreen.y, maxOnScreen.x - minOnScreen.x, minOnScreen.y - maxOnScreen.y);

		//float minX = Mathf.Min( minOnScreen.x, maxOnScreen.x );
		//float minY = Mathf.Min( minOnScreen.y, maxOnScreen.y );
		
		//return new Rect(minX, Screen.height - minY, Mathf.Abs(maxOnScreen.x - minOnScreen.x), Mathf.Abs(minOnScreen.y - maxOnScreen.y));
		
    }
	
	public void Positionize()
	{
					//currentScreenBoundary = ToScreenSpace(this.renderer.bounds, UnityEngine.Camera.mainCamera);
			
			//this.transform.LookAt( UnityEngine.Camera.mainCamera.transform );
			
			//Debug.Log ("Currently in Rect"+currentScreenBoundary.ToString());		
			//Debug.Log ("Need to fit in Rect"+targetScreenBoundary.ToString());
			
			//if(! this.isPositionStable)
			//{
				//we need to move then !
				
				//Vector2 deltaTranslationOnScreen = new Vector2(currentScreenBoundary.x - targetScreenBoundary.x, currentScreenBoundary.y - targetScreenBoundary.y);
				
				//float zDepth = (this.transform.position - UnityEngine.Camera.mainCamera.transform.position).magnitude;
				float zDepth = this.Depth;
							
				Vector3 goal = UnityEngine.Camera.mainCamera.ScreenToWorldPoint(new Vector3(targetScreenBoundary.x+targetScreenBoundary.width*0.5f  ,Screen.height - (targetScreenBoundary.y+targetScreenBoundary.height*0.5f),zDepth) );			
				//Vector3 goal = UnityEngine.Camera.mainCamera.ScreenToWorldPoint(new Vector3(targetScreenBoundary.x,Screen.height - targetScreenBoundary.y,zDepth) );
	
				/*
				Vector3 nextMove = (goal - this.transform.position);
				
				Debug.DrawLine(this.transform.position,this.transform.position+nextMove, Color.red);
				
				
				if(nextMove.magnitude > previousMove.magnitude)
				{
					this.isPositionStable=true;
					return;
				}
				
				//Debug.Log (nextMove.sqrMagnitude);
				
				if(nextMove.sqrMagnitude> StableThreshold)
					this.transform.Translate(nextMove*0.1f,Space.World);
				else
					this.isPositionStable=true;
				*/
				
				this.transform.position = goal;
				this.transform.Translate(0.0f,0.0f,zDepth*0.5f);
				
				this.Position = this.transform.position;
			//}
			
			//if(! this.isScaleStable)
			//{
				//Now we have to scale a little bit.
	
				
				/*
				float deltaSizeX  = (Mathf.Abs(targetScreenBoundary.width)-Mathf.Abs(currentScreenBoundary.width));
				float deltaSizeY  = (Mathf.Abs(targetScreenBoundary.height)-Mathf.Abs(currentScreenBoundary.height));
				deltaSizeX  = Mathf.Abs(deltaSizeX);
				deltaSizeY  = Mathf.Abs(deltaSizeY);
				
				Debug.Log("deltaSizeX="+deltaSizeX+"deltaSizeY="+deltaSizeY);
				if(Mathf.Abs(deltaSizeX)>10.0f)
					this.transform.localScale += new Vector3(0.1f,0.0f,0.0f);
				if(Mathf.Abs(deltaSizeY)>10.0f)
					this.transform.localScale += new Vector3(0.0f,0.1f,0.0f);
				*/
				
				
				
				Vector3 bottomLeftOnWorld = UnityEngine.Camera.mainCamera.ScreenToWorldPoint(new Vector3(targetScreenBoundary.xMin,targetScreenBoundary.yMin,this.Depth) );
				Vector3 bottomRightOnWorld = UnityEngine.Camera.mainCamera.ScreenToWorldPoint(new Vector3(targetScreenBoundary.xMax,targetScreenBoundary.yMin,this.Depth) );
				
				Vector3 UpperLeftOnWorld = UnityEngine.Camera.mainCamera.ScreenToWorldPoint(new Vector3(targetScreenBoundary.xMin,targetScreenBoundary.yMax,this.Depth) );
				Vector3 UpperRightOnWorld = UnityEngine.Camera.mainCamera.ScreenToWorldPoint(new Vector3(targetScreenBoundary.xMax,targetScreenBoundary.yMax,this.Depth) );
								
				float scaleX = (bottomLeftOnWorld - bottomRightOnWorld).magnitude;
				float scaleY = (UpperLeftOnWorld - bottomLeftOnWorld).magnitude;
				
				this.transform.localScale = new Vector3(scaleX,scaleY,this.transform.localScale.z);
				this.Scale = this.transform.localScale;
			//}
		
	}
	
	
	
	void OnGUI()
	{
		//GUI.Box(currentScreenBoundary,"");
		GUI.Box(targetScreenBoundary,this.transform.name);
	
		

	}
	
	
	
	
}

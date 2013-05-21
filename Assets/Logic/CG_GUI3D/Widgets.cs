using UnityEngine;
using System.Collections;
using System.Collections.Generic;




namespace CG_GUI3D
{
	
	namespace Widgets
	{
		public delegate void MyFunction();

		public abstract class Widget 
		{
			public enum Signal {};
			
			
			//public bool isEnabled = true;
			
			 // Fields
    		private bool _isEnabled= true;
		    // Properties
		    public bool isEnabled
		    {
		        get
		        {
		            return this._isEnabled;
		        }
		        set
		        {
		            this._isEnabled = value;
					
					this.Gizmo3D.SetActive(value);
					//this.comp.enabled=value;
		        }
		    }
			
			
			//public List<string> SignalList;
			
			public Dictionary<string,MyFunction>SignalDict;
			//public Dictionary<Signal,System.Delegate>SignalDict;
			/* Could be great, inside having string, lets chosse beetween fixed signals by subClasses*/
			
			
			public string Name="DefaultName";
			public string Label="DefaultLabel";
			
			
			
			//public Rect ScreenPosition;
			
    		protected Rect _ScreenPosition;
		    //public Rect ScreenPosition
			virtual public Rect ScreenPosition
		    {
		        get
		        {
		            return this._ScreenPosition;
		        }
		        set
		        {	//could be overriden in containter
		            this._ScreenPosition = value;
					//when updated, we have to be set:
					this.resize();
		        }
		    }
			
			
			//public GameObject Root;
			public BHV_Motion comp = null;
			public GameObject Gizmo3D;
					
		    public Widget(string _Name)
		    {
				this.Name = _Name;
				
				SignalDict = new Dictionary<string, MyFunction>();
		    }			
			

			public void resize(Rect _TargetScreenBoundary)
			{
				this.ScreenPosition = _TargetScreenBoundary;
				resize();
			}
			public void resize()
			{
				if(this.comp != null)
				{	//Containers have no gameobject..
					this.comp.targetScreenBoundary = ScreenPosition;
					this.comp.Positionize();
				}
			}
		


			//public void connect(Signal _Signal, System.Delegate _CallbackFunction)
			public void connect(string _Signal, MyFunction _CallbackFunction)
			{
				this.SignalDict[_Signal] = _CallbackFunction;
				
				//this.SignalDict[_Signal].Invoke();
				
				//this.SignalDict[_Signal].DynamicInvoke(this);
				
				//_CallbackFunction();
			}
			
			
			/*
			//http://answers.unity3d.com/questions/49943/is-there-an-easy-way-to-get-on-screen-render-size.html
		    public Rect ToScreenSpace(this Bounds bounds, Camera camera)
		    {
		        var origin = camera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.min.y, 0.0f));
		        var extents = camera.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.min.y, 0.0f));
		 
		        return new Rect(origin.x, Screen.height - origin.y, extents.x - origin.x, origin.y - extents.y);
		    }
		    */			
			
		}
		
		//public enum ButtonSignals {Pressed, Hover, Conextual};
		
		public class Button: Widget 
		{
			
			//public enum Signal {Pressed, Hover, Conextual};  //useless, can not be found outside.
			
		    public Button(string _Name):base(_Name)
		    {
				//this.SignalList.Add("Pressed");
				
				//_Root = new CG_GUI3D.Primitives.SampleRectangle();
				this.Gizmo3D = GameObject.CreatePrimitive(PrimitiveType.Cube);
				this.Gizmo3D.name=this.Name;
				this.comp = this.Gizmo3D.AddComponent<BHV_Motion>() as BHV_Motion;
				//this.comp.isPositionStable = false;
				//his.comp.isScaleStable = false;
				
				this.comp.SuperObj = this;
			}
			
			public Button(string _Name, GameObject _Shape):base(_Name)
		    {

				//this.Gizmo3D = GameObject.CreatePrimitive(PrimitiveType.Cube);
				this.Gizmo3D = GameObject.Instantiate(_Shape) as GameObject;
				this.Gizmo3D.transform.Rotate(0.0f,180.0f,0.0f);
				
				this.Gizmo3D.name=this.Name;
				this.comp = this.Gizmo3D.AddComponent<BHV_Motion>() as BHV_Motion;
				//this.comp.isPositionStable = false;
				//his.comp.isScaleStable = false;
				
				this.comp.SuperObj = this;
			}
			
						
		}
		
		public class ComboBox: Widget 
		{
			public List<string> ChoiceList;
					
		    public ComboBox(string _Name):base(_Name)
		    {
				//this.SignalList.Add("IndexChanged");
				ChoiceList= new List<string>();
		    }				
		}		

		public class CheckBox: Widget 
		{
			//public List<string> ChoiceList;
			bool isChecked=true;
					
		    public CheckBox(string _Name):base(_Name)
		    {
				//this.SignalList.Add("IndexChanged");
				//ChoiceList= new List<string>();
		    }				
			
		}				
		
		
	}
	
	
}
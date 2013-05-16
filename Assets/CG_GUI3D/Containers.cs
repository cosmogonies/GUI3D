using UnityEngine;
using System.Collections;
using System.Collections.Generic;




namespace CG_GUI3D
{
	
	
	namespace Containers
	{
		
		//must be a virtual I think
		public class WidgetContainer : CG_GUI3D.Widgets.Widget
		{
			//readonly bool isCollapsed= false;
			//public bool isCollapsed { get;}
			
			public bool isCollapsable = true;
			public bool isCollapsed = false;
			
			//protected readonly float HEADER_HEIGHT = 25.0f;
			protected readonly float HEADER_HEIGHT = 25.0f;
			
			public List<CG_GUI3D.Widgets.Widget> WidgetList;
			
			
			GameObject Gizmo;

			

			
		    public WidgetContainer(string _Name = "DefaultWidgetContainer",bool _IsCollapsable=true): base(_Name)
		    {
				this.Name= _Name;
				this.Label = _Name;
				
				this.isCollapsable = _IsCollapsable;
				
				this.WidgetList = new List<CG_GUI3D.Widgets.Widget>();
				
				if(this.isCollapsable)
				{
					this.Gizmo = new GameObject(this.Name);
					this.comp = this.Gizmo.AddComponent<BHV_Motion>() as BHV_Motion;  //TOASK: maybe we can design another BHV, more adapted to containters ?
					this.comp.SuperObj = this;
					// Interactive Zone is ONLY Header:
					this.comp.targetScreenBoundary = this.ScreenPosition;
					
					this.connect( "Toggled" , this.toggle );
				}
				
		    }	
			
			public void AddWidget(CG_GUI3D.Widgets.Widget _TargetWidget)
			{
				this.WidgetList.Add(_TargetWidget);
			}
			
			
			//virtual protected void placeContained()
			virtual public void placeContained()
			{ // this method move all contained widgets inside.
				//Debug.Log (this.Name);
				Debug.Log ("virtual public void placeContained()");
			}
			
			#region COLLAPSING_LOGIC
			//Collapsing logic:
			public void toggle()
			{
				this.isCollapsed= ! isCollapsed;
				if(!this.isCollapsed)
					Expand();
				else
					Collapse();
			}	
			private void Expand()
			{
				for(int i=0;i<this.WidgetList.Count;i++)
				{
					this.WidgetList[i].isEnabled =true;
				}				
			}
			private void Collapse()
			{
				for(int i=0;i<this.WidgetList.Count;i++)
				{
					this.WidgetList[i].isEnabled =false;
				}
			}
			#endregion
			
		}
		
		
		public class MainWindow : WidgetContainer
		{
			public CG_GUI3D.Containers.WidgetContainer MainHost;
			
			/*
			public MainWindow(float _PercentX, float _PercentY)
			{
				MainWindow( (int)Math.Round(UnityEngine.Screen.width*_PercentX), (int)Math.Round(UnityEngine.Screen.width*_PercentY));
			}
			*/		
			public MainWindow(Rect _ScreenPosition, WidgetContainer _MainHost ) :base("MainWindow")
			{
				//this.WidthOnScreen = _SizeX;
				//this.HeightOnScreen = _SizeY;
				this.ScreenPosition = _ScreenPosition;
					
					
				this.MainHost = _MainHost;
				//this.MainHost.WidthOnScreen = this.WidthOnScreen;
				//this.MainHost.HeightOnScreen = this.HeightOnScreen;
				this.MainHost.ScreenPosition = this.ScreenPosition;
				
				this.MainHost.placeContained();
			}
			
			
			//TODO: META_DATA to add later
			
			//Maybe the DIRECTION OR READING (actually we suppose Left=> Right, THEN: Up=> Down
			//just a matter of iteration from widgetList...
			
			
			
			
			
		}			
		
		
		public class HLayout : WidgetContainer
		{
			public HLayout(string _Name="DefaultHLayout",bool _IsCollapsable=true): base(_Name,_IsCollapsable)
			{}
			
			override public  void placeContained()
			{
				int AvailableSpace = 0;
				int slice=0;
				
				float delta = 0.0f;
				if(this.isCollapsable)
				{
					delta= this.HEADER_HEIGHT;
					this.comp.targetScreenBoundary.height = delta;
				}
				
				AvailableSpace = (int)this.ScreenPosition.width - (int)delta;
				
				
				if(this.WidgetList.Count==0)
					slice = AvailableSpace;
				else
					slice = (int)System.Math.Round( (float)AvailableSpace / this.WidgetList.Count );
				
				//foreach(CG_GUI3D.Widgets.Widget currentWidget in this.WidgetList)
				for(int i=0;i<this.WidgetList.Count;i++)
				{
					Rect newPosition = this.ScreenPosition;
					newPosition.width = slice;
					newPosition.x = this.ScreenPosition.x+delta+(i*slice);
					newPosition.y = this.ScreenPosition.y;
					this.WidgetList[i].resize(newPosition);
					
					
					if(typeof(CG_GUI3D.Containers.WidgetContainer).IsAssignableFrom(this.WidgetList[i].GetType()))
					{	// current widget is a container-heir, so we call its own placement logic:
						CG_GUI3D.Containers.WidgetContainer cur = this.WidgetList[i] as CG_GUI3D.Containers.WidgetContainer;
						cur.placeContained();
					}						
					
					
				}
				
			}			
			
		}
		
		public class VLayout : WidgetContainer
		{
			public VLayout(string _Name="DefaultVLayout",bool _IsCollapsable=true): base(_Name,_IsCollapsable)
			{}
						
			override public void placeContained()
			{
				int AvailableSpace = 0;
				int slice=0;
				
				float deltaHeader = 0.0f;
				if(this.isCollapsable)
				{
					deltaHeader=this.HEADER_HEIGHT;				
					this.comp.targetScreenBoundary.height = deltaHeader;
				}
				AvailableSpace = (int)this.ScreenPosition.height - (int)deltaHeader;
				
				
				if(this.WidgetList.Count==0)
					slice = AvailableSpace;
				else
					slice = (int)System.Math.Round( (float)AvailableSpace / this.WidgetList.Count );				
								
				//foreach(CG_GUI3D.Widgets.Widget currentWidget in this.WidgetList)
				
			
				for(int i=0;i<this.WidgetList.Count;i++)
				{
					Rect newPosition = this.ScreenPosition;
					newPosition.height = slice;
					newPosition.x = this.ScreenPosition.x;
					newPosition.y = this.ScreenPosition.y+deltaHeader+(i*slice);
					//currentWidget.resize(slice, (int)this.ScreenPosition.height);
					this.WidgetList[i].resize(newPosition);
					
					
					//if widget is layout, call placeContained() method also:
					//if( typeof(this.WidgetList[i]).IsSubclassOf(CG_GUI3D.Containers.WidgetContainer)) 
					//if (this.WidgetList[i] is GenericClass<CG_GUI3D.Containers.WidgetContainer>);
					
					//Debug.Log (this.WidgetList[i].GetType().ToString());
					
					//if( this.WidgetList[i].GetType().ToString()=="CG_GUI3D.Containers.HLayout" ) TO CLEAN
					//if( this.WidgetList[i].GetType().IsAssignableFrom(CG_GUI3D.Containers.WidgetContainer) )
					if(typeof(CG_GUI3D.Containers.WidgetContainer).IsAssignableFrom(this.WidgetList[i].GetType()))
					{
						CG_GUI3D.Containers.WidgetContainer cur = this.WidgetList[i] as CG_GUI3D.Containers.WidgetContainer;
						cur.placeContained();
						//this.WidgetList[i].placeContained();
					}					
					
				}				
				
				
			}
			
		}		
		
	}
	
	
}
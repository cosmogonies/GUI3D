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
			

			
			public List<CG_GUI3D.Widgets.Widget> WidgetList;
			
			public void toggle()
			{}	
			private void Expand()
			{}
			private void Collapse()
			{}
			
		    public WidgetContainer(string _Name = "DefaultWidgetContainer",bool _IsCollapsable=true): base(_Name)
		    {
				this.Name= _Name;
				this.Label = _Name;
				
				this.isCollapsable = _IsCollapsable;
				
				this.WidgetList = new List<CG_GUI3D.Widgets.Widget>();
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
			
			
		}			
		
		
		public class HLayout : WidgetContainer
		{
			public HLayout(string _Name="DefaultHLayout",bool _IsCollapsable=true): base(_Name,_IsCollapsable)
			{}
			
			override public  void placeContained()
			{
				int slice;
				if(this.WidgetList.Count==0)
					slice = (int)this.ScreenPosition.width;
				else
					slice = (int)System.Math.Round( (float)this.ScreenPosition.width / this.WidgetList.Count );
				
				//foreach(CG_GUI3D.Widgets.Widget currentWidget in this.WidgetList)
				for(int i=0;i<this.WidgetList.Count;i++)
				{
					Rect newPosition = this.ScreenPosition;
					newPosition.width = slice;
					newPosition.x = this.ScreenPosition.x+(i*slice);
					newPosition.y = this.ScreenPosition.y;
					//currentWidget.resize(slice, (int)this.ScreenPosition.height);
					this.WidgetList[i].resize(newPosition);
					

					if(typeof(CG_GUI3D.Containers.WidgetContainer).IsAssignableFrom(this.WidgetList[i].GetType()))
					{
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
				int slice;
				if(this.WidgetList.Count==0)
					slice = (int)this.ScreenPosition.height;
				else
					slice = (int)System.Math.Round( (float)this.ScreenPosition.height / this.WidgetList.Count );				
								
				//foreach(CG_GUI3D.Widgets.Widget currentWidget in this.WidgetList)
				
			
				for(int i=0;i<this.WidgetList.Count;i++)
				{
					Rect newPosition = this.ScreenPosition;
					newPosition.height = slice;
					newPosition.x = this.ScreenPosition.x;
					newPosition.y = this.ScreenPosition.y+(i*slice);
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
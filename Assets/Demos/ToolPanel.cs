using UnityEngine;
using System.Collections;

using CG_GUI3D;
using CG_GUI3D.Containers;
using CG_GUI3D.Primitives;
using CG_GUI3D.Widgets;


public class ToolPanel : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		//createDemo01();
		//createDemo02();
		createDemo03();
		
	}
	
	
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	
	public void createDemo01()
	{
		//create main Layout
		CG_GUI3D.Containers.VLayout MainLayout = new CG_GUI3D.Containers.VLayout();
		
		
		// create a button
		CG_GUI3D.Widgets.Button Button_A = new CG_GUI3D.Widgets.Button("test");
		
		// add the button to our main layout
		MainLayout.AddWidget(Button_A);
		
		//Finally, create che mainWindow with size AND main layout.
		CG_GUI3D.Containers.MainWindow MainWindow = new CG_GUI3D.Containers.MainWindow(new Rect(50,50,450,250),MainLayout);		
	}

	public void createDemo02()
	{
		CG_GUI3D.Containers.VLayout MainLayout = new CG_GUI3D.Containers.VLayout("MainLayout");

		CG_GUI3D.Widgets.Button Button_A = new CG_GUI3D.Widgets.Button("Button_A");
		MainLayout.AddWidget(Button_A);
				
		CG_GUI3D.Containers.HLayout subH = new CG_GUI3D.Containers.HLayout("SubLayoutH");
		
		CG_GUI3D.Widgets.Button Button_B = new CG_GUI3D.Widgets.Button("Button_B");
		subH.AddWidget(Button_B);		
		CG_GUI3D.Widgets.Button Button_C = new CG_GUI3D.Widgets.Button("Button_C");
		subH.AddWidget(Button_C);		
		CG_GUI3D.Widgets.Button Button_D = new CG_GUI3D.Widgets.Button("Button_D");
		subH.AddWidget(Button_D);		
		
		MainLayout.AddWidget(subH);
		
		CG_GUI3D.Containers.MainWindow MainWindow = new CG_GUI3D.Containers.MainWindow(new Rect(50,50,450,250),MainLayout);
	}
	
	public void createDemo03()
	{
		CG_GUI3D.Containers.VLayout MainLayout = new CG_GUI3D.Containers.VLayout("MainLayout",false);
		
		CG_GUI3D.Containers.HLayout subH1 = new CG_GUI3D.Containers.HLayout("SubLayoutH1",false);
		
		CG_GUI3D.Containers.HLayout subH2 = new CG_GUI3D.Containers.HLayout("SubLayoutH2",false);
		
		CG_GUI3D.Containers.VLayout subV1 = new CG_GUI3D.Containers.VLayout("SubLayoutV1",true);
		
		
		CG_GUI3D.Widgets.Button Button_A = new CG_GUI3D.Widgets.Button("subH1_ButtonA");
		subH1.AddWidget(Button_A);
		CG_GUI3D.Widgets.Button Button_B = new CG_GUI3D.Widgets.Button("subH1_ButtonB");
		subH1.AddWidget(Button_B);
		CG_GUI3D.Widgets.Button Button_C = new CG_GUI3D.Widgets.Button("subH1_ButtonC");
		subH1.AddWidget(Button_C);

		CG_GUI3D.Widgets.Button Button_Menu1 = new CG_GUI3D.Widgets.Button("Menu_1");
		subV1.AddWidget(Button_Menu1);
		CG_GUI3D.Widgets.Button Button_Menu2 = new CG_GUI3D.Widgets.Button("Menu_2");
		subV1.AddWidget(Button_Menu2);
		CG_GUI3D.Widgets.Button Button_Menu3 = new CG_GUI3D.Widgets.Button("Menu_3");
		subV1.AddWidget(Button_Menu3);
		CG_GUI3D.Widgets.Button Button_Menu4 = new CG_GUI3D.Widgets.Button("Menu_4");
		subV1.AddWidget(Button_Menu4);

		subH2.AddWidget(subV1);
		
		CG_GUI3D.Widgets.Button Button_X = new CG_GUI3D.Widgets.Button("Viewport");
		subH2.AddWidget(Button_X);
		

		
		//Connecting logic
		Button_Menu1.connect( "Pressed" , button1Callback );
		Button_Menu2.connect( "Pressed" , button2Callback );
		Button_Menu3.connect( "Pressed" , button3Callback );
		
		MainLayout.AddWidget(subH1);
		MainLayout.AddWidget(subH2);
		
		CG_GUI3D.Containers.MainWindow MainWindow = new CG_GUI3D.Containers.MainWindow(new Rect(20,50,800,600),MainLayout);
	}	
	
	
	
	public void button1Callback(){Debug.Log ("Button1 callback triggered");}
	public void button2Callback(){Debug.Log ("Button2 callback triggered");}
	public void button3Callback(){Debug.Log ("Button3 callback triggered");}
}

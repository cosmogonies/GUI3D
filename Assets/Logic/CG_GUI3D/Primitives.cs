using UnityEngine;
using System.Collections;


namespace CG_GUI3D
{
	
	namespace Primitives
	{
		
		//public class SampleRectangle : UnityEngine.GameObject 
		public class SampleRectangle
		{
			Transform RootTransform;
			
			public SampleRectangle()
		    {
				GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube);	
				this.RootTransform = temp.transform;
		    }			
		}
		
		
		public class Skin
		{
		/*	
			GameObject[] ButtonShapes;
			bool[] ButtonShapes_Scalability;
			
			GameObject[] ChoiceShapes;
			bool[] ChoiceShapes_Scalability;
			
			GameObject[] CheckShapes;
			bool[] CheckShapes_Scalability;			
		*/	
		}
		
		
		
		
		
	}
	
}
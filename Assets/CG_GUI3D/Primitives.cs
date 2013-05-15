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
			//List<GameObject> Shapelist;
			
		    //public SampleRectangle(): base(PrimitiveType.Cube)
			public SampleRectangle()
		    {
				GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube);	
				
				this.RootTransform = temp.transform;
				
		    }			
			
		}
		
	}
	
}
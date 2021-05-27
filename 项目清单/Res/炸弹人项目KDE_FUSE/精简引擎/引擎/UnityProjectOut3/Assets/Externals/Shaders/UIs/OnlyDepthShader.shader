//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Fuse/Custom/OnlyDepthShader"
{
	SubShader{
		Tags
		{
			"Queue" = "Geometry-1"
			"LightMode" = "Deferred"
		}

		//Adia
		//Adia
		Pass
		{
			Name "Only Depth"

			Zwrite On
			ColorMask 0
		}
	}
}
//Adia
//Adia
//===========================================================================
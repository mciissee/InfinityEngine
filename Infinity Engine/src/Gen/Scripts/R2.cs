using UnityEngine;
using InfinityEngine;
using InfinityEngine.Serialization;

namespace InfinityEngine.Serialization {
	
	///<summary> 
	///	 This class is generated automaticaly by InfinityEngine. <br/>
	///  It contains constants for VP plugin.  DO NOT EDIT IT !
	///</summary>
	public static class R2  {

	///<summary> 
	///	 Encapsulates a preference data
	///</summary>
	public struct PrefGetSet<T>{
		
		private string mKey;

		///<summary> The key of the encapsulated preference</summary>
		public string Key { get { return mKey; } }

		///<summary> The value of the encapsulated preference</summary>
		public T Value { get { return VP.Get<T>(mKey); } set { VP.Set(mKey, value); } }

		///<summary>Creates new instance of this struct</summary>
		public PrefGetSet(string key)
		 {
			 mKey = key;
		 }
	}

	///<summary> 
	///	 Integer preferences
	///</summary>
	public static class integers  {

		///<summary> 
		///All Integer keys separated by ','. You can created an array by using string.split() method
		///</summary>		
		public const string Names = "";
		
	 }
	 
	///<summary> 
	///	 Float preferences
	///</summary>	 
	public static class floats  {

		///<summary> 
		///All Float keys separated by ','. You can created an array by using string.split() method
		///</summary>		
		public const string Names = "";

	 }
	 
	///<summary> 
	///	 Double preferences
	///</summary>
	public static class doubles  {

		///<summary> 
		///All Double keys separated by ','. You can created an array by using string.split() method
		///</summary>		
		public const string Names = "";

	 }
	 
	///<summary> 
	///	 Long preferences
	///</summary>
	public static class longs  {

		///<summary> 
		///All Long keys separated by ','. You can created an array by using string.split() method
		///</summary>		
		public const string Names = "";

	 }
	 
	///<summary> 
	///	 String preferences
	///</summary>
	public static class strings  {

		///<summary> 
		///All String keys separated by ','. You can created an array by using string.split() method
		///</summary>		
		public const string Names = "";

	 }
	 
	///<summary> 
	///	 Boolean preferences
	///</summary>
	public static class bools  {

		///<summary> 
		///All Bool keys separated by ','. You can created an array by using string.split() method
		///</summary>		
		public const string Names = "MCEINC_ENABLE_LOG";
	

	 }
	 
	///<summary> 
	///	 Vector2 preferences
	///</summary>
	public static class vector2s  {

		///<summary> 
		///All Vector2 keys separated by ','. You can created an array by using string.split() method
		///</summary>		
		public const string Names = "";

	 }
	 
	///<summary> 
	///	 Vector3 preferences
	///</summary>
	public static class vector3s  {

		///<summary> 
		///All Vector3 keys separated by ','. You can created an array by using string.split() method
		///</summary>		
		public const string Names = "";

	 }
	 
	///<summary> 
	///	 Vector4 preferences
	///</summary>
	public static class vector4s  {

		///<summary> 
		///All Vector4 keys separated by ','. You can created an array by using string.split() method
		///</summary>		
		public const string Names = "";

	 }
	 
	///<summary> 
	///	 Quaternion preferences
	///</summary>
	public static class quaternions  {

		///<summary> 
		///All Quaternion keys separated by ','. You can created an array by using string.split() method
		///</summary>		
		public const string Names = "";

	 }
	 
	///<summary> 
	///	 Color preferences
	///</summary>
	public static class colors  {

		///<summary> 
		///All Color keys separated by ','. You can created an array by using string.split() method
		///</summary>		
		public const string Names = "";

	 }
	 }
}

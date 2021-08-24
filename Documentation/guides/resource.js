$(function() {
    registerGuide({ 
        name: 'ISI-Resource',
        summary: 'A generic solution to manages resources with Unity',
		content: `
		<h1 class='page-heading'>ISI Resource</h1>
		<section>
			<span class='section-heading'>Unity Incremental Compiler</span>
			<p>
				The plugin is written using C# 7 syntax. This means that you must activate this option in Unity Editor.
				If you don't do this, Unity will not compile your project.
			</p>
			<p>
				To enable C# 7 syntax, go to 
				<a href='https://forum.unity.com/threads/unity-incremental-c-compiler.523993/'>this link</a>.
			</p>
		</section>

		<section>
			<span class='section-heading'> What is it ?</span>
			<p>
				A solution to manages resources placed in unity assets folder like <a href="https://developer.android.com/reference/android/R.html"> Android R class</a>.
				You can access directly to many types of object placed in your project folder on
				script without register your object in unity inspector thanks to the class <see cref="R"/>.
			</p>
			<p>
				I am always open to hearing new ideas for improvements or suggestions and of any problems 
				that you might encounter while using ISI Resource plugin.
			<p/>
			<p>
				You can email me any time at 
				<a href="mailto:mciissee@gmail.com?subject=Asset Store">
				mciissee@gmail.com
				</a> 
				and I will respond shortly.
			</p>
		</section>

		<section>
			<span class='section-heading'>How it works ?</span>
			<p>
				ISI Resource provides a set of classes that allow storing, managing and
				editing the resources of any type specified in
				<see cref="ResTypes"/> enumeration.<br/>
				Also it provides an editor window to organize and choose only the type of the 
				resources that you want to manages and where to find them.
			</p>
			<p>
			This plugin really behaves like the R-class of Android, except that unlike the Android, 
			it does not associate an identifier to the resources when generating the R class, 
			it store a static reference to the resource. 
			</p>
			<p>
			This means that there is no need for special functions like <c>findViewById</c>.
			The plugin is <b>very fast and fully optimized</b>.
			</p>
		</section>

		<section> 
			<span class='section-heading'>What's in the Package?</span>
			<b>• <see cref="ISIResource"/></b> : The core of the plugin. <br/> <br/>
			<b>• <see cref="ISIResourceEditor"/></b> : The editor class of the plugin.  <br/> <br/>
			<b>• <see cref="PoolManager"/></b> : A powerful GameObject manager class.<br/> <br/>
		</section>

		<section> 
			<span class='section-heading'>Getting Started</span>
			Go to the tab <b>'Tools/Infinity Engine/Resources/Editor'</b> to open the editor.<br/>
		
			The interface is separated in two tab :<br/>
			<b>• Setting :</b> This tab allow you to choose the resources to include in the database and the folders where to find the resources.<br/>
			<img src='images/ISI-Resource/SettingTab.png' width="720"/><br/><br/>

			<b>• Database :</b> This tab allow you to shows the content of the database.<br/>
			<img src='images/ISI-Resource/DatabaseTab.png' width="720"/><br/><br/>

			The editor window is self documented.
			<img src='images/ISI-Resource/Help.png' width="720"/><br/><br/>
			If you open the editor for the first time, there is not a resource in the database,
			you must choose the type of the resources that you want to add to the
			database in the left area of the tab 'Setting'.<br/>

			After you choose your resources, you have to choose the folders where to find the resources and the folders to not check. <br/><br/>
			<p>
			Note that you cannot include the resources placed in the folders <b>'Assets/InfinityEngine', 'Assets/Plugins' and 'Assets/Standard Assets'</b> or the resources that have a name which starts and ends with <b>'___'</b>. <br/><br/>
			</p>
			If you include a folder, all subfolders of this last will be also included excepts if the sub-folder is placed in the area <b>'Exclusion Area'</b>.  <br/>
			If you exclude a folder, all sub-folders will also be excluded. <br/> <br/>
		</section>

		<section> 
			<span class='section-heading'>Asset Generation</span>
			After you configure the project, you can update the database by pressing the keys <b>'Alt+U'</b> or by clicking in the button 'Update' of the tab 'Database'.<br/>
			During the update, the plugin search all resources of the types that you choose and creates 3 assets, in the folder <b>'Assets/InfinityEngine/Gen'</b>:<br/><br/>

			<b>1 - ISIResource.asset</b><br/><br/>
			<img src='images/ISI-Resource/Asset.png'/><br/><br/>

			This asset contains all resources and the parameters of the plugin, when you export your project, the resources and the settings are kept.<br/><br/>

			<b>2 - ISIResourcePrefab.prefab</b><br/><br/>
			<img src='images/ISI-Resource/Prefab.png'/><br/><br/>

			This is a prefab that will be used at runtime in the build version of your project to provides access to the resource. <br/>
			In the editor, when you get a resource from the database, it is the ScriptableObject <b>'ISIResource.asset'</b> that is used but in the
			build version in some platforms like Android device, the ScriptableObject is not usable, so the plugin use the prefab to provides you access to the resource.
			<b>The plugin has not been tested on all Unity platforms, but it should work in all.</b><br/><br/>

			<b>3 - R.cs</b><br/><br/>
			<img src='images/ISI-Resource/R.png' width="720"/><br/><br/>

			Normaly when you want to get a resource from the database, you have to use the method <see cref="ISIResource.Find{T}(Res, string)"/><br/>
			To save you time, the plugin uses the same system as Android, a static class that contains a reference to all the resources.<br/>
			For example, if you have a GameObject resource in the database which has the name 'myGameObject', you can use the code R.gameobject.MyGameObject to get the GameObject. <br/><br/>

			The fact that this plugin handle a class for you give access to resources makes that you can not name your resources any way.<br/>
			The name of your resource must respect the same convention as the variable naming in programming, it must starts with a letter or the
			char '_' and it cannot contains space or or chars except letters.<br/><br/>

			If your resource is misnamed, it will be included anyway in the database but the name of the variable generated in the class R will be different from
			the name of the resource, all unauthorized chars will be replaced by '_'. <br/>

			If you have many misnamed resources, the search time of the resources will be increased, so give the good name to your resources.<br/><br/>

			The class R contains a reference to :<br/><br/>

			<b>- 13 UnityEngine.Object types :</b><br/>
			<b>o AnimationClip </b><br/>
			<b>o AudioClip </b><br/>
			<b>o Font </b><br/>
			<b>o GameObject </b><br/>
			<b>o GUISkin </b><br/>
			<b>o Material </b><br/>
			<b>o Mesh </b><br/>
			<b>o PhysicMaterial </b><br/>
			<b>o PhysicsMaterial2D </b><br/>
			<b>o Shader </b><br/>
			<b>o Sprite </b><br/>
			<b>o TextAsset </b><br/>
			<b>o Texture2D </b><br/><br/>

			<b>- 5 xml resource types : </b><br/>
			<b>o Int32 </b><br/>
			<b>o Boolean </b><br/>
			<b>o String </b><br/>
			<b>o Color </b><br/>
			<b>o XmlDocument </b><br/><br/>

			and the gameobject <b>tags</b> and <b>layers</b> presents in your projects.<br/>
			<img src='images/ISI-Resource/TagsAndLayers.png'/><br/><br/>


			The resources of type <b>xml</b> must be placed in a xml file named as '{0}_res.xml' where {0} is the type of the resource <b>(String, Color, Int32, Boolean)</b>.<br/><br/>

			The type XmlDocument is special, it is a normal xml file. During the research of theresources, if the plugin find a xml file which is not named as {0}_res.xml , it
			considers it as a XmlDocument resource. The xml resource must look like :<br/>
			<img src='images/ISI-Resource/Xml.png'/><br/><br/>

			You cannot add multiple resource types in the same resource file(for example, the file <b>'String_res.xml'</b> must contains only a string resources) 
			and each xml resource type (String, Color, Int32, Boolean) can have only one resource file in the project (you cannot have two file named <b>'String_res.xml'</b>
			or <b>'Color_res.xml'</b>). If you have a multiple resource files for a xml type, it is the first which is find by the plugin that is used.

		</section>

		<section> 
			<span class='section-heading'>How to use ISI Resource?</span>
			If you want to use ISIResource function in script, you must add the following directive in the head of your scripts :<br/> 
			<code>
				using InfinityEngine.ResourceManagement;
			</code>
			<br/>

			There is 2 way to get a resource from the database :

			<b> - Use the static function <see cref="ISIResource.Find{T}(ResTypes, string)"/> </b><br/>
			<b> - Use the class <see cref="R"/> </b><br/><br/>

			The recommended way is the last because the first use a function that take a string parameter and you can pass a bad value in the function and throw an exception.<br/><br/>

			<b>Example : </b><br/>
			In this example, we play an audio clip resource with the name "sound".
			<code>

			// the namespace of SoundManager
			using InfinityEngine;
			// the namespace of R.
			using InfinityEngine.ResourceManagement;

			public class TestClass : MonoBehaviour {
				
				void Test{
					//SoundManager is shared with this plugin.
					SoundManager.PlayEffect(R.audioclip.Sound);
				}
			}
			</code> 
			<br/>

			The plugin is shared with the component <see cref="PoolManager"/>. You can use it to create a pool management system for any GameObject with 
			one line of code or by using the pre-maded component with unity inspector.<br/>
			<code>
			using InfinityEngine;
			using InfinityEngine.ResourceManagement;

			public class TestClass : MonoBehaviour{
				
				void Start(){
					//This simple code create a pool management system for the prefab named "prefab".
					var myGO = PoolManager.Pop(R.gameobject.prefab);
				}
			}
			</code> 
		</section>
		<section>
			<span class='section-heading'>Final Words</span>
			<p>
				Thanks you if you purchased this asset, if not you can purchase it 
				at <a href="http://u3d.as/J4i">http://u3d.as/J4i</a>
			</p>
			<p>
				If you like the asset, please <a href="http://u3d.as/J4i">rate it.</a>
			</p>
			<p>
				Support is available at <a href="mailto:mciissee@gmail.com?subject=Asset Store">mciissee@gmail.com</a>
			</p>
			<p>
			Make sure you check out my other assets at my <a href="http://u3d.as/riS">assets store page.</a>
			</p>
		</section>
		`
	});
});
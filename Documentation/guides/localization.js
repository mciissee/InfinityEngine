$(function() {
    registerGuide({ 
        name: 'ISI-Localization',
        summary: 'A generic solution to handle localization of any text, audio and image in any number of languages.',
		content: `

		<h1 class='page-heading'>ISI Localization</h1>

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
			<span class='section-heading'>What is it?</span>
			<p>
				ISI Localization is a generic solution to handle localization of any text, 
				audio and image in any number of languages.
			</p>
			<p>
				I am always open to hearing new ideas for improvements or suggestions and of any problems 
				that you might encounter while using ISI Localization plugin.
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

			<span class='section-heading'>How it works?</span>
			<p>
				ISI Localization provides a set of classes that allow storing, managing and editing 
				the translations of any text, audio or image.
			</p>

			<p>
				It provides also an API to allow dynamic translations, import/export, 
				callbacks and lot of other useful features.
			</p>
			<p>
				With ISI Localization, all localized content of a language are stored 
				in the an instance of the class <see cref="LocalizedLanguage"/>.
			</p>

			<p>
				Each time you create a new language in the editor, an instance of LocalizedLanguage class is registered 
				to the singleton <see cref="ISILocalization"/>.
			</p>
			<p> 
				Each time you save the modifications in the editor, The plugin create :
			</p>
			
			<p>
				<b>• ISILocalizationPrefab.prefab</b> <br/>
				<image src="images/ISI-Localization/Prefab.png"/>
				The prefab is global to the project, it is added automatically to the scene when you request a localized content at runtime.
			</p>

			<p>
				<b>• Assets/InfinityEngine/Gen/Xml</b> <br/>
				<image src="images/ISI-Localization/Xml.png"/>
				This folder will contains an xml file for each language that you use.
			</p>
			<p>
				<b>• <see cref="R3"/>.cs</b>. <br/>
				<image src="images/ISI-Localization/R3.png"/>
				This script will store a static reference to the localized contents and 
				prevent you from manipulating hardcoded string in your code.
			</p>
	
			<p>
				ISI Localization provides you access to pre-maded components, 
				all usable in Unity Inspector without coding any line of code to translate your project.
			</p>
		</section>

		<section>
			<span class='section-heading'>What’s in the Package?</span>
			<p>
				<span class='section-subheading'>• <see cref="ISILocalization"/> :</span>
				This is main class of this localization engine.
				It is the class that provides access to all localized contents (string,
				sprites, audio) thanks to it static functions. This class is a singleton
				class, so you don't have to initialize it by script.
			</p>

			<p>
				<span class='section•subheading'>• <see cref="LocalizedLanguage"/> :</span> 
				This class represents a language with all localized contents in the language.
			</p>
			<p>
				<span class='section-subheading'>• <see cref="Language"/> :</span> 
				An enumeration type that contains all supported languages. If you
				want to add a new language, you just need to create new value in this enumeration.
			</p>

			<p>
				<span class='section-subheading'>• <see cref="Flag"/> :</span>
				Component to attach to a button in Unity Inspector. It allows to choose a language. 
				The plugin is shared with a lot of beautiful flags icons.
			</p>

			<p>
				<span class='section-subheading'>• <see cref="LocalizedString"/> :</span>
				Component to attach to a GameObject in Unity Inspector. It allows to change dynamically the text of a label at
				runtime.(the GameObject must have a <c>UI.Text</c>, <c>TextMesh</c>, <c>TextMeshPro</c> or <c>TextMeshUGUI</c> component).
			</p>

			<p>
				<span class='section-subheading'>• <see cref="LocalizedAudio"/> :</span>
				Component to attach to a GameObject in Unity Inspector. It allows to play localized audio at runtime (the GameObject don't need another component like audio source, the plugin
				instantiate automatically an GameObject with AudioSource component at runtime and add the component <see cref="SoundManager"/> into it).
			</p>

			<p>
				<span class='section-subheading'>• <see cref="LocalizedSprite"/>  :</span> 
				Component to attach to a Game Object in Unity Inspector. It allows to use a localized image at runtime (the Game
				Object must have a <c>SpriteRenderer</c> or <c>Image</c> component).
			</p>

			<p>
				<span class='section-subheading'>• <see cref="PopupAttribute"/> :</span>
				Custom attribute that allows to display a string array in inspector like popup.
			</p>
		</section>

		<section>
			<span class='section-heading'>Getting Started</span>
			<p>
				• Add the component <see cref="ISILocalization"/> to a Game Object on Unity Inspector.
				<image src="images/ISI-Localization/Component.png"/>
			</p>

			<p>
				• This component and all the others of the plugin are self documented.
				<image src="images/ISI-Localization/ComponentDoc.png"/>
			</p>
			
			<p>
				• Open the editor
				<image src="images/ISI-Localization/EditorStructure.png"/><br/>
			</p>
			
			<p>The editor is separated in 2 area :</p>

			<p><b>• The left area </b> where you create the keys that you use to access the localized contents (string, audio and sprites).</p>
			<p><b>• The right area </b> where you translate the keys in the case of localized strings, and associate a key to a content in the case of localized audio and localized sprite.</p>

			<p>
				The following steps illustrate the steps displayed in the image above (1 to 5).<br/><br/>

				1) Click on the button <b>New</b> to display an popup allowing you to choose a language.<br/>
				
				2) Create all your key that you want to translate in the left area.<br/>

				3) Translate your keys in the right area. You can format your translation with <b>{i}</b> where i is a number.<br/>
				
				4) Click on the button <b>Export</b> to creates the resources necessary to the plugin.<br/> 

				5) You can also use the plugin to localize audio and images.
			</p>
		</section>

	<section> 
		<span class='section-heading'>Editor mode</span>
		ISI Localization provides you a ways to translate your projects integrally with the editor without coding any line of code.<br/>
		There is a pre-maded component for each localized content type :<br/><br/>

		<b>• LocalizedString</b> <br/>
		<image src="images/ISI-Localization/LocalizedString.png"/><br/><br/>

		<b>• LocalizedAudio</b> <br/>
		<image src="images/ISI-Localization/LocalizedAudio.png"/><br/><br/>

		<b>• LocalizedSprite</b> <br/>
		<image src="images/ISI-Localization/LocalizedSprite.png"/><br/><br/>

		All these components must be attached to a the GameObject that contains the content that you want to localize like <c>UI.Text</b> or <c>SpriteRenderer</c>.<br/>
		Click on the dropdown list to choose the key that you want to associate to the component or use the search tools if you want many localized content.<br/>
		When the game starts, your components are localized automatically.<br/><br/>

		There is also the component flag :<br/>
		<image src="images/ISI-Localization/Flag.png"/><br/><br/>

		This component allow you to created a language selection scene. You can choose a language and change the scene when the user click on a button without coding.<br/>
	</section>


	<section> 
		<span class='section-heading'>Script mode</span>
		If there is a thing that you cannot do with the editor, you can use the script mode that is very easy to use also.<br/>
		In script mode, <see cref="ISILocalization"/>  class contain static methods usable without placing the any GameObject in the scene like the function <see cref="ISILocalization.GetValueOf(string)"/>.<br/>

		<b>Example 1 : Simple Localized String </b><br/><br/>
		For a localized string with the key <span class="code-string">"HelloWorld"</span> and the value <span class="code-string">"Bonjour tout le monde"</span> in French language, you can access the value by script in two ways :
		<code>
		using UnityEngine;
		// use the namespace that provides access to classes of the plugin
		using InfinityEngine.Serialization;

		public class Test : MonoBehaviour {
			
			void Start(){
				//The first way is to use directly the functions of the class ISILocalization
				//As you can see, you use string objects with this method and You can go wrong and write your key wrong.
				var myText = ISILocalization.GetValueOf("HelloWorld");

				Debug.Log(myText);
				
				// This method use the class R3 generated by the plugin
				// This is the recommended way to use the plugin
				var myText2 = R3.strings.HelloWorld;
			
				Debug.Log(myText2);
			}
		}
		</code><br/>

		<b>Example 2 : Formated Localized String </b><br/><br/>
		Let say you want to translate a sentence which can change at runtime. <br/>
		To do that, you have to format your sentence with a brackets <b>'{}'</b>.<br/>
		In this example we use the sentence <span class="code-string">"You have clicked <b>{0}</b> times"</span> which have <b>'MyKey'</b> as key.<br/>
		To get the value of this localized string, you can do it in two ways :

		<code>
		using UnityEngine;
		// use the namespace that provides access to classes of the plugin
		using InfinityEngine.Serialization;

		public class Test : MonoBehaviour {
			
			void Start(){
			
				//As for the first example, this method use string object and it is not recommended
				var myText = ISILocalization.GetFormatedValueOF("MyKey", 10);;

				
				Debug.Log(myText);
				
				// As for the first example, this method use R3 class and it is the way recommended
				var myText2 = R3.strings.HelloWorld.Format(10);
					
				Debug.Log(myText2);
			}
		}
		</code><br/>
	</section>

	<section>
		<span class='section-heading'>Complement</span>

		<p>
			<span class='section-subheading'>• Language Change Event :</span>	
			You can use the delegate "<see cref='ISILocalization.OnLanguageChanged'/>" to do an action when the application language change.
		</p>

		<p>
			<span class='section-subheading'>• Find ISILocalization Components In Scene :</span>
			Go to the menu <b>‘Tools/Infinity Engine/Localization/Localization/Find Dependencies In Scene’</b> to see all Game Objects in current scene with a
			component linked to ISI Localization plugin.
		</p>

		<p>
			<span class='section-subheading'>• Change Language Between Scenes :</span>
			By default when the plugin is instantiated at runtime, the default language is <b>English</b>.<br/>
			The plugin is initialized with the language choosen by the player only at runtime because at runtime you provides the use a way to choose a language
			thanks to a language selection scene or when you start the game from the language selection scene in the editor.<br/>
			The problem is that in editor mode you there is some cases when you want to test a specific language in any scene.<br/>
			The solution provided by ISILocalization is to create a tab located at <b>‘Tools/InfinityEngine/Localization/ISI Localization/Default Language'</b> <br/>
			In this tab you can changes in any the scene and at any moment the default language of the plugin.<br/><br/>
		</p>

		<p>
		<span class='section-subheading'>• Good Practice :</span> 
			You must NEVER directly modify the prefab of
			the ISILocalization in the assets folder. If you want to modify the
			prefab, place it on the scene modify it and when you click on the
			button export, the prefab will be automatically updated. (do not
			forget to delete the prefab which you placed in the scene.
		</p>
	</section>


	<section>
	<span class='section-heading'>Final Words</span>
	<p>
		Thanks you if you purchased this asset, if not you can purchase it 
		at <a href="http://u3d.as/s7c">http://u3d.as/s7c</a>
	</p>
	<p>
		If you like the asset, please <a href="http://u3d.as/s7c">rate it.</a>
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
$(function() {
    registerGuide({ 
        name: 'Speech-Engine',
        summary: 'A generic solution to integrate Google Text To Speech API with Unity.',
		content: `
		<h1 class='page-heading'>Speech Engine</h1>
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
			
			<p>A solution to use Google Text to Speech API on android device with Unity3D.</p>
			<p>
				I am always open to hearing new ideas for improvements or suggestions and of any problems 
				that you might encounter while using Speech Engine plugin.
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
				Speech Engine provides a way to make native function calls to android TextToSpeech with unity. 
				The full source code of the java project is shared with the plugin.
			</p>

			<p>
				All functions of the plugin are static and optimized. 
				You can combine the engine with the localization system of 
				<b>ISI Localization</b> plugin.<br/><br/>
			</p>

			<p>
				The supported all (32 languages without country variation ) like 'en, fr, es...' 
				of Google Text To Speech API and all the others (255 languages with country 
				variation like 'en-US, en-GB, fr-CA').
			</p>
		</section>

		<section> 
			<span class='section-heading'>What's in the Package?</span>
			<p>
				<span class='section-subheading'>• <see cref="SpeechEngine"/> :</span> A singleton class that provides you with 
				static functions to use Google Text To Speech API.
			</p>
			<p>
				<span class='section-subheading'>• <a href="index.html#ISI-Localization"> ISI Localization Plugin :</a></span> 
				The full source code of our localization plugin is integrated and compatible 
				with the Google Text To Speech API.
			</p>
		</section>

		<section> 
			<span class='section-heading'>How to use the plugin ?</span>
			
			<p>
				<span class='section-subheading'>Initialization</span>
				Before using the plugin, the target platform must be set to <b>Android</b>.<br/>
				When SpeechEngine is initialized by calling any function of the class, <b>English</b> is sets as default language.<br/>

				The static function <see cref="SpeechEngine.SetLanguage(Locale)"/> allow to changes this behaviour, 
				so calling this function is the best way to initialize the plugin.
			</p>

			<p>
				<span class='section-subheading'>Speak</span>
				If you want to speak a message, call the function <see cref="SpeechEngine.Speak(string)"/>.<br/>
				There is a second version of this function which take a parameter time,
				the function allow you to makes pauses during the speak.
			</p>

			<p>
				<span class='section-subheading'>Example :</span>
				With the sentence <span class="code-string"> "This {pause} is {pause} a {pause} test" </span>, when you call the function  <see cref="SpeechEngine.SpeakWithPause(string, int)"/>, 
				the engine makes pause during 1 second each time there is <b>'{pause}'</b> in the sentence.
			</p>

			<section>
				<span class='section-heading'>Final Words</span>
				<p>
					Thanks you if you purchased this asset, if not you can purchase it 
					at <a href="http://u3d.as/wrd">http://u3d.as/wrd</a>
				</p>
				<p>
					If you like the asset, please <a href="http://u3d.as/wrd">rate it.</a>
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
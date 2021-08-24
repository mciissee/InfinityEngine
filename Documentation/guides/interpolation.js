$(function() {
    registerGuide({ 
        name: 'ISI-Interpolation',
        summary: 'A generic solution to automate the process of animation with Unity.',
		content: `
		<h1 class='page-heading'>ISI Interpolation</h1>
		
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
			<span class='section-heading'>What is it ?</span>
			
			<p>
				ISI Interpolation was created to automate the process of animation in Unity.<br/>
				The plugin provides to the artists a way to created complex animations in few minutes 
				without coding <b>any line of code</b>.
			</p>

			<p>
				Created with Unity in C#. It ads functionality to many Unity 
				components and it integrates seamlesly within the Unity environment.
				ISI Interpolation is shared with the <b>full source code</b>, as a programer, 
				the plugin show you few methods of profesional coding.
			</p>
			<p>
				I am always open to hearing new ideas for improvements or suggestions and of any problems 
				that you might encounter while using ISI Interpolation plugin.
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
			<span class='section-heading'>Getting started</span>
			
			<p>
				ISI Interpolation plugin is like dotween plugin, it allows you to interpolate the data
				types specified in the array <see cref="Interpolation{T}.SupportedTypes"/>.<br/>
				The plugin is separated in two part :
			</p>

			<p>
				<span class='section-subheading'>1 - A part designed for script programers</span>
				
				This part is composed of a sets of scripts that use the power of the class 
				<see cref="Interpolation{T}"/>.<br/>
				This class allow to interpolate the value of a data between two value in a given time.
				 You cannot 
				instanciate this class because it does not take all types of generic parameters.<br/>

				The only way to use it is the function <see cref="Infinity.To{T}(T, T, float, Action{T})"/>.<br/>
				ISI Interpolation is integrated with some Unity components like <c>Image</c>, 
				<c>Transform</c>, <c>Camera</c>, 
				you can interpolate the properties of these components thanks to extension 
				methods defined in the namespace <see cref="InfinityEngine.Extensions"/>.
			</p>

			<p>
				<span class='section-subheading'>2 - A part designed for artists</span>

				This part is composed of a sets of components usable with unity inspector.<br/>
				These components allow all that you want without coding. For example, you can create a complete and beautiful menu system without coding like :<br/>
				<img src='images/ISI-Interpolation/Into The Hole.gif'/><br/>
				<img src='images/ISI-Interpolation/Play Math.gif'/><br/>
			</p>
		</section>
		<section>
			<span class='section-heading'>Programers</span>
			
			<p>
				<span class='section-subheading'>1 - namespaces</span>

				Before use the plugin in script, you must import the following namespaces in each class/script you want to use it:<br/>
				<code>
				using InfinityEngine;
				using InfinityEngine.Interpolations;
				using InfinityEngine.Extensions;
				</code>
			</p>

			<p>
				<span class='section-subheading'>2 - Interpolation class</span>

				As explained in the section above, it is the class <see cref="Interpolation{T}"/> 
				that allow to interpolate a data in this plugin.<br/>
				The basic way to use it is the following :<br/>

				<code>
				using UnityEngine;
				using InfinityEngine;
				using InfinityEngine.Interpolations;
				using InfinityEngine.Extensions;

				public class TestClass : MonoBehaviour {

					void Start(){
						
						// The following code initialize new interpolation that allow to move the transform of this component
						// from it current position to (0, 0, 10) in 5 seconds.
						var interpolation = InfinityEngine.To(()=> transform.position, new Vector3(0, 0, 10), 5, value => transform.position = value);
						
						// You must call this function to start the interpolation
						interpolation.Start();
					}
				}
				</code>
			</p>

			<p>
				<span class='section-subheading'>3 - Extensions methods</span>

				As stated above, the plugin integrates <b>extension methods</b> for some Unity components. 
				All these methods starts with the prefixe <c>DO</c>.<br/>
				For example the following code do the same thing that the last : <br/>
				<code>
				void Start(){
					transform.DOMove(new Vector3(0, 0, 5), 5).Start();
				}
				</code>
			</p>

			<p>
				<span class='section-subheading'>4 - Interpolation options</span>

				When you use the method transform.DOMove(new Vector3(0, 0, 5), 5).Start();  or any extension method to interpolate a data, 
				the method return an object of type <see cref="Interpolable"/>. 
				The interpolations are not started automatically because you can add different options 
				to the interpolation like add a repetition or apply an ease function specified 
				in the enumeration <see cref="EaseType"/>. <br/><br/>
				To add an option you must use the chained methods of the class 
				<see cref="Interpolation{T}"/> which starts with the prefix <b>Set</b> 
				like <see cref="Interpolation{T}.SetRepeat(int, LoopType)"/><br/>
				<code>
				void Start(){
					transform.DOMove(new Vector3(0, 0, 5), 5)
						.SetRepeat(5, LoopType.Reverse) // repeat the interpolation 5 time and reverse the starts and ends value each completion
						.SetEase(EaseType.ElasticOut) // add an elastic move effect
						.Start();
				}
				</code>
				If you want to use the <b>same options for multiple interpolations</b>, 
				you can use the function <see cref="Interpolation{T}.CopyTo{TOther}(Interpolation{TOther})"/> 
				or use the class <see cref="InterpolationOptions"/> that encapsulate option.<br/> 
				use the class InterpolationOptions that encapsulates the options.
			</p>

			<p>
				<span class='section-subheading'>4 - Interpolation callbacks</span>

				You can add callback functions to your interpolation if you want to do some actions <b>before</b>, <b>during</b> or <b>after</b> the interpolation. The callback function
				of the class <c>Interpolation&lt;T&gt;</c> starts with the prefix <b>On</b> and take an argument of type <see cref="Interpolable"/> that is the interpolation itself.<br/>
				<code>
				void Start(){

					transform.DOMove(new Vector3(0, 0, 5), 5)
						.SetRepeat(5, LoopType.Reverse) // repeat the interpolation 5 time and reverse the starts and ends value each completion
						.SetEase(EaseType.ElasticOut) // add an elastic move effect
						.OnStart(arg => Debug.Log("The interpolation is started")) // print a message when the interpolation starts
						.OnUpdate((arg) => {
								if(arg.CompletedPercent > .5f){
									arg.Terminate();
								}
							}) // Terminate the interpolation when it is completed at 50%
						.OnTerminate(arg => Debug.Log("The interpolation is terminated")) // print a message when the interpolation is terminated
						.Start(); // starts the interpolation
						
				}
				</code>
			</p>

			<p>			
				<span class='section-subheading'>5 - Sequence of interpolation</span>
				The plugin provides you a way to mix different interpolations that forms a sequence. <br/> 
				For example you can create an interpolation that move your GameObject, then rotate 
				it, then scale it, 
				then starts the interpolation attached to another GameObject...<br/>
				To do that the only thing to use is the class <see cref="Sequence"/>
			</p>

			<p>
				<span class='section-subheading'>Example</span>
				<code>
				using UnityEngine;
				using InfinityEngine;
				using InfinityEngine.Interpolations;
				using InfinityEngine.Extensions;
				/*
				* This example is an demo of the extension method 'DOShakePosition' of TransformExtensions.
				*/
				public class ShakeGameObject : MonoBehaviour{

					void Start(){
					
						// Shake the transform in the 3 axis (x, y, z) because Vector3.one = (1, 1, 1)
						var strength = Vector3.one; 
						// Multiply all components of the Vector3 by 0.1
						var amount = .1f; 
						var duration = .3f;
						
						// apply the shake effect to the transform of the MonoBehaviour component
						transform.DOShakePosition(Vector3.one, amount, duration)
								.SetRepeat(-1, LoopType.Restart) // repeat infinitly the shake effect
								.Start(); // starts the shake effect
					
					}
					
				}
				</code>
				This code produces the following result :<br/>
				<img src="images/ISI-Interpolation/shake.gif" />
			</p>
		</section>
		<section>
			<span class='section-heading'>Artists</span>
			If you are an artist and want to express your creativity without coding, ISI Interpolation provides you with tools designed for this situation.<br/>

			<p>
				<span class='section-subheading'>1 - <see cref="Sequencer"/>  component </span>
				This component is an part of the API that allow to create animations inside
				Unity inspector without coding.<br/>
				To add the component to a GameObject, click on the button <b>Add Component</b> 
				in the inspector panel of your GamObject <br/>
				<img src="images/ISI-Interpolation/Sequencer.png" />
				The component if self documented. Click on the help button to show or hides the comments.
			</p>

			<p>
				<span class='section-subheading'>2 - Extension Methods </span>
				These components allow you to use the extension methods specified in the 'Programers' section.
				As for Sequencer component, these component are self documented. 
				<img src="images/ISI-Interpolation/Methods.png" />
			</p>
		</section>

		<section>
			<span class='section-heading'>Final Words</span>
			<p>
				Thanks you if you purchased this asset, if not you can purchase it 
				at <a href="http://u3d.as/GRf">http://u3d.as/GRf</a>
			</p>
			<p>
				If you like the asset, please <a href="http://u3d.as/GRf">rate it.</a>
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
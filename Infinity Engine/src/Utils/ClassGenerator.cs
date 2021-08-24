using InfinityEngine.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
/*
namespace InfinityEngine.Utils
{
    /// <summary>
    /// Allow to generates class by script.
    /// </summary>
    public class ClassGenerator
    {
        static int IndentationLevel;

        /// <summary>
        /// The using namespaces of the class like {"UnityEngine", "System"}
        /// </summary>
        public string[] Usings;

        /// <summary>
        /// Optional custom attributes example : <c>{"System.Serializable"}</c>
        /// </summary>
        public string[] Attributes;

        /// <summary>
        /// The path where the file of the class to generate will be genereted
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The summary comment of the class
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the namespace of the class to generate.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the signature of the class
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Gets or sets the StreamWriter object used to generate the ouput file.
        /// </summary>
        public StreamWriter Writer { get; set; }

        /// <summary>
        /// Creates new instance of <c>ClassGenerator</c>.
        /// </summary>
        /// <param name="signature">The signature of the class</param>
        public ClassGenerator(string signature)
        {
            Signature = signature;
            IndentationLevel = 0;
        }

        /// <summary>
        /// Creates new instance of <c>ClassGenerator</c>.
        /// </summary>
        /// <param name="syntax">The syntax of the class to generates</param>
        /// <param name="args">The elements to replaces in the syntax</param>
        public ClassGenerator(string syntax, params object[] args)
        {
            Signature = string.Format(syntax, args);
            IndentationLevel = 0;
        }

        /// <summary>
        /// Generates the file of the class at the specified path.
        /// </summary>
        /// <example>
        /// Example :
        ///     <code>
        ///         var generator = new ClassGenerator("MyClassName", "public class MyClassName");
        ///         generator.Generate(Path.Combine(Application.dataPath, "MyClassName.cs"), (writer) =&gt;{  
        ///             writer.WriteLine("public int myVariable;");
        ///         });             
        ///     </code>
        ///
        ///     This code produces the following output in the file file name "MyClassName.cs" and placed at Application.dataPath + "MyClassName.cs" :
        ///
        ///         public class MyClassName {
        ///             public int myVariable;
        ///         }
        ///
        /// </example>
        /// <param name="path">file Path.</param>
        /// <param name="OnWriteBody">Action to do when this function write the body of this class.</param>
        public void Generate(string path, Action<StreamWriter> OnWriteBody)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            var writer = new StreamWriter(path);
            try
            {
                if (Usings != null)
                {
                    Usings.For(delegate (string elem)
                    {
                        writer.WriteLine("using {0};", elem);
                    });
                    writer.WriteLine();
                }
                if (!string.IsNullOrEmpty(Namespace))
                {
                    writer.WriteLine("namespace {0} {{", Namespace);
                    IndentationLevel = 1;
                }
                string indentation = GetIndentation(IndentationLevel);
                WriteHeader(indentation, writer);
                OnWriteBody(writer);
                writer.WriteLine("{0}}}", indentation);
                if (!string.IsNullOrEmpty(Namespace))
                {
                    writer.WriteLine("}");
                }
                writer.Close();
            }
            finally
            {
                if (writer != null)
                {
                    ((IDisposable)writer).Dispose();
                }
            }
        }

        private void WriteHeader(string tabulation, StreamWriter writer)
        {
            if (!string.IsNullOrEmpty(Summary))
            {
                writer.Write(CreateCommentaryTag(Summary, "summary", tabulation));
            }
            string attributes = "";
            if (Attributes != null)
            {
                Attributes.For(delegate (string elem)
                {
                    attributes = $"{attributes}\n{tabulation}[{elem}]";
                });
                writer.WriteLine(attributes);
            }
            writer.WriteLine("{0}{1} {{", tabulation, Signature);
            writer.WriteLine();
        }

        /// <summary>
        /// Write the given fields in the current generated file.
        /// </summary>
        /// <example>
        ///     The following code is an example of use of this method :
        ///     <code>
        ///     using System;
        ///     using UnityEngine;
        ///     using InfinityEngine.Serialization;
        ///
        ///     public class TestClass : MonoBehaviour {
        ///
        ///         void Start(){
        ///             var generator = new ClassGenerator("public class MyClassName");
        ///             generator.Summary = "My commentary";
        ///             generator.Usings = new string[]{ "System", "UnityEngine" };
        ///             generator.Namespace = "MyNamespace";
        ///             generator.Attributes = new string[]{"Serializable"};
        ///             generator.Generate(Path.Combine(Application.dataPath, "MyClassName.cs"), (writer) =&gt;{
        ///                 var list = new List&lt;CodeGenerator&gt;();
        ///                 var field = CodeGenerator.New()
        ///                                          .Summary("firstElement commentary")
        ///                                          .Attributes("Range(0, 10)", "SerializeField")
        ///                                          .Body("private int firstElement = 0;"); 
        ///                 list.Add(field);
        ///                 field = CodeGenerator.New()
        ///                                      .Summary("secondElement commentary")
        ///                                      .Body("public GameObject secondElement;");   
        ///                 list.Add(field);
        ///                 ClassGenerator.WriteFields(list, writer);
        ///             }); 
        ///         }
        ///     }
        ///     </code>
        ///     This code create new class file named 'MyClassName', place it at Application.dataPath +"MyClassName.cs" .
        ///     and write the following code in the class file.
        ///
        ///     <code>
        ///     using System;
        ///     using UnityEngine;
        ///     namespace MyNamespace {
        ///         //&lt;summary&gt;
        ///         //My commentary
        ///         //&lt;/summary&gt;
        ///         [Serializable]
        ///         public class MyClassName {      
        ///
        ///             //&lt;summary&gt;
        ///             // firstElement commentary
        ///             //&lt;/summary&gt;
        ///             [Range(0, 10)]
        ///             [SerializedField]
        ///             private int firstElement = 0;
        ///             //&lt;summary&gt;
        ///             // secondElement commentary
        ///             //&lt;/summary&gt;
        ///             private GameObject secondElement;
        ///         }
        ///     }
        ///     </code>
        /// </example>
        /// <param name="fields">The code generator objects</param>
        /// <param name="writer"><see cref="T:System.IO.StreamWriter" /> reference</param>
        public void WriteFields(List<CodeGenerator> fields, StreamWriter writer)
        {
            var indentation = GetIndentation(IndentationLevel + 1);
            int count = fields.Count;
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(fields[i].Build(indentation));
            }
            var generator = new ClassGenerator("");
        }

        /// <summary>
        /// Write a subclass 
        /// </summary>
        /// <example>
        ///     The following code is an example of use of this method :
        ///     <code>
        ///     using System;
        ///     using UnityEngine;
        ///     using InfinityEngine.Serialization;
        ///
        ///     public class TestClass : MonoBehaviour {
        ///
        ///         void Start(){
        ///             var generator = new ClassGenerator("public class MyClassName") {
        ///                 Summary = "My commentary",
        ///                 Usings = new string[]{ "System", "UnityEngine" },
        ///                 Namespace = "MyNamespace",
        ///                 Attributes = new string[]{"Serializable"},
        ///             }
        ///             generator.Generate(Path.Combine(Application.dataPath, "MyClassName.cs"), (writer) =&gt;{
        ///                 var subClass = new ClassGenerator("public class MySubClass");
        ///                
        ///                 var parentFields = new List&lt;CodeGenerator&gt;();
        ///                 var field = CodeGenerator.New()
        ///                                          .Summary("parent class field commentary")
        ///                                          .Attributes("Range(0, 10)", "SerializeField")
        ///                                          .Body("private int parentCodeGenerator = 0;"); 
        ///                 parentFields.Add(field);
        ///
        ///                 var subCodeGenerators = new List&lt;CodeGenerator&gt;();
        ///                 field = CodeGenerator.New()
        ///                                          .Summary("sub class field commentary")
        ///                                          .Attributes("Range(0, 10)", "SerializeField")
        ///                                          .Body("private int subCodeGenerator = 0;"); 
        ///                 subCodeGenerators.Add(field);
        ///                 ClassGenerator.WriteSubClass(subClass, writer, ()=&gt;{  ClassGenerator.WriteFields(subCodeGenerators, writer); });
        ///                 ClassGenerator.WriteFields(parentFields, writer);
        ///             });   
        ///         }
        ///     } 
        ///     </code>
        ///     This code create new class file named 'MyClassName', place it at Application.dataPath +"MyClassName.cs" .
        ///     and write the following code in the class file.
        ///
        /// <code>
        ///  namespace MyNamespace
        ///  {
        ///      ///<summary>
        ///      ///My commentary
        ///      ///</summary>
        ///      [Serializable]
        ///      public class MyClassName
        ///      {
        ///
        ///          public class MySubClass
        ///          {
        ///
        ///              ///<summary>
        ///              ///sub class field commentary
        ///              ///</summary>
        ///              [Range(0, 10)]
        ///              [SerializeField]
        ///              private int subCodeGenerator = 0;
        ///
        ///          }
        ///          ///<summary>
        ///          ///parent class field commentary
        ///          ///</summary>
        ///          [Range(0, 10)]
        ///          [SerializeField]
        ///          private int parentCodeGenerator = 0;
        ///      }
        ///  }
        /// </code>
        /// </example>
        /// <param name="generator">A new ClassGenerator</param>
        /// <param name="writer"><see cref="T:System.IO.StreamWriter" /> reference</param>
        /// <param name="writeBody">Action to do when this function write the body of this class</param>
        public void WriteSubClass(StreamWriter writer, Action writeBody)
        {
            var indentation = GetIndentation(IndentationLevel + 1);
            generator.WriteHeader(indentation, writer);
            IndentationLevel++;
            writeBody();
            IndentationLevel--;
            indentation = GetIndentation(IndentationLevel + 1);
            writer.WriteLine();
            writer.WriteLine("{0} {1}", indentation, "}");
        }

        internal static string CreateCommentaryTag(string commentary, string tag, string tabulation)
        {
            string arg = commentary.Replace("\n", $"\n{tabulation}///");
            commentary = $"{tabulation}///<{tag}>\n{tabulation}///{arg}\n{tabulation}///</{tag}>\n";
            return commentary;
        }

        internal static string GetIndentation(int level)
        {
            return '\t'.Repeat(level);
        }
    }
}

*/
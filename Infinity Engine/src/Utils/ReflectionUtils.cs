#pragma warning disable XS0001 // Find APIs marked as TODO in Mono
#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator

using InfinityEngine.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
// TODO
namespace InfinityEngine.Utils
{
    /// <summary>
    /// Provides access to useful methods whichs simplify the process of the reflection.
    /// </summary>
    public static class ReflectionUtils
    {
        #region Nested
        /// <summary>
        /// Wrapper class for <see cref="!:TypeInfo" /> class.
        /// The class optimizes the call to the methods of <see cref="T:System.Type" /> class.
        /// </summary>
        internal class TypeInformation
        {
            internal static BindingFlags Flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

            internal readonly bool isBuiltin;

            /// <summary>
            /// The wrapped type
            /// </summary>
            internal readonly Type type;

            /// <summary>
            /// The default value of the type
            /// </summary>
            internal readonly object defaultValue;

            /// <summary>
            /// Cached dictionary of all methods of the type
            /// </summary>
            internal readonly Dictionary<string, MethodInfo> methodsCache;

            /// <summary>
            /// Cached dictionary of all fields of the type
            /// </summary>
            internal readonly Dictionary<string, FieldInfo> fieldsCache;

            /// <summary>
            /// Cached dictionary of all properties of the type
            /// </summary>
            internal readonly Dictionary<string, PropertyInfo> propertiesCache;

            internal readonly Dictionary<Type, bool> decorators;

            /// <summary>
            /// All methods of the type
            /// </summary>
            internal readonly MethodInfo[] methods;

            internal readonly MethodInfo[] extensionMethods;

            /// <summary>
            /// All fields of the type
            /// </summary>
            internal readonly FieldInfo[] fields;

            /// <summary>
            /// All properties of the type
            /// </summary>
            internal readonly PropertyInfo[] properties;

            internal TypeInformation(Type type)
            {
                this.type = type;
                isBuiltin = (type.IsEnum || builtins.Contains(type));
                methods = type.GetMethods(Flags);
                extensionMethods = GetExtensionMethods();
                fields = type.GetFields(Flags);
                properties = type.GetProperties(Flags);
                methodsCache = methods.DistinctBy((MethodInfo e) => e.Name).ToDictionary((MethodInfo e) => e.Name, (MethodInfo e) => e);
                fieldsCache = fields.ToDictionary((FieldInfo e) => e.Name, (FieldInfo e) => e);
                propertiesCache = properties.ToDictionary((PropertyInfo e) => e.Name, (PropertyInfo e) => e);
                decorators = new Dictionary<Type, bool>();
                defaultValue = defaultValueMethod.MakeGenericMethod(type).Invoke(null, null);
            }

            internal MethodInfo[] GetExtensionMethods()
            {
                LoadAssemblies();
                var source = from it in (from assembly in assemblies
                                         from type in assembly.GetTypes()
                                         select new
                                         {
                                             assembly,
                                             type
                                         }).Where(it =>
                                            {
                                                if (type.IsSealed && !type.IsGenericType)
                                                {
                                                    return !type.IsNested;
                                                }
                                                return false;
                                            })
                             from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                             where method.IsDefined(typeof(ExtensionAttribute), false)
                             where (object)method.GetParameters()[0].ParameterType == type
                             select method;

                return source.ToArray();
            }

            internal void AddDecorator(Type decoratorType, bool value)
            {
                if (!decorators.TryGetValue(decoratorType, out bool _))
                {
                    decorators.Add(decoratorType, value);
                }
            }

            internal bool HasDecorator(Type decoratorType)
            {
                if (decorators.TryGetValue(decoratorType, out bool value))
                {
                    return value;
                }
                return false;
            }

            internal bool IsRegisteredDecorator(Type decoratorType)
            {
                if (decorators.TryGetValue(decoratorType, out bool _))
                {
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region Fields

        private static readonly Dictionary<string, Type> cache = new Dictionary<string, Type>();
        private static readonly Dictionary<Type, TypeInformation> typeInfos = new Dictionary<Type, TypeInformation>();

        private static BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        private static MethodInfo castMethod = typeof(ReflectionUtils).GetMethod(nameof(_Cast), flags);
        private static MethodInfo defaultValueMethod = typeof(ReflectionUtils).GetMethod(nameof(_DefaultValue), flags);
        private static List<string> assemblyNames;
        private static Assembly[] assemblies;
        private static HashSet<Type> builtins = new HashSet<Type>
        {
            typeof(int),
            typeof(float),
            typeof(double),
            typeof(short),
            typeof(long),
            typeof(byte),
            typeof(decimal),
            typeof(bool),
            typeof(string),
            typeof(string),
            typeof(char),
            typeof(double),
            typeof(StringBuilder),
            typeof(Enum),
            typeof(int[]),
            typeof(float[]),
            typeof(double[]),
            typeof(short[]),
            typeof(long[]),
            typeof(byte[]),
            typeof(decimal[]),
            typeof(bool[]),
            typeof(string[]),
            typeof(string[]),
            typeof(char[]),
            typeof(double[]),
            typeof(StringBuilder[]),
            typeof(Enum[]),
            typeof(List<int>),
            typeof(List<float>),
            typeof(List<double>),
            typeof(List<short>),
            typeof(List<long>),
            typeof(List<byte>),
            typeof(List<decimal>),
            typeof(List<bool>),
            typeof(List<string>),
            typeof(List<string>),
            typeof(List<char>),
            typeof(List<double>),
            typeof(List<StringBuilder>),
            typeof(List<Enum>),
            typeof(DateTime),
            typeof(uint),
            typeof(ushort),
            typeof(uint),
            typeof(UIntPtr),
            typeof(ulong),
            typeof(DateTime[]),
            typeof(uint[]),
            typeof(ushort[]),
            typeof(uint[]),
            typeof(UIntPtr[]),
            typeof(ulong[]),
            typeof(List<DateTime>),
            typeof(List<uint>),
            typeof(List<ushort>),
            typeof(List<uint>),
            typeof(List<UIntPtr>),
            typeof(List<ulong>)
        };

        private static Type listType = typeof(List<>);
        private static Type dictType = typeof(Dictionary<,>);
        #endregion

        private static void LoadAssemblies()
        {
            if (assemblyNames == null)
            {
                assemblyNames = new List<string>();
                assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var array = assemblies;
                foreach (Assembly assembly in array)
                {
                    assemblyNames.Add(assembly.FullName);
                }
            }
        }

        private static TypeInformation CacheTypeIfNot(Type type)
        {
            if (!typeInfos.TryGetValue(type, out var typeInfo))
            {
                typeInfo = new TypeInformation(type);
                typeInfos.Add(type, typeInfo);
            }
            return typeInfo;
        }

        /// <summary>
        /// Gets all types whuch inherits from the given base type.
        /// </summary>
        /// <param name="baseType">The basz type</param>
        /// <returns>All types whichs inherits from baseType</returns>
        public static Type[] GetTypesInheritingFrom(Type baseType)
        {
            LoadAssemblies();
            var source = from assembly in assemblies
                         from type in assembly.GetTypes()
                         where baseType.IsAssignableFrom(type)
                         select type;
            return source.ToArray();
        }

        /// <summary>
        /// Gets a value indicating whether the given type is in UnityEngine or UnityEditor assemblies
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns><c>true</c> if the type is in the assemblies UnityEngine or UnityEditor <c>false</c> otherwise</returns>
        public static bool IsUnityType(Type type)
        {
            if (string.IsNullOrEmpty(type.Namespace))
            {
                return false;
            }
            if (!type.Namespace.StartsWith("UnityEngine", StringComparison.Ordinal))
            {
                return type.Namespace.StartsWith("UnityEditor", StringComparison.Ordinal);
            }
            return true;
        }

        /// <summary>
        /// Gets a value indicating whether the property is an auto property
        /// </summary>
        /// <param name="property">The property</param>
        /// <returns></returns>
        public static bool IsAutoProperty(PropertyInfo property)
        {
            if (property.CanWrite && property.CanRead)
            {
                return HasAttribute(property.GetGetMethod(), typeof(CompilerGeneratedAttribute));
            }
            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the object is a  <see cref="T:System.Collections.Generic.List`1" /> object.
        /// </summary>
        /// <param name="obj">The object to checks</param>
        /// <returns><c>true</c> if the object is a List object <c>false</c> otherwise</returns>
        public static bool IsList(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is IList && obj.GetType().IsGenericType)
            {
                return obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(listType);
            }
            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the object is a  <see cref="T:System.Collections.Generic.Dictionary`2" /> object.
        /// </summary>
        /// <param name="obj">The object to checks</param>
        /// <returns><c>true</c> if the object is a Dictionary object <c>false</c> otherwise</returns>
        public static bool IsDictionary(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is IDictionary && obj.GetType().IsGenericType)
            {
                return obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(dictType);
            }
            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the field is declared <c>const</c> or <c>readonly</c>.
        /// </summary>
        /// <param name="field">The field to checks</param>
        /// <returns><c>true</c> if the field is const or readonly <c>false</c> otherwise</returns>
        public static bool IsConst(FieldInfo field)
        {
            if (field.IsLiteral)
            {
                return !field.IsInitOnly;
            }
            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the type is primitive type or a list or array of a primitive type.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns><c>true</c> if the type is a simple data type <c>false</c> otherwise</returns>
        public static bool IsPrimitive(Type type)
        {
            return CacheTypeIfNot(type).isBuiltin;
        }

        /// <summary>
        /// Gets a value indicating whether the member has an attribute of the given type
        /// </summary>
        /// <param name="member">The member to checks</param>
        /// <param name="type">The type of the attribute to search</param>
        /// <returns><c>true</c> if the member has the given attribute <c>false</c> otherwise</returns>
        public static bool HasAttribute(MemberInfo member, Type type)
        {
            return member.GetCustomAttributes(type, inherit: false).FirstOrDefault() != null;
        }

        /// <summary>
        /// Gets a value indicating whether the property is an indexer '[]'.
        /// </summary>
        /// <param name="property">The property</param>
        /// <returns></returns>
        public static bool IsIndexer(PropertyInfo property)
        {
            return property.GetIndexParameters().Length != 0;
        }

        /// <summary>
        /// Gets a value indicating whether the property is public.
        /// </summary>
        /// <param name="property">The property</param>
        /// <returns></returns>
        public static bool IsPublic(PropertyInfo property)
        {
            return property.GetSetMethod().IsPublic;
        }

        /// <summary>
        /// Gets the first attribute of the given type from the member.
        /// </summary>
        /// <param name="member">The member to checks</param>
        /// <param name="inherit">Includes the type whichs inherits from the attribute type</param>
        /// <returns></returns>
        public static T GetAttribute<T>(MemberInfo member, bool inherit = false)
        {
            var obj = member.GetCustomAttributes(typeof(T), inherit).FirstOrDefault();
            if (obj != null)
            {
                return (T)obj;
            }
            return default(T);
        }

        public static T GetAttribute<T>(Type type, bool inherit = false)
        {
            var obj = type.GetCustomAttributes(typeof(T), inherit).FirstOrDefault();
            if (obj != null)
            {
                return (T)obj;
            }
            return default(T);
        }

        public static T[] GetAttributes<T>(Type type, bool inherit = false)
        {
            return (from e in type.GetCustomAttributes(typeof(T), inherit)
                    select (T)e).ToArray();
        }

        /// <summary>
        /// Gets all attributes of the given type from the member.
        /// </summary>
        /// <param name="member">The member to checks</param>
        /// <param name="inherit">Includes the type whichs inherits from the attribute type</param>
        /// <returns></returns>
        public static T[] GetAttributes<T>(MemberInfo member, bool inherit = false)
        {
            return (from e in member.GetCustomAttributes(typeof(T), inherit)
                    select (T)e).ToArray();
        }

        /// <summary>
        /// Tries to find the Type object with the given name
        /// </summary>
        /// <param name="name">The name of the type</param>
        /// <returns>The type if it exists <c>null</c> otherwise</returns>
        public static Type FindType(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            try
            {
                cache.TryGetValue(name, out Type value);
                if (value != null)
                {
                    return value;
                }

                LoadAssemblies();
                foreach (var it in assemblyNames)
                {
                    value = Type.GetType(name + "," + it);
                    if (value != null)
                    {
                        break;
                    }
                }
                if (value == null)
                {
                    var arg = string.Empty;
                    foreach (var it2 in assemblyNames)
                    {
                        arg = it2.Substring(0, it2.IndexOf(",", StringComparison.Ordinal));
                        value = Type.GetType($"{arg}.{name},{it2}");
                        if (value != null)
                        {
                            break;
                        }
                    }
                }
                if (value == null)
                {
                    return null;
                }
                cache.Add(name, value);
                return value;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            return null;
        }

        public static FieldInfo GetCachedField(Type type, string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            try
            {
                return CacheTypeIfNot(type)?.fieldsCache[name];
            }
            catch
            {
                return null;
            }
        }

        public static FieldInfo[] GetCachedFields(Type type)
        {
            return CacheTypeIfNot(type)?.fields;
        }

        public static PropertyInfo GetCachedProperty(Type type, string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            try
            {
                return CacheTypeIfNot(type)?.propertiesCache[name];
            }
            catch
            {
                return null;
            }
        }

        public static PropertyInfo[] GetCachedProperties(Type type)
        {
            return CacheTypeIfNot(type)?.properties;
        }

        public static MethodInfo GetCachedMethod(Type type, string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            try
            {
                return CacheTypeIfNot(type)?.methodsCache[name];
            }
            catch
            {
                return null;
            }
        }

        public static MethodInfo GetCachedMethod(Type type, Func<MethodInfo, bool> predicate)
        {
            return CacheTypeIfNot(type)?.methods?.FirstOrDefault(predicate);
        }

        public static MethodInfo[] GetCachedMethods(Type type)
        {
            return CacheTypeIfNot(type)?.methods;
        }

        public static MethodInfo GetCachedExtensionMethod(Type type, string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            return GetCachedExtensionMethods(type)?.FirstOrDefault(it => it.Name == name);
        }

        public static MethodInfo GetCachedExtensionMethod(Type type, Func<MethodInfo, bool> predicate)
        {
            return GetCachedExtensionMethods(type)?.FirstOrDefault(predicate);
        }

        public static MethodInfo[] GetCachedExtensionMethods(Type type)
        {
            return CacheTypeIfNot(type)?.extensionMethods;
        }

        public static MethodInfo[] GetMethodsIncludingExtensions(Type type)
        {
            return GetCachedMethods(type)?.Concat(GetCachedExtensionMethods(type))?.ToArray();
        }

        public static object GetFieldValue(object obj, string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            return GetCachedField(obj.GetType(), name)?.GetValue(obj);
        }

        public static object GetValue(object target, MemberInfo member, params object[] args)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return (member as FieldInfo).GetValue(target);
                case MemberTypes.Method:
                    return (member as MethodInfo).Invoke(target, args);
                case MemberTypes.Property:
                    if ((member as PropertyInfo).CanRead)
                    {
                        return (member as PropertyInfo).GetValue(target, args);
                    }
                    break;
            }
            return null;
        }

        /// <summary>
        /// Try to cast explicitly the given object to an object of the specified type.
        /// (The method works only if the object is really castable to the speficied type)
        /// </summary>
        /// <param name="obj">The object to cast</param>
        /// <param name="type">The new type of the object</param>
        /// <returns></returns>
        public static object Cast(object obj, Type type)
        {
            return castMethod.MakeGenericMethod(type).Invoke(null, new object[]
            {
                obj
            });
        }

        /// <summary>
        /// Gets the default value of the Type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns></returns>
        public static object DefaultValue(Type type)
        {
            return CacheTypeIfNot(type)?.defaultValue;
        }

        public static MethodInfo MakeStaticGenericMethod(Type type, string name, Type[] typeParams)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            return type.GetMethod(name, flags).MakeGenericMethod(typeParams);
        }

        private static T _Cast<T>(object obj)
        {
            try
            {
                return (T)obj;
            }
            catch
            {
                throw new ArgumentException($"The parameter obj is not castable to { typeof(T)}");
            }
        }
        private static T _DefaultValue<T>()
        {
            return default(T);
        }
    }
}

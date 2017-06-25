using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ValiDoc.Extensions
{
	public static class TypeExtensions
	{
		public static Dictionary<string, MethodInfo> ExtractMethodInfo(this Type instanceType, string[] methodNames)
		{
			var runtimeMethods = instanceType.GetRuntimeMethods();

			return runtimeMethods?.Where(m => methodNames.Contains(m.Name)).ToDictionary(mi => mi.Name, mi => mi);
		}
	}
}
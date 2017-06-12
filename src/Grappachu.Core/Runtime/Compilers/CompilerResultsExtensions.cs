using System.CodeDom.Compiler;

namespace Grappachu.Core.Runtime.Compilers
{
    /// <summary>
    ///     Defines a set of extension methods for <see cref="CompilerResults" />
    /// </summary>
    public static class CompilerResultsExtensions
    {
        /// <summary>
        ///     Executes functional code and returns the output
        /// </summary>
        /// <param name="compiledAssembly"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="methodParameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Run<T>(this CompilerResults compiledAssembly, string className, string methodName,
            object[] methodParameters)
        {
            var assembly = compiledAssembly.CompiledAssembly;
            var program = assembly.GetType(className);
            var methodToCall = program.GetMethod(methodName);
            return (T) methodToCall.Invoke(null, methodParameters);
        }


        /// <summary>
        ///     Executes procedural code
        /// </summary>
        /// <param name="compiledAssembly"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="methodParameters"></param>
        public static void Run(this CompilerResults compiledAssembly, string className, string methodName,
            object[] methodParameters)
        {
            compiledAssembly.Run<object>(className, methodName, methodParameters);
        }
    }
}
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using Microsoft.CSharp;

namespace Grappachu.Core.Preview.Runtime.Compilers
{
    /// <summary>
    ///     Defines a runtime compiler for C# code
    /// </summary>
    public static class CsCompiler
    {
        /// <summary>
        ///     Compiles a snipped of C# code
        /// </summary>
        /// <param name="csCode">The source code to be compiled</param>
        /// <param name="assemblyReferences">Include the path of referenced assemblied used by code</param>
        /// <returns></returns>
        public static CompilerResults Compile(string csCode, IEnumerable<string> assemblyReferences)
        {
            // Compiling
            CompilerResults results;
            using (var provider = new CSharpCodeProvider())
            {
                var parameters = new CompilerParameters();

                // Add assembly references
                foreach (var assemblyReference in assemblyReferences)
                    parameters.ReferencedAssemblies.Add(assemblyReference);
                parameters.GenerateInMemory = true;
                parameters.GenerateExecutable = false;

                results = provider.CompileAssemblyFromSource(parameters, csCode);
            }

            // Chacking output
            if (results.Errors.HasErrors)
            {
                var sb = new StringBuilder();
                foreach (CompilerError error in results.Errors)
                    sb.AppendLine(string.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                throw new InvalidOperationException(sb.ToString());
            }

            // Get result
            return results;
        }
    }
}
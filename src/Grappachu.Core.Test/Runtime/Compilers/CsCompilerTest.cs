using System;
using System.IO;
using Grappachu.Core.Lang.Domain;
using Grappachu.Core.Preview.Runtime.Compilers;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Runtime.Compilers
{
    [TestFixture]
    public class CsCompilerTest
    {
        [Test]
        public void Compile_should_dinamically_compile_and_execute_code()
        {
            var externalAssemblyRef = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Grappachu.Core.dll");
            const string code = @"
                using System;
                using Grappachu.Core.Lang.Domain;

                namespace Test
                {
                    public class FooClass
                    {
                        public static MessageArgs GetData(int value)
                        {
                              return new MessageArgs(""Runned on: ""+DateTime.Now.ToString(""dd-MM-yyyy"")+"" with param ""+value);
                        }
                    }
                }
            ";

            var val = CsCompiler.Compile(code, new[] {externalAssemblyRef})
                .Run<MessageArgs>("Test.FooClass", "GetData", new object[] {5});

            val.Message.Should().Be.EqualTo($"Runned on: {DateTime.Now:dd-MM-yyyy} with param 5");
        }
    }
}
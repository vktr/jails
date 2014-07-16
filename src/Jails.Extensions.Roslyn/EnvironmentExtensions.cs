using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;

namespace Jails.Extensions.Roslyn
{
    public static class EnvironmentExtensions
    {
        public static void Register(this IEnvironment environment, CSharpSource source)
        {
            // Compile source to a temporary assembly
            var syntaxTree = SyntaxTree.ParseText(source.SourceCode);
            var refs = new List<MetadataReference>
            {
                new MetadataFileReference(typeof (object).Assembly.Location)
            };

            var asmName =
                environment.GetRegistrations()
                    .OfType<AssemblyName>()
                    .Select(an => new MetadataFileReference(an.CodeBase.Replace("file:///", "")));

            refs.AddRange(asmName);

            var compilation = Compilation.Create("JailsTemp.dll",
                syntaxTrees: new[] {syntaxTree},
                references: refs.ToArray(),
                options: new CompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var stream = new MemoryStream())
            {
                var result = compilation.Emit(stream);

                if (!result.Success)
                {
                    throw new Exception("Could not compile file.");
                }

                environment.Register(new InMemoryAssembly(stream.ToArray()));
            }
        }
    }
}

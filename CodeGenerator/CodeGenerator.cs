using Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using SimpleIoc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace CodeGenerator
{
    public class CodeGenerator : ICommand
    {
        private string TextCode;

        public Assembly Assembly { get; private set; }

        public CodeGenerator(string textCode)
        {
            TextCode = textCode;
        }

        public void Execute()
        {
            string codeToCompile = TextCode;

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(codeToCompile);

            var dd = typeof(Enumerable).GetTypeInfo().Assembly.Location;
            var coreDir = Directory.GetParent(dd);

            string assemblyName = Path.GetRandomFileName();
            var refPaths = new[] {
                typeof(object).GetTypeInfo().Assembly.Location,
                typeof(Console).GetTypeInfo().Assembly.Location,
                typeof(IUObject).GetTypeInfo().Assembly.Location,
                typeof(IMovable).GetTypeInfo().Assembly.Location,
                typeof(Ioc).GetTypeInfo().Assembly.Location,
                Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Runtime.dll"),
                coreDir.FullName + Path.DirectorySeparatorChar + "netstandard.dll"
            };


            MetadataReference[] references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("\t{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                }
            }
        }
    }
}

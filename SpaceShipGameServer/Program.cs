using System;
using System.Reflection;

namespace SpaceShipGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IocResolveStrategy.RegisterDependensies();

            //            string textCode = @"
            //using System;

            //namespace SpaceShipGame
            //{
            //    public class SimpleGeneratedClass
            //    {
            //        public SimpleGeneratedClass()
            //        {
            //        }

            //        public void Execute()
            //        {
            //           System.Console.WriteLine(""Hello World!\"" );
            //           System.Console.ReadLine();
            //        }
            //    }
            //}";
            //Assembly assembly;
            //CodeGenerator.CodeGenerator codeGenerator = new CodeGenerator.CodeGenerator(textCode, assembly);
            //codeGenerator.Execute();
            //Type t = codeGenerator.ResultProperty.CompiledAssembly.GetType("SpaceShipGame.SimpleGeneratedClass"); //.GetMethod("Execute").Invoke(null, null);
            //var obj = Activator.CreateInstance(t);
            //var method = t.GetMethod("Execute");
            //method.Invoke(obj, null);
        }
    }
}
 
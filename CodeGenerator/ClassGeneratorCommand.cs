using Interfaces;
using System;
using System.Globalization;
using System.Reflection;

namespace CodeGenerator
{
    public class ClassGeneratorCommand : ICommand
    {
        private string ClassName;
        private Type Type;
        private IUObject UObject;
        
        public object Instance { get; private set; }

        public ClassGeneratorCommand(string className, Type interfaceType, IUObject uobject)
        {
            ClassName = className;
            Type = interfaceType;
            UObject = uobject;
        }

        public void Execute()
        {
            string classCode = PrepareClass();
            var cg = new CodeGenerator(classCode);
            cg.Execute();
            Assembly assembly = cg.Assembly;

            var parameters = new object[1];
            parameters[0] = UObject;
            Instance = assembly.CreateInstance($"SpaceShipGame.{ClassName}", false, BindingFlags.Default, null, parameters, new CultureInfo("es-ES", false), new object[0]) as IMovable;
        }

        private string PrepareClass()
        {
            string paramsCode = string.Empty;
            string constructorCode = $@"
                    private IUObject UObject;

                    public {ClassName}(IUObject uObject)
                    {{
                        this.UObject = uObject;
                    }}
";

            if (Type.GetConstructors().Length > 0)
            {
                ConstructorInfo c = Type.GetConstructors()[0];
                string constructorParamsString = string.Empty;
                string constructorBody = string.Empty;

                foreach (var constructorParam in c.GetParameters())
                {
                    constructorParamsString += constructorParam.ParameterType.ToString() + " " + constructorParam.Name + ",";
                    paramsCode += constructorParam.ParameterType.ToString() + " " + constructorParam.Name + ";";
                    constructorBody += $"this.{constructorParam.Name} = {constructorParam.Name};";
                }

                constructorCode = $@"public {ClassName} ({constructorParamsString.TrimEnd(',')})";
                constructorCode += $"{{{constructorBody};}}";
            }

            string methodsCode = string.Empty;

            foreach (MethodInfo mi in Type.GetMethods())
            {
                string methodsParamsString = string.Empty;
                string methodCode;

                foreach (var methodParam in mi.GetParameters())
                {
                    methodsParamsString += methodParam.ParameterType.Name + " " + methodParam.Name + ",";
                }

                string accessor = "";
                
                if (mi.IsPublic)
                {
                    accessor = "public";
                }
                else if (mi.IsPrivate)
                {
                    accessor = "private";
                }

                methodCode = Environment.NewLine + $@"                    {accessor} {mi.ReturnType.Name} {mi.Name}({methodsParamsString.TrimEnd(',')})";

                var methodBodyString = string.Empty;
                
                if (mi.Name.ToLower().Contains("get"))
                {
                    methodBodyString = GenerateGetMethodBody(mi.Name, mi.ReturnType.Name);
                }
                else if (mi.Name.ToLower().Contains("set"))
                {
                    methodBodyString = GenerateSetMethodBody(mi.Name);
                }
                else
                {
                    methodBodyString = GemerateActionMethodBody(mi.Name);
                }

                methodCode += $@"
                    {{
                    {methodBodyString}
                    }}";

                methodsCode += Environment.NewLine + methodCode;
            }

            methodsCode = methodsCode.Replace("Void", "void");

            return $@"
            using Interfaces;
            using SimpleIoc;

            namespace SpaceShipGame
            {{
                public class {ClassName} : {Type.Name}
                {{
                    { constructorCode}  
                    
                    {methodsCode}
                }}
            }}";
        }

        private string GenerateGetMethodBody(string methodName, string returnType)
        {
            return $@"  return Ioc.Resolve<{returnType}>(""Operations.{Type.Name}.{methodName}"", this.UObject);";
        }

        private string GenerateSetMethodBody(string methodName)
        {
            return $@"  Ioc.Resolve<ICommand>(""Operations.{Type.Name}.{methodName}"", this.UObject);";
        }

        private string GemerateActionMethodBody(string methodName)
        {
            return $@"  Ioc.Resolve<ICommand>(""Operations.{Type.Name}.{methodName}"");";
        }
    }
}

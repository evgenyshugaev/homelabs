using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SimpleIoc
{
    /// <summary>
    /// Простой IoC контейнер.
    /// </summary>
    public static class Ioc
    {
        private static ThreadLocal<IScope> Scopes = new ThreadLocal<IScope>();

        private static string CurrentScope;

        static Ioc()
        {
            Scopes = new ThreadLocal<IScope>(() => { return new Scope(); });
        }

        public static T Resolve<T>(string key, params object[] args)
        {
            //T resolved = default(T);
            
            if (key.Contains("IoC.Register"))
            {
                Register<T>(args);
                return default(T);
            }

            // IoC.Resolve("Scopes.New", "scopeId").Execute();

            if (key.Contains("Scopes.New"))
            {
                Thread t = new Thread(RegisterNewScope);
                t.Start(args[0]);
                t.Join();
                return default(T);
            }

            if (key.Contains("Scopes.Current"))
            {
                CurrentScope = (string)args[0];
                return default(T);
            }

            if (!string.IsNullOrEmpty(CurrentScope) && !Scopes.Values.Any(v => v.ScopeName == CurrentScope))
            {
                throw new Exception($"Скоуп {CurrentScope} не найден.");
            }

            if (!string.IsNullOrEmpty(CurrentScope) 
                && !Scopes.Values.Any(v => v.ScopeName == CurrentScope)
                && !Scopes.Values.FirstOrDefault(v => v.ScopeName == CurrentScope).CheckKey(key))
            {
                throw new Exception($"Зависимость {key} не зарегистрирована в скоупе {CurrentScope}.");
            }

            if (string.IsNullOrEmpty(CurrentScope) && !Scopes.Value.CheckKey(key))
            {
                throw new Exception($"Зависимость {key} не зарегистрирована.");
            }

            return string.IsNullOrEmpty(CurrentScope)
                ? (T)Scopes.Value.GetFunc(key)(args)
                : (T)Scopes.Values.FirstOrDefault(v => v.ScopeName == CurrentScope).GetFunc(key)(args);
        }

        private static void Register<T>(params object[] args)
        {
            Scopes.Value.SetFunc((string)args[0], (Func<object[], object>)args[1]);
        }

        private static void RegisterNewScope(object scopeId)
        {
            string scopeName = (string)scopeId;
            Scope scope = new Scope();
            scope.ScopeName = scopeName;
            Scopes.Values.Add(scope);
        }
    }
}

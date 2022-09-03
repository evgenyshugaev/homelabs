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
        private static ThreadLocal<IScope> Scopes = new ThreadLocal<IScope>(true);
        private static List<string> ScopeNames = new List<string>();

        private static string CurrentScope;

        public static string CurrentScopeProperty 
        {
            get
            {
                return CurrentScope;
            }
        }

        static Ioc()
        {
            Scopes = new ThreadLocal<IScope>(() => { return new Scope(); }, true);
        }

        public static T Resolve<T>(string key, params object[] args)
        {
            if (key.Contains("IoC.Register"))
            {
                Register<T>(args);
                return default(T);
            }

            if (key.Contains("Adapter"))
            {
                key = ((Type)args[0]).Name.TrimStart('I') + "Adapter";
                args[0] = key;

                return string.IsNullOrEmpty(CurrentScope)
                    ? (T)Scopes.Value.GetFunc(key)(args)
                    : (T)Scopes.Values.FirstOrDefault(v => v.ScopeName == CurrentScope).GetFunc(key)(args);
            }

            if (key.Contains("Scopes.New"))
            {
                RegisterNewScope(args[0]);
                return default(T);
            }

            if (key.Contains("Scopes.Current"))
            {
                CurrentScope = (string)args[0];
                return default(T);
            }

            if (!string.IsNullOrEmpty(CurrentScope) && !ScopeNames.Any(v => v == CurrentScope))
                //!Scopes.Values.Any(v => v.ScopeName == CurrentScope))
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

        public static void ClearCurrentScope()
        {
            CurrentScope = null;
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
            Scopes.Values.Last().ScopeName = scopeName;
            ScopeNames.Add(scopeName);
        }
    }
}

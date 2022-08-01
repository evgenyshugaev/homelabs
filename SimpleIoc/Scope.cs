using Interfaces;
using System;
using System.Collections.Generic;

namespace SimpleIoc
{
    public class Scope : IScope
    {
        Dictionary<string, Func<object[], object>> scopes;

        public Scope()
        {
            scopes = new Dictionary<string, Func<object[], object>>();
        }

        public string ScopeName {get; set;}

        public Func<object[], object> GetFunc(string key)
        {
            if (!scopes.ContainsKey(key))
            {
                throw new Exception($"Ключ {key} не найден");
            }

            return scopes[key];
        }

        public void SetFunc(string key, Func<object[], object> func)
        {
            if (scopes.ContainsKey(key))
            {
                scopes[key] = func;
            }
            else
            {
                scopes.Add(key, func);
            }
        }

        public bool CheckKey(string key)
        {
            return scopes.ContainsKey(key);
        }
    }
}

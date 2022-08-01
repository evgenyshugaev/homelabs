using System;

namespace Interfaces
{
    /// <summary>
    /// Скоуп.
    /// </summary>
    public interface IScope
    {
        Func<object[], object> GetFunc(string key);

        void SetFunc(string key, Func<object[], object> func);

        bool CheckKey(string key);

        string ScopeName { get; set; }
    }
}

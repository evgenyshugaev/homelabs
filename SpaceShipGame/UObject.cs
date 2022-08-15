using Interfaces;
using System.Collections.Generic;

namespace SpaceShipGame
{
    public class UObject : IUObject
    {
        Dictionary<string, object> Values;

        public UObject()
        {
            Values = new Dictionary<string, object>();
        }
        
        public object GetProperty(string key)
        {
            return Values[key];
        }

        public void SetProperty(string key, object value)
        {
            Values.Add(key, value);
        }
    }
}

using Interfaces;

namespace SpaceShipGame
{
    /// <summary>
    /// Положить значение в UObject.
    /// </summary>
    public class SetPropertyCommand : ICommand
    {
        private IUObject Obj;
        private string Key;
        private object Value;

        public SetPropertyCommand(IUObject obj, string key, object value)
        {
            Obj = obj;
            Key = key;
            Value = value;
        }

        public void Execute()
        {
            Obj.SetProperty(Key, Value);
        }
    }
}

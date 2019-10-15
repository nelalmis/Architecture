using System.Data.Entity;

namespace Architecture.Base
{
    public abstract class ContextBase:DbContext
    {
        public ContextBase(string nameOrConnectionString) : base(nameOrConnectionString) { }
    }
}

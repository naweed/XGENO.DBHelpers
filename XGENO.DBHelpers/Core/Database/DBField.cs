using System;
using System.Reflection;

namespace XGENO.DBHelpers.Core.Database
{
    internal class DBField
    {
        public string PropertyName { get; set; }
        internal Type PropertyRuntimeType { get; set; }

        public string ColumnName { get; set; }

        internal MethodInfo PropertySetMethod { get; set; }

        public bool IsMapped { get; set; } = false;

        public DBField()
        {
        }
    }
}

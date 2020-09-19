using System;

namespace XGENO.DBHelpers.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    [Serializable]
    public sealed class ColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public ColumnAttribute()
        {
        }

        public ColumnAttribute(string _columnName)
        {
            this.Name = _columnName;
        }
    }
}

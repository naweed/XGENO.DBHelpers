using System;
using System.Collections.Generic;
using System.Reflection;
using XGENO.DBHelpers.Core.Attributes;
using XGENO.DBHelpers.Core.Database;

namespace XGENO.DBHelpers.Utilities
{
    internal static class Reflector
    {
        internal static T GetColumnAttribute<T>(PropertyInfo propertyInfo) where T : class
        {
            T _customAttribute;

            object[] _customAttributes = propertyInfo.GetCustomAttributes(typeof(T), true);

            foreach (object attribute in _customAttributes)
            {
                _customAttribute = attribute as T;

                if (_customAttribute != null)
                {
                    return _customAttribute;
                }
            }

            return default;
        }

        internal static List<DBField> GetObjectProperties<T>()
        {
            Type _objType = typeof(T);
            List<DBField> _dbFields = new List<DBField>();

            foreach (PropertyInfo propertyInfo in _objType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                ColumnAttribute _columnValue = GetColumnAttribute<ColumnAttribute>(propertyInfo);

                DBField _field = new DBField
                {
                    PropertyName = propertyInfo.Name,
                    PropertyRuntimeType = propertyInfo.PropertyType,
                    PropertySetMethod = propertyInfo.GetSetMethod(),
                    ColumnName = (_columnValue == null ? propertyInfo.Name : _columnValue.Name)
                };

                _dbFields.Add(_field);
            }

            return _dbFields;
        }

        internal static void SetObjectValue(object obj, MethodInfo methodInfo, object value, Type valueType)
        {
            Type _nonNullableType = Nullable.GetUnderlyingType(valueType) ?? valueType;
            object[] _params = new object[1] { value == DBNull.Value ? null : Convert.ChangeType(value, _nonNullableType) };

            methodInfo.Invoke(obj, _params);
        }

    }
}

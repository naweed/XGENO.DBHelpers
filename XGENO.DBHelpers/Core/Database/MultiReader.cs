using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using XGENO.DBHelpers.Utilities;

namespace XGENO.DBHelpers.Core.Database
{
    public class MultiReader : IDisposable
    {
        private int _currTableCount;
        private DataSet _dsData;

        internal MultiReader(DataSet fullDataSet)
        {
            _dsData = fullDataSet;
            _currTableCount = 0;
        }

        public List<T> Read<T>()
        {
            List<T> _lstObjects = new List<T>();

            DataTable _dtTable = _dsData.Tables[_currTableCount];

            _currTableCount++;

            //Get Properties List
            var _dbFields = Reflector.GetObjectProperties<T>();

            //Map to DB Output
            foreach (DBField field in _dbFields)
            {
                if (_dtTable.Columns.Contains(field.ColumnName))
                {
                    field.IsMapped = true;
                }
            }

            foreach (DataRow dRow in _dtTable.Rows)
            {
                T _obj = System.Activator.CreateInstance<T>();

                //Set each Parameter value
                foreach (DBField field in _dbFields.Where(f => f.IsMapped == true))
                {
                    Reflector.SetObjectValue(_obj, field.PropertySetMethod, dRow[field.ColumnName], field.PropertyRuntimeType);
                }

                _lstObjects.Add(_obj);
            }

            return _lstObjects;
        }

        public void Dispose()
        {
        }
    }
}

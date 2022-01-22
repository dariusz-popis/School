namespace School.DataAccess.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using static Dapper.SqlMapper;

    public static class DataTableMapper
    {
        public static ICustomQueryParameter Map<T>(this IEnumerable<T> tableValueParameters, string[] names, string tableValueParameterName)
        {
            if (tableValueParameters is null) throw new ArgumentNullException(nameof(tableValueParameters));

            if (tableValueParameterName is null) throw new ArgumentNullException(nameof(tableValueParameterName));

            var properties = typeof(T).GetProperties();

            var dt = new DataTable();
            var propCount = names.Length;
            var rowObjects = new object[propCount];

            for (int i = 0; i < propCount; i++) dt.Columns.Add(names[i], properties[i].PropertyType);

            foreach (var tvpItem in tableValueParameters)
            {
                for (int i = 0; i < propCount; i++)
                {
                    rowObjects[i] = properties[i].GetValue(tvpItem);
                }

                dt.Rows.Add(rowObjects);
            }

            return dt.AsTableValuedParameter(tableValueParameterName);
        }
    }
}

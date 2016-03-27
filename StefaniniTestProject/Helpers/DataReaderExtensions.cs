using System;
using System.Data;
using System.Collections.Generic;

namespace StefaniniTestProject.Helpers
{
    public static class DataReaderExtensions
    {
        public static IEnumerable<T> Select<T>(this IDataReader reader, Func<IDataReader, T> func)
        {
            while(reader.Read())
            {
                yield return func(reader);
            }
        }
    }
}
using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace exposure_extensions
{
    public static class Common
    {

        public static byte[] GetBytes(this Stream body)
        {
            using MemoryStream ms = new MemoryStream();
            body.CopyToAsync(ms);
            return ms.ToArray();
        }

        public static DataTable ConvertToDatatable<T>(this List<T> data, params string[] members)
        {
            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(data, members))
            {
                table.Load(reader);
            }
            return table;
        }
    }
}

using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinemaddict.Services
{
    public static class Extension
    {
        public static List<int> ToIntList(this string[] array)
        {
            if(array.Length==0)
            {
                return new List<int>();
            }
            return array.Select(x => int.Parse(x)).ToList();
        }
        public static List<object> ToListObjFromFirebase(this FirebaseObject<List<object>> item)
        {
            return new List<object>(item.Object.Select(x=> x).ToList());
        }
        public static string ToStringFromIntList(this List<int> list)
        {
            return string.Concat(list.Select(x => x + ";"));
        }
    }
}

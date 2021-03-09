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
            var clearArray = array.ToList();
            clearArray.RemoveAll(x => x == "");
            if (clearArray.Count<2)
            {
                return new List<int>();
            }
            return clearArray.Select(x => int.Parse(x)).ToList();
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

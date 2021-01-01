using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop.Model
{
    public class Global
    {
        public static int SelectedID = 0;

        public class listID
        {
            private int _id = 0;

            public int ID
            {
                get { return _id; }
                set { _id = value; }
            }
            private int _sl = 0;

            public int SL
            {
                get { return _sl; }
                set { _sl = value; }
            }

            public listID(int id, int sl)
            {
                ID = id;
                SL = sl;
            }
        }

        public static class Cart
        {
            private static List<listID> _list;

            public static List<listID> List
            {
                get
                { 
                    if(_list == null)
                    {
                        _list = new List<listID>();
                    }
                    return _list;
                }
                set { _list = value; }
            }

            public static void Add(int id)
            {
                if(!List.Any(x => x.ID == id))
                {
                    List.Add(new listID(id, 1));
                }
                else
                {
                    foreach(var item in List.Where(x => x.ID == id))
                    {
                        item.SL++;
                    }
                }
            }

            public static void Incre(int id)
            {
                foreach (var item in List.Where(x => x.ID == id))
                {
                    item.SL++;
                }
            }

            public static void Decre(int id)
            {
                if(List.Where(x => x.ID == id).SingleOrDefault().SL == 1)
                {
                    List.RemoveAll(x => x.ID == id);
                }
                else
                {
                    foreach (var item in List.Where(x => x.ID == id))
                    {
                        item.SL--;
                    }
                }
            }

            public static void Delete(int id)
            {
                List.RemoveAll(x => x.ID == id);
            }
        }
    }
}

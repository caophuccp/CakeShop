using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class Cake
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
    }

    public class CakeDAO
    {
        private static string ReadFile()
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "cake.json";
            string result = File.ReadAllText(filepath);
            return result;
        }
        public static List<Cake> GetAll()
        {
            string jsonString = ReadFile();
            var result = JsonConvert.DeserializeObject<List<Cake>>(jsonString) ?? new List<Cake>();
            Cache = result;
            return result;
        }

        public static bool Save(List<Cake> all)
        {
            var result = false;
            try
            {
                string jsonString = JsonConvert.SerializeObject(all, Formatting.Indented);
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "cake.json", jsonString);
                result = true;
                Cache = all;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            return result;
        }
        public static bool Insert(Cake cake)
        {
            var oldID = cake.ID;
            var list = GetAll();
            cake.ID = list.Count == 0 ? 0 : list[list.Count - 1].ID + 1;
            list.Add(cake);
            bool result = Save(list);
            if (!result)
            {
                cake.ID = oldID;
            } else
            {
                Cache = list;
            }
            return result;
        }

        public static int Delete(int id)
        {
            int result = 0;
            var list = GetAll();
            result = list.RemoveAll(e => e.ID == id);
            Save(list);
            return result;
        }

        public static bool Update(Cake cake)
        {
            var result = false;
            var list = GetAll();
            var oldCakeIndex = list.FindIndex(e => e.ID == cake.ID);

            if (oldCakeIndex >= 0 && oldCakeIndex < list.Count)
            {
                list[oldCakeIndex] = cake;
                result = Save(list);
            }

            return result;
        }

        public static List<Cake> Cache = GetAll();
    }
}

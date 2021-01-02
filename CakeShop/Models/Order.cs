using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace OrderShop.Models
{

    public class OrderCake
    {
        public int OrderID { get; set; }
        public int Quantity { get; set; }
    }

    public class Order
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<OrderCake> Products { get; set; }
        public double Total { get; set; }
    }

    public class OrderDAO
    {
        private static string ReadFile()
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "order.json";
            string result = File.ReadAllText(filepath);
            return result;
        }
        public static List<Order> GetAll()
        {
            string jsonString = ReadFile();
            var result = JsonConvert.DeserializeObject<List<Order>>(jsonString) ?? new List<Order>();
            return result;
        }

        public static bool Save(List<Order> all)
        {
            var result = false;
            try
            {
                string jsonString = JsonConvert.SerializeObject(all, Formatting.Indented);
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "data.json", jsonString);
                result = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            return result;
        }
        public static bool Insert(Order order)
        {
            var result = false;
            var oldID = order.ID;
            var list = GetAll();
            order.ID = list.Count == 0 ? 0 : list[list.Count - 1].ID + 1;
            list.Add(order);
            result = Save(list);
            if (!result)
            {
                order.ID = oldID;
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

        public static bool Update(Order order)
        {
            var result = false;
            var list = GetAll();
            var oldOrderIndex = list.FindIndex(e => e.ID == order.ID);

            if (oldOrderIndex >= 0 && oldOrderIndex < list.Count)
            {
                list[oldOrderIndex] = order;
                result = Save(list);
            }

            return result;
        }
    }
}

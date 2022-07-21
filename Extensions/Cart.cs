using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BikeShop.Extensions
{
    public class Cart : Dictionary<int, int>
    {
        public int PushItem(int productId)
        {
            if(ContainsKey(productId))
            {
                this[productId] += 1;
            }
            else
            {
                this.Add(productId, 1);
            }
            return GetSize();
        }

        public int RemoveItem(int productId)
        {
            if (ContainsKey(productId))
            {
                this[productId] -= 1;
                if(this[productId] == 0)
                {
                    this.Remove(productId);
                }
            }
            
            return GetSize();
        }

        public int GetSize()
        {
            int size = 0;
            foreach(var item in this)
            {
                size += item.Value;
            }
            return size;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Cart Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Cart>(json);
        }
    }
}

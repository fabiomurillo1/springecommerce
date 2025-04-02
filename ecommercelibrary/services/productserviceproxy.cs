using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using springecommerce.models;

namespace ecommercelibrary.services
{
    public class productserviceproxy
    {
        private productserviceproxy() 
        { 
            
        }

        private static productserviceproxy? instance;
        private static object instanceLock = new object();
        public static productserviceproxy? Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new productserviceproxy();
                    }
                    return instance;
                }
            }
        }
        private List<Product?> list = new List<Product?>();
        public List<Product?> Products => list;
        
    }
}

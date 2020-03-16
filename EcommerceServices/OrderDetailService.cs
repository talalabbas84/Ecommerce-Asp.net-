using EcommerceDatabase;
using EcommerceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceServices
{
    public class OrderDetailService
    {

        public void SaveOrderDetail(OrderDetail orderDetail)
        {
            using (var context = new Db())
            {

                context.OrderDetails.Add(orderDetail);
                context.SaveChanges();
            }
        }

    }
}

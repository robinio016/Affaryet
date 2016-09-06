using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BLL
{
    public class AdminAreaBs : BaseBs 
    {
        public void ApproveOrReject(List<int>Ids,string status)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    foreach(var item in Ids)
                    {
                        var prod = productBs.GetProductByID(item);
                        prod.IsApproved = status;
                        productBs.UpdateProduct(prod);

                    }
                    trans.Complete();
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}

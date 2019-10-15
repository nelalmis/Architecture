using System;
using System.Activities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WCFWorkFlow
{
    public class TransactionBegin : CodeActivity
    {
        public InArgument<SqlConnection> Connection { get; set; }
        public OutArgument<SqlTransaction> Transaction { get; set; }
        public InArgument<bool> IsTransactionRequestBase { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                var connection = Connection.Get<SqlConnection>(context);
                var doRoolBack = IsTransactionRequestBase.Get<bool>(context);

                var transaction = connection.BeginTransaction();
                Transaction.Set(context, transaction);
            }
            catch
            {

            }
        }
    }
}
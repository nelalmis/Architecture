using System;
using System.Activities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WCFWorkFlow
{
    public class TransactionEnd : CodeActivity
    {
        public InArgument<SqlConnection> Connection { get; set; }
        public InArgument<SqlTransaction> Transaction { get; set; }
        public InArgument<bool> IsTransactionRequestBase { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                var connection = Connection.Get<SqlConnection>(context);
                var transaction = Transaction.Get<SqlTransaction>(context);
                var doRoolBack = IsTransactionRequestBase.Get<bool>(context);
                if (doRoolBack)
                {
                    transaction.Rollback();

                }
                else
                {
                    transaction.Commit();
                }
                connection.Close();
                connection.Dispose();
                transaction.Dispose();
            }
            catch
            {

            }
        }
    }
}

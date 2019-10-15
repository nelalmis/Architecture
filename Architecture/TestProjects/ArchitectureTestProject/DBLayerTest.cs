using Architecture.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Architecture.Test.Base
{
    [TestClass()]
    public class DBLayerTest
    {
        DBLayer target = new DBLayer();

        [TestMethod()]
        public void GetDBCommandTest()
        {
            for (int i = 0; i < 1000; i++)
            {
                var target = this.target.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.sel_ParameterByColumns");
                Assert.IsNotNull(target);
            }
        }

        [TestMethod()]
        public void AddInParameterTest()
        {
            SqlCommand command = new SqlCommand();
            target.AddInParameter(command, "fname", System.Data.SqlDbType.NVarChar, "Jon");
            var actual=command.Parameters.Count;
            Assert.AreEqual(1, actual);
            
        }

        [TestMethod()]
        public void ExecuteReaderTest()
        {
            SqlCommand command;
            
            for (int i = 0; i < 10; i++)
            {
                command = target.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.sel_ParameterByColumns");
                Assert.IsNotNull(command);
                
                var sp = target.ExecuteReader(command);
                Assert.IsTrue(sp.Success);

                while (sp.Value.Read())
                {
                    var val = (string)sp.Value["ParamType"];
                    Assert.IsNotNull(val);
                }
            }
            for (int i = 0; i < 10; i++)
            {
                command = target.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.sel_ParameterByColumns");
                Assert.IsNotNull(command);

                var sp = target.ExecuteReader(command);
                Assert.IsTrue(sp.Success);

                while (sp.Value.Read())
                {
                    var val = (string)sp.Value["ParamType"];
                    Assert.IsNotNull(val);
                }
            }


        }

        [TestMethod()]
        public void ExecuteNonQueryTest()
        {
            //Aynı ssn OLABİLİR
            SqlCommand command = target.GetDBCommand(SQLDBHelper.Databases.Company, "ins_Deneme");
            Assert.IsNotNull(command);

            target.AddInParameter(command, "@Ad", SqlDbType.NVarChar, "test");
            target.AddOutParameter(command, "@Id", SqlDbType.Int);

            var actual = target.ExecuteNonQuery(command);
            Assert.AreEqual(0, actual.Results.Count);

            var a = command.Parameters["@Id"].Value;
            Assert.IsNotNull(a);

        }
    }
}
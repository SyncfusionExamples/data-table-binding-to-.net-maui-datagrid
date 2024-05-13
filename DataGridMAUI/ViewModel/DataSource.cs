using System.Data;

namespace DataGridMAUI
{
    internal class DataSource
    {
        internal DataTable CustomerInfoTable { get; set; }
        public DataSource()
        {
            CustomerInfoTable = GetDataTable();
            CustomerInfoTable.TableName = "CustomerInfo";
        }
        internal void AddNewItem(CustomerInfo DataFormObject)
        {
            var dataRow = CustomerInfoTable.NewRow();
            dataRow.SetField("OrderID", DataFormObject.OrderID);
            dataRow.SetField("CustomerID", DataFormObject.CustomerID);
            dataRow.SetField("CustomerName", DataFormObject.CustomerName);
            dataRow.SetField("Country", DataFormObject.Country);
            CustomerInfoTable.Rows.Add(dataRow);
        }
        internal void OnDeleteItem(DataRow selectedItem)
        {
            CustomerInfoTable.Rows.Remove(selectedItem);
        }
        internal void OnSaveItem(DataRow selectedItem, CustomerInfo customerInfo)
        {
            if (selectedItem == null)
                return;
            selectedItem.SetField("OrderID", customerInfo.OrderID);
            selectedItem.SetField("CustomerID", customerInfo.CustomerID);
            selectedItem.SetField("CustomerName", customerInfo.CustomerName);
            selectedItem.SetField("Country", customerInfo.Country);
        }
        internal void EditCustomerInfo(CustomerInfo customerInfo, DataRow dataRow)
        {
            customerInfo.OrderID = (int)dataRow["OrderID"];
            customerInfo.CustomerID = (string)dataRow["CustomerID"];
            customerInfo.CustomerName = (string)dataRow["CustomerName"];
            customerInfo.Country = (string)dataRow["Country"];
        }
        internal DataTable GetDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("OrderID", typeof(int));
            dataTable.Columns.Add("CustomerName", typeof(string));
            dataTable.Columns.Add("CustomerID", typeof(string));
            dataTable.Columns.Add("Country", typeof(string));
            dataTable.Rows.Add(1001, "Maria Anders", "ALFKI", "Germany");
            dataTable.Rows.Add(1002, "Ana Trujilo", "ANATR", "Mexico");
            dataTable.Rows.Add(1003, "Antonio Moreno", "ENDGY", "Mexico");
            dataTable.Rows.Add(1004, "Thomas Hardy", "ANTON", "UK");
            dataTable.Rows.Add(1005, "Christina Berglund", "BERGS", "Sweden");
            dataTable.Rows.Add(1006, "Hanna Moos", "BLAUS", "Germany");
            dataTable.Rows.Add(1007, "Frederique Citeaux", "BLONP", "France");
            dataTable.Rows.Add(1008, "Martin Sommer", "BOLID", "Spain");
            dataTable.Rows.Add(1009, "Laurence Lebihan", "BONAP", "France");
            dataTable.Rows.Add(1010, "Kathryn", "BOTTM", "Canada");
            dataTable.Rows.Add(1011, "Tamer", "XDKLF", "UK");
            dataTable.Rows.Add(1012, "Martin", "QEUDJ", "US");
            dataTable.Rows.Add(1013, "Nancy", "ALOPS", "France");
            dataTable.Rows.Add(1014, "Janet", "KSDIO", "Canada");
            dataTable.Rows.Add(1015, "Dodsworth", "AWSDE", "Canada");
            dataTable.Rows.Add(1016, "Buchanan", "CDFKL", "Germany");
            dataTable.Rows.Add(1017, "Therasa", "WSCJD", "Canada");
            dataTable.Rows.Add(1018, "Margaret", "PLSKD", "UK");
            dataTable.Rows.Add(1019, "Anto", "CCDSE", "Sweden");
            dataTable.Rows.Add(1020, "Edward", "EWUJG", "Germany");
            dataTable.Rows.Add(1021, "Anne", "AWSDK", "US");
            dataTable.Rows.Add(1022, "Callahan", "ODKLF", "UK");
            dataTable.Rows.Add(1023, "Vinet", "OEDKL", "France");
            return dataTable;
        }
    }
}

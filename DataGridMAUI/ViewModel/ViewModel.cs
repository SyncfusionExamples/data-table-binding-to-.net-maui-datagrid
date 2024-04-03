using System.ComponentModel;
using System.Data;

namespace DataGridMAUI
{
    public class ViewModel : INotifyPropertyChanged
    {
        private DataView customerInfoView;
        private string selectedColumn;
        private string selectedCondition;
        private DataRow selectedItem;
        private CustomerInfo dataFormObject;

        public ViewModel()
        {                      
            // DataSource                 
            DataSource = new DataSource();
            CustomerInfoView = new DataView();
            CustomerInfoView.Table = DataSource.CustomerInfoTable;

            // Data operations

            FilteringCommand = new Command<string>(OnFilterTextChanged);
            PopulateFilterOptions();

            // CRUD operations
            CreateCustomerCommand = new Command(OnCreateCustomerInfo);

            EditCustomerCommand = new Command<object>(OnEditCustomerInfo);
            SaveItemCommand = new Command(OnSaveItem);
            DeleteItemCommand = new Command(OnDeleteItem);
            AddItemCommand = new Command(OnAddNewItem);
        }
       

        public event PropertyChangedEventHandler? PropertyChanged;

        // DataGrid source
        public DataView CustomerInfoView
        {
            get { return customerInfoView; }
            set
            {
                customerInfoView = value;
                RaisePropertyChanged("CustomerInfoView");
            }
        }

        // DataForm source
        public CustomerInfo DataFormObject
        {
            get { return dataFormObject; }
            set
            {
                dataFormObject = value;
                RaisePropertyChanged(nameof(DataFormObject));
            }
        }

        // Filtering options and commands.
        public List<string> Columns { get; set; }
        public List<string> Conditions { get; set; }
        public Command<string> FilteringCommand { get; set; }

        // Data operations commands
        public Command CreateCustomerCommand { get; set; }
        public Command<object> EditCustomerCommand { get; set; }
        public Command SaveItemCommand { get; set; }
        public Command DeleteItemCommand { get; set; }
        public Command AddItemCommand { get; set; }

        // Filtering options selected item.
        public string SelectedColumn
        {
            get
            {
                return selectedColumn;
            }
            set
            {
                selectedColumn = value;
                RaisePropertyChanged(nameof(SelectedColumn));
            }
        }

        public string SelectedCondition
        {
            get
            {
                return selectedCondition;
            }
            set
            {
                selectedCondition = value;
                RaisePropertyChanged(nameof(SelectedCondition));
            }
        }

        // DataGrid selected item
        public DataRow SelectedItem
        {
            get
            {
                return selectedItem;
            }

            set
            {
                selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }
     
        internal DataSource DataSource { get; set; }

        #region Filtering
        private void PopulateFilterOptions()
        {
            Conditions = new List<string>();
            Columns = new List<string>();

            Columns.Add("All columns");
            Columns.Add("OrderID");
            Columns.Add("CustomerID");
            Columns.Add("CustomerName");
            Columns.Add("Country");


            Conditions.Add("Equals");
            Conditions.Add("Does not Equal");
            Conditions.Add("Contains");

            SelectedColumn = "OrderID";
            selectedCondition = "Equals";
        }
        private void OnFilterTextChanged(string e)
        {
            if (!string.IsNullOrEmpty(e))
                CustomerInfoView.RowFilter = GetFilterString(e);
            else
                CustomerInfoView.RowFilter = "";
        }

        public string GetFilterString(string FilterText)
        {
            var filterText = FilterText.ToLower();
            var rowFilter = string.Empty;

            if (SelectedColumn != null)
            {
                if (SelectedColumn.ToString() == "All columns")
                {
                    if (SelectedColumn != null)
                    {
                        if (SelectedCondition.ToString() == "Contains")
                        {
                            rowFilter = "(Convert(OrderID, 'System.String') LIKE '" + filterText + "%') OR (Convert(CustomerID, 'System.String') LIKE '" + filterText + "%') OR (Convert(CustomerName, 'System.String') LIKE '" + filterText + "%') OR (Convert(Country, 'System.String') LIKE '" + filterText + "%')";
                            return rowFilter;
                        }
                        else if (SelectedCondition.ToString() == "Equals")
                        {
                            rowFilter = "(Convert(OrderID, 'System.String') LIKE '" + filterText + "') OR (Convert(CustomerID, 'System.String') LIKE '" + filterText + "') OR (Convert(CustomerName, 'System.String') LIKE '" + filterText + "') OR (Convert(Country, 'System.String') LIKE '" + filterText + "')";
                            return rowFilter;
                        }
                        else
                        {
                            rowFilter = "(Convert(OrderID, 'System.String') NOT LIKE '" + filterText + "') AND (Convert(CustomerID, 'System.String') NOT LIKE '" + filterText + "') AND (Convert(CustomerName, 'System.String') NOT LIKE '" + filterText + "') AND (Convert(Country, 'System.String') NOT LIKE '" + filterText + "')";
                            return rowFilter;
                        }
                    }
                    return rowFilter;
                }
                else
                {
                    if (SelectedCondition != null)
                    {
                        var columnName = SelectedColumn.ToString();
                        if (SelectedCondition.ToString() == "Contains")
                        {
                            if (!string.IsNullOrEmpty(columnName) && columnName.Equals("OrderID"))
                                rowFilter = "(Convert(OrderID, 'System.String') LIKE '" + filterText + "%')";
                            else
                                rowFilter = columnName + " LIKE '" + filterText + "%'";

                        }
                        else if (SelectedCondition.ToString() == "Equals")
                        {
                            if (!string.IsNullOrEmpty(columnName) && columnName.Equals("OrderID"))
                                rowFilter = "(Convert(OrderID, 'System.String') LIKE '" + filterText + "')";
                            else
                                rowFilter = columnName + " LIKE '" + filterText + "'";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(columnName) && columnName.Equals("OrderID"))
                                rowFilter = "(Convert(OrderID, 'System.String') NOT LIKE '" + filterText + "')";
                            else
                                rowFilter = columnName + " NOT LIKE '" + filterText + "'";
                        }
                    }
                }
            }
            return rowFilter;
        }

        #endregion

        #region CRUD commands
        private async void OnAddNewItem()
        {                     
            DataSource.AddNewItem(DataFormObject);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        private async void OnDeleteItem()
        {            
            DataSource.OnDeleteItem(SelectedItem);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        private async void OnSaveItem()
        {
            DataSource.OnSaveItem(SelectedItem, DataFormObject);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        private void OnEditCustomerInfo(object obj)
        {
            var rowData = ((obj as Syncfusion.Maui.DataGrid.DataGridCellTappedEventArgs).RowData as DataRowView);
            if (rowData == null)
                return;

            SelectedItem = (rowData as DataRowView).Row;
            DataFormObject = new CustomerInfo();
            DataSource.EditCustomerInfo(DataFormObject, SelectedItem);
            var editPage = new EditPage();
            editPage.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(editPage);
        }
        private async void OnCreateCustomerInfo()
        {
            DataFormObject = new CustomerInfo();
            var editPage = new EditPage();
            editPage.BindingContext = this;
            await Application.Current.MainPage.Navigation.PushAsync(editPage);
        }
        #endregion

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }        
    }
}

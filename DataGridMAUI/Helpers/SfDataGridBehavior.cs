using Syncfusion.Maui.DataGrid;
namespace DataGridMAUI
{
    public class SfDataGridBehavior : Behavior<SfDataGrid>
    {
        SfDataGrid dataGrid;
        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            dataGrid = bindable as SfDataGrid;            
            dataGrid.CellTapped += OnDataGridCellTapped;
            dataGrid.AutoGeneratingColumn += OnDataGridAutoGeneratingColumn;
        }

        private void OnDataGridAutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.MappingName == "OrderID")
                e.Column.HeaderText = "Order ID";
            if (e.Column.MappingName == "CustomerID")
                e.Column.HeaderText = "Customer ID";
            if (e.Column.MappingName == "CustomerName")
                e.Column.HeaderText = "Customer Name";
        }

        private void OnDataGridCellTapped(object? sender, DataGridCellTappedEventArgs e)
        {
            var bindingContext = dataGrid.BindingContext as ViewModel;
            if (bindingContext != null)
            {
                bindingContext.EditCustomerCommand.Execute(e);
            }
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);
            dataGrid.CellTapped -= OnDataGridCellTapped;
            dataGrid = null;
        }
    }
}

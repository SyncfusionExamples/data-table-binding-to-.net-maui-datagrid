# data-table-binding-to-.net-maui-datagrid
This guide explains how to bind a DataTable to the .NET MAUI DataGrid (SfDataGrid) and perform basic CRUD operations (create, read, update, delete). You will learn how to create a DataTable in code, connect it to the grid, define the necessary columns, and wire up commands for adding, editing, and removing rows. For more background, see the official user guide: [Data binding in .NET MAUI DataGrid](https://help.syncfusion.com/maui/datagrid/data-binding) and the overview page: [.NET MAUI DataGrid]( https://www.syncfusion.com/maui-controls/maui-datagrid).

## xaml
```
<ContentPage.BindingContext>
    <local:ViewModel/>
</ContentPage.BindingContext>

<StackLayout>
    <!-- Optional: filter UI (columns, conditions, search) -->
    <StackLayout Orientation="Horizontal">
        <Label Text="Filter Options:" VerticalOptions="Center" Margin="3"/>
        <Picker Margin="3" WidthRequest="200" ItemsSource="{Binding Columns}" SelectedItem="{Binding SelectedColumn}"/>
        <Picker Margin="3" WidthRequest="200" ItemsSource="{Binding Conditions}" SelectedItem="{Binding SelectedCondition}"/>
    </StackLayout>

    <SearchBar x:Name="filterText"
                Placeholder="Search here to filter"
                SearchCommand="{Binding FilteringCommand}"
                SearchCommandParameter="{Binding Source={x:Reference filterText}, Path=Text}" />

    <Grid>
        <syncfusion:SfDataGrid x:Name="dataGrid"
                                ColumnWidthMode="Fill"
                                ItemsSource="{Binding DataTableView}">
            <!--
                If AutoGenerateColumnsMode is None, define columns explicitly matching DataTable columns
                <syncfusion:SfDataGrid.Columns>
                    <syncfusion:DataGridTextColumn MappingName="CustomerID" HeaderText="ID" />
                    <syncfusion:DataGridTextColumn MappingName="Company" HeaderText="Company" />
                    <syncfusion:DataGridTextColumn MappingName="ContactName" HeaderText="Contact Name" />
                    <syncfusion:DataGridTextColumn MappingName="City" HeaderText="City" />
                </syncfusion:SfDataGrid.Columns>
            -->
        </syncfusion:SfDataGrid>

        <!-- Create/Add row -->
        <ImageButton Margin="20"
                        CornerRadius="20"
                        HeightRequest="40" WidthRequest="40"
                        VerticalOptions="End" HorizontalOptions="End"
                        Command="{Binding CreateCustomerCommand}"
                        Source="add.png"/>
    </Grid>
</StackLayout>
```

## C#
The ViewModel creates a DataTable, exposes it through a bindable view (for example, DataView or a wrapper), and provides commands for CRUD. The important details are:
- Create DataTable columns with the correct DataColumn names.
- Assign the DataTable (or DataView) to the grid’s ItemsSource.
- Keep MappingName values identical to your DataColumn names when defining columns explicitly.
- Use commands to add, edit, and delete rows so the UI updates immediately.

## CRUD wiring tips
- Add button: Bind to CreateCustomerCommand (as shown in MainPage.xaml ImageButton). You can also place command buttons in a DataGridTemplateColumn for per-row actions.
- Update: Use selection or an Edit dialog, then modify the DataRow. Remember to notify the grid (raise PropertyChanged on the ItemsSource property if needed).
- Delete: Pass the current row (DataRowView) as CommandParameter to DeleteSelectedCommand.
- Sorting/Grouping: The grid supports SortColumnDescriptions and GroupColumnDescriptions. Enable these to quickly analyze DataTable content.


## Example explicit columns
```
<syncfusion:SfDataGrid.Columns>
    <syncfusion:DataGridTextColumn MappingName="CustomerID" HeaderText="ID"/>
    <syncfusion:DataGridTextColumn MappingName="Company" HeaderText="Company"/>
    <syncfusion:DataGridTextColumn MappingName="ContactName" HeaderText="Contact Name"/>
    <syncfusion:DataGridTextColumn MappingName="City" HeaderText="City"/>
</syncfusion:SfDataGrid.Columns>
```

##### Conclusion
 
I hope you enjoyed learning about DataTable binding and CRUD operations in .NET MAUI DataGrid (SfDataGrid).
 
You can refer to our [.NET MAUI DataGrid’s feature tour](https://www.syncfusion.com/maui-controls/maui-datagrid) page to learn about its other groundbreaking feature representations. You can also explore our [.NET MAUI DataGrid Documentation](https://help.syncfusion.com/maui/datagrid/getting-started) to understand how to present and manipulate data. 
For current customers, you can check out our .NET MAUI components on the [License and Downloads](https://www.syncfusion.com/sales/teamlicense) page. If you are new to Syncfusion, you can try our 30-day [free trial](https://www.syncfusion.com/downloads/maui) to explore our .NET MAUI DataGrid and other .NET MAUI components.
 
If you have any queries or require clarifications, please let us know in the comments below. You can also contact us through our [support forums](https://www.syncfusion.com/forums), [Direct-Trac](https://support.syncfusion.com/create) or [feedback portal](https://www.syncfusion.com/feedback/maui?control=sfdatagrid), or the feedback portal. We are always happy to assist you!

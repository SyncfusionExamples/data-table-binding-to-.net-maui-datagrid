namespace DataGridMAUI
{
    public class SearchBarBehavior : Behavior<SearchBar>
    {
        SearchBar searchBar;
        ViewModel viewModel;
        protected override void OnAttachedTo(SearchBar bindable)
        {
            base.OnAttachedTo(bindable);
            searchBar = bindable as SearchBar;
            searchBar.TextChanged += OnFilterTextChanged;
        }

        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel == null)
                viewModel = searchBar.BindingContext as ViewModel;
            viewModel.FilteringCommand.Execute(e.NewTextValue);
        }

        protected override void OnDetachingFrom(SearchBar bindable)
        {
            base.OnDetachingFrom(bindable);
            searchBar.TextChanged -= OnFilterTextChanged;
            searchBar = null;
            viewModel = null;
        }
    }
}

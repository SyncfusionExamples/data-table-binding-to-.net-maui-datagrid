using System.ComponentModel;

namespace DataGridMAUI
{
    public class CustomerInfo :INotifyPropertyChanged
    {
        private int orderID;
        private string customerID;
        private string customerName;
        private string country;

        public int OrderID
        {
            get { return orderID; }
            set
            {
                orderID = value;
                RaisePropertyChanged(nameof(orderID));
            }
        }

        public string CustomerID
        {
            get { return customerID; }
            set
            {
                customerID = value;
                RaisePropertyChanged(nameof(customerID));
            }
        }

        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
                RaisePropertyChanged(nameof(customerName));
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                RaisePropertyChanged(nameof(country));
            }
        }
        public CustomerInfo()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if(this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

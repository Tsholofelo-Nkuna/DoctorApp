using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Components.Layout
{
    public partial class MainLayout
    {
        private bool _isConnectedToInternet;
        public bool AppIsConnectedToInternet
        {
            set => _isConnectedToInternet = value;
            get
            {

                return _isConnectedToInternet;
            }
           
        }

        public MainLayout()
        {
            Connectivity.Current.ConnectivityChanged += this.OnConnectivityChange;

        }
        protected override Task OnInitializedAsync()
        {
            AppIsConnectedToInternet = Connectivity.NetworkAccess == NetworkAccess.Internet;
            return base.OnInitializedAsync();
        }

        public void OnConnectivityChange(object sender, ConnectivityChangedEventArgs args)
        {
            this.AppIsConnectedToInternet = args.NetworkAccess == NetworkAccess.Internet;
            StateHasChanged();
        }
        ~MainLayout()
        {
            Connectivity.Current.ConnectivityChanged -= this.OnConnectivityChange;
        }
    }
}

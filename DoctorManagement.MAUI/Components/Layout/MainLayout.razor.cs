using DoctorManagement.Shared.Constants;
using Microsoft.AspNetCore.Components;
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
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool AppIsConnectedToInternet
        {
            set => _isConnectedToInternet = value;
            get
            {

                return _isConnectedToInternet;
            }
           
        }

        public bool IsLoginPage
        { 
           get
            {
                return this.NavigationManager.Uri == this.NavigationManager.ToAbsoluteUri(RouteConstants.Login).AbsoluteUri;
            }
        }
            
        public IEnumerable<(string Url, int Index, string Text, string Icon)> NavItems
            = [
                (RouteConstants.Doctors, 0, "Health workers", "bi bi-hospital"),
                (RouteConstants.Login, 1, "My appointments", "bi bi-calendar")
              ];

        public int ActiveIndex { get; set; }
        public MainLayout()
        {
            Connectivity.Current.ConnectivityChanged += this.OnConnectivityChange;

        }

        public Task OnTabLinkClicked((string Url, int Index, string Text, string Icon) item)
        {
            this.ActiveIndex = item.Index;
            this.NavigationManager.NavigateTo(item.Url);
           
            return Task.CompletedTask;
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

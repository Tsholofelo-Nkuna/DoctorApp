
using DoctorManagement.Shared.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

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

        public static bool RenderPatientLayout { get; set; } = true;
        public IEnumerable<string> NavBarPages 
        { 
           get
            {
                return [
                     this.NavigationManager.ToAbsoluteUri(RouteConstants.Doctors).AbsoluteUri,
                     this.NavigationManager.ToAbsoluteUri(RouteConstants.PatientAppointments).AbsoluteUri,
                    ];
            }
        }

        public IEnumerable<(string Url, int Index, string Text, string Icon)> NavItems
            = [
                (RouteConstants.Doctors, 0, "Health workers", "bi bi-heart-pulse"),
                (RouteConstants.PatientAppointments, 1, "My appointments", "bi bi-calendar3")
              ];

        public int ActiveIndex { get; set; }
        public MainLayout()
        {
            Connectivity.Current.ConnectivityChanged += this.OnConnectivityChange;
        }
        public void OnNaviagationLocationChanged(object? sender, LocationChangedEventArgs args)
        {
            if(NavItems.Any(navItem => NavigationManager.ToAbsoluteUri(navItem.Url).AbsoluteUri == args.Location))
            {
                var selectedNavItem = NavItems.FirstOrDefault(navItem => NavigationManager.ToAbsoluteUri(navItem.Url).AbsoluteUri == args.Location);
                this.ActiveIndex = selectedNavItem.Index;
                StateHasChanged();
            }
        }
        public Task OnTabLinkClicked((string Url, int Index, string Text, string Icon) item)
        {
            //this.ActiveIndex = item.Index;
            this.NavigationManager.NavigateTo(item.Url);
           
            return Task.CompletedTask;
        }
        protected override Task OnInitializedAsync()
        {
            AppIsConnectedToInternet = Connectivity.NetworkAccess == NetworkAccess.Internet;
            NavigationManager.LocationChanged += this.OnNaviagationLocationChanged;
            return base.OnInitializedAsync();
        }

        public void OnConnectivityChange(object? sender, ConnectivityChangedEventArgs args)
        {
            this.AppIsConnectedToInternet = args.NetworkAccess == NetworkAccess.Internet;
            StateHasChanged();
        }
        ~MainLayout()
        {
            Connectivity.Current.ConnectivityChanged -= this.OnConnectivityChange;
            NavigationManager.LocationChanged-= this.OnNaviagationLocationChanged;
        }
    }
}

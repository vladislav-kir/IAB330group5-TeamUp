using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamUp.ViewModels;
using TeamUp;
using TeamUp.Services.Firestore;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacebookLogInPage : ContentPage
    {
        // TeamUp App client ID
        private string Facebook_ClientID = "523341988209997";

        public FacebookLogInPage()
        {
            InitializeComponent();

            // Immediately Perform Authentication with Facebook
            FacebookLogin();
        }

        /**
         * Open a WebView , open Facebook page with API Request for user to Log In
         * 
         */
        private void FacebookLogin()
        {
            //Request API for WebView
            string apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + Facebook_ClientID
                + "&display=popup&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html";

            // Create a WebView to display
            var webView = new WebView
            {
                //The URL is the Request API
                Source = apiRequest,
                HeightRequest = 1
            };

            // Action when navigated is the Function written below --> WebViewOnNavigated()
            webView.Navigated += WebViewOnNavigated;

            // Content of the page, now set to WebView
            Content = webView;
        }

        /**
         * The Function when the WebView is navigated (is on)
         */
        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            /* Get the AccessToken from Facebook Server. Using the function written below
             * 
             *  private string ExtractAccessTokenFromUrl(string url)
             */
            var accessToken = ExtractAccessTokenFromUrl(e.Url);

            // If successfully have the AccessToken
            if (accessToken != "")
            {
                // Then we send them to the FacebookLogInPage with its ViewModel
                var vm = BindingContext as FacebookLogInPageViewModel;

                // Set their Profile using the Function written in its ViewModel with the AccessToken
                await vm.SetFacebookUserProfileAsync(accessToken);

                // Authenticate user with Firebase Authentication & send the UID to the Firebase Server
                string FirebaseUserUID = await AuthenticationFirebase.AuthenticateFacebookAsync(accessToken);

                // Set the userUID to the app
                UsersFirestore.userUID = FirebaseUserUID;

                // Check if User is new ??
                bool isNew = await UsersFirestore.IsNewUser();
                if(!isNew)
                {
                    // Welcome back Page
                    WelcomeView.IsVisible = true;
                    Content = MainStackLayout;
                }  
                else
                {
                    // Init a new user details based on Facebook Profile
                    vm.FirstUserInit(FirebaseUserUID);

                    // User question add page
                    RegisterView.IsVisible = true;
                    Content = MainStackLayout;
                }

            }
        }



        /**
         * Function for Extract the AccessToken from the URL that we receive when user successfully logged in
         */
        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }

    }
}
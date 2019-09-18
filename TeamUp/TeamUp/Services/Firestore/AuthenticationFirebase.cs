using Firebase.Auth;
using Plugin.FirebaseAuth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.Services.Firestore
{
    class AuthenticationFirebase
    {
        /*
         * Function for Authenticate User with Facebook & Send the UID to the Firebase server 
         */
        public async static Task<string> AuthenticateFacebookAsync(string FacebookAccessToken)
        {
            // Get credential using Facebook AccessToken
            var credential = CrossFirebaseAuth.Current.FacebookAuthProvider.GetCredential(FacebookAccessToken);

            // Send to the server and get back the result
            var result = await CrossFirebaseAuth.Current.Instance.SignInWithCredentialAsync(credential);

            // Get user UID
            return result.User.Uid;
        }

        /*
         * Function for Authenticate User with Email & Password  
         */
        public async static Task<string> AuthenticateEmailPasswordAsync(string email, string password)
        {
            // Send to the server and get back the result
            var result = await CrossFirebaseAuth.Current.Instance.SignInWithEmailAndPasswordAsync(email, password);

            // Get user UID
            return result.User.Uid;
        }

        /*
         * Function for Register User with Email & Password  
         */
        public async static Task<string> SignUpEmailPasswordAsync(string email, string password)
        {
            // Send to the server and get back the result
            var result = await CrossFirebaseAuth.Current.Instance.CreateUserWithEmailAndPasswordAsync(email, password);

            // Get user UID
            return result.User.Uid;
        }
    }
}

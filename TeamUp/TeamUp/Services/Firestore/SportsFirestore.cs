using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;

namespace TeamUp.Services.Firestore
{
    public class SportsFirestore
    {
        public static List<Sport> sportsList;

        public static async Task SportInit()
        {
            // Load all documents from Cloud Firestore
            var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("Sport")
                                     .GetDocumentsAsync();

            // Convert to List of User Model
            sportsList = query.ToObjects<Sport>().ToList();

            Debug.WriteLine("--------- SPORT LIST ---------");
            foreach(Sport sport in sportsList)
            {
                Debug.WriteLine("**** ID: " + sport.Id);
            }
        }
    }
}

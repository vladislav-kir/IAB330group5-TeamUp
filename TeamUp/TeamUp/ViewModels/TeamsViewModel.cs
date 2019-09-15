using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
using System.Linq;

namespace TeamUp.ViewModels
{
    public class TeamsViewModel
    {
        public async Task<Team> GetTeamById(string id)
        {
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("Team")
                                        .GetDocument(id)
                                        .GetDocumentAsync();

            var team = document.ToObject<Team>();

            return team;
        }

        public async Task<Team> GetTeamByName(string name)
        {
            var query = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("Team")
                                        .WhereEqualsTo("TeamName", name)
                                        .GetDocumentsAsync();

            var team = query.ToObjects<Team>().ToList().First();

            return team;
        }

        public async Task<List<Team>> GetAllTeams()
        {
            var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("Team")
                                     .GetDocumentsAsync();

            var TeamList = query.ToObjects<Team>().ToList();

            return TeamList;
        }
    }
}

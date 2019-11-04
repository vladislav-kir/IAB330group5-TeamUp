using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
using System.Linq;

namespace TeamUp.Services.Firestore
{
    public class TeamsFirestore
    {
        /**
         * Function to get team by their Document ID shown on Firestore
         */
        public static async Task<Team> GetTeamByIdAsync(string id)
        {
            //Load from cloud
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("Team")
                                        .GetDocument(id)
                                        .GetDocumentAsync();

            //Convert to Team Model
            var team = document.ToObject<Team>();

            return team;
        }

        /**
         * Function to get team by their name
         */
        public static async Task<Team> GetTeamByNameAsync(string name)
        {
            var query = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("Team")
                                        .WhereEqualsTo("name", name)
                                        .GetDocumentsAsync();

            var team = query.ToObjects<Team>().ToList().First();

            return team;
        }

        /**
         * Function to get all team
         */
        public static async Task<List<Team>> GetAllTeamsAsync()
        {
            var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("Team")
                                     .GetDocumentsAsync();

            var TeamList = query.ToObjects<Team>().ToList();

            return TeamList;
        }

        /**
         * Function to get a list of members in the team
         */
        public static async Task<List<String>> GetTeamMembersAsync(Team team)
        {
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("Team")
                                        .GetDocument(team.Id)
                                        .GetDocumentAsync();

            var UserList = document.ToObject<Team>().member;

            return UserList;
        }

        /**
         * Function to get a list of request to joining team
         */
        public static async Task<List<String>> GetTeamMemberRequestsAsync(Team team)
        {
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("Team")
                                        .GetDocument(team.Id)
                                        .GetDocumentAsync();

            var requestsList = document.ToObject<Team>().memberRequest;

            return requestsList;
        }

        /**
         * Function to get teams relating to user, authenticated with User UID
         * 
         * User must provide UID in order to have the information about their teams
         * 
         */
        public static async Task<List<Team>> GetUserTeamsAsync(string user_uid)
        {
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("User")
                                        .GetDocument(user_uid)
                                        .GetDocumentAsync();

            // Get all Team IDs
            var teamIDs = document.ToObject<User>().team_uid;

            // Check whether user have a team
            if (teamIDs == null)
                return null;

            // Create a new List of Team
            List<Team> UserTeamList = new List<Team>();

            // Add all team to list, based on its ID
            foreach (string team_uid in teamIDs)
            {
                //Load the team by its ID having in User
                Team team = await GetTeamByIdAsync(team_uid);

                //Add it into collection of team
                UserTeamList.Add(team);
            }

            return UserTeamList;
        }

        /**
         * Function to get my teams, authenticated with User UID
         * 
         * User must provide UID in order to have the information about their teams
         * 
         */
        public static async Task<List<Team>> GetMyTeamsAsync()
        {
            return await GetUserTeamsAsync(UsersFirestore.myProfile.Id);
        }

        public static async Task<bool> IsNewTeam(Team team)
        {
            //Firstly Look up, whether there exists user
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("Team")
                                        .GetDocument(team.Id)
                                        .GetDocumentAsync();

            return !document.Exists;
        }

        /*
         Add a new Team to our Firestore Database
         */
        public static async Task AddTeamAsync(Team team)
        {

            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("Team")
                         .AddDocumentAsync(team);
        }




        //----------------------------------------------------
        /**
         * Check if a relationship between user & team
         */
        enum relationshipType
        {
            isOutside = 0,
            isRequesting = 1,
            isInside = 2
        };
        public static async Task<sbyte> GetRelationshipAsync(string userId, Team team)
        {
            List<String> membersList = await GetTeamMembersAsync(team);

            //Default is outside Team
            sbyte result = (sbyte) relationshipType.isOutside;

            if (membersList.Contains(userId))
                result = (sbyte) relationshipType.isInside;
            else
            //If not inside the team, check if they are requesting to joining team
            {
                List<String> requestsList = await GetTeamMemberRequestsAsync(team);

                if (requestsList.Contains(userId))
                    result = (sbyte)relationshipType.isRequesting;
            }

            return result;
        }

        //----------------------------------------------------

        /**
         * Adds a user request to a team
         */
        public static async Task AddUserRequestToTeamAsync(string userId, Team team)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("Team")
                         .GetDocument(team.Id)
                         .UpdateDataAsync("member_request", FieldValue.ArrayUnion(userId));
        }

        /**
         * Adds a user to a team
         */
        public static async Task AddUserToTeamAsync(string userId, Team team)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("Team")
                         .GetDocument(team.Id)
                         .UpdateDataAsync("member", FieldValue.ArrayUnion(userId));
        }

        /**
         * Remove user from a team
         */
        public static async Task RemoveUserFromTeamAsync(string userId, Team team)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("Team")
                         .GetDocument(team.Id)
                         .UpdateDataAsync("member", FieldValue.ArrayRemove(userId));
        }

        /**
         * Remove user request from a team
         */
        public static async Task RemoveUserRequestFromTeamAsync(string userId, Team team)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("Team")
                         .GetDocument(team.Id)
                         .UpdateDataAsync("member_request", FieldValue.ArrayRemove(userId));
        }
    }
}

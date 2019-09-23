﻿using Plugin.CloudFirestore;
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
            teamIDs.ForEach(async id =>
            {
                //Add to list
                UserTeamList.Add(await GetTeamByIdAsync(id));
            });

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
            return await GetUserTeamsAsync(UsersFirestore.userUID);
        }
    }
}

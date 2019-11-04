using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamUp.Models;
using TeamUp.Services.Firestore;
using TeamUp.ViewModels;
using Xunit;

namespace XUnitTestProject1
{
    public class API_TeamTesting
    {
        /**
         * ------------------------
         * Test 1: Aims to test if function GetAllTeamsAsync works correctly, return a list of all teams from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test1_GetAllTeams()
        {
            //Arrange 
            List<Team> teams = new List<Team>();


            //Act
            teams = await TeamsFirestore.GetAllTeamsAsync();

            //Assert
            Assert.True(teams.Count > 1);
        }


        /**
         * ------------------------
         * Test 2: Aims to test if function GetTeamByIdAsync works correctly, return a team from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test2_GetTeamByID()
        {
            //Arrange 
            string team_id = "1fhFeGGSZsIyQqwPkuMA";

            //Act
            var team = await TeamsFirestore.GetTeamByIdAsync(team_id);

            //Assert
            Assert.True(team.name.Equals("TeamB"));
        }

        /**
         * ------------------------
         * Test 3: Aims to test if function GetTeamByIdAsync works correctly, return a team from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test3_GetTeamByID()
        {
            //Arrange 
            string team_id = "Xu3FwHcyJpeDuXIk1vti";

            //Act
            var team = await TeamsFirestore.GetTeamByIdAsync(team_id);

            //Assert
            Assert.Contains("6H7I6z5BsDcyU1xP53DG2CAHOj53", team.member);
        }

        /**
         * ------------------------
         * Test 4: Aims to test if function GetTeamByIdAsync works correctly, return a team from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test4_GetTeamByID()
        {
            //Arrange 
            string team_id = "sYvjfjfqarGNAptZ5myb";

            //Act
            var team = await TeamsFirestore.GetTeamByIdAsync(team_id);

            //Assert
            Assert.True(team.team_leader.Equals("HcHGkaJGpoOSNZ19hzqUjPqcoHj1"));
        }

        /**
         * ------------------------
         * Test 5: Aims to test if function GetUserTeamsAsync works correctly, return teams of user from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test5_GetTeamByID()
        {
            //Arrange 
            string user_id = "mTwtRUKrzfamBp6PUEMGPMCRNBy1";

            //Act
            var teams = await TeamsFirestore.GetUserTeamsAsync(user_id);

            //Assert
            Assert.True(teams.Count > 0);
        }

        /**
         * ------------------------
         * Test 6: Aims to test if function GetRelationshipAsync works correctly, return the relationship of user & team from the Cloud Firestore
         * ------------------------
         * 
         * **/
        enum relationshipType
        {
            isOutside = 0,
            isRequesting = 1,
            isInside = 2
        }
        [Fact]
        public async Task Test6_GetRelationship()
        {
            //Arrange 
            string user_id = "mTwtRUKrzfamBp6PUEMGPMCRNBy1";
            string team_id = "Xu3FwHcyJpeDuXIk1vti";
            var team = await TeamsFirestore.GetTeamByIdAsync(team_id);

            //Act
            var relationship = await TeamsFirestore.GetRelationshipAsync(user_id, team);

            //Assert
            Assert.True(relationship.Equals(relationshipType.isRequesting));
        }

        /**
         * ------------------------
         * Test 7: Aims to test if function AddTeamAsync works correctly, create a new team and push to the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test7_AddTeamAsync()
        {
            //Arrange 
            Team team = new Team
            {
                bio = "bla bla bla bla",
                level = "intermediate",
                location = "Biota",
                name = "TestingTeam",
                sport = "badminton",
                team_leader = "6xyKcZwxg1cXjUkjaggIf8WGWzu1",
                avatar = "https://image.freepik.com/free-vector/modern-sports-logo-template-with-flat-design_23-2147954941.jpg",
                member = new List<string> { "6xyKcZwxg1cXjUkjaggIf8WGWzu1" },
                memberRequest = new List<string>()
                
            };

            //Act
                //Push
            await TeamsFirestore.AddTeamAsync(team);

                //Fetch again
            var newTeam = await TeamsFirestore.GetTeamByNameAsync("TestingTeam");

            //Assert
            Assert.True(newTeam.location.Equals("Biota"));
        }


        /**
         * ------------------------
         * Test 8: Aims to test if function RemoveUserFromTeamAsync works correctly, remove a user from a team from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test8_RemoveUserFromTeamAsync()
        {
            //Arrange 
            string user_id = "ryZJ4v8INRes4o2ZxEaNOPDtqgo2";

            string team_id = "Xu3FwHcyJpeDuXIk1vti";
            var team = await TeamsFirestore.GetTeamByIdAsync(team_id);

            //Act
                //Remove from database
            await TeamsFirestore.RemoveUserFromTeamAsync(user_id, team);

                //Fetch again
            var refreshTeam = await TeamsFirestore.GetTeamByIdAsync(team_id);

            //Assert
            Assert.DoesNotContain(user_id, refreshTeam.member);
        }
    }
}

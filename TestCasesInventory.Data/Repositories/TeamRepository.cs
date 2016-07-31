﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public class TeamRepository : RepositoryBase, ITeamRepository, IDisposable
    {
        public TeamRepository()
            : base()
        {
        }

        public IEnumerable<TeamDataModel> ListAll()
        {
            return dataContext.Teams.ToList();
        }

        public TeamDataModel GetTeamByID(int teamID)
        {
            return dataContext.Teams.Find(teamID);
        }

        public void InsertTeam(TeamDataModel team)
        {
            dataContext.Teams.Add(team);
        }

        public void DeleteTeam(int teamID)
        {
            TeamDataModel teamDataModel = dataContext.Teams.Find(teamID);
            dataContext.Teams.Remove(teamDataModel);
        }

        public void UpdateTeam(TeamDataModel team)
        {
            dataContext.Entry(team).State = EntityState.Modified;
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public IEnumerable<TeamDataModel> GetExistedTeamByName(string teamName)
        {
            return dataContext.Teams.Where(t => t.Name == teamName).ToList();
        }

        public IEnumerable<ApplicationUser> ListUsersNotBelongTeam()
        {
            return dataContext.Users.Where(u => u.TeamID == null).ToList();
        }
        public IEnumerable<ApplicationUser> ListUsersBelongTeam(int teamID)
        {
            return dataContext.Users.Where(u => u.TeamID == teamID).ToList();
        }
        public void AssignUsersToTeam(ApplicationUser user)
        {
            dataContext.Entry(user).State = EntityState.Modified;
        }

        public ApplicationUser FindUserByID(string userID)
        {
            return dataContext.Users.Find(userID);
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {


            if (!this.disposed)
            {
                if (disposing)
                {
                    dataContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

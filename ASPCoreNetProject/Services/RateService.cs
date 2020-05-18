using ASPCoreNetProject.Data;
using ASPCoreNetProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreNetProject.Services
{
    public class RateService : IRateBase<Rate>
    {
        private readonly ApplicationDbContext context;

        public RateService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int Add(Rate Model)
        {
            var TempRate = context.Rates.Where(d => (d.UserID == Model.UserID) && d.ArtID == Model.ArtID).Include(c => c.Art).Include(c => c.AppUser).FirstOrDefault();
            if (TempRate == null)
            {
                context.Rates.Add(Model);
                context.SaveChanges();
                return 1;
            }
            else
            {
                if (Model.Vote == 0)
                {
                    context.Rates.Remove(TempRate);
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    TempRate.Vote = Model.Vote;
                    context.SaveChanges();
                    return 1;
                }

            }

        }

        public int Delete(string UserID, int ArtID)
        {
            var RemovedRate = context.Rates.Where(d => (d.UserID == UserID) && d.ArtID == ArtID).Include(c => c.Art).Include(c => c.AppUser).FirstOrDefault();
            context.Rates.Remove(RemovedRate);
            context.SaveChanges();
            return 1;
        }

        public double GetVote(int ArtID)
        {
            var ArtRateVaue = context.Rates.Where(d => d.ArtID == ArtID).Include(c => c.Art).Include(c => c.AppUser).Sum(b => b.Vote);
            if (ArtRateVaue != 0)
            {
                return ArtRateVaue / context.Rates.Where(d => d.ArtID == ArtID).Include(c => c.Art).Include(c => c.AppUser).Count();
            }
            return 0;
        }

        public double GetVoteByUser(string UserID, int ArtID)
        {
            var UserRate = context.Rates.Where(d => (d.UserID == UserID) && d.ArtID == ArtID).Include(c => c.Art).Include(c => c.AppUser).FirstOrDefault();
            if (UserRate == null)
            {
                return 0;
            }
            else
            {
                return UserRate.Vote;
            }
        }
    }
}

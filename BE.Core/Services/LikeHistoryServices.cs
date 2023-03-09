using BE.Core.Interfaces;
using BE.Model.Entities;
using BE.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Core.Services
{
    public class LikeHistoryServices : ILikeHistoryServices
    {
        private readonly IUnitOfWork _uow;
        public LikeHistoryServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task TrackingLikeHistory(Guid userId, Guid movieId)
        {
            if (userId == Guid.Empty || movieId == Guid.Empty)
            {
                return;
            }

            var item = await _uow.GetRepository<LikeHistory>()
                .AsQueryable()
                .SingleOrDefaultAsync(x => x.UserId == userId && x.MovieId == movieId && x.isActive);

            if (item == null)
            {
                _uow.GetRepository<LikeHistory>().Add(new LikeHistory {
                    UserId = userId, 
                    MovieId = movieId,
                    isActive = true,
                });

                return;
            }

            _uow.GetRepository<LikeHistory>().Delete(item);
        }
    }
}

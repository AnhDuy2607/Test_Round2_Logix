using BE.Core.Interfaces;
using BE.DTOs.Output;
using BE.DTOs.ResponseTemplate;
using BE.Enums;
using BE.Model.Entities;
using BE.UnitOfWork.Interface;
using BE.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Core.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly IUnitOfWork _uow;
        private readonly ILikeHistoryServices _likeHistoryServices;
        public MovieServices(IUnitOfWork uow, ILikeHistoryServices likeHistoryServices)
        {
            _uow= uow;
            _likeHistoryServices = likeHistoryServices;
        }

        public async Task<ResponseData<List<MovieOutput>>> GetMovies(Guid userId, int pageIndex = 0)
        {
            var lstOfMovies = await _uow.GetRepository<Movie>()
                .AsQueryable()
                .AsNoTracking()
                .Where(x => x.isActive)
                .Skip(pageIndex * ConstantValue.PAGE_SIZE)
                .Take(ConstantValue.PAGE_SIZE)
                .Select(x => new MovieOutput
                {
                    Id = x.Id,
                    Title = x.Title,
                    LikeNo = x.LikeNo,
                    ImageUrl = x.ImageUrl,
                    isLiked = x.LikeHistories.Any(l => l.UserId == userId && l.MovieId == x.Id && l.isActive)
                }).ToListAsync();

            return new ResponseData<List<MovieOutput>>()
            {
                Data = lstOfMovies
            };
        }

        public async Task<ResponseData<LikeInformationOutput>> ModifyLikeNo(Guid movieId, Guid userId, ActionOfLikeEnum action)
        {
            _uow.BeginTransaction();
            try
            {
                var item = await _uow.GetRepository<Movie>()
                .GetByIdAsync(movieId);

                if (item == null)
                {
                    return new ResponseData<LikeInformationOutput>()
                    {
                        Data = null,
                        Message = "not found",
                        Code = 404
                    };
                }

                switch (action)
                {
                    case ActionOfLikeEnum.Increase:
                        item.LikeNo++;
                        break;
                    case ActionOfLikeEnum.Descrease:
                        item.LikeNo--;
                        break;
                }

                await _likeHistoryServices.TrackingLikeHistory(userId, movieId);
                _uow.GetRepository<Movie>().Update(item);

                var resultSaved = await _uow.SaveChangesAsync();
                _uow.CommitTransaction();
                return new ResponseData<LikeInformationOutput>()
                {
                    Data = new LikeInformationOutput
                    {
                        LikeNo = item.LikeNo,
                        MovieId = item.Id
                    }
                };
            }
            catch (Exception)
            {
                _uow.RollbackTransaction();
            }

            return new ResponseData<LikeInformationOutput>()
            {
                Data = null
            };
        }
    }
}

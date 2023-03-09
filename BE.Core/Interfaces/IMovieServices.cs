using BE.DTOs.Output;
using BE.DTOs.ResponseTemplate;
using BE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Core.Interfaces
{
    public interface IMovieServices
    {
        Task<ResponseData<List<MovieOutput>>> GetMovies(Guid userId, int pageIndex = 0);
        Task<ResponseData<LikeInformationOutput>> ModifyLikeNo(Guid movieId, Guid userId, ActionOfLikeEnum action);
    }
}

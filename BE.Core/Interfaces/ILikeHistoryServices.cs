using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Core.Interfaces
{
    public interface ILikeHistoryServices
    {
        Task TrackingLikeHistory(Guid userId, Guid movieId);
    }
}

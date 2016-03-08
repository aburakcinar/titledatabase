using NarayaN.TitleDatabase.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace NarayaN.TitleDatabase.Types.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface ITmdb
    {
        [OperationContract]
        List<string> ListGenres();

        [OperationContract]
        void CreateMovieWithImdbId(string imdbId);

        [OperationContract]
        List<MovieDTOModel> ListSavedAll();
    }
}

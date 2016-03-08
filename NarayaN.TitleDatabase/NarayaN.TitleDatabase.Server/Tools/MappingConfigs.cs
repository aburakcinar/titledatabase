using NarayaN.TitleDatabase.Types.DTOs;
using NarayaN.TitleDatabase.Types.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMDbLib.Objects.Movies;

namespace NarayaN.TitleDatabase.Server.Tools
{
    public class MappingConfigs
    {
        public static void Config()
        {
            AutoMapper.Mapper.CreateMap<TMDB_Movie, Movie>().ReverseMap();
            AutoMapper.Mapper.CreateMap<TMDB_Movie, MovieDTOModel>().ReverseMap();
        }
    }
}
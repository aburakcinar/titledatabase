using NarayaN.TitleDatabase.Server.DataAccess;
using NarayaN.TitleDatabase.Types.Entities;
using NarayaN.TitleDatabase.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Web;
using TMDbLib.Objects.Movies;
using NarayaN.TitleDatabase.Types.DTOs;

namespace NarayaN.TitleDatabase.Server.Services
{
    public class BSTmdb : BaseService, ITmdb
    {
        #region Fields

        private static string apiKey;
        private TMDbLib.Client.TMDbClient _client;

        #endregion

        #region CTOR

        static BSTmdb()
        {
            apiKey = ConfigurationManager.AppSettings["TmdbApiKey"];
        }

        #endregion

        #region Methods

        private TMDbLib.Client.TMDbClient GetTmdbClient()
        {
            if (_client == null)
                _client = new TMDbLib.Client.TMDbClient(apiKey);

            return _client;
        }

        #endregion

        #region ITmdb

        public List<string> ListGenres()
        {
            TMDbLib.Client.TMDbClient client = GetTmdbClient();
            var lst = client.GetGenres();

            return lst.Select(p => p.Name).ToList();
        }

        public void CreateMovieWithImdbId(string imdbId)
        {
            var client = GetTmdbClient();

            var movie = client.GetMovie(imdbId);

            using (var ctx = new TmdbContext())
            {
                var entityMovie = ctx.TMDB_Movie.FirstOrDefault(p => p.ImdbId == imdbId);

                if (entityMovie == null)
                {
                    entityMovie = AutoMapper.Mapper.Map<TMDB_Movie>(movie);

                    ctx.TMDB_Movie.Add(entityMovie);
                }
                else
                {
                    AutoMapper.Mapper.Map<Movie, TMDB_Movie>(movie, entityMovie);
                    ctx.Entry(entityMovie).State = EntityState.Modified;
                }

                ctx.SaveChanges();
            }

        }

        public List<MovieDTOModel> ListSavedAll()
        {
            using (var ctx = new TmdbContext())
            {
                var lst = ctx.TMDB_Movie.ToList();

                var result = new List<MovieDTOModel>();

                foreach (var item in lst)
                {
                    var dto = AutoMapper.Mapper.Map<MovieDTOModel>(item);

                    result.Add(dto);
                }

                return result;
            }
        }

        #endregion
    }
}
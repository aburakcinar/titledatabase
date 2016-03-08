using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarayaN.TitleDatabase.Types.Entities
{
    [Table("tmdb_movie")]
    public class TMDB_Movie : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string ImdbId { get; set; }

        public string Title { get; set; }

        public string OriginalTitle { get; set; }

        public string OriginalLanguage { get; set; }

        public string Status { get; set; }

        public string Tagline { get; set; }

        public string Overview { get; set; }

        public string Homepage { get; set; }

        public string BackdropPath { get; set; }

        public string PosterPath { get; set; }

        public bool Adult { get; set; }

        public bool Video { get; set; }

        //public BelongsToCollection BelongsToCollection { get; set; }

        //public List<Genre> Genres { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public long Revenue { get; set; }

        public long Budget { get; set; }

        public int? Runtime { get; set; }

        public double Popularity { get; set; }

        public double VoteAverage { get; set; }

        public int VoteCount { get; set; }

        //public List<ProductionCompany> ProductionCompanies { get; set; }

        //public List<ProductionCountry> ProductionCountries { get; set; }

        //public List<SpokenLanguage> SpokenLanguages { get; set; }

        //public AlternativeTitles AlternativeTitles { get; set; }

        //public Releases Releases { get; set; }

        //public Credits Credits { get; set; }

        //public Images Images { get; set; }

        //public KeywordsContainer Keywords { get; set; }

        //public ResultContainer<Video> Videos { get; set; }

        //public TranslationsContainer Translations { get; set; }

        //public SearchContainer<MovieResult> Similar { get; set; }

        //public SearchContainer<Review> Reviews { get; set; }

        //public SearchContainer<ListResult> Lists { get; set; }

        //public ChangesContainer Changes { get; set; }

        //public AccountState AccountStates { get; set; }

        //public ResultContainer<ReleaseDatesContainer> ReleaseDates { get; set; }
    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NarayaN.TitleDatabase.Client.Tools;
using NarayaN.TitleDatabase.Types.DTOs;
using NarayaN.TitleDatabase.Types.Interfaces;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarayaN.TitleDatabase.Client.Views.Forms
{
    public class MovieListFormViewModel : ViewModelBase
    {
        #region Properties

        public ReactiveProperty<string> ImdbId { get; private set; }

        public ObservableCollection<MovieDTOModel> Content { get; private set; }

        public RelayCommand AddCommand { get; private set; }

        #endregion


        #region CTOR

        public MovieListFormViewModel()
        {
            Content = new ObservableCollection<MovieDTOModel>();
            ImdbId = new ReactiveProperty<string>();

            AddCommand = new RelayCommand(() => OnAddCommand());

            Fill();
        }

        #endregion

        #region Methods

        private void Fill()
        {
            var lst = ServiceProxyHelper<ITmdb, List<MovieDTOModel>>.Call(p => p.ListSavedAll());

            Content.Clear();
            foreach (var item in lst)
            {
                Content.Add(item);
            }
        }

        private void OnAddCommand()
        {
            ServiceProxyHelper<ITmdb>.Call(p => p.CreateMovieWithImdbId(ImdbId.Value));

            Fill();
        }

        #endregion
    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NarayaN.TitleDatabase.Client.Tools;
using NarayaN.TitleDatabase.Types.Interfaces;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarayaN.TitleDatabase.Client.Views.Forms
{
    public class TestFormViewModel : ViewModelBase
    {
        public ReactiveProperty<string> ImdbId { get; private set; }

        public RelayCommand PingCommand { get; private set; }

        public RelayCommand<string> FormCommand { get; private set; }

        public TestFormViewModel()
        {
            PingCommand = new RelayCommand(() => OnPingCommand());
            FormCommand = new RelayCommand<string>(p => OnFormCommand(p));

            ImdbId = new ReactiveProperty<string>("tt3498820");
        }

        private void OnFormCommand(string parameter)
        {
            switch (parameter)
            {
                case "TEST_LIST_GENRES":
                    var lst = ServiceProxyHelper<ITmdb, List<string>>.Call(p => p.ListGenres());
                    break;

                case "TEST_GET_MOVIE":
                    ServiceProxyHelper<ITmdb>.Call(p => p.CreateMovieWithImdbId(ImdbId.Value));
                    break;
            }
        }

        private void OnPingCommand()
        {
            var result = ServiceProxyHelper<IDummy, bool>.Call(p => p.Ping());


        }
    }
}

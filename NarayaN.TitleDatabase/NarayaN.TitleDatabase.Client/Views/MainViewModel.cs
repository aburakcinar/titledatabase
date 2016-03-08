using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Reactive.Bindings;
using NarayaN.TitleDatabase.Client.Views.Forms;

namespace NarayaN.TitleDatabase.Client.Views
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand<string> MenuCommand { get; private set; }

        public ReactiveProperty<ViewModelBase> CurrentContent { get; private set; }

        public ReactiveProperty<string> Username { get; private set; }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            CurrentContent = new ReactiveProperty<ViewModelBase>();
            Username = new ReactiveProperty<string>("Burak Çýnar");

            MenuCommand = new RelayCommand<string>(p => OnMenuCommand(p));
        }

        private void OnMenuCommand(string p)
        {
            switch (p)
            {
                case "SETTINGS":
                    CurrentContent.Value = new TestFormViewModel();
                    break;

                case "LISTMOVIES":
                    CurrentContent.Value = new MovieListFormViewModel();
                    break;
            }
        }
    }
}
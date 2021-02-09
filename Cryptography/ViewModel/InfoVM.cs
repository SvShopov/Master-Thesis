using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommonServiceLocator;
using Cryptography.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Cryptography.ViewModel
{
    public class InfoVM : ViewModelBase
    {
        #region Definitions
        private Student student;
        #endregion 
        #region Properties
        public Student Student { get { return student; } set { student = value; } }

        public ICommand Back { get; set; }
        #endregion

        #region Constructors
        public InfoVM()
        {
            Back = new RelayCommand(() => BackToStudentMenu());
        }
        #endregion

        #region Methods
        private void BackToStudentMenu()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            StudentMenuVM smvm = ServiceLocator.Current.GetInstance<StudentMenuVM>();
            mvm.WindowHeight = 320;
            mvm.WindowWidth = 400;
            mvm.CurrentViewModel = smvm;
        }
        #endregion

    }
}

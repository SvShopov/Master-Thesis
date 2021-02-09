using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptography.Model;
using System.Windows;

namespace Cryptography.ViewModel
{
    public class StudentMenuVM : ViewModelBase
    {
        #region Definitions
        private Student student;
        #endregion
        #region Properties
        public Student Student { get { return student; } set { student = value; } }

        public ICommand Use { get; set; }
        public ICommand SeeUses { get; set; }
        public ICommand AppInfo { get; set; }
        public ICommand LogOut { get; set; }
        #endregion
        #region Constructors
        public StudentMenuVM()
        {
            Use = new RelayCommand(() => UseApp());
            SeeUses = new RelayCommand(() => SeePreviousUses());
            AppInfo = new RelayCommand(() => Info());
            LogOut = new RelayCommand(() => BackToLogin());
        }
        #endregion
        #region Methods
        private void UseApp()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            UseVM uvm = ServiceLocator.Current.GetInstance<UseVM>();
            uvm.ClearControls();  
            uvm.Student = Student;
            mvm.WindowHeight = 570;
            mvm.WindowWidth = 820;
            mvm.CurrentViewModel = uvm;
        }

        private void SeePreviousUses()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            SeePreviousUsesVM spuvm = ServiceLocator.Current.GetInstance<SeePreviousUsesVM>();
            spuvm.Student = Student;
            Task show = spuvm.ShowUses();
            mvm.WindowHeight = 550;
            mvm.WindowWidth = 1020;
            mvm.CurrentViewModel = spuvm;
        }

        private void Info()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            InfoVM ivm = ServiceLocator.Current.GetInstance<InfoVM>();
            mvm.WindowHeight = 500;
            mvm.WindowWidth = 1200;
            mvm.CurrentViewModel = ivm;
        }

        private void BackToLogin()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            LoginVM lvm = ServiceLocator.Current.GetInstance<LoginVM>();
            mvm.WindowHeight = 440;
            mvm.WindowWidth = 400;
            mvm.CurrentViewModel = lvm;
            lvm.ClearControls();
            Application.Current.Properties["UserRole"] = "None";
        }
        #endregion
    }
}
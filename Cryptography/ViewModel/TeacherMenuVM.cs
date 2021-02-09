using CommonServiceLocator;
using Cryptography.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Cryptography.ViewModel
{
    public class TeacherMenuVM : ViewModelBase
    {
        #region Definitions
        private Teacher teacher;
        #endregion
        #region Properties
        public Teacher Teacher { get { return teacher; } set { teacher = value; } }

        public ICommand Use { get; set; }
        public ICommand SeeUses { get; set; }
        public ICommand Users { get; set; }
        public ICommand LogOut { get; set; }
        #endregion
        #region Constructors
        public TeacherMenuVM()
        {
            Use = new RelayCommand(() => UseApp());
            SeeUses = new RelayCommand(() => SeePreviousUses());
            Users = new RelayCommand(() => SeeUsers());
            LogOut = new RelayCommand(() => BackToLogin());
        }
        #endregion
        #region Methods
        private void UseApp()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            UseVM uvm = ServiceLocator.Current.GetInstance<UseVM>();
            uvm.ClearControls(); 
            uvm.Teacher = Teacher;
            mvm.WindowHeight = 570;
            mvm.WindowWidth = 820;
            mvm.CurrentViewModel = uvm;
        }

        private void SeePreviousUses()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            SeePreviousUsesVM spuvm = ServiceLocator.Current.GetInstance<SeePreviousUsesVM>();
            spuvm.Teacher = Teacher;
            Task show = spuvm.ShowUses();
            mvm.WindowHeight = 550;
            mvm.WindowWidth = 1020;
            mvm.CurrentViewModel = spuvm;
        }

        private void SeeUsers()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            SeeUsersVM suvm = ServiceLocator.Current.GetInstance<SeeUsersVM>();
            suvm.Teacher = Teacher;
            Task show = suvm.SeeUsers();
            //suvm.ClearControls(); 
            mvm.WindowHeight = 500;
            mvm.WindowWidth = 700;
            mvm.CurrentViewModel = suvm;
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
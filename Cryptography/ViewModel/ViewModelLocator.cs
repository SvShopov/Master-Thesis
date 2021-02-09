/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Cryptography"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using Cryptography.Model;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System.Collections.Generic;

namespace Cryptography.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        /// 


        public ViewModelLocator()
        {


            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<RegisterVM>();
            SimpleIoc.Default.Register<LoginVM>();
            SimpleIoc.Default.Register<UseVM>();
            SimpleIoc.Default.Register<SeePreviousUsesVM>();
            SimpleIoc.Default.Register<SeePreviousUsesUserVM>();
            SimpleIoc.Default.Register<SeeUsersVM>();
            SimpleIoc.Default.Register<InfoVM>();
            SimpleIoc.Default.Register<StudentMenuVM>();
            SimpleIoc.Default.Register<TeacherMenuVM>();

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public RegisterVM RegisterVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RegisterVM>();
            }
        }

        public LoginVM LoginVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginVM>();
            }
        }

        public UseVM UseVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UseVM>();
            }

        }

        public SeePreviousUsesVM SeePreviousUsesVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SeePreviousUsesVM>();
            }
        }

        public SeePreviousUsesUserVM SeePreviousUsesUserVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SeePreviousUsesUserVM>();
            }
        }

        public SeeUsersVM SeeUsersVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SeeUsersVM>();
            }
        }

        public InfoVM InfoVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InfoVM>();
            }
        }

        public StudentMenuVM StudentMenuVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StudentMenuVM>();
            }
        }

        public TeacherMenuVM TeacherMenuVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TeacherMenuVM>();
            }
        }

        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels               
        }
    }

}
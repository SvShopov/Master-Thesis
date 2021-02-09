using CommonServiceLocator;
using Cryptography.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cryptography.ViewModel
{
    public class LoginVM : ViewModelBase
    {
        #region Definitions
        private string userName;
        private string password;
        private Visibility wrongData;
        private Student student;
        private Teacher teacher;
        
        #endregion
        #region Properties
        public string UserName { get { return userName; } set { userName = value; RaisePropertyChanged("UserName"); } }
        public string Password { get { return password; } set { password = value; RaisePropertyChanged("Password"); } }
        public Visibility WrongData { get { return wrongData; } set { wrongData = value; RaisePropertyChanged("WrongData"); } }
        public Student Student { get { return student; } set { student = value; } }
        public Teacher Teacher { get { return teacher; } set { teacher = value; } }
        
        public ICommand Login { get; set; }
        public ICommand Register { get; set; }

        #endregion
        #region Constructors
        public LoginVM()
        {
            WrongData = Visibility.Hidden;

            Login = new RelayCommand(() => LoginUser());
            Register = new RelayCommand(() => RegisterUser());
        }


        #endregion
        #region Methods

        private void RegisterUser()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            RegisterVM rvm = ServiceLocator.Current.GetInstance<RegisterVM>();
            mvm.WindowHeight = 590;
            mvm.WindowWidth = 540;
            rvm.ClearControls();
            mvm.CurrentViewModel = rvm;
        }

        private void LoginUser()
        {
            Task.Run(() =>
            {
                bool correctLoginTeacher = LoginTeacher();
                bool correctLoginStudent = LoginStudent();

                using (CryptographyContext context = new CryptographyContext())
                {

                    if (correctLoginTeacher)
                    {
                        Application.Current.Properties["UserRole"] = "Teacher";
                        Application.Current.Properties["Username"] = UserName;
                        lock (context)
                        {
                            WrongData = Visibility.Hidden;

                            Teacher = (from tr in context.Teachers where tr.UserName.Equals(UserName) select tr).First();
                            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
                            TeacherMenuVM tmvm = ServiceLocator.Current.GetInstance<TeacherMenuVM>();
                            tmvm.Teacher = Teacher;
                            mvm.WindowHeight = 320;
                            mvm.WindowWidth = 400;
                            mvm.CurrentViewModel = tmvm;

                            ClearControls();
                        }
                    }
                    else if (correctLoginStudent)
                    {
                        Application.Current.Properties["UserRole"] = "Student";
                        Application.Current.Properties["Username"] = UserName;
                        lock (context)
                        {
                            WrongData = Visibility.Hidden;

                            Student = (from st in context.Students where st.UserName.Equals(UserName) select st).First();
                            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
                            StudentMenuVM smvm = ServiceLocator.Current.GetInstance<StudentMenuVM>();
                            smvm.Student = Student;
                            mvm.WindowHeight = 320;
                            mvm.WindowWidth = 400;
                            mvm.CurrentViewModel = smvm;

                            ClearControls();
                        }
                    }
                    else
                    {
                        WrongData = Visibility.Visible;
                        ClearControls();
                    }                    
                }
            });
        }
                

        private bool LoginTeacher()
        {
            using (CryptographyContext context = new CryptographyContext())
            {
                lock (context)
                {
                    Teacher teacher = (from tr in context.Teachers where tr.UserName.Equals(UserName) select tr).FirstOrDefault();
                    if (UserName != null && Password != null)
                    {
                        if (!UserName.Equals(string.Empty) && !Password.Equals(string.Empty))
                        {
                            if (teacher is null)
                            {
                                return false;
                            }

                            else
                            {
                                bool correctTeacherPassword = PasswordHasher.VerifyPassword(Password, teacher.Password);

                                if (correctTeacherPassword)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }

                        else
                        {
                            return false;
                        }
                    }

                    else
                    {
                        return false;
                    }
                }
            }
        }

        private bool LoginStudent()
        {
            using (CryptographyContext context = new CryptographyContext())
            {
                lock (context)
                {
                    Student student = (from st in context.Students where st.UserName.Equals(UserName) select st).FirstOrDefault();
                    if (UserName != null && Password != null)
                    {
                        if (!UserName.Equals(string.Empty) && !Password.Equals(string.Empty))
                        {
                            if (student is null)
                            {
                                return false;
                            }

                            else
                            {
                                bool correctStudentPassword = PasswordHasher.VerifyPassword(Password, student.Password);

                                if (correctStudentPassword)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }

                        else
                        {
                            return false;
                        }
                    }

                    else
                    {
                        return false;
                    }
                }
            }
        }


        public void ClearControls()
        {
            UserName = null;
            Password = null;
        }
        #endregion
    }
}

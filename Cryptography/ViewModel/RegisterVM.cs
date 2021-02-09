using CommonServiceLocator;
using Cryptography.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cryptography.ViewModel
{
    public class RegisterVM : ViewModelBase
    {
        #region Definitions    
        private List<string> positions;
        private string selectedPosition;
        private string repeatedPassword;
        private Visibility wrongName;
        private Visibility wrongFamilyName;
        private Visibility wrongUserName;
        private Visibility wrongPassword;
        private Visibility wrongRepeatedPassword;
        private List<string> teachers;
        private string selectedTeacher;
        private Visibility teacher;
        private User user;
        #endregion
        #region Properties

        public List<string> Positions { get { return positions; } set { positions = value; RaisePropertyChanged("Positions"); } }
        public string SelectedPosition
        {
            get
            {
                return selectedPosition;
            }

            set
            {
                selectedPosition = value;
                RaisePropertyChanged("SelectedPosition");

                if (SelectedPosition.Equals("Ученик"))
                {
                    Teacher = Visibility.Visible;
                }
                else
                {
                    Teacher = Visibility.Hidden;
                }
            }
        }


        public string RepeatedPassword { get { return repeatedPassword; } set { repeatedPassword = value; RaisePropertyChanged("RepeatedPassword"); } }
        public Visibility WrongName { get { return wrongName; } set { wrongName = value; RaisePropertyChanged("WrongName"); } }
        public Visibility WrongFamilyName { get { return wrongFamilyName; } set { wrongFamilyName = value; RaisePropertyChanged("WrongFamilyName"); } }
        public Visibility WrongUserName { get { return wrongUserName; } set { wrongUserName = value; RaisePropertyChanged("WrongUserName"); } }
        public Visibility WrongPassword { get { return wrongPassword; } set { wrongPassword = value; RaisePropertyChanged("WrongPassword"); } }
        public Visibility WrongRepeatedPassword { get { return wrongRepeatedPassword; } set { wrongRepeatedPassword = value; RaisePropertyChanged("WrongRepeatedPassword"); } }
        public Visibility Teacher
        {
            get
            {
                return teacher;
            }

            set
            {
                teacher = value;
                RaisePropertyChanged("Teacher");
            }
        }

        public User User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
                RaisePropertyChanged("User");
            }
        }
        public List<string> Teachers { get { return teachers; } set { teachers = value; RaisePropertyChanged("Teachers"); } }
        public string SelectedTeacher { get { return selectedTeacher; } set { selectedTeacher = value; RaisePropertyChanged("SelectedTeacher"); } }

        public ICommand Register { get; set; }
        public ICommand Back { get; set; }
        #endregion
        #region Constructors
        public RegisterVM()
        {
            AddPositions();
            ChooseTeacher();
            WrongName = Visibility.Hidden;
            WrongFamilyName = Visibility.Hidden;
            WrongUserName = Visibility.Hidden;
            WrongPassword = Visibility.Hidden;
            WrongRepeatedPassword = Visibility.Hidden;

            //Teachers = new List<string>();
            User = new Student();

            Register = new RelayCommand(() => RegisterCommand());
            Back = new RelayCommand(() => BackToLogin());
        }
        #endregion
        #region Methods

        private void BackToLogin()
        {
            MainViewModel mvm = ServiceLocator.Current.GetInstance<MainViewModel>();
            LoginVM lvm = ServiceLocator.Current.GetInstance<LoginVM>();
            lvm.ClearControls();
            lvm.WrongData = Visibility.Hidden;
            mvm.CurrentViewModel = lvm;
            mvm.WindowHeight = 440;
            mvm.WindowWidth = 440;
        }

        private void RegisterCommand()
        {
            bool isNameCorrect = Validator.ValidateName(User.Name);
            bool isFamilyNameCorrect = Validator.ValidateFamilyName(User.FamilyName);
            bool isUserNameCorrect = Validator.ValidateUserName(User.UserName);
            bool isPasswordCorrect = Validator.ValidatePassword(User.Password);
            bool isRepeatedPasswordCorrect = Validator.ValidateRepeatedPassword(User.Password, RepeatedPassword);

            if (!isNameCorrect)
            {
                WrongName = Visibility.Visible;
            }

            else
            {
                WrongName = Visibility.Hidden;
            }

            if (!isFamilyNameCorrect)
            {
                WrongFamilyName = Visibility.Visible;
            }

            else
            {
                WrongFamilyName = Visibility.Hidden;
            }

            if (!isUserNameCorrect)
            {
                WrongUserName = Visibility.Visible;
            }

            else
            {
                WrongUserName = Visibility.Hidden;
            }

            if (!isPasswordCorrect)
            {
                WrongPassword = Visibility.Visible;
            }

            else
            {
                WrongPassword = Visibility.Hidden;
            }

            if (!isRepeatedPasswordCorrect)
            {
                WrongRepeatedPassword = Visibility.Visible;
            }

            else
            {
                WrongRepeatedPassword = Visibility.Hidden;
            }

            if (isNameCorrect && isFamilyNameCorrect && isUserNameCorrect && isPasswordCorrect &&
                isRepeatedPasswordCorrect)
            {
                if (SelectedPosition.Equals("Учител"))
                {
                    RegisterTeacher(User);
                }

                if (SelectedPosition.Equals("Ученик"))
                {
                    RegisterStudent(User);
                }
            }

        }

        private void RegisterTeacher(User user)
        {
            using (CryptographyContext context = new Model.CryptographyContext())
            {
                lock (context)
                {
                    List<Teacher> teachers = context.Teachers.ToList();

                    if (SelectedPosition.Equals("Учител"))
                    {
                        Teacher teacher = new Teacher();
                        teacher.Name = user.Name;
                        teacher.FamilyName = user.FamilyName;
                        teacher.UserName = user.UserName;
                        teacher.Password = PasswordHasher.CreateSaltedHash(user.Password);

                        Teacher existingTeacher = (from tr in teachers where tr.UserName.Equals(user.UserName) select tr).FirstOrDefault();

                        Student existingStudent = (from st in context.Students where st.UserName.Equals(user.UserName) select st).FirstOrDefault();



                        if (existingTeacher is null && existingStudent is null)
                        {
                            context.Teachers.Add(teacher);
                            context.SaveChanges();
                            MessageBox.Show("Успешна регистрация!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearControls();

                        }
                        else
                        {
                            MessageBox.Show("Има регистриран вече такъв потребител!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
        }

        private void RegisterStudent(User user)
        {
            using (CryptographyContext context = new Model.CryptographyContext())
            {
                lock (context)
                {
                    List<Teacher> teachers = context.Teachers.ToList();
                    List<Student> students = context.Students.ToList();

                    if (SelectedPosition.Equals("Ученик"))
                    {
                        Student student = new Student();
                        student.Name = user.Name;
                        student.FamilyName = user.FamilyName;
                        student.UserName = user.UserName;
                        student.Password = PasswordHasher.CreateSaltedHash(user.Password);

                        Teacher chosenTeacher = (from tr in teachers where tr.UserName.Equals(SelectedTeacher) select tr).FirstOrDefault();

                        Teacher existingTeacher = (from tr in teachers where tr.UserName.Equals(user.UserName) select tr).FirstOrDefault();

                        Student existingStudent = (from st in students where st.UserName.Equals(user.UserName) select st).FirstOrDefault();

                        if (existingTeacher is null && existingStudent is null)
                        {
                            if (chosenTeacher != null)
                            {
                                student.Teacher = chosenTeacher;
                                context.Students.Add(student);
                                context.SaveChanges();
                                MessageBox.Show("Успешна регистрация!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                                ClearControls();
                            }

                            else
                            {
                                MessageBox.Show("Не е избран учител!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }

                        else
                        {
                            MessageBox.Show("Има регистриран вече такъв потребител!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
        }

        private void AddPositions()
        {
            Positions = new List<string>();
            Positions.Add("Ученик");
            Positions.Add("Учител");
        }

        private void ChooseTeacher()
        {
            using (CryptographyContext context = new CryptographyContext())
            {
                lock (context)
                {
                    Teachers = (from tr in context.Teachers
                                select tr.UserName).ToList();
                }
            }
        }

        public void ClearControls()
        {
            User = new Student();
            RepeatedPassword = string.Empty;
            WrongName = Visibility.Hidden;
            WrongFamilyName = Visibility.Hidden;
            WrongUserName = Visibility.Hidden;
            WrongPassword = Visibility.Hidden;
            WrongRepeatedPassword = Visibility.Hidden;
        }
        #endregion
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TeacherDatabase.Models;

namespace TeacherDatabase.ViewModels
{
    internal class ShowTeacherVM : ViewModelBase
    {
        List<Teacher> _teacherList;
        public List<Teacher> TeacherList { get => _teacherList; set => this.RaiseAndSetIfChanged(ref _teacherList, value);}
        public ShowTeacherVM() {
            TeacherList = MainWindowViewModel.myConnection.Teachers
            .Include(x => x.Gender)
            .Include(x => x.TeachersCourses).ThenInclude(x => x.Course)
            .Include(x => x.TeachersSubjects).ThenInclude(x => x.Subject)
            .ToList();


        }

        #region add, update, delete
        public void ToPageAddTeacher() {
            MainWindowViewModel.Instance.PageContent = new AddChangeTeacher();
        }
        public void UpdateTeacher(int idTeacher) {
            MainWindowViewModel.Instance.PageContent = new AddChangeTeacher(idTeacher);
        }
        public async void DeleteTeacher(int idTeacher)
        {
            ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Хотите удалить данного пользователя?", ButtonEnum.YesNo).ShowAsync();
            if (result == ButtonResult.Yes)
            {

                Teacher deleteTeacher = MainWindowViewModel.myConnection.Teachers.FirstOrDefault(x => x.TeacherId == idTeacher);
                MainWindowViewModel.myConnection.Teachers.Remove(deleteTeacher);
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.PageContent = new ShowTeacher();
                await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Пользователь успешно удален", ButtonEnum.Ok).ShowAsync();
            }
        }
        #endregion

        #region sort

        //поисковая строка по ФИО:
        string _searchFIO;
        public string SearchFIO { get => _searchFIO; set { _searchFIO = value; AllFilters(); } }

        //Фильтрация (по стажу, по фамилии):

        int _selectFilter = 0;
        bool _filterUp = true;
        bool _isVisiblePanel = false;
        public int SelectFilter { get => _selectFilter; set { _selectFilter = value; AllFilters(); } }
        public bool FilterUp { get => _filterUp; set { this.RaiseAndSetIfChanged(ref _filterUp, value); AllFilters(); } }
        public bool IsVisiblePanel { get => _isVisiblePanel; set => this.RaiseAndSetIfChanged(ref _isVisiblePanel, value);  }

        //Фильтрация по наличию номера телефона, по дисциплине, по полу:
        //фильтрация по дисциплине:
        List<Subject> _subjectList = [new Subject() { SubjectId = 0, SubjectName = "Все предметы" }, .. MainWindowViewModel.myConnection.Subjects.ToList()];

        public List<Subject> SubjectsList { get => _subjectList; }

        Subject _selectedSubject = null;
        public Subject SelectedSubject { get => _selectedSubject == null ? _subjectList[0] : _selectedSubject; set { _selectedSubject = value; AllFilters(); } }

       

        //CheckBox только номера телефонов:
        bool _checkTelephone = false;
        public bool CheckTelephone { get => _checkTelephone; set { _checkTelephone = value; AllFilters(); } }

        

        //Фильтрация по полу:
        List<Gender> _gendersList = [new Gender() { GenderId = 0, Gender1 = "Пол не выбран" }, .. MainWindowViewModel.myConnection.Genders.ToList()];
        public List<Gender> GendersList { get => _gendersList; set => _gendersList = value; }
        

        Gender _selectGender = null;
        public Gender SelectGender { get => _selectGender; set { _selectGender = value; AllFilters(); } }

        void AllFilters() {
            TeacherList = MainWindowViewModel.myConnection.Teachers
            .Include(x => x.Gender)
            .Include(x => x.TeachersCourses).ThenInclude(x => x.Course)
            .Include(x => x.TeachersSubjects).ThenInclude(x => x.Subject)
            .ToList();

            //поиск строка по ФИО
            if (!string.IsNullOrWhiteSpace(_searchFIO)) { 
                TeacherList = TeacherList.Where(x => x.FIO.ToLower().Contains(_searchFIO.ToLower())).ToList();
            }

            //сортировка по стажу, по фамилии
            switch (_selectFilter) {
                case 0:
                    IsVisiblePanel = false;
                break;
                case 1:
                    IsVisiblePanel = true;
                    TeacherList = _filterUp ? 
                        TeacherList.OrderBy(x => x.Experience).ToList() :
                        TeacherList.OrderByDescending(x => x.Experience).ToList();
                break;
                case 2:
                    IsVisiblePanel = true;
                    TeacherList = _filterUp ?
                        TeacherList.OrderBy(x => x.Surname).ToList() :
                        TeacherList.OrderByDescending(x => x.Surname).ToList();
                break;
                case 3:
                    IsVisiblePanel = true;
                    TeacherList = _filterUp ?
                        TeacherList.OrderBy(x => x.TotalHours).ToList() :
                        TeacherList.OrderByDescending(x => x.TotalHours).ToList();
                break;
            }

            //Фильтрация по наличию номера телефона, по учебной дисциплине, по полу
            //учебная дисциплина:
            if(_selectedSubject!= null && _selectedSubject.SubjectId != 0)
            {
                TeacherList = TeacherList.Where(x => x.TeachersSubjects.Any(y => y.Subject.SubjectId == _selectedSubject.SubjectId)).ToList();
            }

            //CheckBox только номера телефонов:
            if (_checkTelephone) {
                TeacherList = TeacherList.Where(x => x.Phone != "NULL").ToList();
            }
            //по полу
            if (_selectGender != null && _selectGender.GenderId != 0) { 
                TeacherList = TeacherList.Where(x => x.Gender == _selectGender).ToList();
            }
        }

        #endregion
    }
}

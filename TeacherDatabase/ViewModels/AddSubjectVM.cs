using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherDatabase.Models;

namespace TeacherDatabase.ViewModels
{
    public class AddSubjectVM:ViewModelBase
    {
        
        string _nameSubject;
        Subject? _newSubject;
        //Subject? _newSubject;

        public string NameSubject { get => _nameSubject; set => this.RaiseAndSetIfChanged(ref _nameSubject, value); }
        public Subject? NewSubject { get => _newSubject; set => this.RaiseAndSetIfChanged(ref _newSubject, value); }

        public async void AddSubject()
        {
            ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Хотите добавить предммет?", ButtonEnum.YesNo).ShowAsync();
            if (result == ButtonResult.Yes)
            {
                NewSubject = new Subject
                {
                    SubjectName = NameSubject,
                };
                MainWindowViewModel.myConnection.Subjects.Add(NewSubject);
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.PageContent = new AddChangeTeacher();
                await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Предмет успешно добавлен", ButtonEnum.Ok).ShowAsync();
            }
            else MainWindowViewModel.Instance.PageContent = new AddChangeTeacher();

        }
        public void ToBack() { 
            MainWindowViewModel.Instance.PageContent = new AddChangeTeacher();
        }

    }
}

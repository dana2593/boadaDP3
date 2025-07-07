using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using boadaDP3.Services;
using System.Collections.ObjectModel;

namespace boadaDP3.ViewModels
{
    public partial class LogsViewModel : BaseViewModel
    {
        private readonly LogService _logService;

        [ObservableProperty]
        private ObservableCollection<string> logs = new();

        public LogsViewModel(LogService logService)
        {
            _logService = logService;
            Title = "Logs de Registros";
        }

        [RelayCommand]
        private async Task CargarLogs()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var logsList = await _logService.GetLogsAsync();

                Logs.Clear();
                foreach (var log in logsList)
                {
                    Logs.Add(log);
                }
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Error", $"Error al cargar logs: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task OnAppearing()
        {
            await CargarLogs();
        }
    }
}
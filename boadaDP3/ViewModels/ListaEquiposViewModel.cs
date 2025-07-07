using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using boadaDP3.Models;
using boadaDP3.Services;
using System.Collections.ObjectModel;

namespace boadaDP3.ViewModels
{
    public partial class ListaEquiposViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private ObservableCollection<Equipo> equipos = new();

        public ListaEquiposViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Title = "Lista de Equipos";
        }

        [RelayCommand]
        private async Task CargarEquipos()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var equiposList = await _databaseService.GetEquiposAsync();

                Equipos.Clear();
                foreach (var equipo in equiposList)
                {
                    Equipos.Add(equipo);
                }
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Error", $"Error al cargar equipos: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task EliminarEquipo(Equipo equipo)
        {
            if (equipo == null) return;

            var result = await Application.Current!.MainPage!.DisplayAlert(
                "Confirmar",
                $"¿Está seguro de eliminar el equipo {equipo.Dispositivo}?",
                "Sí", "No");

            if (result)
            {
                try
                {
                    await _databaseService.DeleteEquipoAsync(equipo);
                    Equipos.Remove(equipo);
                }
                catch (Exception ex)
                {
                    await Application.Current!.MainPage!.DisplayAlert("Error", $"Error al eliminar: {ex.Message}", "OK");
                }
            }
        }

        public async Task OnAppearing()
        {
            await CargarEquipos();
        }
    }
}
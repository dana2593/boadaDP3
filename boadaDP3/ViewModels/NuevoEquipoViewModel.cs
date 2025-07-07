using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using boadaDP3.Models;
using boadaDP3.Services;

namespace boadaDP3.ViewModels
{
    public partial class NuevoEquipoViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        private readonly LogService _logService;

        [ObservableProperty]
        private string dispositivo = string.Empty;

        [ObservableProperty]
        private string marca = string.Empty;

        [ObservableProperty]
        private bool garantiaActiva;

        [ObservableProperty]
        private int vidaUtilMeses;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        public NuevoEquipoViewModel(DatabaseService databaseService, LogService logService)
        {
            _databaseService = databaseService;
            _logService = logService;
            Title = "Dana Boada - 15/11/2002"; 
        }

        [RelayCommand]
        private async Task GuardarEquipo()
        {
            if (IsBusy) return;

            ErrorMessage = string.Empty;

            // Validaciones
            if (string.IsNullOrWhiteSpace(Dispositivo))
            {
                ErrorMessage = "El dispositivo es requerido";
                return;
            }

            if (string.IsNullOrWhiteSpace(Marca))
            {
                ErrorMessage = "La marca es requerida";
                return;
            }

            // VALIDACIÓN IMPORTANTE: Si garantía activa, vida útil mínima 12 meses
            if (GarantiaActiva && VidaUtilMeses < 12)
            {
                ErrorMessage = "Si la garantía está activa, la vida útil mínima debe ser al menos 12 meses";
                return;
            }

            try
            {
                IsBusy = true;

                var equipo = new Equipo
                {
                    Dispositivo = Dispositivo,
                    Marca = Marca,
                    GarantiaActiva = GarantiaActiva,
                    VidaUtilMeses = VidaUtilMeses
                };

                await _databaseService.SaveEquipoAsync(equipo);
                await _logService.WriteLogAsync(Dispositivo);

                // Limpiar formulario
                LimpiarFormulario();

                await Application.Current!.MainPage!.DisplayAlert("Éxito", "Equipo guardado correctamente", "OK");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al guardar: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void LimpiarFormulario()
        {
            Dispositivo = string.Empty;
            Marca = string.Empty;
            GarantiaActiva = false;
            VidaUtilMeses = 0;
            ErrorMessage = string.Empty;
        }
    }
}

using boadaDP3.ViewModels;

namespace boadaDP3.Views;

public partial class NuevoEquipoPage : ContentPage
{
    public NuevoEquipoPage(NuevoEquipoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
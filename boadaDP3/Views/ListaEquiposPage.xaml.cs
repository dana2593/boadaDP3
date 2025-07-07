using boadaDP3.ViewModels;

namespace boadaDP3.Views;

public partial class ListaEquiposPage : ContentPage
{
    private readonly ListaEquiposViewModel _viewModel;

    public ListaEquiposPage(ListaEquiposViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearing();
    }
}   
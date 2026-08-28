
using BetterBillableHours.Data;
using Microsoft.Maui.Dispatching;
using System.Diagnostics;
using BetterBillableHours.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BetterBillableHours;


//TODO: Change Picker to Collection view
public partial class MainPage : ContentPage
{

    private Page _childPage;
    //public Client? Selected = null;
    public MainPage()
    {
        InitializeComponent();
        this.AddButton.WidthRequest = this.AddButton.Height;

        this.Loaded += MainPage_Loaded;
    }

    private void MainPage_Loaded(object sender, EventArgs e)
    {
        BindingContext = new ClientViewModel();
        
    }


    public void AddClient(object? sender, EventArgs eventArgs)
    {
        this.NewClientEntry.IsVisible = true;
        this.NewClientEntry.Focus();
    }

    public async void OpenClientPage(object? sender, SelectionChangedEventArgs eventArgs)
    {
        var name = (Client)(eventArgs.CurrentSelection.First());
        var selectedClient = ((ClientViewModel)this.BindingContext).Clients.First(c => c.Id == name.Id);

        this._childPage = new ClientPage(selectedClient);

        await Navigation.PushAsync(this._childPage);
    }
    public async void SendEntry(object? sender, EventArgs eventArgs)
    {
        var name = ((Entry)sender).Text;
        var client = new Client()
        {
            Actions = new(),
            LastAccessed = DateTime.Now,
            Name = name
        };
        this.NewClientEntry.Unfocus();
        this.NewClientEntry.IsVisible = false;
        this.NewClientEntry.Text = "";

        ((ClientViewModel)this.BindingContext).Add(client);
        this._childPage = new ClientPage(client);

        await Navigation.PushAsync(this._childPage);
    }



}
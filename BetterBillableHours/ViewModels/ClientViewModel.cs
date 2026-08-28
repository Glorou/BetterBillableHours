using BetterBillableHours.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BetterBillableHours.ViewModels;

//https://learn.microsoft.com/en-us/previous-versions/windows/app-dev/windows-dotnet-maui/walkthrough-first-app#adding-the-mvvm-toolkit
public partial class ClientViewModel : ObservableObject
{
    [ObservableProperty]
    private IList<Client> source;

    [ObservableProperty]
    private Client selected;

    public IList<Client> Clients { get; set; }

    public Client SelectedClient
    {
        get { return selected; }
        set
        {
            if (selected != value)
                selected = value;
        }
    }



    public ClientViewModel()
    {
        source = MauiProgram.Database.Clients.Local.ToObservableCollection();

    }

    void FilterItems(string filter)
    {

        var filteredItems = source.Where(client => client.Name.ToLower().Contains(filter.ToLower())).ToList();
        foreach (var client in source)
        {
            if (!filteredItems.Contains(client))
            {
                Clients.Remove(client);
            }
            else
            {
                if (!Clients.Contains(client))
                {
                    Clients.Add(client);
                }
            }
        }
    }

    [RelayCommand]
    public void Add(Client client)
    {
        this.source.Add(client);
        MauiProgram.Database.SaveChangesAsync();
    }
    void ClientSelectionChanged() => OnPropertyChanged();

    [RelayCommand]
    void RemoveClient(Client client)
    {
        if (Clients.Contains(client) && source.Contains(client))
        {
            Clients.Remove(client);
            source.Remove(client);
        }

    }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
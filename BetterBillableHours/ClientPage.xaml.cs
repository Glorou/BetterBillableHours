using BetterBillableHours.Data;
using Microsoft.Maui.Dispatching;
using System.Diagnostics;
using System.Globalization;

namespace BetterBillableHours;

public partial class ClientPage : ContentPage
{
    private IDispatcherTimer dispatcherTimer;
    private bool isRunning = false;
    private DateTime? start;
    private DateTime? end;
    public Client Client;
    private readonly HoursDatabase _database;

    public ClientPage(Client client)
    {
        this.Client = client;
        InitializeComponent();
        this.ClientName.Text = client.Name;
        dispatcherTimer = Dispatcher.CreateTimer();
        _database = MauiProgram.Database;
    }

    private void OnCounterClicked(object? sender, EventArgs e)
    {
        dispatcherTimer.Start();
        isRunning = !isRunning;
        if (isRunning)
        {
            CounterBtn.Text = "Stop";
            end = null;
            this.start = DateTime.Now;
            this.dispatcherTimer.Tick += DispatcherTimerOnTick;
        }
        else
        {
            CounterBtn.Text = $"Start";
            end = DateTime.Now;
            var c = this._database.Clients.Local.First(c => c.Id == this.Client.Id);
            long actionID;
            if (c.Actions.Count == 0)
                actionID = 0;
            else
                actionID = c.Actions.Last().Id + 1;
            c.Actions.Add(new Time()
            {
                End = this.end!.Value, Id = actionID, Start = this.start!.Value

            });
        }

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void DispatcherTimerOnTick(object? sender, EventArgs e)
    {
        this.Time.Text = $"{((end ?? DateTime.Now) - start):hh\\:mm\\:ss}";
    }
}
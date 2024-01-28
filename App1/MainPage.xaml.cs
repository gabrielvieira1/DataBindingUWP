using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();

      var info = new TruckInfo()
      {
        ID = "1",
        Name = "Taco Talent",
        Style = "Mexican",
      };

      var infos = new TruckInfo[]
      {
        new TruckInfo()
        {
          ID = "1",
          Name = "Taco Talent",
          Style = "Mexican",
        },
        new TruckInfo()
        {
          ID = "2",
          Name = "Cake Shack",
          Style = "Desserts",
        },
        new TruckInfo()
        {
          ID = "3",
          Name = "Ice Heaven",
          Style = "Cold Drinks",
        },
      };

      //TruckID.Text = info.ID;
      //TruckName.Text = info.Name;
      //TruckStyle.Text = info.Style;

      //DataContext = info;

      //DataContext = infos;

      var data = new TruckData()
      {
        Trucks = new ObservableCollection<TruckInfo>(infos),
      };

      //DataContext = data;

      Data = data;
    }

    public TruckData Data { get; set; }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      (DataContext as TruckData).Trucks.Add(new TruckInfo()
      {
        ID = "4",
        Name = "Francy Fish",
        Style = "Fish",
      });
    }
  }

  public class StyleToBrushConverter : IValueConverter
  {
    public StyleToBrushConverter() { }

    private SolidColorBrush _Default = new SolidColorBrush(Windows.UI.Colors.White);
    private SolidColorBrush _Mexican = new SolidColorBrush(Windows.UI.Colors.LightPink);
    private SolidColorBrush _Desserts = new SolidColorBrush(Windows.UI.Colors.Chocolate);

    public object Convert(object value, Type targetType, object parameter, string language)
    {
      switch (value as string)
      {
        case null:
        default:
          return _Default;
        case "Mexican":
          return _Mexican;
        case "Desserts":
          return _Desserts;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotImplementedException();
    }
  }

  public class NotificationBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string name = null)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(name));
    }
  }

  public class TruckData : NotificationBase
  {
    public ObservableCollection<TruckInfo> Trucks { get; set; }
    private TruckInfo _SelectedTruck;

    public TruckInfo SelectedTruck
    {
      get { return _SelectedTruck; }
      set
      {
        if (_SelectedTruck == value)
          return;
        _SelectedTruck = value;

        NotifyPropertyChanged();
      }
    }

    public object SelectedTruckObject
    {
      get { return _SelectedTruck; }
      set
      {
        if (_SelectedTruck == value)
          return;

        _SelectedTruck = (TruckInfo)value;

        NotifyPropertyChanged("SelectedTruck");
      }
    }
  }

  public class TruckInfo
  {
    public string ID { get; set; }
    public string Name { get; set; }
    public string Style { get; set; }
  }
}

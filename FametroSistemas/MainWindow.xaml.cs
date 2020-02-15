using FametroSistemas.Model;
using FametroSistemas.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FametroSistemas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<HeaderParam> _collection = new ObservableCollection<HeaderParam>();
        ObservableCollection<Usuario> _result = new ObservableCollection<Usuario>();
        public MainWindow()
        {
            InitializeComponent();

            headerData.ItemsSource = _collection;
            _collection.Add(new HeaderParam() { Key = "Nome", Value = "" });
            ResultDataGrid.ItemsSource = _result;
        } 

        private void Button_Click_Adicionar(object sender, RoutedEventArgs e)
        {
            _collection.Add(new HeaderParam() { Key = "", Value = "" });
        }

        private void Button_Click_Remover(object sender, RoutedEventArgs e)
        {
            _collection.RemoveAt(_collection.Count - 1);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var httpVerb = ((ComboBoxItem)HttpVerbBox.SelectedItem).Content.ToString();
                var Uri = UriText.Text;
                var headerParamsList = new List<HeaderParam>(_collection);
                var service = new WebService();

                if (httpVerb.Equals("Get"))
                {
                    var usuariosTask = Task.Run(() => service.Get(Uri));
                    usuariosTask.Wait();
                    var usuarios = usuariosTask.Result;
                    _result.Clear();
                    _result.Add(usuarios);

                }
                else if (httpVerb.Equals("GetAll"))
                {
                    var usuariosTask = Task.Run(() => service.GetAll(Uri));
                    usuariosTask.Wait();
                    var usuarios = usuariosTask.Result;
                    _result.Clear();
                    foreach (var usuario in usuarios)
                    {
                        _result.Add(usuario);
                    }

                }
                else if (httpVerb.Equals("Post"))
                {
                    var usuariosTask = Task.Run(() => service.Cadastrar(Uri, headerParamsList));
                    usuariosTask.Wait();
                    var usuarios = usuariosTask.Result;
                    _result.Clear();
                    _result.Add(usuarios);
                }
                else if (httpVerb.Equals("Put"))
                {
                    var usuariosTask = Task.Run(() => service.Atualizar(Uri, headerParamsList));
                    usuariosTask.Wait();
                    var usuarios = usuariosTask.Result;
                    _result.Clear();
                    _result.Add(usuarios);
                }
                else if (httpVerb.Equals("Delete"))
                {
                    var usuariosTask = Task.Run(() => service.Delete(Uri, headerParamsList));
                    usuariosTask.Wait();
                    var usuarios = usuariosTask.Result;
                    _result.Clear();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro aqui: " + ex.Message + "  " + ex.StackTrace);
            }
            
        }
    }

    public class HeaderParam
    {
        public string Key { get; set; } 
        public string Value { get; set; }
    }

}

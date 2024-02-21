using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppLembrete.Models;//adicionei
using AppLembrete.Services;//adicionei

namespace AppLembrete.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCadastrar : ContentPage
    {
        public PageCadastrar()
        {
            InitializeComponent();
        }
        public PageCadastrar(ModelNotas nota)
        {
            InitializeComponent();
            btSalvar.Text = "Alterar";
            txtCodigo.IsVisible = true;
            btExcluir.IsVisible = true;
            txtCodigo.Text = nota.Id.ToString();
            txtTitulo.Text = nota.Titulo;
            txtDados.Text = nota.Dados;
            swFavorito.IsToggled = nota.Favorito;
        }

        private void btSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                ModelNotas notas = new ModelNotas();
                notas.Titulo = txtTitulo.Text;
                notas.Dados = txtDados.Text;
                notas.Favorito = swFavorito.IsToggled;//Istoggled é se o botão está ativado ou não
                ServiceDBNotas dBNotas = new ServiceDBNotas(App.DbPath);
                if (btSalvar.Text == "Inserir")
                {
                    dBNotas.Inserir(notas);
                    DisplayAlert("Resultado", dBNotas.StatusMessage, "OK");
                }
                else
                {
                    notas.Id = Convert.ToInt32(txtCodigo.Text);
                    dBNotas.Alterar(notas);
                    DisplayAlert("Nota alterada com sucesso!",dBNotas.StatusMessage,"OK");
                }
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void btExcluir_Clicked(object sender, EventArgs e)
        {
            var resp = await DisplayAlert("Excluir Nota", "Deseja EXCLUIR a nota selecionada?", "Sim", "Não"); //como eu quero uma resposta do DisplayAlert, eu preciso colocar esse await /o SIM é true e o NÃO é FALSE
            if (resp==true)
            {
                ServiceDBNotas dBNotas = new ServiceDBNotas(App.DbPath);
                int id = Convert.ToInt32(txtCodigo.Text);
                dBNotas.Excluir(id);
                DisplayAlert("Nota excluída com sucesso", dBNotas.StatusMessage, "OK");
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
        }

        private void btCancelar_Clicked(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageHome());
        }
    }
}
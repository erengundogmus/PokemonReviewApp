using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;
namespace PokemonWinFormsApp.Owner
{
    public partial class OwnerDetailForm : Form
    {
        private readonly int _ownerId;
        private readonly string apiUrl = "https://localhost:7013/api/owner/";
        private readonly HttpClient client = new HttpClient();

        public OwnerDetailForm(int ownerId)
        {
            InitializeComponent();
            _ownerId = ownerId;

            //kesin çalışması için load olayını beklemeden constructor içinde tetikleyelim
            _ = LoadOwnerDetailDirectlyAsync();
        }

        private async Task LoadOwnerDetailDirectlyAsync()
        {
            try
            {
                var owner = await client.GetFromJsonAsync<OwnerOutputDto>(apiUrl + _ownerId);

                if (owner != null)
                {
                    textId.Text = owner.Id.ToString();
                    textName.Text = owner.Name;
                    textGym.Text = owner.Gym;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
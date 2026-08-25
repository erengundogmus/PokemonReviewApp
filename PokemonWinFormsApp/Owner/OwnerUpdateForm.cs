using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;
namespace PokemonWinFormsApp.Owner
{
    public partial class OwnerUpdateForm : Form
    {
        private readonly int _ownerId;
        private readonly string apiUrl = "https://localhost:7013/api/owner/";
        private readonly HttpClient client = new HttpClient();

        public OwnerUpdateForm(int ownerId)
        {
            InitializeComponent();
            _ownerId = ownerId;

            //form açıldığı an mevcut bilgileri form kutularına doldurur
            _ = LoadOwnerDataAsync();
        }

        private async Task LoadOwnerDataAsync()
        {
            try
            {
                var owner = await client.GetFromJsonAsync<OwnerOutputDto>(apiUrl + _ownerId);
                if (owner != null)
                {
                    textName.Text = owner.Name;
                    textGym.Text = owner.Gym;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            var updatedOwner = new OwnerInputDto
            {
                Name = textName.Text,
                Gym = textGym.Text,
                CountryId = int.TryParse(textCountryId.Text, out int countryId) ? countryId : 0
            };

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(apiUrl + _ownerId, updatedOwner);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Owner successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to update owner: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
using PokemonReviewApp.InputDtos;
using System.Net.Http.Json;


namespace PokemonWinFormsApp.Food
{
    public partial class FoodCreateForm : Form
    {
        private readonly string apiUrl = "https://localhost:7013/api/food";
        private readonly HttpClient client = new HttpClient();

        public FoodCreateForm()
        {
            InitializeComponent();
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            var newFood = new FoodInputDto
            {
                Name = textName.Text,
                Hp = int.TryParse(textHp.Text, out int hp) ? hp : 0
            };

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, newFood);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Food successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to create food: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
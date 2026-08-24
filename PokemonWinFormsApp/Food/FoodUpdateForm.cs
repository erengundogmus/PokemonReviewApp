using PokemonReviewApp.InputDtos;
using PokemonReviewApp.OutputDtos;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace PokemonWinFormsApp.Food
{
    public partial class FoodUpdateForm : Form
    {
        private readonly int _foodId;
        private readonly string apiUrl = "https://localhost:7013/api/food/";
        private readonly HttpClient client = new HttpClient();

        public FoodUpdateForm(int foodId)
        {
            InitializeComponent();
            _foodId = foodId;

            // Form açıldığı an mevcut bilgileri form kutularına dolduralım
            _ = LoadFoodDataAsync();
        }

        private async Task LoadFoodDataAsync()
        {
            try
            {
                var food = await client.GetFromJsonAsync<FoodOutputDto>(apiUrl + _foodId);
                if (food != null)
                {
                    textName.Text = food.Name;
                    textHp.Text = food.Hp.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            var updatedFood = new FoodInputDto
            {
                Name = textName.Text,
                Hp = int.TryParse(textHp.Text, out int hp) ? hp : 0
            };

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(apiUrl + _foodId, updatedFood);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Food successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Failed to update food: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
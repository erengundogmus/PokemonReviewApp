using PokemonReviewApp.OutputDtos;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonWinFormsApp.Food
{
    public partial class FoodDetailForm : Form
    {
        private readonly int _foodId;
        private readonly string apiUrl = "https://localhost:7013/api/food/";
        private readonly HttpClient client = new HttpClient();

        public FoodDetailForm(int foodId)
        {
            InitializeComponent();
            _foodId = foodId;

            //kesin çalışması için load olayını beklemeden constructor içinde tetikleyelim
            _ = LoadFoodDetailDirectlyAsync();
        }

        private async Task LoadFoodDetailDirectlyAsync()
        {
            try
            {
                var food = await client.GetFromJsonAsync<FoodOutputDto>(apiUrl + _foodId);

                if (food != null)
                {
                    textId.Text = food.Id.ToString();
                    textName.Text = food.Name;
                    textHp.Text = food.Hp.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
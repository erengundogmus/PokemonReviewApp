using PokemonReviewApp.OutputDtos;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PokemonWinFormsApp;

namespace PokemonWinFormsApp.Pokemon
{
    public partial class PokemonForm : Form
    {
        private readonly string apiUrl = "https://localhost:7013/api/pokemon";

        public PokemonForm()
        {
            InitializeComponent();
        }

        private async void PokemonForm_Load(object sender, EventArgs e)
        {
            await LoadCountriesAsync();
        }

        //list butonuna basıldığında verileri çekecek metod
        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadCountriesAsync();
        }

        private async Task LoadCountriesAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var countries = await client.GetFromJsonAsync<List<PokemonOutputDto>>(apiUrl);

                    if (countries != null)
                    {
                        dataGridView1.DataSource = countries;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API connection error or failed while loading data: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDetail_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedPokemon = dataGridView1.CurrentRow.DataBoundItem as PokemonOutputDto;
                if (selectedPokemon != null)
                {
                    PokemonWinFormsApp.Pokemon.PokemonDetailForm detailForm = new PokemonWinFormsApp.Pokemon.PokemonDetailForm(selectedPokemon.Id);
                    detailForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to PokemonOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a pokemon item from the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonWinFormsApp.Pokemon.PokemonCreateForm createForm = new PokemonWinFormsApp.Pokemon.PokemonCreateForm();
                createForm.ShowDialog();
                //işlemden sonra otomatik listeyi yeniler
                await LoadCountriesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the create form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedPokemon = dataGridView1.CurrentRow.DataBoundItem as PokemonOutputDto;
                if (selectedPokemon != null)
                {
                    //gridden id alıyor
                    PokemonWinFormsApp.Pokemon.PokemonUpdateForm updateForm = new PokemonWinFormsApp.Pokemon.PokemonUpdateForm(selectedPokemon.Id);
                    updateForm.ShowDialog();

                    _ = LoadCountriesAsync();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to PokemonOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a pokemon item from the list to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedPokemon = dataGridView1.CurrentRow.DataBoundItem as PokemonOutputDto;
                if (selectedPokemon != null)
                {
                    DialogResult dialogResult = MessageBox.Show(
                        $"Are you sure you want to delete '{selectedPokemon.Name}'?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            using (var client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserSession.Token);

                                HttpResponseMessage response = await client.DeleteAsync(apiUrl + "/" + selectedPokemon.Id);

                                if (response.IsSuccessStatusCode)
                                {
                                    MessageBox.Show("Pokemon successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    await LoadCountriesAsync();
                                }
                                else
                                {
                                    string errorMessage = await response.Content.ReadAsStringAsync();
                                    MessageBox.Show("Failed to delete pokemon: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to PokemonOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a pokemon item from the list to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
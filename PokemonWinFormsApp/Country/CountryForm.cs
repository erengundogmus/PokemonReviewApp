using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;

namespace PokemonWinFormsApp.Country
{
    public partial class CountryForm : Form
    {

        private readonly string apiUrl = "https://localhost:7013/api/country";
        private readonly HttpClient client = new HttpClient();

        public CountryForm()
        {
            InitializeComponent();
        }

        private async void CountryForm_Load(object sender, EventArgs e)
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
                var countries = await client.GetFromJsonAsync<List<CountryOutputDto>>(apiUrl);

                if (countries != null)
                {
                    dataGridView1.DataSource = countries;
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
                var selectedCountry = dataGridView1.CurrentRow.DataBoundItem as CountryOutputDto;
                if (selectedCountry != null)
                {
                    PokemonWinFormsApp.Country.CountryDetailForm detailForm = new PokemonWinFormsApp.Country.CountryDetailForm(selectedCountry.Id);
                    detailForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to CountryOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a country item from the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonWinFormsApp.Country.CountryCreateForm createForm = new PokemonWinFormsApp.Country.CountryCreateForm();
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
                var selectedCountry = dataGridView1.CurrentRow.DataBoundItem as CountryOutputDto;
                if (selectedCountry != null)
                {
                    //gridden id alıyor
                    PokemonWinFormsApp.Country.CountryUpdateForm updateForm = new PokemonWinFormsApp.Country.CountryUpdateForm(selectedCountry.Id);
                    updateForm.ShowDialog();

                    _ = LoadCountriesAsync();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to CountryOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a country item from the list to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedCountry = dataGridView1.CurrentRow.DataBoundItem as CountryOutputDto;
                if (selectedCountry != null)
                {
                    DialogResult dialogResult = MessageBox.Show(
                        $"Are you sure you want to delete '{selectedCountry.Name}'?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            HttpResponseMessage response = await client.DeleteAsync(apiUrl + "/" + selectedCountry.Id);

                            if (response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Country successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadCountriesAsync();
                            }
                            else
                            {
                                string errorMessage = await response.Content.ReadAsStringAsync();
                                MessageBox.Show("Failed to delete country: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Could not cast selected row to CountryOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a country item from the list to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }












    }
}

using Microsoft.Extensions.DependencyInjection;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Country
{
    public partial class CountryForm : Form
    {
        private readonly IApiService _apiService;
        private readonly IServiceProvider _serviceProvider;

        public CountryForm(IApiService apiService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _apiService = apiService;
            _serviceProvider = serviceProvider;
        }

        private async void CountryForm_Load(object sender, EventArgs e)
        {
            await LoadCountriesAsync();
        }

        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadCountriesAsync();
        }

        private async Task LoadCountriesAsync()
        {
            try
            {
                var countries = await _apiService.GetAllAsync<CountryOutputDto>("country");

                if (countries != null)
                {
                    dataGridView1.DataSource = countries.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API connection error or failed while loading data: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonDetail_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedCountry = dataGridView1.CurrentRow.DataBoundItem as CountryOutputDto;
                if (selectedCountry != null)
                {
                    var detailForm = _serviceProvider.GetRequiredService<CountryDetailForm>();
                    await detailForm.LoadCountryDetailAsync(selectedCountry.Id);
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
                var createForm = _serviceProvider.GetRequiredService<CountryCreateForm>();
                createForm.ShowDialog();
                await LoadCountriesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the create form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedCountry = dataGridView1.CurrentRow.DataBoundItem as CountryOutputDto;
                if (selectedCountry != null)
                {
                    var updateForm = _serviceProvider.GetRequiredService<CountryUpdateForm>();
                    await updateForm.LoadCountryForUpdateAsync(selectedCountry.Id);
                    updateForm.ShowDialog();

                    await LoadCountriesAsync();
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
                            bool isSuccess = await _apiService.DeleteAsync("country", selectedCountry.Id);

                            if (isSuccess)
                            {
                                MessageBox.Show("Country successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadCountriesAsync();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete country.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Country
{
    public partial class CountryForm : Form
    {
        private readonly IApiService _apiService;

        public CountryForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
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
                MessageBox.Show("API connection error or failed while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonDetail_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("CountryDetail"))
            {
                MessageBox.Show("You do not have permission to view country details.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.CurrentRow != null)
            {
                var selectedCountry = dataGridView1.CurrentRow.DataBoundItem as CountryOutputDto;
                if (selectedCountry != null)
                {
                    var detailForm = new CountryDetailForm();
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
            if (!UserSession.HasPermission("CountryCreate"))
            {
                MessageBox.Show("You do not have permission to create a country.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var createForm = new CountryCreateForm();
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
            if (!UserSession.HasPermission("CountryUpdate"))
            {
                MessageBox.Show("You do not have permission to update a country.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.CurrentRow != null)
            {
                var selectedCountry = dataGridView1.CurrentRow.DataBoundItem as CountryOutputDto;
                if (selectedCountry != null)
                {
                    var updateForm = new CountryUpdateForm();
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
            if (!UserSession.HasPermission("CountryDelete"))
            {
                MessageBox.Show("You do not have permission to delete a country.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
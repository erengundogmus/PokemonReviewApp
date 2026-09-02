using PokemonReviewApp.OutputDtos;
using PokemonWinFormsApp.Owner;

namespace PokemonWinFormsApp
{
    public partial class OwnerForm : Form
    {
        private readonly IApiService _apiService;

        public OwnerForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        private async void OwnerForm_Load(object sender, EventArgs e)
        {
            await LoadOwnersAsync();
        }

        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadOwnersAsync();
        }

        private async Task LoadOwnersAsync()
        {
            try
            {
                var owners = await _apiService.GetAllAsync<OwnerOutputDto>("owner");

                if (owners != null)
                {
                    dataGridView1.DataSource = owners.ToList();
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
                var selectedOwner = dataGridView1.CurrentRow.DataBoundItem as OwnerOutputDto;
                if (selectedOwner != null)
                {
                    var detailForm = new OwnerDetailForm();
                    await detailForm.LoadOwnerDetailAsync(selectedOwner.Id);
                    detailForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to OwnerOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an owner item from the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var createForm = new OwnerCreateForm();
                createForm.ShowDialog();

                await LoadOwnersAsync();
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
                var selectedOwner = dataGridView1.CurrentRow.DataBoundItem as OwnerOutputDto;
                if (selectedOwner != null)
                {
                    var updateForm = new OwnerUpdateForm();
                    await updateForm.LoadOwnerForUpdateAsync(selectedOwner.Id);
                    updateForm.ShowDialog();

                    await LoadOwnersAsync();
                }
                else
                {
                    MessageBox.Show("Could not cast selected row to OwnerOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an owner item from the list to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedOwner = dataGridView1.CurrentRow.DataBoundItem as OwnerOutputDto;
                if (selectedOwner != null)
                {
                    DialogResult dialogResult = MessageBox.Show(
                        $"Are you sure you want to delete '{selectedOwner.Name}'?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            bool isSuccess = await _apiService.DeleteAsync("owner", selectedOwner.Id);

                            if (isSuccess)
                            {
                                MessageBox.Show("Owner successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadOwnersAsync();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete owner.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Could not cast selected row to OwnerOutputDto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an owner item from the list to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
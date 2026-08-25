using PokemonReviewApp.OutputDtos;
using System.Net.Http.Json;
namespace PokemonWinFormsApp
{
    public partial class OwnerForm : Form
    {
        private readonly string apiUrl = "https://localhost:7013/api/owner";
        private readonly HttpClient client = new HttpClient();

        public OwnerForm()
        {
            InitializeComponent();
        }
        private async void OwnerForm_Load(object sender, EventArgs e)
        {
            await LoadOwnersAsync();
        }

        //list butonuna basıldığında verileri çekecek metod
        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadOwnersAsync();
        }

        private async Task LoadOwnersAsync()
        {
            try
            {
                var owners = await client.GetFromJsonAsync<List<OwnerOutputDto>>(apiUrl);

                if (owners != null)
                {
                    dataGridView1.DataSource = owners;
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
                var selectedOwner = dataGridView1.CurrentRow.DataBoundItem as OwnerOutputDto;
                if (selectedOwner != null)
                {
                    PokemonWinFormsApp.Owner.OwnerDetailForm detailForm = new PokemonWinFormsApp.Owner.OwnerDetailForm(selectedOwner.Id);
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
                PokemonWinFormsApp.Owner.OwnerCreateForm createForm = new PokemonWinFormsApp.Owner.OwnerCreateForm();
                createForm.ShowDialog();
                //işlemden sonra otomatik listeyi yeniler
                await LoadOwnersAsync();
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
                var selectedOwner = dataGridView1.CurrentRow.DataBoundItem as OwnerOutputDto;
                if (selectedOwner != null)
                {
                    //gridden seçilen ownerın idsini alıyoruz
                    PokemonWinFormsApp.Owner.OwnerUpdateForm updateForm = new PokemonWinFormsApp.Owner.OwnerUpdateForm(selectedOwner.Id);
                    updateForm.ShowDialog();

                    _ = LoadOwnersAsync();
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
                            HttpResponseMessage response = await client.DeleteAsync(apiUrl + "/" + selectedOwner.Id);

                            if (response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Owner successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadOwnersAsync();
                            }
                            else
                            {
                                string errorMessage = await response.Content.ReadAsStringAsync();
                                MessageBox.Show("Failed to delete owner: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
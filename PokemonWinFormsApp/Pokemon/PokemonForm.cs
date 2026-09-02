using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Pokemon
{
    public partial class PokemonForm : Form
    {
        private readonly IApiService _apiService;

        public PokemonForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        private async void PokemonForm_Load(object sender, EventArgs e)
        {
            await LoadPokemonsAsync();
        }

        private async void buttonList_Click(object sender, EventArgs e)
        {
            await LoadPokemonsAsync();
        }

        private async Task LoadPokemonsAsync()
        {
            try
            {
                var pokemons = await _apiService.GetAllAsync<PokemonOutputDto>("pokemon");

                if (pokemons != null)
                {
                    dataGridView1.DataSource = pokemons.ToList();
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
                var selectedPokemon = dataGridView1.CurrentRow.DataBoundItem as PokemonOutputDto;
                if (selectedPokemon != null)
                {
                    var detailForm = new PokemonDetailForm();
                    await detailForm.LoadPokemonDetailAsync(selectedPokemon.Id);
                    detailForm.ShowDialog();
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
                var createForm = new PokemonCreateForm();
                createForm.ShowDialog();

                await LoadPokemonsAsync();
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
                var selectedPokemon = dataGridView1.CurrentRow.DataBoundItem as PokemonOutputDto;
                if (selectedPokemon != null)
                {
                    var updateForm = new PokemonUpdateForm();
                    await updateForm.LoadPokemonForUpdateAsync(selectedPokemon.Id);
                    updateForm.ShowDialog();

                    await LoadPokemonsAsync();
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
                            bool isSuccess = await _apiService.DeleteAsync("pokemon", selectedPokemon.Id);

                            if (isSuccess)
                            {
                                MessageBox.Show("Pokemon successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadPokemonsAsync();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete pokemon.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a pokemon item from the list to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
using PokemonReviewApp.Dto;
using PokemonReviewApp.OutputDtos;

namespace PokemonWinFormsApp.Pokemon
{
    public partial class PokemonUpdateForm : Form
    {
        private readonly IApiService _apiService;
        private int _pokemonId;
        private byte[]? _selectedPhotoBytes = null;

        public PokemonUpdateForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        public async Task LoadPokemonForUpdateAsync(int pokemonId)
        {
            _pokemonId = pokemonId;
            try
            {
                var pokemon = await _apiService.GetByIdAsync<PokemonOutputDto>("pokemon", _pokemonId);
                if (pokemon != null)
                {
                    textName.Text = pokemon.Name;
                    textBirthDate.Text = pokemon.BirthDate.ToString("dd-MM-yyyy");
                    textOwnerId.Text = pokemon.OwnerId.ToString();
                    textCategoryId.Text = pokemon.CategoryId.ToString();

                    if (pokemon.Photo != null && pokemon.Photo.Length > 0)
                    {
                        _selectedPhotoBytes = pokemon.Photo;
                        using (var ms = new MemoryStream(pokemon.Photo))
                        {
                            pictureBoxPhoto.Image = Image.FromStream(ms);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("PokemonUpdate"))
            {
                MessageBox.Show("You do not have permission to update a pokemon.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var updatedPokemon = new PokemonInputDto
            {
                Name = textName.Text,
                BirthDate = DateTime.TryParse(textBirthDate.Text, out DateTime birthDate) ? birthDate.Date : DateTime.Today,
                OwnerId = int.TryParse(textOwnerId.Text, out int ownerId) ? ownerId : 0,
                CategoryId = int.TryParse(textCategoryId.Text, out int categoryId) ? categoryId : 0,
                Photo = _selectedPhotoBytes
            };

            try
            {
                bool isSuccess = await _apiService.UpdateAsync("pokemon", _pokemonId, updatedPokemon);

                if (isSuccess)
                {
                    MessageBox.Show("Pokemon successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update pokemon.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                ofd.Title = "Select a Photo";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string ext = Path.GetExtension(ofd.FileName).ToLower();
                    if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                    {
                        MessageBox.Show("Only JPG, JPEG, and PNG formats are supported.", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    pictureBoxPhoto.Image = Image.FromFile(ofd.FileName);
                    _selectedPhotoBytes = File.ReadAllBytes(ofd.FileName);
                }
            }
        }
    }
}
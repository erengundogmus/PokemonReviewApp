using PokemonReviewApp.Dto;

namespace PokemonWinFormsApp.Pokemon
{
    public partial class PokemonCreateForm : Form
    {
        private readonly IApiService _apiService;
        private byte[]? _selectedPhotoBytes = null;

        public PokemonCreateForm()
        {
            InitializeComponent();
            _apiService = ResolveHelper.GetInstance<IApiService>();
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("PokemonCreate"))
            {
                MessageBox.Show("You do not have permission to create a pokemon.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newPokemon = new PokemonInputDto
            {
                Name = textName.Text,
                BirthDate = DateTime.TryParse(textBirthDate.Text, out DateTime birthDate) ? birthDate.Date : DateTime.Today,
                OwnerId = int.TryParse(textOwnerId.Text, out int ownerId) ? ownerId : 0,
                CategoryId = int.TryParse(textCategoryId.Text, out int categoryId) ? categoryId : 0,
                Photo = _selectedPhotoBytes
            };

            try
            {
                bool isSuccess = await _apiService.CreateAsync("pokemon", newPokemon);

                if (isSuccess)
                {
                    MessageBox.Show("Pokemon successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create pokemon.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
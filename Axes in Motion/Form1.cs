using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Forms;

namespace Axes_in_Motion
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(15) };
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
        private readonly TextBox userNameBox = new();
        private readonly TextBox serverUrlBox = new();
        private readonly Label userNameLabel = new();
        private readonly Label serverUrlLabel = new();
        private readonly Label connectionLabel = new();
        private readonly Button saveButton = new();
        private readonly Label saveStatusLabel = new();
        private int selectedGameId;
        private string selectedGameName = "";
        private CancellationTokenSource? refreshCancellation;

        public Form1()
        {
            InitializeComponent();
            ConfigureLayoutAndControls();
            ConnectEvents();
            LoadGames();
            ClearEntryAndSavedData();
            Shown += async (_, _) => await RefreshUserDataAsync();
        }

        private void ConfigureLayoutAndControls()
        {
            Text = "Axes In Motion — Player Data";
            titleHeadingLabel.Text = "Player Game Data";
            titleDescriptionLabel.Text = "Select a game to enter player information in place. Your latest saved values and every earlier submission appear below the form.";
            headerPanel.Height = 96;
            headerLabel.Location = new Point(26, 5);
            headerLabel.Size = new Size(450, 28);
            searchHintLabel.Text = "SEARCH GAMES";
            searchBox.Font = new Font("Segoe UI", 9F);

            StyleHeaderLabel(userNameLabel, "USER NAME");
            StyleHeaderLabel(serverUrlLabel, "SERVER URL");
            StyleHeaderLabel(searchHintLabel, "SEARCH GAMES");
            StyleHeaderLabel(connectionLabel, "API STATUS");
            userNameBox.Text = Environment.UserName;
            userNameBox.AccessibleName = "User name for saved records";
            serverUrlBox.Text = Environment.GetEnvironmentVariable("AXES_IN_MOTION_API_URL") ?? "http://localhost:5080/";
            serverUrlBox.AccessibleName = "Axes in Motion server address";
            ConfigureHeaderTextBox(userNameBox);
            ConfigureHeaderTextBox(serverUrlBox);
            connectionLabel.Text = "Not connected";
            connectionLabel.ForeColor = Color.Silver;
            headerPanel.Controls.Add(userNameLabel);
            headerPanel.Controls.Add(userNameBox);
            headerPanel.Controls.Add(serverUrlLabel);
            headerPanel.Controls.Add(serverUrlBox);
            headerPanel.Controls.Add(connectionLabel);
            headerPanel.Resize += (_, _) => LayoutHeaderControls();
            LayoutHeaderControls();

            informationPanel.Size = new Size(1080, 500);
            informationHeadingLabel.Text = "Select a game, then enter values directly in the table below.";
            informationHeadingLabel.Size = new Size(1035, 35);
            informationGrid.Location = new Point(20, 55);
            informationGrid.Size = new Size(1035, 385);
            ConfigureEntryGrid();

            saveButton.Text = "Save all data";
            saveButton.BackColor = Color.FromArgb(42, 126, 190);
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.ForeColor = Color.White;
            saveButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            saveButton.Cursor = Cursors.Hand;
            saveButton.Location = new Point(20, 450);
            saveButton.Size = new Size(145, 34);
            informationPanel.Controls.Add(saveButton);

            saveStatusLabel.AutoEllipsis = true;
            saveStatusLabel.ForeColor = Color.Silver;
            saveStatusLabel.Location = new Point(180, 452);
            saveStatusLabel.Size = new Size(855, 30);
            saveStatusLabel.Text = "Enter a user name, select a game, and fill in the values you have.";
            informationPanel.Controls.Add(saveStatusLabel);

            gameInfoPanel.Location = new Point(24, 982);
            gameInfoPanel.Size = new Size(1080, 320);
            gameInfoHeadingLabel.Text = "Data entered for selected game";
            gameInfoGrid.Location = new Point(20, 55);
            gameInfoGrid.Size = new Size(1035, 245);
            ConfigureReadOnlyGrid(gameInfoGrid);
            ConfigureColumns(gameInfoGrid, ("Field", "Field"), ("Value", "Saved value"));

            historyPanel.Location = new Point(24, 1318);
            historyPanel.Size = new Size(1080, 450);
            historyHeadingLabel.Text = "Saved history";
            historyGrid.Location = new Point(20, 55);
            historyGrid.Size = new Size(1035, 380);
            ConfigureReadOnlyGrid(historyGrid);
            ConfigureColumns(
                historyGrid,
                ("HistoryUser", "User"),
                ("HistoryGame", "Game"),
                ("HistoryDate", "Saved at"),
                ("HistoryField", "Field"),
                ("HistoryValue", "Value"));

            contentPanel.AutoScrollMinSize = new Size(1140, 1790);
        }

        private static void StyleHeaderLabel(Label label, string text)
        {
            label.Text = text;
            label.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label.ForeColor = Color.Silver;
            label.Height = 18;
        }

        private static void ConfigureHeaderTextBox(TextBox textBox)
        {
            textBox.BackColor = Color.FromArgb(35, 41, 56);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 9F);
            textBox.ForeColor = Color.White;
            textBox.Height = 27;
        }

        private void LayoutHeaderControls()
        {
            int width = headerPanel.ClientSize.Width;
            int userWidth = Math.Clamp(width / 7, 115, 160);
            int serverWidth = Math.Clamp(width / 5, 145, 220);
            int searchWidth = Math.Clamp(width / 4, 175, 300);
            int gap = 14;
            int totalWidth = userWidth + serverWidth + searchWidth + gap * 2;
            int left = Math.Max(24, width - totalWidth - 24);
            int labelTop = 34;
            int inputTop = 54;

            userNameLabel.Location = new Point(left, labelTop);
            userNameLabel.Width = userWidth;
            userNameBox.Location = new Point(left, inputTop);
            userNameBox.Width = userWidth;

            int serverLeft = left + userWidth + gap;
            serverUrlLabel.Location = new Point(serverLeft, labelTop);
            serverUrlLabel.Width = serverWidth;
            serverUrlBox.Location = new Point(serverLeft, inputTop);
            serverUrlBox.Width = serverWidth;

            int searchLeft = serverLeft + serverWidth + gap;
            searchHintLabel.Location = new Point(searchLeft, labelTop);
            searchHintLabel.Width = searchWidth;
            searchBox.Location = new Point(searchLeft, inputTop);
            searchBox.Width = searchWidth;

            connectionLabel.Location = new Point(26, 64);
            connectionLabel.Width = Math.Max(100, left - 40);
        }

        private void ConfigureEntryGrid()
        {
            informationGrid.Columns.Clear();
            informationGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FieldName",
                HeaderText = "Data needed",
                ReadOnly = true,
                FillWeight = 28,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            informationGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Example",
                HeaderText = "Example format",
                ReadOnly = true,
                FillWeight = 32,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            informationGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Value",
                HeaderText = "Your value (type here)",
                ReadOnly = false,
                FillWeight = 40,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            informationGrid.ReadOnly = false;
            informationGrid.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            informationGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            informationGrid.MultiSelect = false;
            informationGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            informationGrid.RowTemplate.Height = 25;
            informationGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            informationGrid.Columns[0].ToolTipText = "What to record for the selected game.";
            informationGrid.Columns[1].ToolTipText = "An example of the expected format. Examples are guides, not verified player data.";
        }

        private static void ConfigureReadOnlyGrid(DataGridView grid)
        {
            grid.Columns.Clear();
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            grid.RowTemplate.Height = 25;
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        }

        private static void ConfigureColumns(DataGridView grid, params (string Name, string Header)[] columns)
        {
            foreach ((string name, string header) in columns)
            {
                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = name,
                    HeaderText = header,
                    ReadOnly = true,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });
            }
        }

        private void ConnectEvents()
        {
            overviewButton.Click += (_, _) => NavigateTo(titlePanel, "Overview");
            gamesButton.Click += (_, _) => NavigateTo(gamesPanel, "Games catalogue");
            informationButton.Click += (_, _) => NavigateTo(informationPanel, "Enter game data");
            gameInfoButton.Click += (_, _) => NavigateTo(gameInfoPanel, "Latest saved data");
            historyButton.Click += (_, _) => NavigateTo(historyPanel, "Saved history");
            gamesGrid.CellClick += GamesGrid_CellClick;
            informationGrid.CellValueChanged += (_, _) => RenderCurrentEntryPreview();
            informationGrid.CellEndEdit += (_, _) => RenderCurrentEntryPreview();
            searchBox.TextChanged += SearchBox_TextChanged;
            saveButton.Click += async (_, _) => await SaveCurrentEntryAsync();
            userNameBox.Leave += async (_, _) => await RefreshUserDataAsync();
            serverUrlBox.Leave += async (_, _) => await RefreshUserDataAsync();
            userNameBox.KeyDown += HeaderTextBox_KeyDown;
            serverUrlBox.KeyDown += HeaderTextBox_KeyDown;
        }

        private async void HeaderTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            await RefreshUserDataAsync();
        }

        private void NavigateTo(Control target, string section)
        {
            contentPanel.ScrollControlIntoView(target);
            headerLabel.Text = section;
        }

        private void SearchBox_TextChanged(object? sender, EventArgs e)
        {
            string query = searchBox.Text.Trim();
            foreach (DataGridViewRow row in gamesGrid.Rows)
            {
                bool matches = string.IsNullOrWhiteSpace(query);
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string value = Convert.ToString(cell.Value) ?? "";
                    if (value.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        matches = true;
                        break;
                    }
                }
                row.Visible = matches;
            }
        }

        private void LoadGames()
        {
            gamesGrid.Rows.Clear();
            gamesGrid.Rows.Add(1, "Extreme Car Driving Simulator", "2014", "Car driving");
            gamesGrid.Rows.Add(2, "Extreme Motorcycle Simulator", "2025", "Motorcycle simulation");
            gamesGrid.Rows.Add(3, "Extreme Car Driving Stunts", "2025", "Car stunts");
            gamesGrid.Rows.Add(4, "Highway Moto Rush: Traffic", "2026", "Motorcycle racing");
            gamesGrid.Rows.Add(5, "Car Stunt Races: Mega Ramps", "2019", "Stunt racing");
            gamesGrid.Rows.Add(6, "Extreme SUV Driving Simulator", "2014", "SUV driving");
        }

        private sealed record GameField(string Name, string Example, string Purpose);

        private static List<GameField> GetFieldsForGame(int gameId)
        {
            var fields = new List<GameField>
            {
                new("Player name", "Your player name", "Name associated with this record."),
                new("Player ID", "player_10482", "The identifier assigned to this game's profile."),
                new("Nickname", "SpeedRunner", "The name shown to other players."),
                new("First played date", "2025-03-14", "The date this player first started the game."),
                new("Total playtime", "18 hours 35 minutes", "Total time spent playing this game."),
                new("Player level", "12", "The player's current level, if available."),
                new("Favorite vehicle", "Sport Coupe", "Vehicle the player uses or likes most."),
                new("Achievements", "Drift Master; First Win", "Achievements unlocked by the player."),
                new("Cloud-save status", "Enabled", "Whether cloud saving is enabled or available.")
            };

            switch (gameId)
            {
                case 1:
                    fields.AddRange(new[]
                    {
                        new GameField("Vehicles unlocked", "8", "Number of cars unlocked."),
                        new GameField("Best driving record", "Score: 12500", "A notable driving score or record."),
                        new GameField("Distance driven", "245 km", "Total distance driven, if available.")
                    });
                    break;
                case 2:
                    fields.AddRange(new[]
                    {
                        new GameField("Motorcycles unlocked", "5", "Number of motorcycles unlocked."),
                        new GameField("Best riding record", "Score: 9400", "A notable motorcycle score or record.")
                    });
                    break;
                case 3:
                    fields.AddRange(new[]
                    {
                        new GameField("Stunt records", "Long jump: 320 m", "Best or notable stunt results."),
                        new GameField("Levels completed", "16", "Number of stunt levels completed."),
                        new GameField("Vehicles unlocked", "6", "Number of cars unlocked.")
                    });
                    break;
                case 4:
                    fields.AddRange(new[]
                    {
                        new GameField("Distance ridden", "128 km", "Total distance ridden in traffic mode."),
                        new GameField("Best speed", "286 km/h", "Highest recorded speed."),
                        new GameField("Motorcycles unlocked", "4", "Number of motorcycles unlocked.")
                    });
                    break;
                case 5:
                    fields.AddRange(new[]
                    {
                        new GameField("Levels completed", "21", "Number of mega-ramp levels completed."),
                        new GameField("Best times", "Level 3: 01:24.58", "Best completion times for levels."),
                        new GameField("Vehicles unlocked", "7", "Number of vehicles unlocked.")
                    });
                    break;
                case 6:
                    fields.AddRange(new[]
                    {
                        new GameField("Distance driven", "312 km", "Total distance driven off-road and on-road."),
                        new GameField("SUVs unlocked", "5", "Number of SUVs unlocked."),
                        new GameField("Off-road progress", "8 of 12 challenges", "Current off-road challenge progress.")
                    });
                    break;
            }

            return fields;
        }

        private void ClearEntryAndSavedData()
        {
            informationGrid.Rows.Clear();
            gameInfoGrid.Rows.Clear();
            historyGrid.Rows.Clear();
            gameInfoGrid.Rows.Add("No game selected", "Select a game to view its saved values.");
        }

        private async void GamesGrid_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = gamesGrid.Rows[e.RowIndex];
            selectedGameId = Convert.ToInt32(row.Cells["GameID"].Value);
            selectedGameName = Convert.ToString(row.Cells["GameName"].Value) ?? "";
            ShowFieldsForSelectedGame(selectedGameId, selectedGameName);
            headerLabel.Text = "Enter data — " + selectedGameName;
            SetSaveStatus("Loading this user's saved values…", false);
            await RefreshUserDataAsync();
            NavigateTo(informationPanel, "Enter game data — " + selectedGameName);
        }

        private void ShowFieldsForSelectedGame(int gameId, string gameName)
        {
            informationGrid.Rows.Clear();
            informationHeadingLabel.Text = $"{gameName} — fill in the Your value column, then save.";
            gameInfoHeadingLabel.Text = $"Data entered for {gameName}";

            foreach (GameField field in GetFieldsForGame(gameId))
            {
                int rowIndex = informationGrid.Rows.Add(field.Name, field.Example, "");
                informationGrid.Rows[rowIndex].Cells[0].ToolTipText = field.Purpose;
                informationGrid.Rows[rowIndex].Cells[1].ToolTipText = "Example format only; enter the player's actual value.";
            }

            RenderCurrentEntryPreview();
        }

        private async Task RefreshUserDataAsync()
        {
            string userName = userNameBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(userName))
            {
                historyGrid.Rows.Clear();
                SetConnectionStatus("Enter a user name", Color.Orange);
                SetSaveStatus("A user name is required to load or save records.", false);
                return;
            }

            if (!TryGetServerBaseUri(out Uri? baseUri, out string error))
            {
                SetConnectionStatus("Invalid server URL", Color.Orange);
                SetSaveStatus(error, false);
                return;
            }

            refreshCancellation?.Cancel();
            refreshCancellation?.Dispose();
            refreshCancellation = new CancellationTokenSource();
            CancellationToken token = refreshCancellation.Token;
            int gameId = selectedGameId;
            string gameName = selectedGameName;
            SetConnectionStatus("Connecting…", Color.Silver);

            try
            {
                IReadOnlyList<SavedEntryDto> history = await GetHistoryAsync(baseUri!, userName, token);
                if (token.IsCancellationRequested || userName != userNameBox.Text.Trim()) return;
                RenderHistory(history);

                if (gameId > 0)
                {
                    Dictionary<string, string> latest = await GetLatestValuesAsync(baseUri!, userName, gameId, token);
                    if (token.IsCancellationRequested || gameId != selectedGameId || userName != userNameBox.Text.Trim()) return;
                    ApplySavedValuesToEntryGrid(latest);
                    gameInfoHeadingLabel.Text = $"Data entered for {gameName} — {userName}";
                }

                SetConnectionStatus("Connected", Color.LightGreen);
                SetSaveStatus("Saved data is stored on the server. Each save creates a history entry.", false);
            }
            catch (OperationCanceledException)
            {
                // A newer user, game, or server selection started a fresh load.
            }
            catch (Exception ex)
            {
                SetConnectionStatus("Server unavailable", Color.Salmon);
                SetSaveStatus("Could not load server data: " + ex.Message, true);
            }
        }

        private async Task SaveCurrentEntryAsync()
        {
            string userName = userNameBox.Text.Trim();
            if (selectedGameId == 0)
            {
                SetSaveStatus("Select a game from the catalogue first.", true);
                return;
            }
            if (string.IsNullOrWhiteSpace(userName))
            {
                SetSaveStatus("Enter a user name before saving.", true);
                return;
            }
            if (!TryGetServerBaseUri(out Uri? baseUri, out string error))
            {
                SetSaveStatus(error, true);
                return;
            }

            informationGrid.EndEdit();
            var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (DataGridViewRow row in informationGrid.Rows)
            {
                string field = Convert.ToString(row.Cells["FieldName"].Value) ?? "";
                string value = (Convert.ToString(row.Cells["Value"].Value) ?? "").Trim();
                if (!string.IsNullOrWhiteSpace(value)) values[field] = value;
            }

            if (values.Count == 0)
            {
                SetSaveStatus("Fill in at least one value in the table before saving.", true);
                return;
            }

            try
            {
                saveButton.Enabled = false;
                SetSaveStatus("Saving to the SQL database…", false);
                var entry = new SaveEntryRequest
                {
                    UserName = userName,
                    GameId = selectedGameId,
                    GameName = selectedGameName,
                    Values = values
                };
                await PostEntryAsync(baseUri!, entry);
                RenderCurrentEntryPreview();
                await RefreshUserDataAsync();
                SetSaveStatus($"Saved {values.Count} field(s) for {selectedGameName}. The submission was added to history.", false);
            }
            catch (Exception ex)
            {
                SetConnectionStatus("Server unavailable", Color.Salmon);
                SetSaveStatus("Save failed: " + ex.Message, true);
            }
            finally
            {
                saveButton.Enabled = true;
            }
        }

        private static async Task<IReadOnlyList<SavedEntryDto>> GetHistoryAsync(Uri baseUri, string userName, CancellationToken token)
        {
            string route = "api/history?userName=" + Uri.EscapeDataString(userName);
            using HttpResponseMessage response = await Http.GetAsync(new Uri(baseUri, route), token);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<SavedEntryDto>>(JsonOptions, token) ?? new List<SavedEntryDto>();
        }

        private static async Task<Dictionary<string, string>> GetLatestValuesAsync(Uri baseUri, string userName, int gameId, CancellationToken token)
        {
            string route = $"api/entries/latest?userName={Uri.EscapeDataString(userName)}&gameId={gameId}";
            using HttpResponseMessage response = await Http.GetAsync(new Uri(baseUri, route), token);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            response.EnsureSuccessStatusCode();
            SavedEntryDto? entry = await response.Content.ReadFromJsonAsync<SavedEntryDto>(JsonOptions, token);
            return entry?.Values ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        private static async Task PostEntryAsync(Uri baseUri, SaveEntryRequest entry)
        {
            using HttpResponseMessage response = await Http.PostAsJsonAsync(new Uri(baseUri, "api/entries"), entry, JsonOptions);
            if (!response.IsSuccessStatusCode)
            {
                string details = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"The server returned {(int)response.StatusCode}: {details}");
            }
        }

        private void ApplySavedValuesToEntryGrid(IReadOnlyDictionary<string, string> values)
        {
            foreach (DataGridViewRow row in informationGrid.Rows)
            {
                string fieldName = Convert.ToString(row.Cells["FieldName"].Value) ?? "";
                row.Cells["Value"].Value = values.TryGetValue(fieldName, out string? value) ? value : "";
            }

            RenderCurrentEntryPreview();
        }

        private void RenderCurrentEntryPreview()
        {
            if (gameInfoGrid is null || selectedGameId == 0) return;
            gameInfoGrid.Rows.Clear();
            bool hasValues = false;
            foreach (DataGridViewRow row in informationGrid.Rows)
            {
                string fieldName = Convert.ToString(row.Cells["FieldName"].Value) ?? "";
                string value = (Convert.ToString(row.Cells["Value"].Value) ?? "").Trim();
                if (string.IsNullOrWhiteSpace(value)) continue;
                gameInfoGrid.Rows.Add(fieldName, value);
                hasValues = true;
            }

            if (!hasValues) gameInfoGrid.Rows.Add("No values entered", "Values typed in the form will appear here.");
        }

        private void RenderHistory(IReadOnlyList<SavedEntryDto> entries)
        {
            historyGrid.Rows.Clear();
            if (entries.Count == 0)
            {
                historyGrid.Rows.Add(userNameBox.Text.Trim(), "—", "—", "No saved records", "Save data for a game to start history.");
                return;
            }

            foreach (SavedEntryDto entry in entries.OrderByDescending(item => item.SavedAt).ThenByDescending(item => item.SnapshotId))
            {
                foreach (KeyValuePair<string, string> value in entry.Values.OrderBy(pair => pair.Key, StringComparer.OrdinalIgnoreCase))
                {
                    historyGrid.Rows.Add(
                        entry.UserName,
                        entry.GameName,
                        entry.SavedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm"),
                        value.Key,
                        value.Value);
                }
            }
        }

        private bool TryGetServerBaseUri(out Uri? uri, out string error)
        {
            string text = serverUrlBox.Text.Trim();
            if (!text.EndsWith('/')) text += "/";
            if (Uri.TryCreate(text, UriKind.Absolute, out Uri? parsed) &&
                (parsed.Scheme == Uri.UriSchemeHttp || parsed.Scheme == Uri.UriSchemeHttps))
            {
                uri = parsed;
                error = "";
                return true;
            }

            uri = null;
            error = "Enter a complete server URL such as http://localhost:5080/";
            return false;
        }

        private void SetConnectionStatus(string text, Color color)
        {
            connectionLabel.Text = "SERVER: " + text;
            connectionLabel.ForeColor = color;
        }

        private void SetSaveStatus(string text, bool isError)
        {
            saveStatusLabel.Text = text;
            saveStatusLabel.ForeColor = isError ? Color.Salmon : Color.Silver;
        }

        public sealed class SaveEntryRequest
        {
            public string UserName { get; set; } = "";
            public int GameId { get; set; }
            public string GameName { get; set; } = "";
            public Dictionary<string, string> Values { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        }

        public sealed class SavedEntryDto
        {
            public long SnapshotId { get; set; }
            public string UserName { get; set; } = "";
            public int GameId { get; set; }
            public string GameName { get; set; } = "";
            public DateTimeOffset SavedAt { get; set; }
            public Dictionary<string, string> Values { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        }
    }
}

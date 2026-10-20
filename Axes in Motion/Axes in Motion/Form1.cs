
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Axes_in_Motion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ConnectEvents();
            LoadResearchData();
        }
        /* ConnectEvents method attaches event handlers to the buttons and search box.
         * It defines the actions to be taken when each button is clicked or when the search box text changes. */

        /* Navigateion and SearchBox Methods Start here*/

        private void ConnectEvents()
        {
            overviewButton.Click += (sender, e) =>
                NavigateTo(titlePanel, "Overview");

            gamesButton.Click += (sender, e) =>
                NavigateTo(gamesPanel, "Games catalogue");

            informationButton.Click += (sender, e) =>
                NavigateTo(informationPanel, "Information fields");

            gameInfoButton.Click += (sender, e) =>
                NavigateTo(gameInfoPanel, "Game-by-game data");

            historyButton.Click += (sender, e) =>
                NavigateTo(historyPanel, "Example player history");
            gamesGrid.CellDoubleClick += GamesGrid_CellDoubleClick;
            informationGrid.CellDoubleClick += InformationGrid_CellDoubleClick;
            //gameInfoGrid.CellDoubleClick += GameInfoGrid_CellDoubleClick;
            //historyGrid.CellDoubleClick += HistoryGrid_CellDoubleClick;

            searchBox.TextChanged += SearchBox_TextChanged;
        }

        private void NavigateTo(Control target, string section)
        {
            contentPanel.ScrollControlIntoView(target);
            headerLabel.Text = section;
        }
        /* NavigateTo method scrolls the contentPanel to bring the specified target control
         * into view and updates the headerLabel text to reflect the current section. */


        /*SearchBOX Starts here*/

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            string query = searchBox.Text.Trim();

            foreach (DataGridViewRow row in informationGrid.Rows)
            {
                bool matches = string.IsNullOrWhiteSpace(query);

                foreach (DataGridViewCell cell in row.Cells)
                {
                    string value = Convert.ToString(cell.Value) ?? "";

                    if (value.IndexOf(
                        query,
                        StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        matches = true;
                        break;
                    }
                }

                row.Visible = matches;
            }

            foreach (DataGridViewRow row in gamesGrid.Rows)
            {
                bool matches = string.IsNullOrWhiteSpace(query);
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string value = Convert.ToString(cell.Value) ?? "";
                    if (value.IndexOf(
                        query,
                        StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        matches = true;
                        break;
                    }
                }
                row.Visible = matches;
            }
            foreach (DataGridViewRow row in gameInfoGrid.Rows)
            {
                bool matches = string.IsNullOrWhiteSpace(query);
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string value = Convert.ToString(cell.Value) ?? "";
                    if (value.IndexOf(
                        query,
                        StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        matches = true;
                        break;
                    }
                }
                row.Visible = matches;
            }
            foreach (DataGridViewRow row in historyGrid.Rows)
            {
                bool matches = string.IsNullOrWhiteSpace(query);
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string value = Convert.ToString(cell.Value) ?? "";
                    if (value.IndexOf(
                        query,
                        StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        matches = true;
                        break;
                    }
                }
                row.Visible = matches;
            }
        }
        /*Research Data Loading Methods Start here*/

        private void LoadResearchData()
        {
            LoadGames();
            LoadInformationFields();
            LoadGameSpecificData();
        }
        /* SeacrhBox_TextChanged method filters the rows of the All DataGridViews
         * controls based on the search query entered in the searchBox. 
         * It checks each cell in each row for a match with the query and sets the row's visibility accordingly. */

        /* LoadResearchData method populates the DataGridView controls with predefined research data
         * by calling specific methods for each category of data. */
        private void LoadGames()
        {
            AddGame(1, "Extreme Car Driving Simulator", "2014", "Car driving");
            AddGame(2, "Extreme Motorcycle Simulator", "2025", "Motorcycle simulation");
            AddGame(3, "Extreme Car Driving Stunts", "2025", "Car stunts");
            AddGame(4, "Highway Moto Rush: Traffic", "2026", "Motorcycle racing");
            AddGame(5, "Car Stunt Races: Mega Ramps", "2019", "Stunt racing");
            AddGame(6, "Extreme SUV Driving Simulator", "2014", "SUV driving");
        }

        public int SelectedGameID
        {
            get
            {
                if (gamesGrid.SelectedRows.Count == 0)
                    return 0;

                return Convert.ToInt32(
                    gamesGrid.SelectedRows[0].Cells["GameID"].Value);
            }
        }

        public string SelectedGameName
        {
            get
            {
                if (gamesGrid.SelectedRows.Count == 0)
                    return "";

                return Convert.ToString(
                    gamesGrid.SelectedRows[0].Cells["GameName"].Value) ?? "";
            }
        }

        public void SelectGame()
        {
            int id = SelectedGameID;
            string name = SelectedGameName;

            if (id == 0)
            {
                MessageBox.Show("Select a game first.");
                return;
            }

            MessageBox.Show(
                $"Selected game ID: {id}\nGame: {name}");
        }

        private int selectedGameId;
        private string selectedGameName = "";

        private sealed class GameField
        {
            public string Name { get; }
            public string Purpose { get; }
            public string Example { get; }

            public GameField(string name, string purpose, string example)
            {
                Name = name;
                Purpose = purpose;
                Example = example;
            }
        }

        // These are illustrative entry examples, not verified player records from the games.
        private List<GameField> GetFieldsForGame(int gameId)
        {
            var common = new List<GameField>
            {
                new("Player ID", "The identifier assigned to this game's player profile.", "player_10482"),
                new("Nickname", "The name shown for the player in the game.", "SpeedRunner"),
                new("First played date", "When the player first started playing this game.", "2025-03-14"),
                new("Total playtime", "Total time spent playing this game.", "18 hours 35 minutes"),
                new("Player level", "The player's current level, if the game has levels.", "12"),
                new("Favorite vehicle", "The vehicle the player uses or likes most.", "Sport Coupe"),
                new("Achievements", "Achievements unlocked by the player.", "Drift Master; First Win"),
                new("Cloud-save status", "Whether cloud saving is enabled or available.", "Enabled")
            };

            switch (gameId)
            {
                case 1:
                    common.AddRange(new[] {
                        new GameField("Vehicles unlocked", "Number of cars unlocked.", "8"),
                        new GameField("Best driving record", "A notable driving score or record.", "Score: 12500"),
                        new GameField("Distance driven", "Total distance driven, if available.", "245 km")
                    });
                    break;
                case 2:
                    common.AddRange(new[] {
                        new GameField("Motorcycles unlocked", "Number of motorcycles unlocked.", "5"),
                        new GameField("Best riding record", "A notable motorcycle score or record.", "Score: 9400")
                    });
                    break;
                case 3:
                    common.AddRange(new[] {
                        new GameField("Stunt records", "Best or notable stunt results.", "Long jump: 320 m"),
                        new GameField("Levels completed", "Number of stunt levels completed.", "16"),
                        new GameField("Vehicles unlocked", "Number of cars unlocked.", "6")
                    });
                    break;
                case 4:
                    common.AddRange(new[] {
                        new GameField("Distance ridden", "Total distance ridden in traffic mode.", "128 km"),
                        new GameField("Best speed", "Highest recorded speed.", "286 km/h"),
                        new GameField("Motorcycles unlocked", "Number of motorcycles unlocked.", "4")
                    });
                    break;
                case 5:
                    common.AddRange(new[] {
                        new GameField("Levels completed", "Number of mega-ramp levels completed.", "21"),
                        new GameField("Best times", "Best completion times for levels.", "Level 3: 01:24.58"),
                        new GameField("Vehicles unlocked", "Number of vehicles unlocked.", "7")
                    });
                    break;
                case 6:
                    common.AddRange(new[] {
                        new GameField("Distance driven", "Total distance driven off-road and on-road.", "312 km"),
                        new GameField("SUVs unlocked", "Number of SUVs unlocked.", "5"),
                        new GameField("Off-road progress", "Current off-road challenge progress.", "8 of 12 challenges")
                    });
                    break;
            }
            return common;
        }

        private void LoadInformationFields()
        {
            informationGrid.Rows.Clear();
            gameInfoGrid.Rows.Clear();
            selectedGameId = 0;
            selectedGameName = "";
            informationGrid.Rows.Add("Select a game", "Double-click a game in Games catalogue to load its fields.");
            gameInfoGrid.Rows.Add("How to use", "Double-click a game in the catalogue. Examples and field purposes will appear here.");
        }

        private void LoadGameSpecificData()
        {
            // Game-specific examples are loaded after the user chooses a game by its GameID.
            gameInfoGrid.Rows.Add("Note", "Examples are formats to guide entry, not official or real player data.");
        }

        private void ShowFieldsForSelectedGame(int gameId, string gameName)
        {
            selectedGameId = gameId;
            selectedGameName = gameName;
            informationGrid.Rows.Clear();
            gameInfoGrid.Rows.Clear();
            gameInfoGrid.Rows.Add("Selected GameID", gameId.ToString());
            gameInfoGrid.Rows.Add("Game", gameName);
            gameInfoGrid.Rows.Add("Entry examples");

            foreach (GameField field in GetFieldsForGame(gameId))
            {
                informationGrid.Rows.Add(field.Name, field.Purpose);
                gameInfoGrid.Rows.Add(field.Name, "Purpose: " + field.Purpose + " Example: " + field.Example);
            }
        }
        /* LoadGames method populates the gamesGrid DataGridView with a predefined list of games,
         * including their ID, name, release year, and category. */
        public void AddGame(int id, string name, string year, string category)
        {
            gamesGrid.Rows.Add(id, name, year, category);
        }

        public void AddInformation(string field, string purpose)
        {
            informationGrid.Rows.Add(field, purpose);
        }
        /* AddInformation method adds a new row to the informationGrid DataGridView,
         * representing an information field and its purpose. */
        private void AddGameInfo(
            string game,
            string data)
        {
            gameInfoGrid.Rows.Add(game, data);
        }
        /* AddGameInfo method adds a new row to the gameInfoGrid DataGridView,
         * representing a game and its specific data. */
        private void InformationGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || selectedGameId == 0)
            {
                MessageBox.Show("First double-click a game in Games catalogue.", "Select a game");
                return;
            }

            string field = Convert.ToString(informationGrid.Rows[e.RowIndex].Cells[0].Value) ?? "";
            string purpose = Convert.ToString(informationGrid.Rows[e.RowIndex].Cells[1].Value) ?? "";
            if (string.IsNullOrWhiteSpace(field) || field == "Select a game") return;

            GameField? definition = GetFieldsForGame(selectedGameId).Find(f => f.Name == field);
            string example = definition?.Example ?? "Type the player's actual value here.";
            string historyKey = selectedGameName + " / " + field;
            string currentValue = "";
            foreach (DataGridViewRow row in historyGrid.Rows)
            {
                if (Convert.ToString(row.Cells[0].Value) == historyKey)
                {
                    currentValue = Convert.ToString(row.Cells[1].Value) ?? "";
                    break;
                }
            }

            using Form editor = new Form();
            editor.Text = "Enter " + field;
            editor.StartPosition = FormStartPosition.CenterParent;
            editor.FormBorderStyle = FormBorderStyle.FixedDialog;
            editor.ClientSize = new System.Drawing.Size(500, 225);
            editor.MinimizeBox = false;
            editor.MaximizeBox = false;

            Label gameLabel = new Label { Text = "Game: " + selectedGameName + " (GameID " + selectedGameId + ")", Left = 15, Top = 12, Width = 470, Height = 22 };
            Label purposeLabel = new Label { Text = "Purpose: " + purpose, Left = 15, Top = 40, Width = 470, Height = 42 };
            Label exampleLabel = new Label { Text = "Example format: " + example, Left = 15, Top = 83, Width = 470, Height = 35 };
            TextBox input = new TextBox { Text = currentValue, Left = 15, Top = 122, Width = 470 };
            Button saveButton = new Button { Text = "Save", DialogResult = DialogResult.OK, Left = 320, Top = 170, Width = 80, Height = 30 };
            Button cancelButton = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Left = 405, Top = 170, Width = 80, Height = 30 };
            editor.Controls.AddRange(new Control[] { gameLabel, purposeLabel, exampleLabel, input, saveButton, cancelButton });
            editor.AcceptButton = saveButton;
            editor.CancelButton = cancelButton;
            editor.Shown += (_, _) => { input.Focus(); input.SelectAll(); };

            if (editor.ShowDialog(this) != DialogResult.OK) return;
            string value = input.Text.Trim();
            if (string.IsNullOrWhiteSpace(value))
            {
                MessageBox.Show("Please enter a value.", "Value required");
                return;
            }

            // History is keyed by game and field. Saving again updates the same history row automatically.
            foreach (DataGridViewRow row in historyGrid.Rows)
            {
                if (Convert.ToString(row.Cells[0].Value) == historyKey)
                {
                    row.Cells[1].Value = value;
                    row.Cells[2].Value = "User input";
                    return;
                }
            }
            AddHistory(historyKey, value, "User input");
        }

        public void AddHistory(string field, string example, string type)
        {
            historyGrid.Rows.Add(field, example, type);
        }
        private void GamesGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow game = gamesGrid.Rows[e.RowIndex];
            int id = Convert.ToInt32(game.Cells["GameID"].Value);
            string name = Convert.ToString(game.Cells["GameName"].Value) ?? "";
            ShowFieldsForSelectedGame(id, name);
            headerLabel.Text = "Information fields — " + name;
        }
    }
}

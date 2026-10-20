namespace Axes_in_Motion
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel contentPanel;

        private System.Windows.Forms.Label brandLabel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label searchHintLabel;
        private System.Windows.Forms.TextBox searchBox;

        private System.Windows.Forms.Button overviewButton;
        private System.Windows.Forms.Button gamesButton;
        private System.Windows.Forms.Button informationButton;
        private System.Windows.Forms.Button gameInfoButton;
        private System.Windows.Forms.Button historyButton;

        private System.Windows.Forms.Panel titlePanel;
        private System.Windows.Forms.Panel gamesPanel;
        private System.Windows.Forms.Panel informationPanel;
        private System.Windows.Forms.Panel gameInfoPanel;
        private System.Windows.Forms.Panel historyPanel;

        private System.Windows.Forms.Label titleHeadingLabel;
        private System.Windows.Forms.Label titleDescriptionLabel;
        private System.Windows.Forms.Label gamesHeadingLabel;
        private System.Windows.Forms.Label informationHeadingLabel;
        private System.Windows.Forms.Label gameInfoHeadingLabel;
        private System.Windows.Forms.Label historyHeadingLabel;

        private System.Windows.Forms.DataGridView gamesGrid;
        private System.Windows.Forms.DataGridView informationGrid;
        private System.Windows.Forms.DataGridView gameInfoGrid;
        private System.Windows.Forms.DataGridView historyGrid;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            sidebarPanel = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            brandLabel = new Label();
            overviewButton = new Button();
            gamesButton = new Button();
            informationButton = new Button();
            gameInfoButton = new Button();
            historyButton = new Button();
            headerPanel = new Panel();
            headerLabel = new Label();
            searchHintLabel = new Label();
            searchBox = new TextBox();
            contentPanel = new Panel();
            titlePanel = new Panel();
            titleHeadingLabel = new Label();
            titleDescriptionLabel = new Label();
            gamesPanel = new Panel();
            gamesHeadingLabel = new Label();
            gamesGrid = new DataGridView();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            informationPanel = new Panel();
            informationHeadingLabel = new Label();
            informationGrid = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            gameInfoPanel = new Panel();
            gameInfoHeadingLabel = new Label();
            gameInfoGrid = new DataGridView();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            historyPanel = new Panel();
            historyHeadingLabel = new Label();
            historyGrid = new DataGridView();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            sidebarPanel.SuspendLayout();
            headerPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            titlePanel.SuspendLayout();
            gamesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gamesGrid).BeginInit();
            informationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)informationGrid).BeginInit();
            gameInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gameInfoGrid).BeginInit();
            historyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)historyGrid).BeginInit();
            SuspendLayout();
            // 
            // sidebarPanel
            // 
            sidebarPanel.BackColor = Color.FromArgb(21, 25, 36);
            sidebarPanel.Controls.Add(flowLayoutPanel1);
            sidebarPanel.Controls.Add(brandLabel);
            sidebarPanel.Controls.Add(overviewButton);
            sidebarPanel.Controls.Add(gamesButton);
            sidebarPanel.Controls.Add(informationButton);
            sidebarPanel.Controls.Add(gameInfoButton);
            sidebarPanel.Controls.Add(historyButton);
            sidebarPanel.Dock = DockStyle.Left;
            sidebarPanel.Location = new Point(0, 0);
            sidebarPanel.Name = "sidebarPanel";
            sidebarPanel.Size = new Size(225, 900);
            sidebarPanel.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(200, 563);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(250, 125);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // brandLabel
            // 
            brandLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            brandLabel.ForeColor = Color.FromArgb(102, 190, 255);
            brandLabel.Location = new Point(20, 25);
            brandLabel.Name = "brandLabel";
            brandLabel.Size = new Size(190, 65);
            brandLabel.TabIndex = 0;
            brandLabel.Text = "AXES IN MOTION";
            brandLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // overviewButton
            // 
            overviewButton.BackColor = Color.FromArgb(38, 49, 69);
            overviewButton.Cursor = Cursors.Hand;
            overviewButton.FlatAppearance.BorderSize = 0;
            overviewButton.FlatStyle = FlatStyle.Flat;
            overviewButton.ForeColor = Color.White;
            overviewButton.Location = new Point(12, 125);
            overviewButton.Name = "overviewButton";
            overviewButton.Padding = new Padding(14, 0, 0, 0);
            overviewButton.Size = new Size(201, 45);
            overviewButton.TabIndex = 1;
            overviewButton.Text = "Overview";
            overviewButton.TextAlign = ContentAlignment.MiddleLeft;
            overviewButton.UseVisualStyleBackColor = false;
            // 
            // gamesButton
            // 
            gamesButton.BackColor = Color.FromArgb(21, 25, 36);
            gamesButton.Cursor = Cursors.Hand;
            gamesButton.FlatAppearance.BorderSize = 0;
            gamesButton.FlatStyle = FlatStyle.Flat;
            gamesButton.ForeColor = Color.Gainsboro;
            gamesButton.Location = new Point(12, 178);
            gamesButton.Name = "gamesButton";
            gamesButton.Padding = new Padding(14, 0, 0, 0);
            gamesButton.Size = new Size(201, 45);
            gamesButton.TabIndex = 2;
            gamesButton.Text = "Games catalogue";
            gamesButton.TextAlign = ContentAlignment.MiddleLeft;
            gamesButton.UseVisualStyleBackColor = false;
            // 
            // informationButton
            // 
            informationButton.BackColor = Color.FromArgb(21, 25, 36);
            informationButton.Cursor = Cursors.Hand;
            informationButton.FlatAppearance.BorderSize = 0;
            informationButton.FlatStyle = FlatStyle.Flat;
            informationButton.ForeColor = Color.Gainsboro;
            informationButton.Location = new Point(12, 231);
            informationButton.Name = "informationButton";
            informationButton.Padding = new Padding(14, 0, 0, 0);
            informationButton.Size = new Size(201, 45);
            informationButton.TabIndex = 3;
            informationButton.Text = "Information fields";
            informationButton.TextAlign = ContentAlignment.MiddleLeft;
            informationButton.UseVisualStyleBackColor = false;
            // 
            // gameInfoButton
            // 
            gameInfoButton.BackColor = Color.FromArgb(21, 25, 36);
            gameInfoButton.Cursor = Cursors.Hand;
            gameInfoButton.FlatAppearance.BorderSize = 0;
            gameInfoButton.FlatStyle = FlatStyle.Flat;
            gameInfoButton.ForeColor = Color.Gainsboro;
            gameInfoButton.Location = new Point(12, 284);
            gameInfoButton.Name = "gameInfoButton";
            gameInfoButton.Padding = new Padding(14, 0, 0, 0);
            gameInfoButton.Size = new Size(201, 45);
            gameInfoButton.TabIndex = 4;
            gameInfoButton.Text = "Game-by-game data";
            gameInfoButton.TextAlign = ContentAlignment.MiddleLeft;
            gameInfoButton.UseVisualStyleBackColor = false;
            // 
            // historyButton
            // 
            historyButton.BackColor = Color.FromArgb(21, 25, 36);
            historyButton.Cursor = Cursors.Hand;
            historyButton.FlatAppearance.BorderSize = 0;
            historyButton.FlatStyle = FlatStyle.Flat;
            historyButton.ForeColor = Color.Gainsboro;
            historyButton.Location = new Point(12, 337);
            historyButton.Name = "historyButton";
            historyButton.Padding = new Padding(14, 0, 0, 0);
            historyButton.Size = new Size(201, 45);
            historyButton.TabIndex = 5;
            historyButton.Text = "Example player history";
            historyButton.TextAlign = ContentAlignment.MiddleLeft;
            historyButton.UseVisualStyleBackColor = false;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(24, 29, 41);
            headerPanel.Controls.Add(headerLabel);
            headerPanel.Controls.Add(searchHintLabel);
            headerPanel.Controls.Add(searchBox);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(225, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(1175, 82);
            headerPanel.TabIndex = 1;
            // 
            // headerLabel
            // 
            headerLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            headerLabel.ForeColor = Color.White;
            headerLabel.Location = new Point(26, 17);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(500, 48);
            headerLabel.TabIndex = 0;
            headerLabel.Text = "Overview";
            headerLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // searchHintLabel
            // 
            searchHintLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            searchHintLabel.ForeColor = Color.Silver;
            searchHintLabel.Location = new Point(770, 12);
            searchHintLabel.Name = "searchHintLabel";
            searchHintLabel.Size = new Size(340, 20);
            searchHintLabel.TabIndex = 1;
            searchHintLabel.Text = "SEARCH THE GAMES CATALOGUE";
            // 
            // searchBox
            // 
            searchBox.BackColor = Color.FromArgb(35, 41, 56);
            searchBox.BorderStyle = BorderStyle.FixedSingle;
            searchBox.Font = new Font("Segoe UI", 10F);
            searchBox.ForeColor = Color.White;
            searchBox.Location = new Point(770, 35);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(340, 30);
            searchBox.TabIndex = 2;
            // 
            // contentPanel
            // 
            contentPanel.AutoScroll = true;
            contentPanel.BackColor = Color.FromArgb(15, 18, 26);
            contentPanel.Controls.Add(titlePanel);
            contentPanel.Controls.Add(gamesPanel);
            contentPanel.Controls.Add(informationPanel);
            contentPanel.Controls.Add(gameInfoPanel);
            contentPanel.Controls.Add(historyPanel);
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(225, 82);
            contentPanel.Name = "contentPanel";
            contentPanel.Padding = new Padding(24);
            contentPanel.Size = new Size(1175, 818);
            contentPanel.TabIndex = 0;
            // 
            // titlePanel
            // 
            titlePanel.BackColor = Color.FromArgb(24, 29, 41);
            titlePanel.Controls.Add(titleHeadingLabel);
            titlePanel.Controls.Add(titleDescriptionLabel);
            titlePanel.Location = new Point(24, 24);
            titlePanel.Name = "titlePanel";
            titlePanel.Size = new Size(1080, 145);
            titlePanel.TabIndex = 0;
            // 
            // titleHeadingLabel
            // 
            titleHeadingLabel.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            titleHeadingLabel.ForeColor = Color.White;
            titleHeadingLabel.Location = new Point(24, 18);
            titleHeadingLabel.Name = "titleHeadingLabel";
            titleHeadingLabel.Size = new Size(1020, 43);
            titleHeadingLabel.TabIndex = 0;
            titleHeadingLabel.Text = "Player Information Research";
            // 
            // titleDescriptionLabel
            // 
            titleDescriptionLabel.Font = new Font("Segoe UI", 10F);
            titleDescriptionLabel.ForeColor = Color.Gainsboro;
            titleDescriptionLabel.Location = new Point(26, 69);
            titleDescriptionLabel.Name = "titleDescriptionLabel";
            titleDescriptionLabel.Size = new Size(1020, 60);
            titleDescriptionLabel.TabIndex = 1;
            titleDescriptionLabel.Text = "A structured overview of game catalogues, possible account and gameplay data, and example player history. Example records are illustrative, not verified account data.";
            // 
            // gamesPanel
            // 
            gamesPanel.BackColor = Color.FromArgb(24, 29, 41);
            gamesPanel.Controls.Add(gamesHeadingLabel);
            gamesPanel.Controls.Add(gamesGrid);
            gamesPanel.Location = new Point(24, 185);
            gamesPanel.Name = "gamesPanel";
            gamesPanel.Size = new Size(1080, 265);
            gamesPanel.TabIndex = 1;
            // 
            // gamesHeadingLabel
            // 
            gamesHeadingLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            gamesHeadingLabel.ForeColor = Color.White;
            gamesHeadingLabel.Location = new Point(20, 12);
            gamesHeadingLabel.Name = "gamesHeadingLabel";
            gamesHeadingLabel.Size = new Size(1020, 35);
            gamesHeadingLabel.TabIndex = 0;
            gamesHeadingLabel.Text = "Games catalogue";
            // 
            // gamesGrid
            // 
            gamesGrid.AllowUserToAddRows = false;
            gamesGrid.AllowUserToDeleteRows = false;
            gamesGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gamesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gamesGrid.BackgroundColor = Color.FromArgb(19, 23, 33);
            gamesGrid.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(38, 49, 69);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gamesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gamesGrid.ColumnHeadersHeight = 29;
            gamesGrid.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn8, dataGridViewTextBoxColumn9, dataGridViewTextBoxColumn10, dataGridViewTextBoxColumn11 });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(27, 32, 45);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.Gainsboro;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(48, 91, 132);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            gamesGrid.DefaultCellStyle = dataGridViewCellStyle2;
            gamesGrid.EnableHeadersVisualStyles = false;
            gamesGrid.GridColor = Color.FromArgb(48, 54, 69);
            gamesGrid.Location = new Point(20, 55);
            gamesGrid.MultiSelect = false;
            gamesGrid.Name = "gamesGrid";
            gamesGrid.ReadOnly = true;
            gamesGrid.RowHeadersVisible = false;
            gamesGrid.RowHeadersWidth = 51;
            gamesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gamesGrid.Size = new Size(1035, 190);
            gamesGrid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.HeaderText = "GameID";
            dataGridViewTextBoxColumn8.MinimumWidth = 6;
            dataGridViewTextBoxColumn8.Name = "GameID";
            dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewTextBoxColumn9.HeaderText = "Game Name";
            dataGridViewTextBoxColumn9.MinimumWidth = 6;
            dataGridViewTextBoxColumn9.Name = "GameName";
            dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewTextBoxColumn10.HeaderText = "Release year";
            dataGridViewTextBoxColumn10.MinimumWidth = 6;
            dataGridViewTextBoxColumn10.Name = "ReleaseYear";
            dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewTextBoxColumn11.HeaderText = "Category";
            dataGridViewTextBoxColumn11.MinimumWidth = 6;
            dataGridViewTextBoxColumn11.Name = "GameCategory";
            dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // informationPanel
            // 
            informationPanel.BackColor = Color.FromArgb(24, 29, 41);
            informationPanel.Controls.Add(informationHeadingLabel);
            informationPanel.Controls.Add(informationGrid);
            informationPanel.Location = new Point(24, 466);
            informationPanel.Name = "informationPanel";
            informationPanel.Size = new Size(1080, 365);
            informationPanel.TabIndex = 2;
            // 
            // informationHeadingLabel
            // 
            informationHeadingLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            informationHeadingLabel.ForeColor = Color.White;
            informationHeadingLabel.Location = new Point(20, 12);
            informationHeadingLabel.Name = "informationHeadingLabel";
            informationHeadingLabel.Size = new Size(1020, 35);
            informationHeadingLabel.TabIndex = 0;
            informationHeadingLabel.Text = "Potential information fields and their purposes";
            // 
            // informationGrid
            // 
            informationGrid.AllowUserToAddRows = false;
            informationGrid.AllowUserToDeleteRows = false;
            informationGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            informationGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            informationGrid.BackgroundColor = Color.FromArgb(19, 23, 33);
            informationGrid.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(38, 49, 69);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            informationGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            informationGrid.ColumnHeadersHeight = 29;
            informationGrid.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2 });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(27, 32, 45);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = Color.Gainsboro;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(48, 91, 132);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            informationGrid.DefaultCellStyle = dataGridViewCellStyle4;
            informationGrid.EnableHeadersVisualStyles = false;
            informationGrid.GridColor = Color.FromArgb(48, 54, 69);
            informationGrid.Location = new Point(20, 55);
            informationGrid.MultiSelect = false;
            informationGrid.Name = "informationGrid";
            informationGrid.ReadOnly = true;
            informationGrid.RowHeadersVisible = false;
            informationGrid.RowHeadersWidth = 51;
            informationGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            informationGrid.Size = new Size(1035, 290);
            informationGrid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Field";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Possible purpose";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // gameInfoPanel
            // 
            gameInfoPanel.BackColor = Color.FromArgb(24, 29, 41);
            gameInfoPanel.Controls.Add(gameInfoHeadingLabel);
            gameInfoPanel.Controls.Add(gameInfoGrid);
            gameInfoPanel.Location = new Point(24, 847);
            gameInfoPanel.Name = "gameInfoPanel";
            gameInfoPanel.Size = new Size(1080, 290);
            gameInfoPanel.TabIndex = 3;
            // 
            // gameInfoHeadingLabel
            // 
            gameInfoHeadingLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            gameInfoHeadingLabel.ForeColor = Color.White;
            gameInfoHeadingLabel.Location = new Point(20, 12);
            gameInfoHeadingLabel.Name = "gameInfoHeadingLabel";
            gameInfoHeadingLabel.Size = new Size(1020, 35);
            gameInfoHeadingLabel.TabIndex = 0;
            gameInfoHeadingLabel.Text = "Game-by-game data examples";
            // 
            // gameInfoGrid
            // 
            gameInfoGrid.AllowUserToAddRows = false;
            gameInfoGrid.AllowUserToDeleteRows = false;
            gameInfoGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gameInfoGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gameInfoGrid.BackgroundColor = Color.FromArgb(19, 23, 33);
            gameInfoGrid.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(38, 49, 69);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            gameInfoGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            gameInfoGrid.ColumnHeadersHeight = 29;
            gameInfoGrid.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(27, 32, 45);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.Gainsboro;
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(48, 91, 132);
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            gameInfoGrid.DefaultCellStyle = dataGridViewCellStyle6;
            gameInfoGrid.EnableHeadersVisualStyles = false;
            gameInfoGrid.GridColor = Color.FromArgb(48, 54, 69);
            gameInfoGrid.Location = new Point(20, 55);
            gameInfoGrid.MultiSelect = false;
            gameInfoGrid.Name = "gameInfoGrid";
            gameInfoGrid.ReadOnly = true;
            gameInfoGrid.RowHeadersVisible = false;
            gameInfoGrid.RowHeadersWidth = 51;
            gameInfoGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gameInfoGrid.Size = new Size(1035, 215);
            gameInfoGrid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Game";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Illustrative data";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // historyPanel
            // 
            historyPanel.BackColor = Color.FromArgb(24, 29, 41);
            historyPanel.Controls.Add(historyHeadingLabel);
            historyPanel.Controls.Add(historyGrid);
            historyPanel.Location = new Point(24, 1153);
            historyPanel.Name = "historyPanel";
            historyPanel.Size = new Size(1080, 365);
            historyPanel.TabIndex = 4;
            // 
            // historyHeadingLabel
            // 
            historyHeadingLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            historyHeadingLabel.ForeColor = Color.White;
            historyHeadingLabel.Location = new Point(20, 12);
            historyHeadingLabel.Name = "historyHeadingLabel";
            historyHeadingLabel.Size = new Size(1020, 35);
            historyHeadingLabel.TabIndex = 0;
            historyHeadingLabel.Text = "Hypothetical player history";
            // 
            // historyGrid
            // 
            historyGrid.AllowUserToAddRows = false;
            historyGrid.AllowUserToDeleteRows = false;
            historyGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            historyGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            historyGrid.BackgroundColor = Color.FromArgb(19, 23, 33);
            historyGrid.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(38, 49, 69);
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = Color.White;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            historyGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            historyGrid.ColumnHeadersHeight = 29;
            historyGrid.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7 });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.FromArgb(27, 32, 45);
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle8.ForeColor = Color.Gainsboro;
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(48, 91, 132);
            dataGridViewCellStyle8.SelectionForeColor = Color.White;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            historyGrid.DefaultCellStyle = dataGridViewCellStyle8;
            historyGrid.EnableHeadersVisualStyles = false;
            historyGrid.GridColor = Color.FromArgb(48, 54, 69);
            historyGrid.Location = new Point(20, 55);
            historyGrid.MultiSelect = false;
            historyGrid.Name = "historyGrid";
            historyGrid.ReadOnly = true;
            historyGrid.RowHeadersVisible = false;
            historyGrid.RowHeadersWidth = 51;
            historyGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            historyGrid.Size = new Size(1035, 290);
            historyGrid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Field";
            dataGridViewTextBoxColumn5.MinimumWidth = 6;
            dataGridViewTextBoxColumn5.Name = "HistoryField";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Example value";
            dataGridViewTextBoxColumn6.MinimumWidth = 6;
            dataGridViewTextBoxColumn6.Name = "HistoryExample";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.HeaderText = "Value type";
            dataGridViewTextBoxColumn7.MinimumWidth = 6;
            dataGridViewTextBoxColumn7.Name = "HistoryType";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 18, 26);
            ClientSize = new Size(1400, 900);
            Controls.Add(contentPanel);
            Controls.Add(headerPanel);
            Controls.Add(sidebarPanel);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1050, 650);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Axes In Motion — Sign-up Information Research";
            sidebarPanel.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            contentPanel.ResumeLayout(false);
            titlePanel.ResumeLayout(false);
            gamesPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gamesGrid).EndInit();
            informationPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)informationGrid).EndInit();
            gameInfoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gameInfoGrid).EndInit();
            historyPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)historyGrid).EndInit();
            ResumeLayout(false);
        }

        private FlowLayoutPanel flowLayoutPanel1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    }
}
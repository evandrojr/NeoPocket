namespace Neo.Pocket.Controls
{
    #region Using

    using System;
    using System.Windows.Forms;
    using System.ComponentModel;
    using System.Collections;
    using System.Data;

    #endregion

    #region Documentation
    /// <summary>
    /// <autor>Tiago Freire Nascimento</autor>
    /// <data>10:50</data>
    /// <description>Componente criado para paginar um componente NeoDataGrid</description>
    /// </summary>
    #endregion

    [Serializable()]
    public class NeoDataGridPager : Control
    {
        #region Layout Fields

        private Button BtnFirst;
        private Button BtnBack;
        private Button BtnNext;
        private Button BtnLast;
        private Label LblRodape;

        #endregion

        #region Fields

        private NeoDataGrid owner;
        private int pageIndex = 0;
        private int pageSize = 50;
        private object originalDataSource = null;
        private object currentDataSource = null;

        #endregion

        #region Ctor

        public NeoDataGridPager()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public NeoDataGrid Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;

                if (owner != null)
                {
                    OriginalDataSource = owner.DataSource;
                }
            }
        }

        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public Int32 PageCount
        {
            get
            {
                if (PageSize > 0)
                {
                    Int32 result = 0;

                    if (OriginalDataSource != null)
                    {
                        if ((OriginalDataSource as IListSource).GetList().Count % PageSize == 0)
                        {
                            return ((OriginalDataSource as IListSource).GetList().Count / PageSize) - 1;
                        }
                        else
                        {
                            return (OriginalDataSource as IListSource).GetList().Count / PageSize;
                        }
                    }
                    return result;
                }
                else
                    return 0;
            }
        }

        public Int32 RowCount
        {
            get
            {
                if (this.OriginalDataSource != null)
                {
                    return (OriginalDataSource as DataTable).Rows.Count;
                }
                else
                    return 0;
            }
        }

        public object CurrentDataSource
        {
            get { return currentDataSource; }
            internal set
            {
                currentDataSource = value;

                if (this.Owner != null)
                {
                    this.Owner.DataSource = currentDataSource;
                    this.Owner.Invalidate();
                }
            }
        }

        internal object OriginalDataSource
        {
            get { return originalDataSource; }
            set
            {
                originalDataSource = value;

                if (originalDataSource != null)
                {
                    Paginate();
                }
                else
                {
                    CurrentDataSource = null;
                }
            }
        }

        #endregion

        #region Pagination Methods

        public void Paginate()
        {
            // Válida o tamanho da página e o indice da página
            if ((PageSize > 0) && (PageIndex >= 0))
            {
                IList sourceList = (OriginalDataSource as IListSource).GetList();

                if (sourceList.Count > 0)
                {
                    // A lista original tem menos itens ou é igual ao limite de itens
                    if (sourceList.Count <= PageSize)
                    {
                        CurrentDataSource = originalDataSource;
                    }
                    else
                    {
                        DataTable paginado = (OriginalDataSource as DataTable).Clone();
                        paginado.Rows.Clear();

                        Int32 begin = 0;
                        Int32 end = 0;

                        if (PageIndex == 0) // Primeira página
                        {
                            begin = 0;
                            end = PageSize;
                        }
                        else // Páginas maiores que zero
                        {
                            begin = PageIndex * PageSize;
                            end = begin + PageSize;

                            if (end > sourceList.Count) // Se o final ultrapassar o tamanho da collection
                                end = sourceList.Count;
                        }

                        for (int i = begin; i < end; i++)
                        {
                            paginado.ImportRow((OriginalDataSource as DataTable).Rows[i]);
                        }

                        CurrentDataSource = paginado;

                        LblRodape.Text = PageIndex * PageSize + " até " + ((PageIndex * PageSize) + PageSize) + " de " + RowCount;

                        try
                        {
                            this.Owner.Select(0);
                        }
                        catch { }

                        BtnFirst.Enabled = sourceList.Count > 0 && pageIndex > 0;   
                        BtnBack.Enabled = sourceList.Count > 0 && pageIndex > 0;
                        BtnNext.Enabled = sourceList.Count > 0 && pageIndex < PageCount;
                        BtnLast.Enabled = sourceList.Count > 0 && pageIndex < PageCount;
                    }
                }
                else
                    CurrentDataSource = null;
            }
            else
            {
                CurrentDataSource = null;
            }
        }

        public void FirstPage()
        {
            PageIndex = 0;
            Paginate();
        }
        public void LastPage()
        {
            PageIndex = PageCount;
            Paginate();
        }
        public void BackPage()
        {
            if (PageIndex > 0)
            {
                PageIndex--;
                Paginate();
            }
        }
        public void NextPage()
        {
            if (PageIndex < PageCount)
            {
                PageIndex++;
                Paginate();
            }
        }

        #endregion

        #region Pagination Event Handlers

        private void BtnLast_Click(object sender, EventArgs e)
        {
            this.LastPage();
        }

        private void BtnFirst_Click(object sender, EventArgs e)
        {
            this.FirstPage();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.BackPage();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            this.NextPage();
        }

        #endregion

        #region InitializeComponent

        private void InitializeComponent()
        {
            this.BtnFirst = new System.Windows.Forms.Button();
            this.BtnBack = new System.Windows.Forms.Button();
            this.BtnNext = new System.Windows.Forms.Button();
            this.BtnLast = new System.Windows.Forms.Button();
            this.LblRodape = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnFirst
            // 
            this.BtnFirst.Enabled = false;
            this.BtnFirst.Location = new System.Drawing.Point(0, 3);
            this.BtnFirst.Name = "BtnFirst";
            this.BtnFirst.Size = new System.Drawing.Size(30, 17);
            this.BtnFirst.TabIndex = 0;
            this.BtnFirst.Text = "<<";
            this.BtnFirst.Click += new System.EventHandler(this.BtnFirst_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.Enabled = false;
            this.BtnBack.Location = new System.Drawing.Point(32, 3);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(30, 17);
            this.BtnBack.TabIndex = 2;
            this.BtnBack.Text = "<";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // BtnNext
            // 
            this.BtnNext.Enabled = false;
            this.BtnNext.Location = new System.Drawing.Point(64, 3);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.Size = new System.Drawing.Size(30, 17);
            this.BtnNext.TabIndex = 3;
            this.BtnNext.Text = ">";
            this.BtnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // BtnLast
            // 
            this.BtnLast.Enabled = false;
            this.BtnLast.Location = new System.Drawing.Point(96, 3);
            this.BtnLast.Name = "BtnLast";
            this.BtnLast.Size = new System.Drawing.Size(30, 17);
            this.BtnLast.TabIndex = 1;
            this.BtnLast.Text = ">>";
            this.BtnLast.Click += new System.EventHandler(this.BtnLast_Click);
            // 
            // LblRodape
            // 
            this.LblRodape.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.LblRodape.Location = new System.Drawing.Point(128, 3);
            this.LblRodape.Name = "LblRodape";
            this.LblRodape.Size = new System.Drawing.Size(100, 20);
            this.LblRodape.Visible = false;
            // 
            // NeoDataGridPager
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.LblRodape);
            this.Controls.Add(this.BtnLast);
            this.Controls.Add(this.BtnNext);
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.BtnFirst);
            this.Size = new System.Drawing.Size(30, 17);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

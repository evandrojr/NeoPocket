namespace Neo.Pocket.Controls
{
    #region Using

    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;

    #endregion

    #region Documentation
    /// <summary>
    /// <autor>Tiago Freire Nascimento</autor>
    /// <data>25/08/2009</data>
    /// <description>Define uma coluna para o NeoDataGrid customizavel, permitindo funcionalidades
    /// que não são implementadas na DataGrid nativa do framework como por exemplo: ordenação, paginação, customização
    /// individual de cada célula.</description>
    /// <pendencias>
    /// 1) Formatação da célula não está funcionando;
    /// 2) Implementar eventos de validação por célula;
    /// 3) Conseguir selecionar a linha quando ela está em edição;
    /// </pendencias>
    /// </summary>
    #endregion

    [Serializable()]
    public abstract class NeoDataGridCustomColumnBase : DataGridTextBoxColumn
    {
        #region Fields

        private const String DesignTimeCategoryName = "Neo";

        private StringFormat stringFormat = null;
        private NeoDataGrid owner = null;
        private Int32 columnNumber = -1;
        private Int32 rowNumber = 0;
        private Control hostedControl = null;
        private Rectangle bounds = Rectangle.Empty;
        private Boolean readOnly = false;
        private Color? alternatingColor = SystemColors.Window;
        private Color foreColor = SystemColors.WindowText;
        private Color backColor = SystemColors.Window;
        private String text = String.Empty;

        #endregion

        #region Delegates and Events

        public delegate void NeoDataGridTextBoxColumnEventHandler(object sender, NeoDataGridCustomColumnEventArgs e);
        public event NeoDataGridTextBoxColumnEventHandler OnDrawColumnCell;

        #endregion

        #region Public Properties

        public Rectangle Bounds
        {
            get { return bounds; }
        }

        public Color ForeColor
        {
            get { return foreColor; }
            set
            {
                foreColor = value;
                Invalidate();
            }
        }

        public Color BackColor
        {
            get { return backColor; }
            set
            {
                backColor = value;
                Invalidate();
            }
        }

        public Color? AlternatingColor
        {
            get { return alternatingColor; }
            set
            {
                alternatingColor = value;
                Invalidate();
            }
        }

        public HorizontalAlignment Alignment
        {
            get
            {
                return (this.StringFormat.Alignment == StringAlignment.Center) ? HorizontalAlignment.Center : (this.StringFormat.Alignment == StringAlignment.Far) ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            }
            set
            {
                this.StringFormat.Alignment = (value == HorizontalAlignment.Center) ? StringAlignment.Center : (value == HorizontalAlignment.Right) ? StringAlignment.Far : StringAlignment.Near;
                Invalidate();
            }
        }

        public virtual Boolean ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
                Invalidate();
            }
        }

        public StringFormat StringFormat
        {
            get
            {
                if (stringFormat == null)
                {
                    stringFormat = new StringFormat();
                    this.Alignment = HorizontalAlignment.Left;
                }
                return stringFormat;
            }
        }

        public virtual object Nullvalue
        {
            get
            {
                return this.NullText;
            }
            set
            {
                this.NullText = value.ToString();
            }
        }

        public int RowNumber
        {
            get
            {
                return rowNumber;
            }
        }

        public int ColumnNumber
        {
            get
            {
                if ((columnNumber == -1) && (Owner != null))
                {
                    foreach (DataGridTableStyle table in this.Owner.TableStyles)
                    {
                        this.columnNumber = table.GridColumnStyles.IndexOf(this);
                        if (this.columnNumber != -1)
                        {
                            break;
                        }
                    }
                }
                return columnNumber;
            }
        }

        public NeoDataGrid Owner
        {
            get
            {
                if (owner == null)
                {
                    throw new InvalidOperationException("Coluna não está vinculada a um NeoDataGrid");
                }
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        public Control HostedControl
        {
            get
            {
                if ((hostedControl == null) && (this.Owner != null))
                {
                    this.hostedControl = this.CreateHostedControl();
                    this.hostedControl.Visible = false;
                    this.hostedControl.Name = this.HeaderText;
                    this.hostedControl.Font = this.Owner.Font;
                    this.Owner.Controls.Add(this.hostedControl);

                    this.hostedControl.DataBindings.Add(this.GetBoundPropertyName(), Owner.DataSource, this.MappingName, true, DataSourceUpdateMode.OnValidation, this.Nullvalue);
                    HScrollBar hScroll = null;
                    foreach (Control c in this.Owner.Controls)
                    {
                        if ((hScroll = c as HScrollBar) != null)
                        {
                            hScroll.ValueChanged += new EventHandler(GridScrolled);
                            break;
                        }
                    }
                }
                return hostedControl;
            }
        }

        public String Text
        {
            get
            {
                return text;
            }
        }

        #endregion

        #region Private Methods

        private void GridScrolled(object sender, EventArgs e)
        {
            UpdateHostedControl();
        }

        #endregion

        #region Protected Methods

        protected void UpdateHostedControl()
        {
            Rectangle selectedBounds = this.Owner.GetCellBounds(this.Owner.CurrentCell.RowNumber, this.Owner.CurrentCell.ColumnNumber);

            if (!this.ReadOnly && (this.ColumnNumber == this.Owner.CurrentCell.ColumnNumber)
                && this.Owner.HitTest(selectedBounds.Left, selectedBounds.Top).Type == DataGrid.HitTestType.Cell
                && this.Owner.HitTest(selectedBounds.Right, selectedBounds.Bottom).Type == DataGrid.HitTestType.Cell)
            {
                if (selectedBounds != this.bounds)
                {
                    this.bounds = selectedBounds;
                    this.HostedControl.Size = new Size(selectedBounds.Width, selectedBounds.Height);
                    this.HostedControl.Bounds = selectedBounds;
                    this.HostedControl.Focus();
                    this.HostedControl.Update();
                }
                if (!this.HostedControl.Visible)
                {
                    this.HostedControl.Show();
                    this.HostedControl.Focus();
                }
            }
            else if (this.HostedControl.Visible)
            {
                this.HostedControl.Hide();
            }
        }

        protected void Invalidate()
        {
            if (this.owner != null)
            {
                this.owner.Invalidate();
            }
        }

        protected abstract String GetBoundPropertyName();

        protected abstract Control CreateHostedControl();

        protected virtual void DrawBackground(Graphics g, Rectangle bounds, int rowNum, Brush backBrush)
        {
            Brush background = backBrush;
            Color color = backColor;

            if (alternatingColor != null)
            {
                if ((background != null) && ((rowNum & 1) != 0) && !Owner.IsSelected(rowNum))
                {
                    color = alternatingColor.Value;
                }
            }

            if (!Owner.IsSelected(rowNum))
            {
                background = new SolidBrush(color);
            }
            else
                background = new SolidBrush(this.Owner.SelectionBackColor);

            g.FillRectangle(background, bounds);
        }

        protected virtual String FormatText(object cellData)
        {
            String cellText = String.Empty;

            if ((cellData == null) || (DBNull.Value == cellData))
            {
                cellText = this.NullText;
            }
            else if (cellData is IFormattable)
            {
                cellText = ((IFormattable)cellData).ToString(this.Format, this.FormatInfo);
            }
            else if (cellData is IConvertible)
            {
                cellText = ((IConvertible)cellData).ToString(this.FormatInfo);
            }
            else
            {
                cellText = cellData.ToString();
            }

            return cellText;
        }

        #endregion

        #region Events Overrides

        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
        {
            RectangleF textBounds;
            object cellData;

            cellData = this.PropertyDescriptor.GetValue(source.List[rowNum]);
            rowNumber = rowNum;

            text = FormatText(cellData);
           
            DrawBackground(g, bounds, rowNum, backBrush);

            bounds.Inflate(-2, -2);

            textBounds = new RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            foreBrush = new SolidBrush(foreColor);

            g.DrawString(this.Text, this.Owner.Font, foreBrush, textBounds, this.StringFormat);

            this.UpdateHostedControl();

            if (OnDrawColumnCell != null)
            {
                OnDrawColumnCell(this, new NeoDataGridCustomColumnEventArgs(this));
            }
        }

        #endregion
    }
}

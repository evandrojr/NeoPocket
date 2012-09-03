namespace Neo.Pocket.Controls
{
    #region Using

    using System.Windows.Forms;
    using System;

    #endregion

    [Serializable()]
    [System.ComponentModel.DesignTimeVisible(false)]
    public class NeoDataGridCustomTextBoxColumn : NeoDataGridCustomColumnBase
    {
        protected override string GetBoundPropertyName()
        {
            return "Text";
        }

        protected override System.Windows.Forms.Control CreateHostedControl()
        {
            TextBox box = new TextBox();
            box.BorderStyle = BorderStyle.None;
            box.Multiline = false;
            box.TextAlign = this.Alignment;
            return box;
        }
    }
}

using System;
using System.Windows.Forms;

namespace PearlCalculatorWFA
{
    public interface DataCheckModifier<T>
    {
        void Check(T data);
    }


    public sealed class ZeroToOneCheckModifier : DataCheckModifier<TextBoxZeroToOneCheckerArgs>
    {
        public static ZeroToOneCheckModifier CheckModifier = new ZeroToOneCheckModifier();

        public void Check(TextBoxZeroToOneCheckerArgs data)
        {
            var textBox = data.TextBox;

            if (textBox.Text.Length == 0)
            {
                data.Value = 0;
                return;
            }

            if (textBox.Text.Length == 1)
            {
                if (textBox.Text[0] == '1')
                {
                    data.Value = 1;
                    return;
                }

                if (textBox.Text[0] == '0' || textBox.Text[0] == '.')
                {
                    data.Value = 0;
                    return;
                }
            }

            if (textBox.Text.Length == 2 && textBox.Text[0] == '0' && textBox.Text[1] == '.')
            {
                data.Value = 0;
                return;
            }

            if (Double.TryParse(textBox.Text, out var value))
            {
                value = value > 1 ? 1 : value;
                value = value < 0 ? 0 : value;

                data.Value = value;

                if (value != Double.Parse(textBox.Text))
                {
                    textBox.Text = value.ToString();
                    textBox.Select(textBox.Text.Length, 0);
                }
            }
        }
    }


    public class TextBoxZeroToOneCheckerArgs
    {
        public TextBox TextBox;
        public double Value;
    }
}

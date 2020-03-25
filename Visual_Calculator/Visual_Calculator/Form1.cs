using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
namespace Visual_Calculator
{
    //--------------------------------------------------------------------
    
    //--------------------------------------------------------------------
    enum Calculator_Opertaions {
        none=-1,
        add=0,
        subtract=1,
        multiplicate=2,
        divide=3,
    }//Calculator_Opertaions
    enum Calculator_Current_Number {
        leftNumber=0,
        rightNumber=1,
    }//Calculator_Current_Number
    public partial class FrmCalculator : Form
    {
        double dblNum1, dblNum2, dblResult;
        Calculator_Opertaions _operation;
        Calculator_Current_Number ccn;
        bool numlock;
        private bool protectKeys;
        private void btnNumeric_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            lblDisplay.Text += btn.Text;
        }//btnNumeric_Click

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ccn == Calculator_Current_Number.leftNumber)
            {
                dblNum1 = double.Parse(lblDisplay.Text);
                lblNum1.Text = lblDisplay.Text;
                lblDisplay.Text="";
                ccn = Calculator_Current_Number.rightNumber;
                _operation = Calculator_Opertaions.add;
                lblOperation.Text = "+";
            }//if left number
            else if (ccn == Calculator_Current_Number.rightNumber) {

            }//else if it is not left number
        }//btnAdd_Click

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (ccn == Calculator_Current_Number.rightNumber)
            {
                dblNum2 = double.Parse(lblDisplay.Text);
                lblNum2.Text = lblDisplay.Text;
                switch (_operation)
                {
                    case Calculator_Opertaions.none:
                        break;
                    case Calculator_Opertaions.add:
                        dblResult = dblNum1 + dblNum2;
                        break;
                    case Calculator_Opertaions.subtract:
                        dblResult = dblNum1 - dblNum2;
                        break;
                    case Calculator_Opertaions.multiplicate:
                        dblResult = dblNum1 * dblNum2;
                        break;
                    case Calculator_Opertaions.divide:
                        dblResult = dblNum1 / dblNum2;
                        break;
                    default:
                        break;
                }//case
                
                lblDisplay.Text = dblResult.ToString();
                restartCalculator();
            }//if second number is entered 
        }//btnEnter_Click

        private void restartCalculator()
        {   
            dblNum1 = 0;
            dblNum2 = 0;
            dblResult = 0;
            ccn = Calculator_Current_Number.leftNumber;
            _operation = Calculator_Opertaions.none;
        }//restartCalculator

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (ccn == Calculator_Current_Number.leftNumber)
            {
                dblNum1 = double.Parse(lblDisplay.Text);
                lblNum1.Text = lblDisplay.Text;
                lblDisplay.Text = "";
                ccn = Calculator_Current_Number.rightNumber;
                _operation = Calculator_Opertaions.subtract;
                lblOperation.Text = "-";
            }//if left number
            else if (ccn == Calculator_Current_Number.rightNumber)
            {

            }//else if it is not left number
        }//btnMinus_Click

        private void btnMultiplicate_Click(object sender, EventArgs e)
        {
            if (ccn == Calculator_Current_Number.leftNumber)
            {
                dblNum1 = double.Parse(lblDisplay.Text);
                lblNum1.Text = lblDisplay.Text;
                lblDisplay.Text = "";
                ccn = Calculator_Current_Number.rightNumber;
                _operation = Calculator_Opertaions.multiplicate;
                lblOperation.Text = "X";
            }//if left number
            else if (ccn == Calculator_Current_Number.rightNumber)
            {

            }//else if it is not left number
        }//btnMultiplicate_Click

        private void btnDivide_Click(object sender, EventArgs e)
        {
            if (ccn == Calculator_Current_Number.leftNumber)
            {
                dblNum1 = double.Parse(lblDisplay.Text);
                lblNum1.Text = lblDisplay.Text;
                lblDisplay.Text = "";
                ccn = Calculator_Current_Number.rightNumber;
                _operation = Calculator_Opertaions.divide;
                lblOperation.Text = "/";
            }//if left number
            else if (ccn == Calculator_Current_Number.rightNumber)
            {

            }//else if it is not left number
        }//btnDivide_Click

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = "";
            lblNum1.Text = "";
            lblNum2.Text = "";
            lblOperation.Text = "";
            restartCalculator();
        }//btnClear_Click

        private void FrmCalculator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumLock) {
                if (numlock) {
                    btnNumLock.BackColor = btn0.BackColor;
                    numlock = false;
                }//if num lock is on 
                else {
                    btnNumLock.BackColor = Color.Green;
                    numlock = true;
                }//else if numLock is off
            }//if num lock is pressed 
        }//FrmCalculator_KeyDown

        private void btnNumLock_Click(object sender, EventArgs e)
        {
            if (protectKeys)
                return;
            protectKeys = true;
            NativeMethods.SimulateKeyPress(NativeMethods.VK_NUMLOCK);
            protectKeys = false;
        }//btnNumLock_Click

        private void FrmCalculator_Load(object sender, EventArgs e)
        {
            dblNum1 = 0;
            dblNum2 = 0;
            dblResult = 0;
            _operation = Calculator_Opertaions.none;
            ccn = Calculator_Current_Number.leftNumber;
            if (Control.IsKeyLocked(Keys.NumLock)) {
                btnNumLock.BackColor = Color.Green;
                numlock = true;
            }//if  Num Lock is On 
        }//FrmCalculator_Load

        public FrmCalculator()
        {
            InitializeComponent();
        }//FrmCalculator
    }//Form

    public static class NativeMethods
    {
        public const byte VK_NUMLOCK = 0x90;
        public const uint KEYEVENTF_EXTENDEDKEY = 1;
        public const int KEYEVENTF_KEYUP = 0x2;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        public static void SimulateKeyPress(byte keyCode)
        {
            keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }
    }


}//Visual_Calculator
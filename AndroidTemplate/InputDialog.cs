using Android.App;
using Android.Content;
using Android.Widget;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Icu.Text.CaseMap;

namespace AndroidTemplate
{
    public class InputDialog
    {
        private AndroidGameActivity _activity;

        private bool isShowing;
        private Action<string> returnFunction;

        public InputDialog(AndroidGameActivity activity)
        {
            _activity = activity;
        }

        /// <summary>
        /// Creates and shows an input dialog
        /// </summary>
        /// <param name="title">Title of the dialog</param>
        /// <param name="_returnFunction">Function to call when the input is entered</param>
        public void ShowInputDialog(string title, Action<string> _returnFunction)
        {
            if (!isShowing)
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(_activity);
                EditText editText = new EditText(_activity);
                editText.Id = 42;
                dialog.SetTitle(title);
                dialog.SetView(editText);
                dialog.SetPositiveButton("OK", InputDialogResponse);
                dialog.SetCancelable(false);
                dialog.Show();

                isShowing = true;
                returnFunction = _returnFunction;
            }
        }

        private void InputDialogResponse(object sender, DialogClickEventArgs e)
        {
            string dialogText = ((sender as AlertDialog).FindViewById(42) as EditText).Text;
            returnFunction(dialogText);

            isShowing = false;
            returnFunction = null;
        }
    }
}

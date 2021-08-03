using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CIS_DBLayer;
using CIS_ImageViewer;
using CIS_ImageViewer.Tools;
using Copperfield.Windows.Forms.Native;
	

namespace CIS_Textil
{
    /// <summary>
    /// Demonstrates how to build an image viewer with CrocusPictureShow, CrocusImageGridView, and CrocusPictureShowController
    /// </summary>
    public partial class frmSrchImg : Form
    {
        #region Fields

        /// <summary>
        /// Controller object that bundles the CrocusPictureShow and CrocusImageGridView controls together
        /// and provides the most common operations between them.
        /// </summary>
        public string sSearchText = "";
        private CrocusPictureShowController _picController = new CrocusPictureShowController();

        private Assembly _thisAssembly = null;

        private FormWindowState _oldWindowState;
        private Size _oldSize;
        private Point _oldLocation;

        private System.Windows.Forms.Orientation _oldOrientation;
        private bool _thumbCollapsed;
        private bool _imageCollapsed;

        private CrocusViewMode _oldViewMode;

        #endregion

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmSrchImg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Open Folder menu item event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = _picController.Collector.ImageLocation;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // Initializes the collector object with the current folder 
                _picController.InitCollector(folderBrowserDialog1.SelectedPath, 0);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Initializes the default thumbnail image.  This is the image the user sees
        /// while the grid is populating the actual images from the disk.
        /// </summary>
        private void InitDefaultThumb()
        {
            Image defaultThumb =
                CrocusTools.GetAssemblyImage(_thisAssembly, "CIS_Textil.resources.DefaultThumb.png");
            Bitmap theDefaultImage = new Bitmap(defaultThumb);
            CrocusTools.DefaultImage = theDefaultImage;
        }

        /// <summary>
        /// Initializes the picture show controller object.
        /// This ties a bunch of miscelleaneous controls together:
        /// left/right toolbar buttons for navigation, combo box for zoom magnification,
        /// trackbar for magnification.
        /// Plus, the CrocusPictureShow and CrocusImageGridView objects!
        /// </summary>
        private void InitController()
        {
            _picController.InitNavButtons(rightButton, leftButton);
            _picController.InitZoomControls(zoomToolStripTrackBar, zoomComboBox);

            _picController.MainAppCaption = "";

            _picController.CrocusImageDisplayed += new EventHandler(_picController_CrocusImageDisplayed);

            _picController.InitController(crystalImageGridView1,
                                        viewerMain,
                                        this);

            _picController.InitTrackerControl();
            _picController.CurrentViewMode = CrocusViewMode.SplitView;

            //_picController.InitCollector(string.Empty, 0);  // MyPictures folder default
        }

        /// <summary>
        /// Handles the CrocusImageDisplayed event from the controller.
        /// We do nothing here in this demo, but if you need it in the future,
        /// here it is.
        /// </summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event arguments.</param>
        void _picController_CrocusImageDisplayed(object sender, EventArgs e)
        {
            CrocusImageSelectEventArgs imageEventArgs = (CrocusImageSelectEventArgs)e;
            // do whatever you like with the selected image...
            // imageEventArgs.selectedImage;
        }

        /// <summary>
        /// The load event for this form.
        /// Initializes the default thumbnail image and the controller.
        /// </summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event arguments.</param>
        private void SrchImg_Load(object sender, EventArgs e)
        {
            _thisAssembly = Assembly.GetAssembly(typeof(frmSrchImg));
            // Uncomment this line to see the list of resources
            // within the assembly.
            //CrocusTools.DumpManifestResourceNames(_thisAssembly);


            //InitDefaultThumb();
            InitController();
            cboFilterType.SelectedIndex = 0;
            if (sSearchText.Length > 0)
            {
                txtSearch.Text = sSearchText;
                btnShowImg_Click(null, null);
            }
            else
                txtSearch.Text = "";
            txtSearch.Focus();
        }

        /// <summary>
        /// Allows the form to switch into a total thumbnail view.
        /// </summary>
        private void ShowThumbnailView()
        {
            this.imageSplitContainer.Panel1Collapsed = true;
            this.imageSplitContainer.Panel2Collapsed = false;

            btnShowImg.Visible = true;
            txtSearch.Visible = true;
            cboFilterType.Visible = true;

            crystalImageGridView1.Orientation = System.Windows.Forms.Orientation.Vertical;
            crystalImageGridView1.Focus();
            _picController.CurrentViewMode = CrocusViewMode.ThumbView;
            _picController.CenterCurrentImage();
        }

        /// <summary>
        /// Allows the form to have a split view between thumbnails and the selected image.
        /// </summary>
        private void ShowSplitView()
        {
            this.imageSplitContainer.Panel1Collapsed = false;
            this.imageSplitContainer.Panel2Collapsed = false;

            btnShowImg.Visible = true;
            txtSearch.Visible = true;
            cboFilterType.Visible = true;

            try
            {
                if (crystalImageGridView1 == null)
                    crystalImageGridView1 = new CrocusImageGridView();

                crystalImageGridView1.Orientation = System.Windows.Forms.Orientation.Horizontal;
                crystalImageGridView1.Focus();
                _picController.CurrentViewMode = CrocusViewMode.SplitView;
                _picController.CenterCurrentImage();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Allows the form to hide thumbnails and just show the selected image.
        /// </summary>
        private void ShowImageView()
        {
            this.imageSplitContainer.Panel1Collapsed = false;
            this.imageSplitContainer.Panel2Collapsed = true;

            btnShowImg.Visible = false;
            txtSearch.Visible = false;
            cboFilterType.Visible = false;

            try
            {
                viewerMain.Focus();
                _picController.CurrentViewMode = CrocusViewMode.ImageView;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

        }

        private void thumbnailButton_Click(object sender, EventArgs e)
        {
            ShowThumbnailView();
        }

        private void splitViewButton_Click(object sender, EventArgs e)
        {
            ShowSplitView();
        }

        private void imageViewButton_Click(object sender, EventArgs e)
        {
            ShowImageView();
        }

        /// <summary>
        /// Forces a invalidation of the entire window, which includes the form border.
        /// </summary>
        private void InvalidateWindow()
        {
            NativeMethods.RedrawWindow(this, RDW.FRAME | RDW.UPDATENOW | RDW.INVALIDATE | RDW.ALLCHILDREN);
        }

        /// <summary>
        /// Zooms the form into full screen mode by hiding the border, toolbars, and menus.
        /// </summary>
        private void FormZoomMode()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            _picController.FullScreenMode();

            WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Puts the form back into sizeable mode by showing the border, toolbars, and menus.
        /// </summary>
        private void SizableFormMode()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.navToolStrip.Visible = true;
            Cursor.Show();
            viewerMain.ShowTooltip();
            viewerMain.ShowScrollBars(true);
        }

        /// <summary>
        /// Saves the current windows state in order to restore it after full screen or slideshow modes.
        /// </summary>
        private void SaveWindowState()
        {
            _oldWindowState = WindowState;
            _oldSize = this.Size;
            _oldLocation = this.Location;
            _oldOrientation = crystalImageGridView1.Orientation;

            _imageCollapsed = imageSplitContainer.Panel1Collapsed;
            _thumbCollapsed = imageSplitContainer.Panel2Collapsed;

            _oldViewMode = _picController.CurrentViewMode;
        }

        /// <summary>
        /// Restores the window state after full screen or slideshow modes.
        /// </summary>
        private void RestoreWindowState()
        {
            WindowState = _oldWindowState;
            this.Size = _oldSize;
            this.Location = _oldLocation;
            crystalImageGridView1.Orientation = _oldOrientation;

            imageSplitContainer.Panel1Collapsed = _imageCollapsed;
            imageSplitContainer.Panel2Collapsed = _thumbCollapsed;

            _picController.CurrentViewMode = _oldViewMode;

            switch (_picController.CurrentViewMode)
            {
                case CrocusViewMode.ImageView:
                    ShowImageView();
                    break;
                case CrocusViewMode.SplitView:
                    ShowSplitView();
                    break;
                case CrocusViewMode.ThumbView:
                    ShowThumbnailView();
                    break;
            }
        }

        /// <summary>
        /// Starts a slideshow at a given index in the current collector.
        /// </summary>
        /// <param name="startIndex"></param>
        private void StartSlideShow(int startIndex)
        {
            //if (!_picController.CanStartSlideShow())
            //    return;

            //if (startIndex < 0)
            //    startIndex = 0;

            //// Show the Slideshow options form.
            //OptionsForm optForm = new OptionsForm();
            //optForm.SlideshowOptions = _picController.PictureShow.SlideShowOptions;
            //if (optForm.ShowDialog(this) == DialogResult.OK)
            //    _picController.PictureShow.SlideShowOptions = optForm.SlideshowOptions;
            //else
            //    return;

            //SaveWindowState();

            //this.Visible = false;
            //InvalidateWindow();

            //FormZoomMode();

            //// Start the slide show.
            //// Note that you can start at any index within the current collector/model.
            //_picController.StartSlideShow(startIndex);

            //this.Visible = true;
            //InvalidateWindow();
            //viewerMain.Focus();
        }

        private void slideshowButton_Click(object sender, EventArgs e)
        {
            StartSlideShow(0);
        }

        /// <summary>
        /// Handle the slide show terminated event.
        /// </summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event arguments.</param>
        private void viewerMain_SlideShowTerminated(object sender, EventArgs e)
        {
            CrocusImageSelectEventArgs imageEvent = (CrocusImageSelectEventArgs)e;

            this.Visible = false;
            InvalidateWindow();

            SizableFormMode();
            this.Visible = true;
            RestoreWindowState();

            // The SlideShow Terminated events tells us what image was last viewed in the
            // slideshow.  We take that image, select it in the CrocusImageGridView control,
            // then center it to make it obvious to the viewer.
            if (imageEvent.selectedImage != null)
            {
                _picController.ImageGridView.SelectImage(imageEvent.selectedImage);
                _picController.CenterCurrentImage();
            }

            InvalidateWindow();
        }

        /// <summary>
        /// Sets up the full screen mode.
        /// </summary>
        private void SetupFullScreen()
        {
            SaveWindowState();

            this.Visible = false;
            InvalidateWindow();

            FormZoomMode();

            this.Visible = true;
            InvalidateWindow();
            viewerMain.Focus();
        }

        private void fullScreenButton_Click(object sender, EventArgs e)
        {
            SetupFullScreen();
        }

        /// <summary>
        /// Terminates the full screen mode.
        /// </summary>
        private void TerminateFullScreenMode()
        {
            //if (_picController.CurrentViewMode != CrocusViewMode.FullScreenView)
            // return;

            // FormBorderStyle.None is only used in FullScreenMode.
            if (this.FormBorderStyle != FormBorderStyle.None)
                return;

            this.Visible = false;
            InvalidateWindow();

            SizableFormMode();
            this.Visible = true;
            RestoreWindowState();

            // Centers the currently selected image in the CrocusImageGridView control.
            _picController.CenterCurrentImage();

            InvalidateWindow();
        }

        /// <summary>
        /// Handles the full screen image viewing mode termination event.
        /// Note that this is different than the screen saver termination event.
        /// </summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event arguments.</param>
        private void viewerMain_FullScreenTerminated(object sender, EventArgs e)
        {
            TerminateFullScreenMode();
        }

        /// <summary>
        /// Handles the Crocus Image Double clicked event in CrocusImageGridView control.
        /// </summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event arguments.</param>
        private void crystalImageGridView1_CrocusImageDoubleClicked(object sender, EventArgs e)
        {
            // When the user double clicks on an image in the grid view,
            // we show the image view.
            ShowImageView();
        }

        /// <summary>
        /// Handles the double click event in the CrocusPictureShow control.
        /// </summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event arguments.</param>
        private void viewerMain_DoubleClick(object sender, EventArgs e)
        {
            if (_picController.CurrentViewMode == CrocusViewMode.SlideShowMode)
                return;
            try
            {
                // Double clicking on the image in the CrocusPictureShow control
                // flips back and forth between image view and split view.
                if (_picController.CurrentViewMode != CrocusViewMode.SplitView)
                {
                    ShowSplitView();
                    Cursor.Show();

                    txtSearch.Visible = true;
                    btnShowImg.Visible = true;
                    cboFilterType.Visible = true;
                    _oldViewMode = CrocusViewMode.SplitView;
                }
                else
                {
                    ShowImageView();
                    Cursor.Hide();
                    txtSearch.Visible = false;
                    btnShowImg.Visible = false;
                    cboFilterType.Visible = false;
                    _oldViewMode = CrocusViewMode.ImageView;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Processes the command keys, looking for the Escape key.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == (int)WindowsMessages.WM_KEYDOWN)
            {
                // We need to look for the ESC key pressed while the CrocusImageGridView
                // control is focused.  In case this control is active and full screen mode is
                // in effect, we need to terminate the full screen mode.

                switch ((int)keyData)
                {
                    case (int)Keys.Escape:
                        //if (_picController.CurrentViewMode == CrocusViewMode.FullScreenView)
                        // FormBorderStyle.None is only used in FullScreenMode.
                        if (this.FormBorderStyle == FormBorderStyle.None)
                        {
                            try
                            {
                                TerminateFullScreenMode();
                            }
                            catch { }
                            return true;
                        }
                        break;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void trackerButton_Click(object sender, EventArgs e)
        {
            _picController.ShowTrackerOnZoom(!_picController.PictureShow.ShowPanOnZoom);
        }

        private void btnShowImg_Click(object sender, EventArgs e)
        {
            string sPath = "";
            if (txtSearch.Text.Trim().Length > 0)
            {
                DataTable Dt = DB.GetDT("sp_GetProductImage " + CommonLogic.SQuote(txtSearch.Text.Trim()) + "," + cboFilterType.SelectedIndex + "", false);
                sPath = Application.StartupPath.ToString() + @"\tmpImg";
                if (Directory.Exists(sPath))
                {
                    crystalImageGridView1 = null;
                    Directory.Delete(sPath, true);
                    Directory.CreateDirectory(sPath);
                    //string[]  file = Directory.GetFiles(sPath);
                }
                else
                    Directory.CreateDirectory(sPath);

                PictureBox px = null;
                if (Dt.Rows.Count > 0)
                {
                    int irow = 0;
                    foreach (DataRow r in Dt.Rows)
                    {
                        byte[] imageData = (byte[])Dt.Rows[irow][1];
                        //Get image data from gridview column.
                        //Initialize image variable
                        Image newImage;
                        //Read image data into a memory stream
                        using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                        {
                            try
                            {
                                ms.Write(imageData, 0, imageData.Length);
                                newImage = Image.FromStream(ms, true);

                                px = new PictureBox();
                                px.Image = newImage;
                                px.Image.Save((sPath + "\\" + Dt.Rows[irow][0] + "_" + Dt.Rows[irow][2].ToString().Replace(" ", "") + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                                irow++;
                            }
                            catch (Exception ex)
                            {
                                Navigate.logError(ex.Message, ex.StackTrace);
                            }

                        }
                    }
                }
                _picController.InitCollector(sPath, 0);
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                try
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlCaptionBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void SrchImg_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
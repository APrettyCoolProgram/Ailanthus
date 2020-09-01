using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;

namespace Ailanthus
{
  public partial class frmAilanthusMain : Form
  {
    WebClient webpageSource = new WebClient();        // HTML source to be parsed
    MessagePopup statusUpdate = new MessagePopup();   // Status update window
    Random rng = new Random();                        // Random number generator for various things
    Dictionary<string, string> parseRules, sourceURLs; // Rules for parsing a HTML, and the source URLs
    Dictionary<string, string> choruses;                   // lists for thresholds
    string[] exclusions, keepUpper;                   // Words that will cause a source HTML to be invalid, and list of terms to always keep uppercase
    string[] interludes, prepositions, punctuation;   // Lists of possible interludes, propositionslist, and punctuation
    Dictionary<string, string> sliderValues, options;     // Settings for chances and options
    int chorusSliderStorage; // Temp holder for when stuff is disabled

    /// Main form
    /// [1] Set the genre combobox to "None" as a default, get the genre and source defaults settings for the form
    /// controls, then set all of the form control values.
    public frmAilanthusMain()
    {
      InitializeComponent();

      cmbSongGenre.Text = "None";    // [1]
      GetGenreDefaults();
      GetSourceDefaults();
      SetDefaults();
      ControlValues("All", "set");
    }

    /// Get genre defaults
    /// [1] Setup a nice looking prefix string
    /// [2] Get the lists of terms to be excluded, and those to capitalize
    /// [3] Get list of possible interludes, prepositions, and punctuation
    /// [4] Get the chance percentages for special components, and settings for various form controls
    private void GetGenreDefaults()
    {
      string defaultPrefix = "Ailanthus.embedded.genres." + cmbSongGenre.Text; // [1]

      exclusions = EmbeddedData.ToArray(defaultPrefix + ".exclusions");        // [2]
      keepUpper = EmbeddedData.ToArray(defaultPrefix + ".keepUpper");

      interludes = EmbeddedData.ToArray(defaultPrefix + ".interludes");        // [3]
      prepositions = EmbeddedData.ToArray(defaultPrefix + ".prepositions");
      punctuation = EmbeddedData.ToArray(defaultPrefix + ".punctuation");

      sliderValues = EmbeddedData.ToDictionary(defaultPrefix + ".sliderValues");         // [4]
      options = EmbeddedData.ToDictionary(defaultPrefix + ".options");

      choruses = EmbeddedData.ToDictionary(defaultPrefix + ".choruses");

    }

    ///  Get source defaults
    ///  [1] Setup some nice looking prefix strings for the master and source data
    ///  [2] Get the URL and parsing rules for the source 
    private void GetSourceDefaults()
    {
      string masterPrefix = "Ailanthus.embedded.sources.";                        // [1]
      string defaultsPrefix = "Ailanthus.embedded.sources." + cmbSongSource.Text;

      sourceURLs = EmbeddedData.ToDictionary(masterPrefix + "sourceURLs");        // [2]
      parseRules = EmbeddedData.ToDictionary(defaultsPrefix + ".parseRules");
    }


    private void SetDefaults()
    {
      // Chorus defaults
      SetupChorusSection();


    }

    // Set or refresh form controls
    private void ControlValues(string section, string action)
    {
      if (section.Contains("Left") || section.Contains("All"))                               // If request is for left or all
      {
        if (action == "set")                                                                 // If setting, these should not be refreshed
        {
          tbrNumberOfComponents.Value = Int32.Parse(options["component"]);                   // Components slider
          cboLockTitle.Checked = Convert.ToBoolean(options["lockTitle"]);                    // Title matches genre
          tbrPunctuationWeight.Value = Int32.Parse(options["punctuationWeight"]);            // Punctuation wieght slider
        }

        if (action == "set" || action == "refresh")                                          // If setting or refreshing
        {
          tbxNumberOfComponentsDisplay.Text = Convert.ToString(tbrNumberOfComponents.Value); // Components display
          tbxPunctuationWeightDisplay.Text = Convert.ToString(tbrPunctuationWeight.Value);   // Punctuation wieght display
        }
      }

      if (section.Contains("Center") || section.Contains("All"))                             // If request is for center or all
      {
        if (action == "set")                                                                 // If setting, these should not be refreshed
        {
          lblSongTitle.Text = String.Empty;                                                  // Clear the title
          tbxSongLyrics.Text = String.Empty;                                                 // Clear the lyrics
        }
      }

      if (section.Contains("Right") || section.Contains("All"))                              // If request if for right or all
      {
        if (action == "set")                                                                 // If setting, these should not be refreshed
        {
         // SetChorusSection(action);
          cboLockInterludes.Checked = Convert.ToBoolean(options["lockInterlude"]);           // Interludes match genre
          cboLockPrepositions.Checked = Convert.ToBoolean(options["lockPreposition"]);       // Prepositions match genre
          tbrInterludeChance.Value = Int32.Parse(sliderValues["interlude"]);                      // Chance of interlude slider
          tbrPrepositionChance.Value = Int32.Parse(sliderValues["preposition"]);                  // Chance of preposition slider
          tbrNewlineChance.Value = Int32.Parse(sliderValues["newline"]);                          // Chance of newline slider
        }

        if (action == "set" || action == "refresh")                                          // If setting or refreshing
        {
         // SetChorusSection(action);
          cboLockInterludes.Checked = Convert.ToBoolean(options["lockInterlude"]);           // Interludes match genre
          cboLockPrepositions.Checked = Convert.ToBoolean(options["lockPreposition"]);       // Prepositions match genre
          tbxInterludeChanceDisplay.Text = Convert.ToString(tbrInterludeChance.Value);       // Chance of interlude display
          tbxPrepositionChanceDisplay.Text = Convert.ToString(tbrPrepositionChance.Value);   // Chance of preposition display
          tbxNewlineChanceDisplay.Text = Convert.ToString(tbrNewlineChance.Value);           // Chance of newline display
        }
      }
    }


    // When user moves a slider.
    private void UpdateControlValues(object sender, EventArgs e)
    {
      ControlValues("LeftRight", "refresh"); // Refresh left and right
    }

    // When genre is updated
    private void SetControlValues(object sender, EventArgs e)
    {
      GetGenreDefaults();                 // Get genre defaults
      ControlValues("LeftRight", "set"); // Refresh left and right
    }

    // Create new song.
    private void btnCreateSong_Click(object sender, EventArgs e)
    {
      ControlValues("Center", "set");                     // Set center
      statusUpdate.Show();                                 // Initiate status window for user

      string songSource = sourceURLs[cmbSongSource.Text];  // Get source URL

      lblSongTitle.Text = GenerateSongTitle(songSource);   // Generate title
      tbxSongLyrics.Text = GenerateSongLyrics(songSource); // Generate lyrics

      statusUpdate.Hide();                                 // Hide status popup
    }

    // Generate title.
    private string GenerateSongTitle(string songSource)
    {
      statusUpdate.SetContent("Creating song title", "Please wait...", ""); // Update status popup

      string sourceHTML = webpageSource.DownloadString(songSource);         // Get the source HTML
      string component = GetComponentFromHTML(sourceHTML);                  // Get the component from the HTML

      return component;
    }

    // Generate song lyrics.
    private string GenerateSongLyrics(string songSource)
    {
      string songLyrics = "";                                     // Completed song.
      string lyric;                                               // Lyric component
      bool capitalize = true;                                     // Flag for capitalization
      bool hasChorus, hasInterlude, hasPreposition, hasLinebreak; // Flags for special components
      int maxChorus = 100;  // Max chorus default
      int numChorus = 0; // Starting counter for choruses

      // Massage some chorus stuff
      if (cboMaxChoruses.Checked)  // If maxChorus is checked
      {
        maxChorus = Int32.Parse(tbxMaxChroruses.Text); // num max choruses = textbox
      }

      for (int component = 0; component < tbrNumberOfComponents.Value; component++) // Create lyrics for the number of lines the user requested.
      {
        statusUpdate.SetContent("Creating song lyrics", "Lyric component: " + (component + 1) + " of " + tbrNumberOfComponents.Value, ""); // Let the user know what line we are on, flubbing so it doesn't show "0".

        // Get chances of stuff happening
        hasChorus = SetBoolean(tbrChorusChance.Value);           // Get chance of a chorus
        hasInterlude = SetBoolean(tbrInterludeChance.Value);     // Get chance of an interlude
        hasPreposition = SetBoolean(tbrPrepositionChance.Value); // Get chance of a preposition
        hasLinebreak = SetBoolean(tbrNewlineChance.Value);       // Get chance of a linebreak

        // Make any changes to the chances
        if (hasChorus) // If there is a chorus
        {
          numChorus++; // Incriment num chroruses so far
          if (numChorus > maxChorus) // If we've already done the num choruses
          {
            hasChorus = false; // override and set to false here on out
          }
        }



        lyric = GetComponentFromHTML(GetValidSource(songSource));  // Get a valid source file, then the name of the card from that source.

        if (capitalize && lyric != String.Empty)               // If capitalization flag is true and the lyric is empty
        {
          lyric = char.ToUpper(lyric[0]) + lyric.Substring(1); // Beginning of line, capitalize
          capitalize = false;                                  // Flip capitalization flag
        }

        if (hasChorus | hasInterlude)
        {
          hasLinebreak = false;
        }

        if (hasChorus | hasInterlude | hasLinebreak)
        {
          capitalize = true;
        }
        else
        {
          lyric = lyric + " ";
        }

        lyric = LyricMetalizer(lyric, hasChorus, hasInterlude, hasPreposition, hasLinebreak);

        // Add the metalized lyric to the rest of the song.
        songLyrics = songLyrics + lyric;

      }

      return songLyrics;
    }

    // Get the component from HTML
    private string GetComponentFromHTML(string lyricSource)
    {
      if (!keepUpper.Any(lyricSource.Contains) & lblSongTitle.Text != "")              // Not part of keepUpper and not title
      {
        lyricSource = lyricSource.ToLower();                                           // Make lowercase
      }

      int prefixIndex = lyricSource.IndexOf(parseRules["prefix"]);                     // Get prefix index
      lyricSource = lyricSource.Substring(prefixIndex);                                // Remove anything prior to prefix
      int postfixIndex = lyricSource.IndexOf(parseRules["postfix"]);                   // Get postfix index
      lyricSource = lyricSource.Substring(0, postfixIndex);                            // Remove anything after postfix
      if (parseRules["replaceThis"] != String.Empty)                                   // If there is something to replace
      {
        lyricSource =
            lyricSource.Replace(parseRules["replaceThis"], parseRules["replaceWith"]); // Replace it
      }
      lyricSource = lyricSource.Replace(parseRules["cleanup"], "").Trim();             // Cleanup and trim

      return lyricSource;
    }

    // Get valid HTML
    private string GetValidSource(string songSource)
    {
      string lyricSource = ""; // Start empty

      while (!IsSourceValid(lyricSource))                       // While the HTML is not valid...
      {
        lyricSource = webpageSource.DownloadString(songSource); // Get HTML source
      }

      return lyricSource;
    }

    // Metalizes!
    private string LyricMetalizer(string songLyric, bool hasChorus, bool hasInterlude, bool hasPreposition, bool hasLinebreak)
    {
      string prep;

      if (hasChorus | hasInterlude)
      {
        songLyric = songLyric + punctuation[rng.Next(0, punctuation.Length - 1)] + "\r\n\r\n";
      }
 
      if (hasChorus)
      {
        int startAt = Int32.Parse(tbxMinLinesPerChorus.Text);
        int stopAt =  Int32.Parse(tbxMaxLinesPerChorus.Text);
        int chorusRepeat = rng.Next(startAt, stopAt);
        string workLyric = "\r\n" + lblSongTitle.Text;
        
        for (int i = 0; i <= chorusRepeat; i++)
        {
          if ((i == chorusRepeat) && (cboEndChorusBlockCapitalized.Checked))
          {
            workLyric.ToUpper();
          }

          if (cboEndChorusLinesExclaimed.Checked)
          {
            songLyric = workLyric + "\r\n" + lblSongTitle.Text + "!\r\n";
          }
          else
          {
            songLyric = workLyric + "\r\n" + lblSongTitle.Text + "\r\n";
          }
          
          if ((i == chorusRepeat) && (cboEndChorusBlockExclaimed.Checked))
          {
            songLyric = workLyric + "\r\n" + lblSongTitle.Text + "!\r\n";
          }
        }
        songLyric = songLyric + "\r\n";
      }

      if (hasInterlude)
      {
        int interlude = rng.Next(0, interludes.Length - 1);
        songLyric = songLyric + interludes[interlude] + "\r\n\r\n";
      }

      if (hasPreposition && !hasLinebreak)
      {
        if (hasChorus | hasInterlude)
        {
          prep = prepositions[rng.Next(0, prepositions.Length - 1)];
          prep = char.ToUpper(prep[0]) + prep.Substring(1);
        }
        else
        {
          prep = prepositions[rng.Next(0, prepositions.Length - 1)];
        }
        songLyric = songLyric + prep + " ";
      }

      if (hasLinebreak)
      {
        songLyric = songLyric + punctuation[rng.Next(0, punctuation.Length - 1)] + "\r\n";
      }

      // Replace dual card titles with a comma.
      if (songLyric.Contains(@"//"))
      {
        songLyric = songLyric.Replace(@"//", "and");
      }

      return songLyric;
    }

    // Determine chance
    private bool SetBoolean(int chance)
    {
      if (rng.Next(1, 101) <= chance) // If the RNG is less than the chance
      {
        return true;                  // Valid source
      }
      return false;                   // Not valid source
    }

    // Get a preposition
    public static string GetPreposition(string songLyric, string[] prepositions, int randNum)
    {
      if (randNum > prepositions.Length - 1)              // If the RNG is greater than the prep array
      {
        return (songLyric + " ");                         // Return lyric w/space
      }
      return (songLyric + prepositions[randNum] + " ");   // Return lyric w/prep and space
    }

    // Check source validity
    private bool IsSourceValid(string lyricSource)
    {
      bool validity = false;
      switch (cmbSongSource.Text) // Depending on source
      {
        case "Gatherer":
          if (exclusions.Any(lyricSource.Contains))
          {
            validity = false;
          }
          else if ((cmbSongGenre.Text == "Black metal" && lyricSource.Contains("alt=\"Black\"")))
          {
            validity = false;
          }
          else if (lyricSource == String.Empty)
          {
            validity = false;
          }
          else
          {
            validity = true;
          }
          break;
        case "Wikipedia":
          if (lyricSource == String.Empty)
          {
            validity = false;
          }
          else
          {
            validity = true;
          }
          break;
        default:
          break;
      }

      return validity;

    }

    // Start region for update stuff





    //CHORUS CHANGES SECTION


    // Chorus threshold stuff
    private void SetupChorusSection()
    {
      tbrChorusChance.Value = Int32.Parse(choruses["threshold"]);                           // Chance of chorus slider
      tbxChorusChanceDisplay.Text = Convert.ToString(tbrChorusChance.Value);             // Chance of chorus display

      // Max choruses settings
      if (choruses["maxChoruses"] == "-")
      {
        cboMaxChoruses.Checked = false;
      }
      else
      {
        cboMaxChoruses.Checked = true;
      }

      // Set the other options
      tbxMaxChroruses.Text = choruses["maxChoruses"];
      tbxMinLinesPerChorus.Text = choruses["minLines"];
      tbxMaxLinesPerChorus.Text = choruses["maxLines"];
      cboEndChorusLinesExclaimed.Checked = Convert.ToBoolean(choruses["endLinesExclaimed"]);
      cboEndChorusBlockExclaimed.Checked = Convert.ToBoolean(choruses["endBlockExclaimed"]);
      cboEndChorusBlockCapitalized.Checked = Convert.ToBoolean(choruses["endBlockCapitalized"]);
      cboEndWithChorus.Checked = Convert.ToBoolean(choruses["endWithChorus"]);
      cboDisableChoruses.Checked = Convert.ToBoolean(choruses["disableChoruses"]);
    }

    // When the chorus threshold slider changes
    private void ChorusThresholdChange(object sender, EventArgs e)
    {
      tbxChorusChanceDisplay.Text = Convert.ToString(tbrChorusChance.Value);             // Set the display text to the slider value
    }

    // When cboMaxChorus changes
    private void MaxChorusChange(object sender, EventArgs e)
    {
      cboDisableChoruses.Enabled = !Convert.ToBoolean(cboMaxChoruses.Checked); // Flip enabledness 
    }

    // When cboEndChorusLinesExclaimed changes
    private void EndChorusLinesExclaimedChange(object sender, EventArgs e)
    {
      cboEndChorusBlockExclaimed.Enabled = !Convert.ToBoolean(cboEndChorusLinesExclaimed.Checked);
      cboEndChorusBlockCapitalized.Enabled = !Convert.ToBoolean(cboEndChorusLinesExclaimed.Checked);
      cboDisableChoruses.Enabled = !Convert.ToBoolean(cboEndChorusLinesExclaimed.Checked);
    }

    // When cboEndChorusBlockExclaimed changes
    private void EndChorusBlockExclaimedChange(object sender, EventArgs e)
    {
      cboEndChorusLinesExclaimed.Enabled = !Convert.ToBoolean(cboEndChorusBlockExclaimed.Checked);
      cboEndChorusBlockCapitalized.Enabled = !Convert.ToBoolean(cboEndChorusBlockExclaimed.Checked);
      cboDisableChoruses.Enabled = !Convert.ToBoolean(cboEndChorusBlockExclaimed.Checked);
    }

    // When cboEndWithChorus changes
    private void EndWithChorusChange(object sender, EventArgs e)
    {
      cboDisableChoruses.Enabled = !Convert.ToBoolean(cboEndWithChorus.Checked);
    }

    private void DisableChorusesChange(object sender, EventArgs e)
    {
      // If cboDisableChoruses is checked, store the current slider value, set the slider to 0, and update the display text
      if (Convert.ToBoolean(cboDisableChoruses.Checked))
      {
        chorusSliderStorage = tbrChorusChance.Value;
        tbrChorusChance.Value = 0;
        tbxChorusChanceDisplay.Text = Convert.ToString(tbrChorusChance.Value);
      }
      // If cboDisableChoruses then becomes unchecked, reset the slider value to what it was before and set the display text
      else
      {
        tbrChorusChance.Value = chorusSliderStorage;
        tbxChorusChanceDisplay.Text = Convert.ToString(tbrChorusChance.Value);
      }

      // Flip enabledness
      tbrChorusChance.Enabled = !Convert.ToBoolean(cboDisableChoruses.Checked);
      cboMaxChoruses.Enabled = !Convert.ToBoolean(cboDisableChoruses.Checked);
      cboEndChorusLinesExclaimed.Enabled = !Convert.ToBoolean(cboDisableChoruses.Checked);
      cboEndChorusBlockExclaimed.Enabled = !Convert.ToBoolean(cboDisableChoruses.Checked);
      cboEndWithChorus.Enabled = !Convert.ToBoolean(cboDisableChoruses.Checked);
    }

  }
}

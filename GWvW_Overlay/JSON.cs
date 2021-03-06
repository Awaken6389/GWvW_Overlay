﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using GWvW_Overlay.Annotations;

namespace GWvW_Overlay
{
    //Match Details
    public class WvwObjective : INotifyPropertyChanged
    {
        private double _left;
        private double _top;
        private double _left_base;
        private double _top_base;

        public int id { get; set; }
        
        public string map { get; set; }
        public int points { get; set; }
        public string type {get; set; }
        public double res_width { get; set; }
        public double res_height { get; set; }

        public string name_de { get; set; }
        public string name_en { get; set; }
        public string name_es { get; set; }
        public string name_fr { get; set; }

        public string name {
            get 
            {
                switch (Properties.Settings.Default["show_names_lang"].ToString())
                {
                    case "English":
                        return name_en;
                    case "German":
                        return name_de;
                    case "Spanish":
                        return name_es;
                    case "French":
                        return name_fr;
                    default:
                        return name_en;
                }
            }
        }

        public double top_base
        {
            set { _top_base = value; }
            get { return _top_base; }
        }

        public double left_base
        {
            set { _left_base = value; }
            get { return _left_base;  }
        }

        public double top
        {
            get { return _top; }
            set
            {
                if (_top_base == 0.0)
                    _top_base = value;

                if (value != _top)
                {
                    _top = value;
                    OnPropertyChanged();
                }
            }
        }

        public double left
        {
            get { return _left; }
            set
            {
                if (value != _left)
                {
                    if (_left_base == 0.0)
                        _left_base = value;

                    _left = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "none passed")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


    }
    public class getName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)Properties.Settings.Default["show_names"])
                return value;
            else
                return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class getClaimed : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return new BitmapImage(new Uri("Resources/claimed2.png", UriKind.Relative));
            else
                return new BitmapImage(new Uri("Resources/empty.png", UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class getIMG : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length == 1)
                return getPNG(values[0], null);
            if(values.Length == 2)
                return getPNG(values[0], values[1]);

            
            return null;
        }
        public object[] ConvertBack(object values, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private ImageSource getPNG(object type, object color)
        {
            string y;
            if (color == null || color.ToString() == "none")
            {
                y = string.Format("Resources/{0}.png", type);
            }
            else
            {
                y = string.Format("Resources/{0}_{1}.png", type, color.ToString().ToLower());

            }

            ImageSource x = new BitmapImage(new Uri(y, UriKind.Relative));
            return x;
        }
    }

    public class getMatches : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length < 4)
                return "Value count is low";

            return string.Format("{0}. {1} vs {2} vs {3}", values[0], values[1], values[2], values[3]);
        }
        public object[] ConvertBack(object values, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ObjectiveNames_
    {
        public List<WvwObjective> wvw_objectives { get; set; }
    }
    public class Match_Details_
    {
        public string match_id { get; set; }
        public List<int> Scores { get; set; }
        public List<Map> Maps { get; set; }

        public double ScoresSum
        {
            get { return Scores.Sum(); }
        }
    }

    public class Objective : INotifyPropertyChanged
    {
        public Objective()
        {
            ObjData = new WvwObjective();
        }
        public WvwObjective ObjData { get; set; }
        public int id { get; set; }
        private string _owner;
        private string _owner_guild;
        public DateTime _last_change;
        public string _time_left;

        public string time_left {
            get { return _time_left; }
            set
            {
                if (value != _time_left)
                {
                    _time_left = value;
                    OnPropertyChanged();
                }
            }
        }

        public string owner_guild_icon
        {
            get
            {
                if(owner_guild != null)
                    return "Resources/claimed2.png";
                else
                    return "Resources/empty.png";
            }
        }

        public DateTime last_change
        {
            get { return _last_change; }
            set
            {
                if (value != _last_change)
                {
                    _last_change = value;
                    OnPropertyChanged();
                }
            }
        }

        public string owner_guild
        {
            get
            {
                return _owner_guild;
            }
            set
            {
                if (_owner_guild == value)
                    return;

                _owner_guild = value;
                OnPropertyChanged();
            }
        }

        public string owner
        {
            get { return _owner; }
            set
            {
                if (_owner == null)
                {
                    _owner = value;
                    OnPropertyChanged();
                }
                if (value != _owner && _owner != null)
                {
                    _owner = value;
                    last_change = DateTime.Now;
                    OnPropertyChanged();
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "none passed")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Map
    {
        public string Type { get; set; }
        public List<int> Scores { get; set; }
        public List<Objective> Objectives { get; set; }

        public double ScoresSum
        {
            get { return Scores.Sum(); }
        }

        public int CountObjType(string type, string color)
        {
            return Objectives.Count(obj => obj.ObjData.type.ToLower() == type.ToLower() && obj.owner.ToLower() == color.ToLower());
        }
    }

    public class Options_ : INotifyPropertyChanged
    {
        private string _active_bl = "Center";
        public Dictionary<string, int> blid;
        public int _active_blid;
        public string HomeServerColor;

        private string _active_map_img = "Resources/mapeb_normal.png";

        private double _width = 400;
        private double _height = 400;

        private double _min_width;
        private double _min_height;

        private string _active_bl_title = "Eternal Battlegrounds";
        public string _active_match;// = "1-1";

        public int active_blid
        {
            get { return blid[_active_bl]; }
        }

        public string active_bl_title
        {
            get { return _active_bl_title; }
            set
            {
                _active_bl_title = value;
                OnPropertyChanged();
            }
        }

        public string active_match
        { 
            get { return _active_match; }
            set 
            {
                if (value != _active_match)
                {
                    _active_match = value;
                    OnPropertyChanged();
                }
            }
        }

        public string active_bl
        {
            get { return _active_bl; }
            set
            {
                switch (value)
                {
                    case "RedHome":
                        active_bl_title = "Red Borderlands";
                        active_map_img = "Resources/mapbl_normal.png";
                        ChangeWindowSize(false);
                        break;
                    case "GreenHome":
                        active_bl_title = "Green Borderlands";
                        active_map_img = "Resources/mapbl_normal.png";
                        ChangeWindowSize(false);
                        break;
                    case "BlueHome":
                        active_bl_title = "Blue Borderlands";
                        active_map_img = "Resources/mapbl_normal.png";
                        ChangeWindowSize(false);
                        break;
                    default:
                        active_bl_title = "Eternal Battlegrounds";
                        active_map_img = "Resources/mapeb_normal.png";
                        ChangeWindowSize(true);
                        break;
                }

                _active_bl = value;
                OnPropertyChanged();
            }
        }

        public string active_map_img
        { 
            get { return _active_map_img; }
            set 
            {
                if (value != _active_map_img)
                {
                    _active_map_img = value;
                    OnPropertyChanged();
                }
            }
        }

        public double min_width
        {
            get { if (active_bl == "Center") return Properties.Settings.Default.main_eb_width; else return Properties.Settings.Default.main_bl_width; }
        }
        public double min_height
        {
            get { if (active_bl == "Center") return Properties.Settings.Default.main_eb_height; else return Properties.Settings.Default.main_bl_height; }
        }
        public double width
        {
            get { return _width; }
            set
            {
                if (value != _width)
                {
                    Console.WriteLine("W:{0}", value.ToString());
                    _width = value;
                    OnPropertyChanged();
                }
            }
        }

        public double height
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    Console.WriteLine("H:{0}", value.ToString());
                    _height = value;
                    OnPropertyChanged();
                }
            }
        }

        public void ChangeWindowSize(bool etrnBattle)
        {
            if (etrnBattle)
                ChangeWindowSize(Properties.Settings.Default.main_eb_width, Properties.Settings.Default.main_eb_height);
            else
                ChangeWindowSize(Properties.Settings.Default.main_bl_width, Properties.Settings.Default.main_bl_height);
        }

        public void ChangeWindowSize(double Width, double Height)
        {
            width = Width;
            height = Height;
        }
        
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "none passed")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Matches
    public class WvwMatch_
    {
        public WvwMatch_()
        {
            Options = new Options_();
        }
        public Options_ Options { get; set; }
        public List<Matches> Match { get; set; }
        public List<World_Names_> World { get; set; }
        public Match_Details_ Details { get; set; }
        public List<WvwObjective> ObjectiveNames { get; set; }

        //Get BL IDs to Type
        public void GetBLID()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (int i = 0; i < Details.Maps.Count; i++)
            {
                int map_id = i;
                dict.Add(Details.Maps[map_id].Type, map_id);
            }
            Options.blid = dict;
        }

        public void ListWorlds()
        {
            foreach (var x in World)
            {
                Console.WriteLine("<ComboBoxItem Content=\"{0}\"/>", x.name);
            }
        }

        public Dictionary<string, string> GetMatchesList()
        {
            var ret = new Dictionary<string, string>();
            foreach (var x in Match)
            {
                var matchName = string.Format("{0}. {3} vs {2} vs {1}", x.wvw_match_id, getServerName(x.red_world_id), getServerName(x.blue_world_id), getServerName(x.green_world_id));
                x.wvw_match_string = matchName;
                ret.Add(x.wvw_match_id, matchName);
            }
            return ret.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public string getServerName(string color)
        { 
            foreach(var x in Match)
            {
                if (Options.active_match == x.wvw_match_id)
                {
                    switch (color.ToLower())
                    {
                        case "red": return getServerName(x.red_world_id); 
                        case "blue": return getServerName(x.blue_world_id); 
                        case "green": return getServerName(x.green_world_id);
                        case "neutral": return "Neutral"; 
                    }
                }
            }
            return string.Format("MATCH_ID-{0}_NOT_FOUND", Options.active_match);
        }

        public string getServerName(int ID)
        {
            foreach (var x in World)
            {
                if (ID == x.id)
                    return x.name;
            }
            return string.Format("SERVER_{0}_NOT_FOUND", ID);
        }

        public string HomeServerColor
        {
            get { return Options.HomeServerColor ?? (Options.HomeServerColor = GetServerColor()); }
        }

        public string GetServerColor()
        {
            foreach (var x in Match)
            {
                if ((int)Properties.Settings.Default["home_server"] == x.red_world_id)// && x.red_world_id == id)
                    return "red";
                if ((int)Properties.Settings.Default["home_server"] == x.green_world_id)// && x.green_world_id == id)
                    return "green";
                if ((int)Properties.Settings.Default["home_server"] == x.blue_world_id)// && x.blue_world_id == id)
                    return "blue";
            }
            return "white";
        }
    }

    public class Matches_
    {
        public List<Matches> wvw_matches { get; set; }
    }

    public class Matches
    {
        public string wvw_match_id { get; set; }
        public int red_world_id { get; set; }
        public int blue_world_id { get; set; }
        public int green_world_id { get; set; }
        public string wvw_match_string { get; set; }
    }

    // World Names
    public class World_Names_
    {

        private string _name;

        public int id { get; set; }
        
        public string name
        {
            get { return _name; }
            set { _name = value ?? "Server ID " + id; }
        }
    }
}
